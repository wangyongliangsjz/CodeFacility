using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DALFactory.CodeMaker;
using AccessDal.CodeMaker;
using Model.CodeMaker;
using Model;

namespace CodeFacility.CodeMaker
{
    public partial class FormImportData : FormWin
    {
        DbDataInfo dinfo;
        DataSet ds;
        bool key = false;
        string extend = "";

        public FormImportData()
        {
            InitializeComponent();
            //订阅了信息发送事件，即接受参数值
            Common.MidModule.EventSend += new Common.DataDlg(MidModule_EventSend);
        }

        //接受参数事件
        private void MidModule_EventSend(object sender, object data, object e)
        {
            if (sender != null)
            {
                Model.EventInfo einfo = e as Model.EventInfo;
                if (einfo.Title != "数据导入")
                    return;
                Form fr = sender as Form;
                if (fr.Text == "对象资源管理器")
                {
                    dinfo = data as DbDataInfo;
                    DbDataTypeEnum ddt = DbDataType.GetDbDataType(dinfo.NameType);
                    if (ddt != DbDataTypeEnum.表)
                    {
                        MessageBox.Show("请选择表。");
                        return;
                    }
                    string msg = "数据库：" + dinfo.DbName;
                    if (DbDataType.GetDbDataType(dinfo.NameType).ToString() != "数据库")
                    {
                        msg = msg + " " + DbDataType.GetDbDataType(dinfo.NameType).ToString() + "：" + dinfo.Name;
                    }
                    lb_DbMessage.Text = msg;
                    QueryData();
                }
            }

        }

        private void FormImportData_Load(object sender, EventArgs e)
        {
            Init();
            InitOption();
        }

        private void InitOption()
        {
            tv_table.BackColor = BaseConfigure.ColorTheme;
            panel1.BackColor = BaseConfigure.ColorTheme;
        }

        private void Init()
        {
            IDbLink idal = new DbLink();
            IList<DbLinkInfo> ilist = idal.DbLinkGetList();
            foreach (DbLinkInfo info in ilist)
            {
                string dbName = string.IsNullOrEmpty(info.DbAbbreviation) ? info.DbName : info.DbName + "(" + info.DbAbbreviation + ")";
                comboBoxDB.Items.Add(new ListItem(info.ID.ToString(), dbName));
            }
            comboBoxDB.SelectedIndex = 0;

        }


        private void QueryData()
        {
            //获取菜单数据
            DataBaseInfo dbinfo = GetDbInfo(dinfo.DbLinkID);
            string tablename=dinfo.Name;
            TableInfo tinfo = dbinfo.Tables[tablename];

            if (tv_table.Nodes.Count > 0)
                tv_table.Nodes.Clear();
            TreeNode tn = new TreeNode();
            tn.Text = "表("+dinfo.Name+")";
            TreeNode fnode = null;
            foreach (FieldInfo info in tinfo.Fields)
            {
                fnode = new TreeNode();
                fnode.Text = info.Name;
                tn.Nodes.Add(fnode);
            }
            tn.ExpandAll();
            tv_table.Nodes.Add(tn);
        }

        private DataBaseInfo GetDbInfo(int DbLinkId)
        {
            IDbLink dal = new DbLink();
            DbLinkInfo dlinfo = dal.DbLinkGetInfo(DbLinkId);

            IDataBase dbDal = new CurrencyDal.CodeMaker.DataBase();
            string rstmsg = "";
            string tableName = "";
            List<string> tableNameList = new List<string>();
            DbDataTypeEnum dtype = DbDataType.GetDbDataType(dinfo.NameType);
            if (dtype == DbDataTypeEnum.表)
            {
                tableName = dinfo.Name;

                //oracle数据库表名大写
                if (dlinfo.DbType == 2)
                    tableName = tableName.ToUpper();

                tableNameList.Add(tableName);
            }
            DataBaseInfo dbinfo = dbDal.DataBaseGetInfo(dlinfo, tableNameList, out rstmsg);
            return dbinfo;
        }

