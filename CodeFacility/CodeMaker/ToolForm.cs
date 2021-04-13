using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

using DALFactory.CodeMaker;
using AccessDal.CodeMaker;
using Model.CodeMaker;

namespace CodeFacility.CodeMaker
{
    public partial class ToolForm : DockContent
    {
        ContextMenuStrip cms = new ContextMenuStrip();
        IDbLink dal = new DbLink();
        public ToolForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 在dockPanel中查找已经打开的窗口
        /// </summary>
        /// <param name="text">传入的窗口标题</param>
        /// <returns>返回的窗口</returns>
        private IDockContent FindDocument(string text)
        {
            if (DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    if (form.Text == text)
                        return form as IDockContent;

                return null;
            }
            else
            {
                foreach (IDockContent content in DockPanel.Documents)
                    if (content.DockHandler.TabText == text)
                        return content;

                return null;
            }
        }

        private void ToolForm_Load(object sender, EventArgs e)
        {
                QueryData();
                InitOption();
        }

        private void InitOption()
        {
            tv_Db.BackColor = BaseConfigure.ColorTheme;
        }

        /// <summary>
        /// 菜单添加数据库
        /// </summary>
        private void QueryData()
        {
            try
            {
                dal = new DbLink();
                IList<DbLinkInfo> ilist = dal.DbLinkGetList();
                tv_Db.Nodes.Clear();
                TreeNode tn = new TreeNode();
                tn.Text = "服务器";
                TreeNode tnode = null;
                foreach (DbLinkInfo info in ilist)
                {
                    tnode = new TreeNode();
                    tnode.Text = string.IsNullOrEmpty(info.DbAbbreviation) ? info.DbName : info.DbName + "(" + info.DbAbbreviation + ")";
                    DbDataInfo ServerInfo = new DbDataInfo();
                    ServerInfo.DbLinkID = info.ID;
                    ServerInfo.DbName = info.DbName;
                    ServerInfo.NameType = (int)DbDataTypeEnum.数据库;
                    ServerInfo.Name = info.DbName;
                    tnode.Tag = ServerInfo;
                    tn.Nodes.Add(tnode);
                }
                tn.ExpandAll();
                tv_Db.Nodes.Add(tn);
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取数据库失败，" + ex.Message);
            }
           
        }

        private DataBaseInfo GetDbInfo(int ID)
        {
            dal = new DbLink();
            DbLinkInfo dlinfo = dal.DbLinkGetInfo(ID);

            IDataBase dbDal = new CurrencyDal.CodeMaker.DataBase();
            string rstmsg = "";
            List<string> tableNameList = new List<string>();
            DataBaseInfo dbinfo = dbDal.DataBaseGetInfo(dlinfo, tableNameList, out rstmsg);

            //DataBaseInfo dbinfo = new DataBaseInfo();
            //DataBaseInfo2 info = new DataBaseInfo2();
            //try
            //{
            //    switch (dlinfo.DbType)
            //    {

            //        case 1:
            //            System.Data.Common.DbConnection connSql = new System.Data.SqlClient.SqlConnection();
            //            connSql.ConnectionString = "Data Source=" + dlinfo.DataSource + ";Initial Catalog=" + dlinfo.DbName + ";User ID=" + dlinfo.UserName + ";Password=" + dlinfo.PassWord;
            //            info.LoadFromSQLServer(connSql);
            //            break;
            //        case 2:
            //            System.Data.Common.DbConnection connOracle = new Devart.Data.Oracle.OracleConnection();

            //            connOracle.ConnectionString = "Data Source=" + dlinfo.DbName + ";User ID=" + dlinfo.UserName + ";Password=" + dlinfo.PassWord + "";
            //            info.GetOracleDb(connOracle);
            //            break;
            //        case 3:
            //            string ConnString = dlinfo.DataSource;
            //            info.LoadFromAccess2000(ConnString);
            //            break;

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("操作失败。" + ex.Message);
            //}

            return dbinfo;
        }

        private void tv_Db_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                tv_Db.SelectedNode = e.Node;
                DbDataInfo info = null;
                if (tv_Db.SelectedNode != null)
                {
                    string str1 = tv_Db.SelectedNode.Text;
                    info = tv_Db.SelectedNode.Tag as DbDataInfo;
                }

                //if ((sender as TreeView) != null) 
                //{ tv_Db.SelectedNode = tv_Db.GetNodeAt(e.X, e.Y); }

