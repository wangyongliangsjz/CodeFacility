using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

using DALFactory.CodeMaker;
using AccessDal.CodeMaker;
using Model.CodeMaker;

namespace CodeFacility.CodeMaker
{
    public partial class FormCreater : FormWin
    {
        ITempletType menuDal = new TempletType();
        IList<TempletTypeInfo> menuList; //TreeView目录所有数据
        TempletInfo templetInfo; //父目录实体
        Boolean nodekey; //TreeView目录递归搜索 nodekey=true 执行 nodekey=false 停止
        int index = 0; //TreeView目录递归搜索序号
        int nodeid = 0; //TreeView目录递归搜索序号暂存
        int nodeMax1 = 0; //TreeView最后节点
        int nodeMax2 = 0; //TreeView当前节点
        IList<TempletInfo> templetList = new List<TempletInfo>(); //表单模板数据
        DbDataInfo dinfo = new DbDataInfo(); //
        IList<FieldInfo> filedSettingsList = new List<FieldInfo>();
        

        public FormCreater()
        {
            InitializeComponent();
        }

        private void FormCreater_Load(object sender, EventArgs e)
        {
            QueryMenu();
            //订阅了信息发送事件，即接受参数值
            Common.MidModule.EventSend += new Common.DataDlg(MidModule_EventSend);

            InitOption();
        }

        private void InitOption()
        {
            panel1.BackColor = BaseConfigure.ColorTheme;
            tv_left.BackColor = BaseConfigure.ColorTheme;
            textEditorControl.BackColor = BaseConfigure.ColorTheme;
            //ICSharpCode.TextEditor.Document.TextMarker
            textEditorControl.Font = new System.Drawing.Font(BaseConfigure.FontTypeface, BaseConfigure.FontSize, FontStyle.Regular);

        }