        private void btb_scan_Click(object sender, EventArgs e)
        {
            //获取导入文件数据
            dgv1.DataSource = null;
            dgv2.DataSource = null;
            dgv3.DataSource = null;
            dgv4.DataSource = null;
            dgv5.DataSource = null;
            dgv6.DataSource = null;
            string rstmsg = "";

            #region 打开文件
            OpenFileDialog openFileDialog1 = new OpenFileDialog();     //显示选择文件对话框
            openFileDialog1.InitialDirectory = "c:\\";
            //openFileDialog1.Filter = "Excel 工作簿(*.xlsx)|*.xlsx|Miscrosoft Office Excel 97-2003 工作表|*.xls|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.Filter = "Miscrosoft Office Excel 97-2003 工作表|*.xls|excel07文件(*.xlsx)|*.xlsx|txt 文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            extend = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_file.Text = openFileDialog1.FileName;          //显示文件路径
                extend = System.IO.Path.GetExtension(openFileDialog1.FileName);
                //System.IO.FileInfo f = new System.IO.FileInfo(openFileDialog1.FileName);
                //extend = f.Extension.ToLower();
                extend = extend.Replace(".", "");
            }
            #endregion
            int rst= GetDataSet(out rstmsg);
            if (rst != 1)
            {
                MessageBox.Show(rstmsg);
                return;
            }
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                string dgvname = "dgv" + (i + 1).ToString();
                DataGridView dgv = (DataGridView)this.Controls.Find(dgvname, true)[0];
                dgv.DataSource = ds.Tables[i];
            }
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            //导入Excel表格时将列展开，如果列显示不全可能缺少某列
            int rst = 0;
            string rstmsg = "";
            //获取文件数据
            //rst = GetDataSet(out rstmsg);
            //if (rst != 1)
            //{
            //    MessageBox.Show(rstmsg);
            //    return;
            //}
            btnRefresh_Click(null, null);
            //导入文件数据
            if (ds == null)
            {
                MessageBox.Show("导入数据为空。");
                return;
            }
            if (ds ==null || ds.Tables==null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("导入数据为空。");
                return;
            }

            CurrencyDal.CodeMaker.DataBase dal = new CurrencyDal.CodeMaker.DataBase();
            IDbLink dldal = new DbLink();


            DbLinkInfo dlinfo = new DbLinkInfo();
            string tableName="";
            if(ckb_IsAddTable.Checked)
            {
                var lisitem = comboBoxDB.SelectedItem as ListItem;
                int DbLinkID = string.IsNullOrEmpty(lisitem.ID) ? 0 : int.Parse(lisitem.ID);
                dlinfo = dldal.DbLinkGetInfo(DbLinkID);
                tableName = tb_AddTableName.Text.Trim();
                dinfo = new DbDataInfo();
                dinfo.DbLinkID = DbLinkID;
                dinfo.DbName = dlinfo.DbName;
                dinfo.Name = tableName;
                dinfo.NameType = (int)DbDataTypeEnum.表;
            }
            else
            {
                dlinfo = dldal.DbLinkGetInfo(dinfo.DbLinkID);
                tableName=dinfo.Name;
            }

            List<string> fieldlist = new List<string>();
            string field=tb_field.Text.Trim();
            field = field.Replace(" ", "");

            DataBaseInfo dbinfo = new DataBaseInfo();
            int index = tabControl1.SelectedIndex;
            DataTable dt = ds.Tables[index];
            #region 新建表
            if(ckb_IsAddTable.Checked && dt !=null && dt.Rows.Count>0)
            {
                string sqlmsg = "";
                CurrencyDal.CodeMaker.RunSql rdal = new CurrencyDal.CodeMaker.RunSql();
                field = "";
                StringBuilder sql = new StringBuilder();
                tableName = tb_AddTableName.Text.Trim();

                string sqljc = "select 1 As Num from " + tableName;
                DataSet dsjs = rdal.Run(dlinfo, sql.ToString(), out sqlmsg, out rstmsg);
                try
                {
                    if (dsjs != null && dsjs.Tables[0] != null)
                    {
                        MessageBox.Show("表" + tableName + "已存在。");
                        return;
                    }
                }
                catch { }


                sql.Append("Create Table ");
                sql.Append(tableName);
                sql.Append(" ( ");
                string createField = "";
                string fieldType = "";
                if (dlinfo.DbType == 1)
                    fieldType = "nvarchar";
                else if (dlinfo.DbType == 2)
                    fieldType = "nvarchar2";
                else if (dlinfo.DbType == 3)
                    fieldType = "nvarchar";
                else
                    fieldType = "nvarchar";

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string cname = dt.Columns[i].ColumnName;
                    //sql.Append("[" + cname + "] [nvarchar](255) NULL,");

                    if (createField == "")
                        createField = cname + " " + fieldType + "(255) NULL";
                    else
                        createField += "," + cname + " " + fieldType + "(255) NULL";
                    
                    if (field == "")
                        field = cname;
                    else
                        field += "," +cname;
                }
                sql.Append(createField);
                sql.Append(")");
                