                if (e.Button == MouseButtons.Right && info != null)
                {
                    Point p = PointToScreen(new Point(e.X + 10, e.Y + 35));
                    cms.Items.Clear();
                    DbDataTypeEnum dtype = DbDataType.GetDbDataType(info.NameType);
                    switch (dtype)
                    {
                        case DbDataTypeEnum.服务器:
                            break;
                        case DbDataTypeEnum.数据库:
                            GetDbMenu(cms, info);
                            break;
                        case DbDataTypeEnum.表:
                            GetTableMenu(cms, info);
                            break;
                        case DbDataTypeEnum.视图:
                            GetViewMenu(cms, info);
                            break;
                        case DbDataTypeEnum.存储过程:
                            break;
                    }
                    cms.Show(p);
                }

                if (e.Button == MouseButtons.Left && info != null)
                {
                    Model.EventInfo einfo = new Model.EventInfo();
                    einfo.Title = "表单代码生成器";
                    Common.MidModule.SendData(this, info, einfo);//发送参数值
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        #region 添加菜单
        protected void GetDbMenu(ContextMenuStrip cms, DbDataInfo info)
        {
            ToolStripMenuItem subItem;
            subItem = AddContextMenu("连接",info, cms.Items, new EventHandler(MenuClicked));
            subItem = AddContextMenu("断开", info, cms.Items, new EventHandler(MenuClicked));
            subItem = AddContextMenu("重连接", info, cms.Items, new EventHandler(MenuClicked));
            subItem = AddContextMenu("注销", info, cms.Items, new EventHandler(MenuClicked));
        }

        protected void GetTableMenu(ContextMenuStrip cms, DbDataInfo info)
        {
            //添加菜单一
            ToolStripMenuItem subItem;
            //subItem = AddContextMenu("生成SQL语句", info, cms.Items, new EventHandler(MenuClicked));
            //subItem = AddContextMenu("生成存储过程", info, cms.Items, new EventHandler(MenuClicked));
            subItem = AddContextMenu("浏览表结构", info, cms.Items, new EventHandler(MenuClicked));
            subItem = AddContextMenu("导入数据", info, cms.Items, new EventHandler(MenuClicked));
            subItem = AddContextMenu("修改字段数据", info, cms.Items, new EventHandler(MenuClicked));
            //subItem = AddContextMenu("导出文件", info, cms.Items, null);
            //AddContextMenu("数据文件TXT", info, subItem.DropDownItems, new EventHandler(MenuClicked));
            subItem = AddContextMenu("-", info, cms.Items, new EventHandler(MenuClicked));
            subItem = AddContextMenu("表单代码生成器", info, cms.Items, new EventHandler(MenuClicked));
        }

        protected void GetViewMenu(ContextMenuStrip cms, DbDataInfo info)
        {
            //添加菜单一
            ToolStripMenuItem subItem;
            //subItem = AddContextMenu("生成SQL查询语句", info, cms.Items, new EventHandler(MenuClicked));
            subItem = AddContextMenu("浏览视图结构", info, cms.Items, new EventHandler(MenuClicked));
            //subItem = AddContextMenu("导出文件", info, cms.Items, null);
            //AddContextMenu("数据文件TXT", info, subItem.DropDownItems, new EventHandler(MenuClicked));
            subItem = AddContextMenu("-", info, cms.Items, new EventHandler(MenuClicked));
            subItem = AddContextMenu("表单代码生成器", info, cms.Items, new EventHandler(MenuClicked));
        }

        /// <summary>
        /// 添加子菜单
        /// </summary>
        /// <param name="text">要显示的文字，如果为 - 则显示为分割线</param>
        /// <param name="cms">要添加到的子菜单集合</param>
        /// <param name="callback">点击时触发的事件</param>
        /// <returns>生成的子菜单，如果为分隔条则返回null</returns>
        ToolStripMenuItem AddContextMenu(string text,DbDataInfo info, ToolStripItemCollection cms, EventHandler callback)
        {
            if (text == "-")
            {
                ToolStripSeparator tsp = new ToolStripSeparator();
                cms.Add(tsp);

                return null;
            }
            else if (!string.IsNullOrEmpty(text))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(text);
                tsmi.Tag = info;
                if (callback != null) tsmi.Click += callback;
                cms.Add(tsmi);

                return tsmi;
            }

            return null;
        }
        #endregion