        //接受参数事件
        private void MidModule_EventSend(object sender, object data, object e)
        {
            if (sender != null)
            {
                Model.EventInfo einfo = e as Model.EventInfo;
                if (einfo.Title != "表单代码生成器")
                    return;
                Form fr = sender as Form;
                if (fr.Text == "对象资源管理器")
                {
                    dinfo = data as DbDataInfo;
                    string msg = "数据库：" + dinfo.DbName;
                    if (DbDataType.GetDbDataType(dinfo.NameType).ToString() != "数据库")
                    {
                        msg = msg + " " + DbDataType.GetDbDataType(dinfo.NameType).ToString() + "：" + dinfo.Name;

                        getdgv_left_Data(dinfo.Name);
                    }
                    lb_DbMessage.Text = msg;
                    textEditorControl.Text = "";
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
            textEditorControl.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
            textEditorControl.Encoding = Encoding.GetEncoding("GB2312");
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            string Content = "";
            string rstmsg = "";
            if (templetInfo == null)
            {
                MessageBox.Show("请选择模板。");
                return;
            }
            switch (templetInfo.Postfix)
            {
                case "xslt":
                    Content=GetCodeByXslt(out rstmsg);
                    break;
                case "html":
                    Content = GetCodeByHtml(out rstmsg);
                    break;
                case "txt":
                    Content = GetCodeByTxt(out rstmsg);
                    break;
            }

            if (rstmsg == "")
            {
                textEditorControl.Text = Content;
            }
            else
            {
                MessageBox.Show(rstmsg);
            }
        }

        #region 生成代码相关方法

        private string GetCodeByXslt(out string rstmsg)
        {
            rstmsg = "";
            string Content = "";

            DataBaseInfo info = GetDbInfo(dinfo.DbLinkID);
            string xml = null;
            string tablename = dinfo.Name;
            TableInfo table = null;
            DbDataTypeEnum ddt = DbDataType.GetDbDataType(dinfo.NameType);
            switch (ddt)
            {
                case DbDataTypeEnum.表:
                    table = info.Tables[tablename];
                    break;
                case DbDataTypeEnum.视图:
                    table = info.View[tablename];
                    break;
            }

            if (table == null)
            {
                MessageBox.Show("获取数据失败。");
                return Content;
            }
            try
            {
                #region 生成设置
                if (dgv_right.Rows.Count > 0)
                {
                    if (dgv_right.Rows[0].Cells["rName"].Value != null && dgv_right.Rows.Count != 1)
                    {
                        IList<FieldInfo> olist = table.Fields.GetList();
                        table.Fields.Clear();

                        for (int i = 0; i < dgv_right.Rows.Count; i++)
                        {
                            if (dgv_right.Rows[i].Cells["rName"].Value != null)
                            {
                                string name = dgv_right.Rows[i].Cells["rName"].Value == null ? "" : dgv_right.Rows[i].Cells["rName"].Value.ToString().Trim();
                                string description = dgv_right.Rows[i].Cells["rDescription"].Value == null ? "" : dgv_right.Rows[i].Cells["rDescription"].Value.ToString().Trim();
                                string PrimaryKey = dgv_right.Rows[i].Cells["rPrimaryKey"].Value == null ? "" : dgv_right.Rows[i].Cells["rPrimaryKey"].Value.ToString().Trim();
                                string IsIdentity = dgv_right.Rows[i].Cells["rIsIdentity"].Value == null ? "" : dgv_right.Rows[i].Cells["rIsIdentity"].Value.ToString().Trim();
                                string Nullable = dgv_right.Rows[i].Cells["rNullable"].Value == null ? "" : dgv_right.Rows[i].Cells["rNullable"].Value.ToString().Trim();
                                string FieldType = dgv_right.Rows[i].Cells["rFieldType"].Value == null ? "" : dgv_right.Rows[i].Cells["rFieldType"].Value.ToString().Trim();

                                var wlist = olist.Where(t => t.Name == name).ToList();
                                if (wlist.Count > 0)
                                {
                                    var winfo = wlist[0];
                                    winfo.Description = description;
                                    table.Fields.Add(winfo);
                                }
                                else if (name != "")
                                {

                                    FieldInfo nfinfo = new FieldInfo();
                                    nfinfo.Name = name;
                                    nfinfo.Description = description;
                                    nfinfo.PrimaryKey = PrimaryKey=="" ? false : Convert.ToBoolean( PrimaryKey);
                                    nfinfo.IsIdentity = IsIdentity =="" ? false : Convert.ToBoolean(IsIdentity);
                                    nfinfo.Nullable = Nullable == "" ? false : Convert.ToBoolean(Nullable);
                                    nfinfo.FieldType = FieldType;

                                    table.Fields.Add(nfinfo);
                                }
                            }
                        }
                    }
                }
                #endregion

                xml = GetXMLString(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show("失败。"+ex.Message);
            }

            IUpLoadFile ufDal = new AccessDal.CodeMaker.UpLoadFile();
            UpLoadFileInfo upfInfo = new UpLoadFileInfo();
            upfInfo = ufDal.UpLoadFileGetInfo("Cm_Templet", templetInfo.ID);
            string path = "";
            if (upfInfo != null && upfInfo.FileID != 0)
            {
                path = AppDomain.CurrentDomain.BaseDirectory + upfInfo.FilePath + "\\" + upfInfo.UniqueName;
            }

            //获取模板
            string templet = Common.FileHandle.ReadFile(path);
            string strtemplet = ReplaceLable(templet);
            string fpath = AppDomain.CurrentDomain.BaseDirectory + @"CodeMaker\Temp_" +upfInfo.UniqueName;
            //保存临时模板
            Common.FileHandle.WirteCreateFile(strtemplet, fpath);
            
            try
            {
                // 启动了XSLT模板，执行XSLT转换
                //System.Xml.Xsl.XslTransform transform = new System.Xml.Xsl.XslTransform();
                System.Xml.Xsl.XslCompiledTransform transform = new System.Xml.Xsl.XslCompiledTransform();
                transform.Load(fpath);
                System.IO.StringWriter writer = new System.IO.StringWriter();
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(xml);
                //transform.Transform(doc, null, writer, null);
                transform.Transform(doc, null, writer);
                writer.Close();
                Content = writer.ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成失败，"+ex.Message);
            }
            //删除临时模板
            Common.FileHandle.DeleteFile(fpath);

            //Content = ReplaceLable(Content);

            Content = Content.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            Content = Content.Replace("&lt;", "<");
            Content = Content.Replace("&gt;", ">");

            Content = Content.Replace("&amp;", "&");
            return Content;
        }
        private string GetCodeByHtml(out string rstmsg)
        {
            rstmsg = "";
            string Content = "";
            string templet = "";

            IUpLoadFile ufDal = new AccessDal.CodeMaker.UpLoadFile();
            UpLoadFileInfo upfInfo = new UpLoadFileInfo();
            upfInfo = ufDal.UpLoadFileGetInfo("Cm_Templet", templetInfo.ID);
            string path = "";
            if (upfInfo != null && upfInfo.FileID != 0)
            {
                path = AppDomain.CurrentDomain.BaseDirectory + upfInfo.FilePath + "\\" + upfInfo.UniqueName;
            }
            //获取模板
            templet = Common.FileHandle.ReadFile(path);
            Content = ReplaceLable(templet);
                    
            
            return Content;
        }
        private string GetCodeByTxt(out string rstmsg)
        {
            rstmsg = "";
            string Content = "";

            return Content;
        }

        /// <summary>
        /// 获取模板内的标签列表
        /// </summary>
        /// <param name="templet">模板</param>
        /// <returns>已替换标签的模板</returns>
        private string ReplaceLable(string templet)
        {
            string Content = "";
            int t1 = 0;
            int t2 = 0;
            List<string> lablist = new List<string>();
            string tp = "";
            tp = templet;
            while (t2 < tp.Length)
            {
                t1 = tp.IndexOf("{%");
                if (t1 > -1)
                {
                    tp = tp.Substring(t1, tp.Length - t1);
                    t2 = tp.IndexOf("%}") + 2;
                    if (t2 > 2)
                    {
                        lablist.Add(tp.Substring(0, t2));
                        tp = tp.Substring(t2, tp.Length - t2);
                    }
                }
                else
                {
                    t2 = tp.Length;
                }
            }

            ILabel ldal = new AccessDal.CodeMaker.Label();
            IList<LabelInfo> lblist = new List<LabelInfo>();
            //获取标签
            foreach (string title in lablist)
            {
                LabelInfo info = ldal.LabelGetInfo(title);
                if (info != null)
                {
                    if (info.Title != "")
                    {
                        lblist.Add(info);
                    }
                }
            }

            Content = templet;
            foreach (LabelInfo info in lblist)
            {
                //替换标签
                Content = Content.Replace(info.Title, info.Content);
            }

            return Content;
        }

        /// <summary>
        /// 将指定对象序列化成XML文档，然后返回获得的XML字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>XML字符串</returns>
        private string GetXMLString(object obj)
        {
            System.IO.StringWriter myStr = new System.IO.StringWriter();
            System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(myStr);
            writer.Indentation = 3;
            writer.IndentChar = ' ';
            writer.Formatting = System.Xml.Formatting.Indented;
            System.Xml.Serialization.XmlSerializer sc =
                new System.Xml.Serialization.XmlSerializer(obj.GetType());
            sc.Serialize(writer, obj);
            writer.Close();
            string xml = myStr.ToString();
            int index = xml.IndexOf("?>");
            if (index > 0)
                xml = xml.Substring(index + 2);
            return xml.Trim();
        }

        private DataBaseInfo GetDbInfo(int ID)
        {
            IDbLink dal = new DbLink();
            DbLinkInfo dlinfo = dal.DbLinkGetInfo(ID);

            IDataBase dbDal=new  CurrencyDal.CodeMaker.DataBase();
            string rstmsg="";
            List<string> tableNameList = new List<string>();
            string tableName = "";
            DbDataTypeEnum dtype = DbDataType.GetDbDataType(dinfo.NameType);
            if (dtype == DbDataTypeEnum.表)
            {
                tableName = dinfo.Name;
                tableNameList.Add(tableName);
            }
            DataBaseInfo dbinfo = dbDal.DataBaseGetInfo(dlinfo, tableNameList, out rstmsg);

            //DataBaseInfo2 info = new DataBaseInfo2();

            //try
            //{
                //switch (dlinfo.DbType)
                //{

                //    case 1:
                //        System.Data.Common.DbConnection connSql = new System.Data.SqlClient.SqlConnection();
                //        connSql.ConnectionString = "Data Source=" + dlinfo.DataSource + ";Initial Catalog=" + dlinfo.DbName + ";User ID=" + dlinfo.UserName + ";Password=" + dlinfo.PassWord;
                //        info.LoadFromSQLServer(connSql);
                //        break;
                //    case 2:
                //        System.Data.Common.DbConnection connOracle = new Devart.Data.Oracle.OracleConnection();

                //        connOracle.ConnectionString = "Data Source=" + dlinfo.DbName + ";User ID=" + dlinfo.UserName + ";Password=" + dlinfo.PassWord + "";
                //        info.GetOracleDb(connOracle);
                //        break;
                //    case 3:
                //        string ConnString = dlinfo.DataSource;
                //        info.LoadFromAccess2000(ConnString);
                //        break;

                //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("操作失败。" + ex.Message);
            //}

            return dbinfo;
        }
        #endregion


        #region 菜单Treeview相关事件
        private void QueryToolStripBtn_Click(object sender, EventArgs e)
        {
            tv_left.Focus();
            index = 0;
            nodekey = true;
            nodeMax2 = 0;
            string title = QueryToolStripTb.Text;
            TreeNodeCollection tnc = tv_left.Nodes;
            QueryNode(tnc, title);
            if (nodeMax1 == nodeMax2 && nodeMax1 != 0 && nodeMax2 != 0)
            {
                nodeid = 0;
                MessageBox.Show("搜索结束。");
            }
        }
        private void ClearToolStripBtn_Click(object sender, EventArgs e)
        {
            QueryToolStripTb.Text = "";
            nodeid = 0;
            QueryMenu();
        }
        private void tv_left_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tv_left.SelectedNode = e.Node;
            if (tv_left.SelectedNode != null)
            {

                if (tv_left.SelectedNode.Name == "Templet")
                {
                    TempletInfo info = tv_left.SelectedNode.Tag as TempletInfo;
                    if (info != null)
                    {
                        templetInfo = info;
                        lb_Templet.Text = info.Title;
                    }
                }
                else
                {
                    templetInfo = null;
                    lb_Templet.Text = "";
                }
                textEditorControl.Text = "";

            }
        }
        #endregion

        #region 菜单Treeview相关方法
        /// <summary>
        /// 显示菜单Treeview
        /// </summary>
        private void QueryMenu()
        {
            nodeid = 0;
            menuList = menuDal.TempletTypeGetList();
            ITemplet tdal = new Templet();
            templetList = tdal.TempletGetList();

            TreeNode tn = new TreeNode("根目录");
            TempletTypeInfo info = new TempletTypeInfo();
            info.ID = 0;
            GetNode(tn, info);
            tn.ExpandAll();
            tv_left.Nodes.Clear();
            tv_left.Nodes.Add(tn);

            nodeMax1 = 0;
            TreeNodeCollection tnc = tv_left.Nodes;
            GetNodeNameMax(tnc);

        }

        /// <summary>
        /// 递归获取目录节点
        /// </summary>
        /// <param name="tn">节点</param>
        /// <param name="tinfo">实体</param>
        private void GetNode(TreeNode tn, TempletTypeInfo tinfo)
        {
            var list = from tl in menuList
                       where tl.ParentID == tinfo.ID
                       select tl;
            foreach (TempletTypeInfo info in list)
            {
                TreeNode tnode = new TreeNode(info.Title);
                GetNode(tnode, info);
                var tplist = from tp in templetList
                             where tp.ParentID == info.ID
                             select tp;
                foreach( TempletInfo tpinfo in tplist)
                {
                    TreeNode tpnode = new TreeNode(tpinfo.Title);
                    tpnode.Tag = tpinfo;
                    tpnode.Name = "Templet";
                    tnode.Nodes.Add(tpnode);
                }
                tnode.Tag = info;
                tn.Nodes.Add(tnode);
            }
        }

        /// <summary>
        /// 递归获取实体
        /// </summary>
        /// <param name="ilist">节点</param>
        /// <param name="ninfo">实体</param>
        private void GetMenuInfo(IList<TempletTypeInfo> ilist, TempletTypeInfo ninfo)
        {
            ilist.Add(ninfo);
            var list = from tl in menuList
                       where tl.ParentID == ninfo.ID
                       select tl;
            foreach (TempletTypeInfo info in list)
            {
                GetMenuInfo(ilist, info);
            }
        }

        /// <summary>
        /// 菜单节点数量Treeview
        /// </summary>
        /// <param name="tnc">节点控件</param>
        /// <param name="title">节点标题</param> 
        private void GetNodeNameMax(TreeNodeCollection tnc)
        {
            foreach (TreeNode tn in tnc)
            {
                nodeMax1++;
                if (tn.Nodes.Count > 0)
                {
                    GetNodeNameMax(tn.Nodes);
                }
            }
        }

        /// <summary>
        /// 搜索菜单Treeview
        /// </summary>
        /// <param name="tnc">节点控件</param>
        /// <param name="title">节点标题</param> 
        private void QueryNode(TreeNodeCollection tnc, string title)
        {
            foreach (TreeNode tn in tnc)
            {
                nodeMax2++;
                if (tn.Text.IndexOf(title) > -1)
                {
                    index++;
                    if (index > nodeid)
                    {
                        tv_left.SelectedNode = tn;
                        nodeid = index;
                        nodekey = false;
                        return;
                    }
                }
                if (tn.Nodes.Count > 0 && nodekey)
                {
                    QueryNode(tn.Nodes, title);
                }
            }
        }
        #endregion

        private void btn_Copy_Click(object sender, EventArgs e)
        {
            if (textEditorControl.Text.Trim() != "")
            {
                Clipboard.SetDataObject(textEditorControl.Text);
            }

        }

        private void chb_Rows_CheckedChanged(object sender, EventArgs e)
        {
            //if (chb_Rows.Checked)
            //{
            //    textEditorControl.WordWrap = true;
            //}
            //else
            //{
            //    textEditorControl.WordWrap = false;
            //}
        }


        #region 生成设置
        private void getdgv_left_Data(string tableName)
        {
            DataBaseInfo info = GetDbInfo(dinfo.DbLinkID);
            string tablename = dinfo.Name;
            TableInfo table = null;
            DbDataTypeEnum ddt = DbDataType.GetDbDataType(dinfo.NameType);
            switch (ddt)
            {
                case DbDataTypeEnum.表:
                    table = info.Tables[tablename];
                    break;
                case DbDataTypeEnum.视图:
                    table = info.View[tablename];
                    break;
            }

            if (table == null)
            {
                MessageBox.Show("获取数据失败。");
            }

            dgv_left.DataSource = table.Fields;
            dgv_right.Rows.Clear();
            filedSettingsList = null;

        }

        

        private void btn_toRightAll_Click(object sender, EventArgs e)
        {
            dgv_right.Rows.Clear();
            for (int i = 0; i < dgv_left.Rows.Count; i++)
            {
                //如果DataGridView是可编辑的，将数据提交，否则处于编辑状态的行无法取到 
                //dgv_left.EndEdit();
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgv_left.Rows[i].Cells["select"];

                //从 DATAGRIDVIEW 中获取数据项 
                string fanme = dgv_left.Rows[i].Cells["ltName"].Value.ToString().Trim();
                string description = dgv_left.Rows[i].Cells["ltDescription"].Value.ToString().Trim();
                string PrimaryKey = dgv_left.Rows[i].Cells["PrimaryKey"].Value == null ? "" : dgv_left.Rows[i].Cells["PrimaryKey"].Value.ToString().Trim();
                string IsIdentity = dgv_left.Rows[i].Cells["IsIdentity"].Value == null ? "" : dgv_left.Rows[i].Cells["IsIdentity"].Value.ToString().Trim();
                string Nullable = dgv_left.Rows[i].Cells["Nullable"].Value == null ? "" : dgv_left.Rows[i].Cells["Nullable"].Value.ToString().Trim();
                string FieldType = dgv_left.Rows[i].Cells["FieldType"].Value == null ? "" : dgv_left.Rows[i].Cells["FieldType"].Value.ToString().Trim();

                this.dgv_right.Rows.Add(false, fanme, description, PrimaryKey, IsIdentity, Nullable, FieldType);

            }
        }

        private void btn_toRight_Click(object sender, EventArgs e)
        {

            int count = Convert.ToInt32(dgv_left.Rows.Count.ToString());
            for (int i = 0; i < count; i++)
            {
                //如果DataGridView是可编辑的，将数据提交，否则处于编辑状态的行无法取到 
                //dgv_left.EndEdit();
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgv_left.Rows[i].Cells["select"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag == true)     //查找被选择的数据行 
                {
                    //从 DATAGRIDVIEW 中获取数据项 
                    string fanme = dgv_left.Rows[i].Cells["ltName"].Value == null ? "" : dgv_left.Rows[i].Cells["ltName"].Value.ToString().Trim();
                    string description = dgv_left.Rows[i].Cells["ltDescription"].Value == null ? "" : dgv_left.Rows[i].Cells["ltDescription"].Value.ToString().Trim();
                    string PrimaryKey = dgv_left.Rows[i].Cells["PrimaryKey"].Value == null ? "" : dgv_left.Rows[i].Cells["PrimaryKey"].Value.ToString().Trim();
                    string IsIdentity = dgv_left.Rows[i].Cells["IsIdentity"].Value == null ? "" : dgv_left.Rows[i].Cells["IsIdentity"].Value.ToString().Trim();
                    string Nullable = dgv_left.Rows[i].Cells["Nullable"].Value == null ? "" : dgv_left.Rows[i].Cells["Nullable"].Value.ToString().Trim();
                    string FieldType = dgv_left.Rows[i].Cells["FieldType"].Value == null ? "" : dgv_left.Rows[i].Cells["FieldType"].Value.ToString().Trim();

                    if (!GetSelectedRowIndex(dgv_right, fanme))
                    {
                        this.dgv_right.Rows.Add(false, fanme, description, PrimaryKey, IsIdentity, Nullable, FieldType);

                    }
                }
            }
        }

        private void btn_toLeft_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_right.Rows.Count; i++)
            {
                //如果DataGridView是可编辑的，将数据提交，否则处于编辑状态的行无法取到 
                //dgv_left.EndEdit();
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgv_right.Rows[i].Cells["rSelect"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag == true)     //查找被选择的数据行 
                {
                    //从 DATAGRIDVIEW 中删除项 
                    dgv_right.Rows.RemoveAt(i);
                    i--;
                }

            }

        }