                DataSet dset = rdal.Run(dlinfo, sql.ToString(), out sqlmsg, out rstmsg);

            }
            #endregion

            dbinfo = GetDbInfo(dinfo.DbLinkID);
            TableInfo tinfo = dbinfo.Tables[tableName];
            rst = dal.ImportData(dlinfo, tinfo, field, dt, out rstmsg);
            MessageBox.Show(rstmsg);
            
        }

        /// <summary>
        /// 获取文件数据
        /// </summary>
        /// <param name="rstmsg"></param>
        /// <returns></returns>
        private int GetDataSet(out string rstmsg)
        {
            int rst = 0;
            rstmsg = "";
            try
            {
                string filepath = tb_file.Text.Trim();
                if (filepath == "")
                {
                    rstmsg = "请选择文件。";
                    return -1;
                }
                ds = null;
                if (filepath != "")
                {
                    if (extend.ToLower() == "xls" || extend.ToLower() == "xlsx")
                    {
                        //ds = Common.Excel.ExcelToDataSet(filepath);
                        ds = Common.Excel.ExcelToDS(filepath);
                    }
                    else if (extend.ToLower() == "txt")
                    {
                        ds = Common.FileHandle.TxtToDataSet(filepath);
                    }
                }
                if (ds == null)
                {
                    rstmsg = "请选择文件失败。";
                    return -1;
                }
                if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    rstmsg = "请选择文件失败。";
                    return -1;
                }
                rst = 1;
                return rst;
            }
            catch(Exception ex)
            {
                rst = -1;
                rstmsg = ex.Message;
                return rst;
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            tb_field.Text = "";
            key = true;
            tv_table.Nodes[0].Checked = false;
            TreeNodeCollection tnc = tv_table.Nodes[0].Nodes;
            foreach (TreeNode tn in tnc)
            {
                tn.Checked = false;
            }
            key = false;
        }

        private void tv_table_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            //设置导入字段
            if (e.Node.Text.Trim() == "")
                return;
            if (key)
            {
                return;
            }
            string field = e.Node.Text;
            if (field.IndexOf("表") > -1)
            {
                key = true;
                if (e.Node.Checked)
                {
                    tb_field.Text = "";
                    TreeNodeCollection tnc = tv_table.Nodes[0].Nodes;
                    foreach (TreeNode tn in tnc)
                    {
                        tn.Checked = false;
                    }
                }
                else
                {
                    tb_field.Text = "";
                    TreeNodeCollection tnc = tv_table.Nodes[0].Nodes;
                    foreach (TreeNode tn in tnc)
                    {
                        if (tn.Text != null)
                        {
                            tn.Checked = true;
                            //增加字段
                            if (tb_field.Text.Trim() == "")
                            {
                                tb_field.Text = tn.Text;
                            }
                            else
                            {
                                tb_field.Text = tb_field.Text + "," + tn.Text;
                            }
                        }
                    }
                }
                key = false;
                return;
            }

            if (key)
            {
                return;
            }

            if (e.Node.Checked)
            {
                //删除字段
                if (tb_field.Text.IndexOf(",") > -1)
                {
                    //if (tb_field.Text.EndsWith(field))
                    //{
                    //    tb_field.Text = tb_field.Text.Replace(field, "");
                    //}
                    string[] fieldItem = tb_field.Text.Trim().Split(',');
                    string strf = "";
                    for (int i = 0; i < fieldItem.Length; i++)
                    {
                        string fi = fieldItem[i];
                        if (fieldItem[i].ToString() == field)
                        {
                            fieldItem[i] = "";
                        }
                        if (fieldItem[i] != "")
                        {
                            if (strf == "")
                            {
                                strf = fieldItem[i];
                            }
                            else
                            {
                                strf = strf +","+ fieldItem[i];
                            }
                        }
                    }
                    tb_field.Text = strf;
                }
                else
                {
                    tb_field.Text = tb_field.Text.Replace(field, "");
                }
            }
            else
            {
                //增加字段
                if (tb_field.Text.Trim() == "")
                {
                    if (tb_field.Text.IndexOf(field) > -1)
                    {
                        MessageBox.Show("已选择此字段。");
                        return;
                    }
                    tb_field.Text = field;
                }
                else
                {
                    if (tb_field.Text.IndexOf(field) > -1)
                    {
                        MessageBox.Show("已选择此字段。");
                        return;
                    }
                    tb_field.Text = tb_field.Text + "," + field;
                }
            }

            if (tb_field.Text.IndexOf(" ") > -1)
            {
                tb_field.Text = tb_field.Text.Replace(" ", "");
            }
            if (tb_field.Text.IndexOf(",,") > -1)
            {
                tb_field.Text = tb_field.Text.Replace(",,", ",");
            }
            if (tb_field.Text.StartsWith(","))
            {
                tb_field.Text = tb_field.Text.Substring(1, tb_field.Text.Length - 1);
            }
            if (tb_field.Text.EndsWith(","))
            {
                tb_field.Text = tb_field.Text.Substring(0, tb_field.Text.Length - 1);
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            int rst = 0;
            string rstmsg = "";
            //获取文件数据
            rst = GetDataSet(out rstmsg);
            if (rst != 1)
            {
                MessageBox.Show(rstmsg);
                return;
            }

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                string dgvname = "dgv" + (i + 1).ToString();
                DataGridView dgv = (DataGridView)this.Controls.Find(dgvname, true)[0];
                dgv.DataSource = ds.Tables[i];
            }
        }

        private void FormImportData_FormClosed(object sender, FormClosedEventArgs e)
        {
            tv_table = new TreeView();
        }
    }
}