        #region 菜单事件
        private void MenuClicked(object sender, EventArgs e)
        {
            dal = new DbLink();
            string MenuName = (sender as ToolStripMenuItem).Text;
            ToolStripMenuItem tmi = sender as ToolStripMenuItem;
            DbDataInfo dinfo = tmi.Tag as DbDataInfo;
            Model.EventInfo einfo = new Model.EventInfo();
            switch (MenuName)
            {
                case "连接":                    
                    GetdbData(dinfo);
                    break;
                case "断开":
                    tv_Db.SelectedNode.Nodes.Clear();
                    break;
                case "重连接":
                    GetdbData(dinfo);
                    break;
                case "注销":
                    if (dal.DbLink_Del(dinfo.DbLinkID) == 1)
                    {
                        tv_Db.Nodes.Remove(tv_Db.SelectedNode);
                    }
                    break;
                case "浏览表结构":
                    if (FindDocument("表结构查询") == null)
                    {
                        FormTableQuery sm = new FormTableQuery();
                        sm.Show(DockPanel);
                    }
                    else
                    {
                        Form f = FindDocument("表结构查询") as Form;
                        f.Focus();
                    }

                    einfo.Title = "表结构查询";
                    Common.MidModule.SendData(this, dinfo, einfo);//发送参数值
                    break;
                case "浏览视图结构":
                    if (FindDocument("表结构查询") == null)
                    {
                        FormTableQuery sm = new FormTableQuery();
                        sm.Show(DockPanel);
                    }
                    else
                    {
                        Form f = FindDocument("表结构查询") as Form;
                        f.Focus();
                    }

                    einfo.Title = "表结构查询";
                    Common.MidModule.SendData(this, dinfo, einfo);//发送参数值
                    break;
                case "导入数据":
                    if (FindDocument("数据导入") == null)
                    {
                        FormImportData sm = new FormImportData();
                        sm.Show(DockPanel);
                    }
                    else
                    {
                        Form f = FindDocument("数据导入") as Form;
                        f.Focus();
                    }

                    einfo.Title = "数据导入";
                    Common.MidModule.SendData(this, dinfo, einfo);//发送参数值
                    break;
                case "修改字段数据":
                    if (FindDocument("修改字段数据") == null)
                    {
                        FormFieldData sm = new FormFieldData();
                        sm.Show(DockPanel);
                    }
                    else
                    {
                        Form f = FindDocument("修改字段数据") as Form;
                        f.Focus();
                    }

                    einfo.Title = "修改字段数据";
                    Common.MidModule.SendData(this, dinfo, einfo);//发送参数值
                    break;
                case "表单代码生成器":
                    if (FindDocument("表单代码生成器") == null)
                    {
                        FormCreater sm = new FormCreater();
                        sm.Show(DockPanel);
                    }
                    else
                    {
                        Form f = FindDocument("表单代码生成器") as Form;
                        f.Focus();
                    }
                    einfo.Title = "表单代码生成器";
                    Common.MidModule.SendData(this, dinfo, einfo);//发送参数值
                    break;
            }
        }
        #endregion