        private void btn_toLeftAll_Click(object sender, EventArgs e)
        {
            dgv_right.Rows.Clear();
        }

        private void btn_rightUp_Click(object sender, EventArgs e)
        {

            int selectedRowIndex = GetSelectedRowIndex(this.dgv_right);

            if (selectedRowIndex >= 1)
            {
                // 拷贝选中的行  
                DataGridViewRow newRow = dgv_right.Rows[selectedRowIndex];

                // 删除选中的行  
                dgv_right.Rows.Remove(dgv_right.Rows[selectedRowIndex]);

                // 将拷贝的行，插入到选中的上一行位置  
                dgv_right.Rows.Insert(selectedRowIndex - 1, newRow);

                // 选中最初选中的行  
                dgv_right.Rows[selectedRowIndex - 1].Selected = true;
            }  
        }


        private void btn_rightDown_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = GetSelectedRowIndex(this.dgv_right);
            if (selectedRowIndex < dgv_right.Rows.Count - 1)
            {
                // 拷贝选中的行  
                DataGridViewRow newRow = dgv_right.Rows[selectedRowIndex];

                // 删除选中的行  
                dgv_right.Rows.Remove(dgv_right.Rows[selectedRowIndex]);

                // 将拷贝的行，插入到选中的下一行位置  
                dgv_right.Rows.Insert(selectedRowIndex + 1, newRow);

                // 选中最初选中的行  
                dgv_right.Rows[selectedRowIndex + 1].Selected = true;
            }  
        }

        // 获取DataGridView中选择的行索引号  
        private int GetSelectedRowIndex(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0)
            {
                return 0;
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Selected)
                {
                    return row.Index;
                }
            }
            return 0;
        }

        // 判断DataGridView是否有此行  
        private bool GetSelectedRowIndex(DataGridView dgv,string name)
        {
            if (dgv.Rows.Count == 0)
            {
                return false;
            }

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                //从 DATAGRIDVIEW 中获取数据项 
                var a1 = dgv.Rows[i].Cells["rName"];
                var a2=a1.Value;

                string fanme = dgv.Rows[i].Cells["rName"] == null || dgv.Rows[i].Cells["rName"].Value ==null ? "" : dgv.Rows[i].Cells["rName"].Value.ToString().Trim();
                if (fanme == name)
                    return true;
            }
            return false;
        }

        #endregion

    }
}
