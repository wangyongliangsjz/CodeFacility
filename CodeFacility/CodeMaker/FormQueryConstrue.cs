using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

using System.Data.Common;
using System.Data.SQLite;

using DALFactory.CodeMaker;
using AccessDal.CodeMaker;
using CurrencyDal.CodeMaker;
using Model.CodeMaker;
using Model;

namespace CodeFacility.CodeMaker
{
    public partial class FormQueryConstrue : FormWin
    {
        //编辑器：https://github.com/KindDragon/ICSharpCode.TextEditor
        DbDataInfo dinfo;
        RunSql dal = new RunSql();

        public FormQueryConstrue()
        {
            InitializeComponent();
        }

        private void FormQueryConstrue_Load(object sender, EventArgs e)
        {
            //订阅了信息发送事件，即接受参数值
            Common.MidModule.EventSend += new Common.DataDlg(MidModule_EventSend);
            DGV1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            QueryData();
            ini();
            InitForm();

            InitOption();
        }

        private void InitOption()
        {
            textEditorControl.ForeColor = BaseConfigure.ColorTheme;
            textEditorControl.BackColor = BaseConfigure.ColorTheme;
            textEditorControl.Font = new System.Drawing.Font(BaseConfigure.FontTypeface, BaseConfigure.FontSize, FontStyle.Regular);
            toolStrip1.BackColor = BaseConfigure.ColorTheme;
            DGV1.RowsDefaultCellStyle.BackColor = BaseConfigure.ColorTheme;
            DGV2.RowsDefaultCellStyle.BackColor = BaseConfigure.ColorTheme;
            DGV3.RowsDefaultCellStyle.BackColor = BaseConfigure.ColorTheme;
            DGV4.RowsDefaultCellStyle.BackColor = BaseConfigure.ColorTheme;
            DGV5.RowsDefaultCellStyle.BackColor = BaseConfigure.ColorTheme;
            rtb_Message.BackColor = BaseConfigure.ColorTheme;
        }

        //接受参数事件
        private void MidModule_EventSend(object sender, object data, object e)
        {
            if (sender != null)
            {
                Form fr = sender as Form;
                if (fr.Text == "对象资源管理器")
                {
                    dinfo = data as DbDataInfo;
                    if (dinfo != null)
                    {
                    }
                    if (e == null)
                        return;
                    Model.EventInfo einfo = e as Model.EventInfo;
                }
            }
        }

        private void ini()
        {
            textEditorControl.ShowEOLMarkers = false;
            textEditorControl.ShowHRuler = false;
            textEditorControl.ShowInvalidLines = false;
            textEditorControl.ShowLineNumbers = true;
            textEditorControl.ShowMatchingBracket = true;
            textEditorControl.ShowSpaces = false;
            textEditorControl.ShowTabs = false;
            textEditorControl.ShowVRuler = true;
            textEditorControl.AllowCaretBeyondEOL = false;
            textEditorControl.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("TSQL");
            textEditorControl.Encoding = Encoding.GetEncoding("GB2312");
        }

        private void QueryData()
        {
            IDbLink idal = new DbLink();
            IList<DbLinkInfo> ilist = idal.DbLinkGetList();
            foreach (DbLinkInfo info in ilist)
            {
                string dbName = string.IsNullOrEmpty(info.DbAbbreviation) ? info.DbName : info.DbName + "(" + info.DbAbbreviation + ")";
                DBToolStripComboBox.Items.Add(new ListItem(info.ID.ToString(), dbName));
            }
            DBToolStripComboBox.SelectedIndex = 0;
        }

        private void RunToolScriptBtn_Click(object sender, EventArgs e)
        {
            string sqlmsg = "";
            string rstmsg="";
            string sql=textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText.Trim();
            sql = GetSqlZC(sql);
            if (sql == "")
            {
                sql = textEditorControl.Text.Trim();
            }
            if (sql == "")
                return;
            sql=sql.Replace("\r\n", " ");
            
            ListItem item = DBToolStripComboBox.SelectedItem as ListItem;

            int DbLinkID = int.Parse(item.ID.ToString());
            if (DbLinkID == 0)
            {
                MessageBox.Show("请选择数据库");
            }

            List<DataTable> listDt = new List<DataTable>();
            IDbLink ldal = new  DbLink();
            DbLinkInfo dlinfo = ldal.DbLinkGetInfo(DbLinkID);
            if(dlinfo.DbType==(int)DataBaseTypeEnum.Oracle)
            {
                string[] arr = sql.Split(';');
                foreach(var sqlitem in arr)
                {
                    string sqlorc = sqlitem.Replace("\r\n", "");
                    DataSet ds = dal.Run(dlinfo, sqlorc, out sqlmsg, out rstmsg);
                    if (ds != null)
                    {
                        listDt.Add(ds.Tables[0]);
                    }
                }
            }
            else
            {
                DataSet ds = dal.Run(dlinfo, sql, out sqlmsg, out rstmsg);
                rtb_Message.Text = sqlmsg;
                if (ds == null)
                {
                    MessageBox.Show(rstmsg);
                    return;
                }
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    listDt.Add(ds.Tables[i]);
                }
            }