        #region 菜单添加表，视图过程
        /// <summary>
        /// 菜单添加表，视图过程
        /// </summary>
        /// <param name="dinfo"></param>
        private void GetdbData(DbDataInfo dinfo)
        {
            if (tv_Db.SelectedNode.Text.IndexOf( dinfo.Name)<0)
            {
                return;
            }
            DataBaseInfo dbinfo = GetDbInfo(dinfo.DbLinkID);
            tv_Db.SelectedNode.Nodes.Clear();

            TreeNode tnTable = new TreeNode("表");
            foreach (TableInfo tinfo in dbinfo.Tables)
            {
                DbDataInfo dainfo = new DbDataInfo();
                dainfo.DbLinkID = dinfo.DbLinkID;
                dainfo.DbName = dinfo.DbName;
                dainfo.Table = dinfo.Table;
                dainfo.View = dinfo.View;
                dainfo.Procedure = dinfo.Procedure;

                TreeNode tnode = new TreeNode();
                dainfo.Name = tinfo.Name;
                dainfo.NameType = (int)DbDataTypeEnum.表;
                tnode.Text = dainfo.Name;
                tnode.Tag = dainfo;
                
                foreach (FieldInfo finfo in tinfo.Fields)
                {
                    TreeNode fnode = new TreeNode();
                    fnode.Text = finfo.Name + "(" + finfo.FieldType + ")";
                    tnode.Nodes.Add(fnode);
                }
                tnTable.Nodes.Add(tnode);                
            }
            tv_Db.SelectedNode.Nodes.Add(tnTable);

            TreeNode tnView = new TreeNode("视图");
            foreach (TableInfo tinfo in dbinfo.View)
            {
                DbDataInfo dainfo = new DbDataInfo();
                dainfo.DbLinkID = dinfo.DbLinkID;
                dainfo.DbName = dinfo.DbName;
                dainfo.Table = dinfo.Table;
                dainfo.View = dinfo.View;
                dainfo.Procedure = dinfo.Procedure;

                TreeNode tnode = new TreeNode();
                dainfo.Name = tinfo.Name;
                dainfo.NameType = (int)DbDataTypeEnum.视图;
                tnode.Text = dainfo.Name;
                tnode.Tag = dainfo;
                foreach (FieldInfo finfo in tinfo.Fields)
                {
                    TreeNode fnode = new TreeNode();
                    fnode.Text = finfo.Name + "(" + finfo.FieldType + ")";
                    tnode.Nodes.Add(fnode);
                }
                tnView.Nodes.Add(tnode);
            }
            tv_Db.SelectedNode.Nodes.Add(tnView);

            TreeNode tnProcedure = new TreeNode("存储过程");
            foreach (TableInfo tinfo in dbinfo.Procedure)
            {
                DbDataInfo dainfo = new DbDataInfo();
                dainfo.DbLinkID = dinfo.DbLinkID;
                dainfo.DbName = dinfo.DbName;
                dainfo.Table = dinfo.Table;
                dainfo.View = dinfo.View;
                dainfo.Procedure = dinfo.Procedure;

                TreeNode tnode = new TreeNode();
                dainfo.Name = tinfo.Name;
                dainfo.NameType = (int)DbDataTypeEnum.存储过程;
                tnode.Text = dainfo.Name;
                tnode.Tag = dainfo;
                foreach (FieldInfo finfo in tinfo.Fields)
                {
                    TreeNode fnode = new TreeNode();
                    fnode.Text = finfo.Name+"("+finfo.FieldType+")";
                    tnode.Nodes.Add(fnode);
                }
                tnProcedure.Nodes.Add(tnode);
            }
            tv_Db.SelectedNode.Nodes.Add(tnProcedure);


        }
        #endregion

        private void LinkToolScriptBtn_Click(object sender, EventArgs e)
        {
            try
            {
                dal = new DbLink();

                ToolStripButton tbtn = sender as ToolStripButton;
                string title = tbtn.Text;

                if (title == "刷新")
                {
                    QueryData();
                    return;
                }

                DbDataInfo dinfo = tv_Db.SelectedNode.Tag as DbDataInfo;
                if (dinfo == null)
                {
                    MessageBox.Show("请选择服务器。");
                    return;
                }
                DbDataTypeEnum ddt = DbDataType.GetDbDataType(dinfo.NameType);
                if (ddt != DbDataTypeEnum.数据库)
                {
                    MessageBox.Show("请选择服务器。");
                    return;
                }
                switch (title)
                {
                    case "连接":
                        GetdbData(dinfo);
                        break;
                    case "断开":
                        tv_Db.SelectedNode.Nodes.Clear();
                        break;
                    case "重连接":
                        GetdbData(dinfo);
                        break;
                    case "注销":
                        if (dal.DbLink_Del(dinfo.DbLinkID) == 1)
                        {
                            tv_Db.Nodes.Remove(tv_Db.SelectedNode);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取数据库失败，" + ex.Message);
            }
        }

        private void tv_Db_KeyUp(object sender, KeyEventArgs e)
        {
            var keyData = e.KeyData;
            //tv_Db.SelectedNode.FullPath.ToString().Replace("\\", ".");
            try
            {
                if (keyData == Keys.C)
                {
                    if (tv_Db.SelectedNode != null)
                    {
                        string strval = "";
                        string str = tv_Db.SelectedNode.Text;
                        int index = str.IndexOf("(");
                        if (index > 1)
                        {
                            strval = str.Substring(0, index);
                        }
                        else
                            strval = str;
                        if (strval != "")
                            Clipboard.SetText(strval);
                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }







 



    }
}