            this.tabControl1.SelectTab("tabPage1");
            DGV1.DataSource = null;
            DGV2.DataSource = null; 
            DGV3.DataSource = null;
            DGV4.DataSource = null;
            DGV5.DataSource = null;
            for (int i = 0; i < listDt.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        DGV1.DataSource = listDt[i];
                        break;
                    case 1:
                        DGV2.DataSource = listDt[i];
                        break;
                    case 2:
                        DGV3.DataSource = listDt[i];
                        break;
                    case 3:
                        DGV4.DataSource = listDt[i];
                        break;
                    case 4:
                        DGV5.DataSource = listDt[i];
                        break;
                }
            }

        }

        /// <summary>
        /// sql语句转换 正则
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private string GetSqlZC(string sql)
        {
            string result = "";
            try
            {
                if (sql.IndexOf("--") >= 0 && sql.IndexOf("\r\n") >= 0)
                {
                    Regex r = new Regex("(?<=--).*?(?=\r\n)", RegexOptions.IgnoreCase);
                    result = r.Replace(sql, " ");
                    result = result.Replace("-- \r\n", "");
                }
                else
                    result = sql;
            }
            catch { }
            return result;
        }

        /// <summary>
        /// sql语句转换 有问题
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private string GetSql(string sql)
        {
            try
            {
                if (sql.IndexOf("--") >= 0 && sql.IndexOf("\r\n") >= 0)
                {
                    sql = sql.Replace("--", " --");
                    //while (sql.IndexOf("\r\n") <=4)
                    //{
                    //    int index = sql.IndexOf("\r\n");
                    //    sql = sql.Substring(4, sql.Length - 4);
                    //}
                    while (sql.IndexOf("--") >= 0)
                    {
                        int index1 = sql.IndexOf("--");
                        int index2 = sql.IndexOf("\r\n");
                        string value = "";
                        if (index2 > index1)
                        {
                            int len = "\r\n".Length;
                            int len2 = "\r".Length;
                            var a1 = sql.Substring(index1, index2);
                            value = sql.Substring(index1, index2);
                            if (value.IndexOf("\r") >= 0 && value.IndexOf("\r\n") < 0)
                                value = value.Replace("\r", "\r\n");

                            sql = sql.Replace(value, "");
                            int kgcount = sql.Length - sql.TrimStart().Length;
                            sql = sql.Remove(0, kgcount);
                        }
                        else
                        {
                            sql = sql.Remove(index2, "\r\n".Length);
                            sql = sql.Insert(index2, " ");

                        }
                    }
                }
            }
            catch { }
            return sql;
        }

        private void StopToolStripBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void SaveToolStripBtn_Click(object sender, EventArgs e)
        {
            ExportData();
        }

        private void toolStripBtn_Insert_Click(object sender, EventArgs e)
        {
            ExportInsert();
        }


        #region 导出文件
        /// <summary>
        /// 导出文件
        /// </summary>
        private void ExportData()
        {
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.FileName = "temp.xls";
            //SaveFile.Filter = "Excel 工作簿(*.xlsx)|*.xlsx|Miscrosoft Office Excel 97-2003 工作表|*.xls|所有文件(*.*)|*.*";
            SaveFile.Filter = "Miscrosoft Office Excel 97-2003 工作表|*.xls|excel07文件(*.xlsx)|*.xlsx|txt 文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";
            SaveFile.RestoreDirectory = true;
            string filePath = "";
            string extend = "";
            if (SaveFile.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                filePath = SaveFile.FileName;
                extend = System.IO.Path.GetExtension(SaveFile.FileName);

                extend = extend.Replace(".", "");
            }

            string sqlmsg = "";
            string rstmsg = "";
            string sql = textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText.Trim();
            if (sql == "")
            {
                sql = textEditorControl.Text.Trim();
            }
            if (sql == "")
                return;
            sql = GetSqlZC(sql);
            sql = sql.Replace("\r\n", " ");

            ListItem item = DBToolStripComboBox.SelectedItem as ListItem;

            int DbLinkID = int.Parse(item.ID.ToString());
            if (DbLinkID == 0)
            {
                MessageBox.Show("请选择数据库");
            }
            IDbLink ldal = new DbLink();
            DbLinkInfo dlinfo = ldal.DbLinkGetInfo(DbLinkID);
            DataSet ds = dal.Run(dlinfo, sql, out sqlmsg, out rstmsg);
            rtb_Message.Text = sqlmsg;
            if (ds == null)
            {
                MessageBox.Show(rstmsg);
                return;
            }
            if (extend == "xls")
            {
                Common.Excel.DataSetToExcel(ds, filePath, out rstmsg);
            }
            else if (extend == "xlsx")
            {
                
            }
            else if (extend == "txt")
            {
                Common.FileHandle.DataSetToTxt(ds, filePath,out rstmsg);
            }
            MessageBox.Show(rstmsg);
        }
        #endregion

        #region 导出Insert文件
        private void ExportInsert()
        {
            string sqlmsg = "";
            string rstmsg = "";
            string sql = textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText.Trim();
            if (sql == "")
            {
                sql = textEditorControl.Text.Trim();
            }
            if (sql == "")
                return;
            sql = GetSqlZC(sql);
            sql = sql.Replace("\r\n", " ");

            ListItem item = DBToolStripComboBox.SelectedItem as ListItem;

            int DbLinkID = int.Parse(item.ID.ToString());
            if (DbLinkID == 0)
            {
                MessageBox.Show("请选择数据库");
            }
            IDbLink ldal = new DbLink();
            DbLinkInfo dlinfo = ldal.DbLinkGetInfo(DbLinkID);
            DataSet ds = dal.Run(dlinfo, sql, out sqlmsg, out rstmsg);
            if(ds!=null && ds.Tables !=null && ds.Tables[0].Rows.Count>0)
            {
                DataBaseTypeEnum dbtype = DataBaseTypeEnum.SQLServer;
                string TargetDbType =toolStripComboBox_TargetDbType.Text;

                if (TargetDbType == "SQLServer")
                    dbtype = DataBaseTypeEnum.SQLServer;
                else if (TargetDbType == "Oracle")
                    dbtype = DataBaseTypeEnum.Oracle;
                else if (TargetDbType == "MySql")
                    dbtype = DataBaseTypeEnum.MySql;
                string strSql = GetInserSql(ds.Tables[0], dbtype, out rstmsg);
                Clipboard.SetDataObject(strSql);
            }
            
        }


        private string GetInserSql(DataTable dt, DataBaseTypeEnum dbType, out string rstmsg)
        {
            TableInfo tInfo=new TableInfo();
            rstmsg = "";
            string sql = "";
            List<string> listSql = new List<string>();
            string field = "";
            foreach (DataColumn col in dt.Columns)
            {
                field += field == "" ? col.ColumnName : "," + col.ColumnName;
            }
            
            string[] fieldItem = field.Split(',');
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string fvalue = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string valueTypeName = dt.Columns[j].DataType.FullName.Replace("System.", "");
                    if (j == 0)
                    {
                        if (valueTypeName.ToLower() == "string")
                        {
                            fvalue = "'" + dr[j].ToString().Trim() + "'";
                        }
                        else if (valueTypeName.ToLower() == "datetime")
                        {
                            if (dr[j].ToString() == "" || dr[j].ToString().ToLower() == "null")
                                fvalue = "NULL";
                            else
                                fvalue = "'" + dr[j].ToString().Trim() + "'";
                        }
                        else
                        {
                            if (dr[j].ToString() == "" || dr[j].ToString().ToLower() == "null")
                                fvalue = "NULL";
                            else
                                fvalue = dr[j].ToString();
                        }
                    }
                    else
                    {
                        if (valueTypeName.ToLower() == "string")
                        {
                            fvalue = fvalue + ",'" + dr[j].ToString().Trim() + "'";
                        }
                        else if (valueTypeName.ToLower().IndexOf("datetime")>=0)
                        {
                            if (dr[j].ToString() == "" || dr[j].ToString().ToLower() == "null")
                                fvalue = fvalue + ",NULL";
                            else
                                fvalue = fvalue + "," + GetTimeData(dr[j].ToString().Trim(), dbType);
                        }
                        else
                        {
                            if (dr[j].ToString() == "" || dr[j].ToString().ToLower() == "null")
                                fvalue = fvalue + ",NULL";
                            else
                                fvalue = fvalue + "," + dr[j].ToString();
                        }
                    }
                }
                sql = "insert into '' (" + field + ")" + " values(" + fvalue + ")";
                listSql.Add(sql);
            }

            StringBuilder insertSql = new StringBuilder();
            foreach(var item in listSql)
            {
                insertSql.Append(item + ";\r\n");
            }

            return insertSql.ToString();
        }

        private string GetTimeData(string variable, DataBaseTypeEnum dbType)
        {
            variable = DateTime.Parse(variable).ToString("yyyy-MM-dd HH:mm:ss");
            switch (dbType)
            {
                case DataBaseTypeEnum.SQLServer:
                    variable = "Convert(datetime,'" + variable + "',120)";
                    break;
                case DataBaseTypeEnum.Oracle:
                    variable = "to_date('" + variable + "','yyyy-mm-dd hh24:mi:ss')";
                    break;
                case DataBaseTypeEnum.Access:

                    break;
                case DataBaseTypeEnum.SQLite:

                    break;
                case DataBaseTypeEnum.MySql:
                    variable = "date_format('" + variable + "', '%Y-%m-%d %H:%i:%S')";
                    break;
            }
            return variable;
        }
        #endregion

        #region

        //private void miEditCut_Click(object sender, EventArgs e)
        //{

        //}

        //private void miEditCopy_Click(object sender, EventArgs e)
        //{

        //}

        //private void miEditPaste_Click(object sender, EventArgs e)
        //{

        //}

        //private void miEditDelete_Click(object sender, EventArgs e)
        //{

        //}

        //private void miEditFind_Click(object sender, EventArgs e)
        //{

        //}

        //private void miEditReplace_Click(object sender, EventArgs e)
        //{

        //}

        //private void miEditFindNext_Click(object sender, EventArgs e)
        //{

        //}

        //private void miEditFindPrev_Click(object sender, EventArgs e)
        //{

        //}

        //private void miToggleBookmark_Click(object sender, EventArgs e)
        //{

        //}

        //private void miGoToNextBookmark_Click(object sender, EventArgs e)
        //{

        //}

        //private void miGoToPrevBookmark_Click(object sender, EventArgs e)
        //{

        //}

        //private void miSplitWindow_Click(object sender, EventArgs e)
        //{

        //}

        //private void miShowSpacesTabs_Click(object sender, EventArgs e)
        //{

        //}

        //private void miShowEOLMarkers_Click(object sender, EventArgs e)
        //{

        //}

        //private void miShowInvalidLines_Click(object sender, EventArgs e)
        //{

        //}

        //private void miShowLineNumbers_Click(object sender, EventArgs e)
        //{

        //}

        //private void miHLCurRow_Click(object sender, EventArgs e)
        //{

        //}

        //private void miBracketMatchingStyle_Click(object sender, EventArgs e)
        //{

        //}

        //private void miEnableVirtualSpace_Click(object sender, EventArgs e)
        //{

        //}

        //private void miConvertTabsToSpaces_Click(object sender, EventArgs e)
        //{

        //}

        //private void miSetTabSize_Click(object sender, EventArgs e)
        //{

        //}

        //private void miSetFont_Click(object sender, EventArgs e)
        //{

        //}

        #endregion

        #region 编辑器事件

        #region
        private string _xnOriginalXml;
        public string OriginalXml
        {
            get { return _xnOriginalXml; }
            set { _xnOriginalXml = value; }
        }

        public string TextContent { get; set; }

        bool _isEditPad = false;
        public bool IsEditPad
        {
            get { return _isEditPad; }
            set { _isEditPad = value; }
        }
        private TextEditorControl ActiveEditor
        {
            get
            {
                //if (fileTabs.TabPages.Count == 0) return null;
                //return fileTabs.SelectedTab.Controls.OfType<TextEditorControl>().FirstOrDefault();
                return this.textEditorControl;
            }
        }

        /// <summary>This variable holds the settings (whether to show line numbers, 
        /// etc.) that all editor controls share.</summary>
        ITextEditorProperties _editorSettings;

        /// <summary>
        /// Replaces the entire text of the xml view with the xml in the
        /// specified. The xml will be formatted.
        /// </summary>
        //public void FormatXml(string xml)
        //{
        //    string formattedXml = IndentedFormat(SimpleFormat(IndentedFormat(xml)));
        //    ActiveEditor.Document.Replace(0, ActiveEditor.Document.TextLength, formattedXml);
        //    UpdateFolding();
        //}

        /// <summary>
        /// Forces the editor to update its folds.
        /// </summary>
        void UpdateFolding()
        {
            ActiveEditor.Document.FoldingManager.UpdateFoldings(String.Empty, null);
            ActiveEditor.ActiveTextAreaControl.TextArea.Refresh();
        }

        private void InitForm()
        {
           
            if (_editorSettings == null)
            {
                _editorSettings = ActiveEditor.TextEditorProperties;
                OnSettingsChanged();
            }
            else
                ActiveEditor.TextEditorProperties = _editorSettings;

            if (!(ActiveEditor.Document.FoldingManager.FoldingStrategy is XmlFoldingStrategy))
            {
                ActiveEditor.Document.FoldingManager.FoldingStrategy = new XmlFoldingStrategy();
            }
            UpdateFolding();
        }

        #endregion


        #region 菜单事件
        void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void miOpen_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    //dialog.Filter = SharpPadFileFilter;
                    dialog.FilterIndex = 0;
                    if (DialogResult.OK == dialog.ShowDialog())
                    {
                        editor.LoadFile(dialog.FileName);
                        //CheckCurrentViewMode(editor.Document.HighlightingStrategy.Name);
                        //if (Path.GetExtension(dialog.FileName).ToLower() == ".xml")
                        //{
                        //    if (!(ActiveEditor.Document.FoldingManager.FoldingStrategy is XmlFoldingStrategy))
                        //    {
                        //        ActiveEditor.Document.FoldingManager.FoldingStrategy = new XmlFoldingStrategy();
                        //    }
                            
                        //}
                        UpdateFolding();
                    }
                }
            }
        }

        //private void CheckCurrentViewMode(string modeName)
        //{
        //    TextEditorControl editor = ActiveEditor;
        //    if (editor != null)
        //    {
        //        if (editor.Document.HighlightingStrategy != null && this.miViewMode.DropDownItems.Count > 0)
        //        {
        //            foreach (ToolStripMenuItem mi in this.miViewMode.DropDownItems)
        //            {
        //                if (mi.Tag != null && mi.Tag.ToString().Equals(modeName, StringComparison.OrdinalIgnoreCase))
        //                {
        //                    mi.Checked = true;
        //                }
        //                else
        //                {
        //                    mi.Checked = false;
        //                }
        //            }
        //        }
        //    }
        //}

        void miSaveAs_Click(object sender, System.EventArgs e)
        {
            //SaveAs();
        }

        //void SaveAs()
        //{
        //    TextEditorControl editor = ActiveEditor;
        //    if (editor != null)
        //    {
        //        using (SaveFileDialog dialog = new SaveFileDialog())
        //        {
        //            dialog.Filter = SharpPadFileFilter;
        //            dialog.FilterIndex = 0;
        //            if (DialogResult.OK == dialog.ShowDialog())
        //            {
        //                editor.SaveFile(dialog.FileName);
        //                editor.FileName = dialog.FileName;
        //            }
        //        }
        //    }
        //}

        void miSave_Click(object sender, System.EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                if (editor.FileName != null)
                {
                    editor.SaveFile(editor.FileName);
                }
                else
                {
                    //SaveAs();
                }
            }
        }

        #endregion
        #region 按钮事件
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument xdTmp = new XmlDocument();
                xdTmp.LoadXml(ActiveEditor.Document.TextContent);
                this.TextContent = xdTmp.InnerXml;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        private void miSplitWindow_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.Split();
            OnSettingsChanged();
        }

        /// <summary>Show current settings on the Options menu</summary>
        /// <remarks>We don't have to sync settings between the editors because 
        /// they all share the same DefaultTextEditorProperties object.</remarks>
        private void OnSettingsChanged()
        {
            //this.miSplitWindow.Checked = ActiveEditor.IsSplited;
            this.miShowSpacesTabs.Checked = _editorSettings.ShowSpaces;
            this.miShowEOLMarkers.Checked = _editorSettings.ShowEOLMarker;
            this.miShowInvalidLines.Checked = _editorSettings.ShowInvalidLines;
            this.miHLCurRow.Checked = _editorSettings.LineViewerStyle == LineViewerStyle.FullRow;
            this.miBracketMatchingStyle.Checked = _editorSettings.BracketMatchingStyle == BracketMatchingStyle.After;
            this.miEnableVirtualSpace.Checked = _editorSettings.AllowCaretBeyondEOL;
            this.miShowLineNumbers.Checked = _editorSettings.ShowLineNumbers;
            this.miConvertTabsToSpaces.Checked = ActiveEditor.ConvertTabsToSpaces;
        }

        private void miShowSpacesTabs_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.ShowSpaces = editor.ShowTabs = !editor.ShowSpaces;
            OnSettingsChanged();
        }

        private void miShowEOLMarkers_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.ShowEOLMarkers = !editor.ShowEOLMarkers;
            OnSettingsChanged();
        }

        private void miShowInvalidLines_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.ShowInvalidLines = !editor.ShowInvalidLines;
            OnSettingsChanged();
        }

        private void miShowLineNumbers_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.ShowLineNumbers = !editor.ShowLineNumbers;
            OnSettingsChanged();
        }

        private void miHLCurRow_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.LineViewerStyle = editor.LineViewerStyle == LineViewerStyle.None
                ? LineViewerStyle.FullRow : LineViewerStyle.None;
            OnSettingsChanged();
        }

        private void miBracketMatchingStyle_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.BracketMatchingStyle = editor.BracketMatchingStyle == BracketMatchingStyle.After
                ? BracketMatchingStyle.Before : BracketMatchingStyle.After;
            OnSettingsChanged();
        }

        private void miEnableVirtualSpace_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.AllowCaretBeyondEOL = !editor.AllowCaretBeyondEOL;
            OnSettingsChanged();
        }

        private void miConvertTabsToSpaces_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.ConvertTabsToSpaces = !editor.ConvertTabsToSpaces;
            OnSettingsChanged();
        }

        private void miSetTabSize_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
            {
                string result = InputBox.Show("请指定制表符大小：", "制表符大小", _editorSettings.TabIndent.ToString());
                int value;
                if (result != null && int.TryParse(result, out value) && Globals.IsInRange(value, 1, 32))
                {
                    ActiveEditor.TabIndent = value;
                }
            }
        }

        private void miSetFont_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                fontDialog.Font = editor.Font;
                if (fontDialog.ShowDialog(this) == DialogResult.OK)
                {
                    editor.Font = fontDialog.Font;
                    OnSettingsChanged();
                }
            }
        }

        /// <summary>Performs an action encapsulated in IEditAction.</summary>
        /// <remarks>
        /// There is an implementation of IEditAction for every action that 
        /// the user can invoke using a shortcut key (arrow keys, Ctrl+X, etc.)
        /// The editor control doesn't provide a public funciton to perform one
        /// of these actions directly, so I wrote DoEditAction() based on the
        /// code in TextArea.ExecuteDialogKey(). You can call ExecuteDialogKey
        /// directly, but it is more fragile because it takes a Keys value (e.g.
        /// Keys.Left) instead of the action to perform.
        /// <para/>
        /// Clipboard commands could also be done by calling methods in
        /// editor.ActiveTextAreaControl.TextArea.ClipboardHandler.
        /// </remarks>
        private void DoEditAction(TextEditorControl editor, ICSharpCode.TextEditor.Actions.IEditAction action)
        {
            if (editor != null && action != null)
            {
                TextArea area = editor.ActiveTextAreaControl.TextArea;
                editor.BeginUpdate();
                try
                {
                    lock (editor.Document)
                    {
                        action.Execute(area);
                        if (area.SelectionManager.HasSomethingSelected && area.AutoClearSelection /*&& caretchanged*/)
                        {
                            if (area.Document.TextEditorProperties.DocumentSelectionMode == DocumentSelectionMode.Normal)
                            {
                                area.SelectionManager.ClearSelection();
                            }
                        }
                    }
                }
                finally
                {
                    editor.EndUpdate();
                    area.Caret.UpdateCaretPosition();
                }
            }
        }

        private bool HaveSelection()
        {
            TextEditorControl editor = ActiveEditor;
            return editor != null &&
                editor.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected;
        }

        private void miEditCut_Click(object sender, EventArgs e)
        {
            if (HaveSelection())
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Cut());
        }

        private void miEditCopy_Click(object sender, EventArgs e)
        {
            if (HaveSelection())
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Copy());
        }

        private void miEditPaste_Click(object sender, EventArgs e)
        {
            DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Paste());
        }

        private void miEditDelete_Click(object sender, EventArgs e)
        {
            if (HaveSelection())
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Delete());
        }

        FindAndReplaceForm _findForm = new FindAndReplaceForm();



        private void miEditFind_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            _findForm.ShowFor(editor, false);
        }

        private void miEditReplace_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            _findForm.ShowFor(editor, true);
        }

        private void miEditFindNext_Click(object sender, EventArgs e)
        {
            _findForm.FindNext(true, false,
                string.Format("没有找到你要查找的内容！", _findForm.LookFor));
        }

        private void miEditFindPrev_Click(object sender, EventArgs e)
        {
            _findForm.FindNext(true, true,
                string.Format("没有找到你要查找的内容！", _findForm.LookFor));
        }

        private void miToggleBookmark_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.ToggleBookmark());
                editor.IsIconBarVisible = editor.Document.BookmarkManager.Marks.Count > 0;
            }
        }

        private void miGoToNextBookmark_Click(object sender, EventArgs e)
        {
            DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.GotoNextBookmark(new Predicate<Bookmark>(delegate(Bookmark bookmark)
            {
                return true;
            })));
        }

        private void miGoToPrevBookmark_Click(object sender, EventArgs e)
        {
            DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.GotoPrevBookmark(new Predicate<Bookmark>(delegate(Bookmark bookmark)
            {
                return true;
            })));
        }


        #endregion

        /// <summary>
        /// 复制数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void tabControl1_KeyUp(object sender, KeyEventArgs e)
        {
            var keyData = e.KeyData;
            DataGridView dgv_Fbfx = new DataGridView();
            switch (tabControl1.SelectedTab.Name)
            {
                case "tabPage1":
                    dgv_Fbfx = DGV1;
                    break;
                case "tabPage2":
                    dgv_Fbfx = DGV1;
                    break;
                case "tabPage3":
                    dgv_Fbfx = DGV1;
                    break;
                case "tabPage4":
                    dgv_Fbfx = DGV1;
                    break;
                case "tabPage5":
                    dgv_Fbfx = DGV1;
                    break;

            }

            if (keyData == Keys.C)
            {

                if (dgv_Fbfx.CurrentCell != null)
                {

                    int row = dgv_Fbfx.CurrentCell.RowIndex;
                    int col = dgv_Fbfx.CurrentCell.ColumnIndex;
                    if (col < dgv_Fbfx.Columns.Count - 1)
                    {
                        if (dgv_Fbfx.Rows[row].Cells[col].Value != null)
                        {
                            //Clipboard.SetText(dgv_Fbfx.Rows[row].Cells[col].Value.ToString());
                            //Clipboard.SetDataObject(dgv_Fbfx.GetClipboardContent());//将控件选中的数据置于系统剪贴板中
                            //dgv_Fbfx.RowHeadersVisible = false;
                            if (dgv_Fbfx.GetClipboardContent() != null)
                                Clipboard.SetText(dgv_Fbfx.GetClipboardContent().GetData(DataFormats.UnicodeText, true).ToString());

                            //dgv_Fbfx.RowHeadersVisible = true;
                        }
                        else
                        {
                            Clipboard.SetText("");
                        }
                    }
                }
            }
            else if (keyData == Keys.V)
            {
                if (dgv_Fbfx.CurrentCell != null)
                {
                    int col = dgv_Fbfx.CurrentCell.ColumnIndex;
                    if (col < dgv_Fbfx.Columns.Count - 1)
                    {
                        int insertRowIndex = dgv_Fbfx.CurrentCell.RowIndex;
                        string pasteText = Clipboard.GetText();
                        dgv_Fbfx.Rows[insertRowIndex].Cells[col].Value = pasteText;
                    }
                }
            }
        }

        private void textEditorControl_KeyUp(object sender, KeyEventArgs e)
        {
            var keyData = e.KeyData;
            if (keyData == Keys.F5)
            {
                RunToolScriptBtn_Click(null, null);
            }
        }











    }
}
