using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SQLite;

using Model.CodeMaker;
using DALFactory.CodeMaker;
using AccessDal.CodeMaker;
using DALProfile;

namespace CodeFacility.CodeMaker
{
    public partial class FormDbLink : FormWin
    {
        IDbLink dal = new DbLink();


        public FormDbLink()
        {
            InitializeComponent();
        }

        private void FormDbLink_Load(object sender, EventArgs e)
        {
            Initcob_DbType();
            Initdgv_DbLink();
            InitOption();
        }

        private void InitOption()
        {
           dgv_DbLink.RowsDefaultCellStyle.BackColor = BaseConfigure.ColorTheme;
           panel1.BackColor = BaseConfigure.ColorTheme;
        }

        private void Initdgv_DbLink()
        {
            var list= dal.DbLinkGetList();
            dgv_DbLink.DataSource = list;
        }

        private void Initcob_DbType()
        {
            var dic = DataBaseType.GetDicDataBaseType();
            BindingSource bs = new BindingSource();
            bs.DataSource = dic;
            cob_DbType.DataSource = bs;
            cob_DbType.ValueMember = "Key";
            cob_DbType.DisplayMember = "Value";
        }

        private void tb_Clear_Click(object sender, EventArgs e)
        {
            tb_DbName.Text = "";
            tb_UserName.Text = "";
            tb_Password.Text = "";
            tb_DataSource.Text = "";
            cob_DbType.Text = "";
            tb_DbAbbreviation.Text = "";
            tb_Port.Text = "";
            cob_DbType.SelectedIndex = 0;
            tb_ID.Text = "";
            txt_Charset.Text = "";
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            DbLinkInfo info = GetDbLinkInfo();

            string DbTime = "";
            string rstmsg = "";
            if (GetDbLink(out DbTime, out rstmsg) == -1)
            {
                MessageBox.Show("保存失败。" + rstmsg);
                return;
            }

            int rst = 0;
            if (info.ID == 0)
                rst = dal.DbLink_Add(info);
            else if (info.ID > 0)
                rst = dal.DbLink_Edit(info);

            if (rst > 0)
            {
                Initdgv_DbLink();
                MessageBox.Show("保存成功。");
            }
            else
            {
                MessageBox.Show("保存失败。");
            }
        }

        private DbLinkInfo GetDbLinkInfo()
        {
            DbLinkInfo info = new DbLinkInfo();

            info.DbName = tb_DbName.Text;
            info.UserName = tb_UserName.Text;
            info.PassWord = tb_Password.Text;
            info.DataSource = tb_DataSource.Text;
            info.Port = tb_Port.Text;
            info.DbAbbreviation = tb_DbAbbreviation.Text;
            info.ID = tb_ID.Text == "" || tb_ID.Text == "0" ? 0 : int.Parse(tb_ID.Text);
            info.DbType = int.Parse(cob_DbType.SelectedValue.ToString());
            info.Charset = txt_Charset.Text;
            //switch (cob_DbType.Text)
            //{
            //    case "SQLServer":
            //        info.DbType = (int)DataBaseTypeEnum.SQLServer;
            //        break;
            //    case "Oracle":
            //        info.DbType = (int)DataBaseTypeEnum.Oracle;
            //        break;
            //    case "Access":
            //        info.DbType = (int)DataBaseTypeEnum.Access;
            //        break;
            //    case "SQLite":
            //        info.DbType = (int)DataBaseTypeEnum.SqLite;
            //        break;
            //    case "MySql":
            //        info.DbType = (int)DataBaseTypeEnum.MySql;
            //        break;
            //    case "MongoDB":
            //        info.DbType = (int)DataBaseTypeEnum.MongoDB;
            //        break;
            //    case "Redis":
            //        info.DbType = (int)DataBaseTypeEnum.Redis;
            //        break;
            //}

            return info;
        }

        private void btn_Link_Click(object sender, EventArgs e)
        {
            try
            {
                int rst = 0;
                string DbTime = "";
                string rstmsg = "";
                if (tb_DbName.Text.Trim() == "")
                {
                    MessageBox.Show("请输入数据库名称。");
                    return;
                }
                rst = GetDbLink(out DbTime, out rstmsg);
                if (rst == 1)
                {
                    MessageBox.Show("连接成功。数据库当前时间：" + DbTime);
                }
                else
                {
                    MessageBox.Show("连接失败。" + rstmsg);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("连接失败。" + ex.Message);
            }

        }

        private int GetDbLink(out string DbTime,out string rstmsg)
        {
            int rst = 0;
            DbTime = "";
            rstmsg = "";
            string connstring = "";
            DataTable dt = new DataTable();
            string sql = "";
            //DbLinkInfo info = GetDbLinkInfo();
            //DbConnection conn = DBConfig.GetDbConnection(info);
            switch (cob_DbType.Text)
            {
                case "SQLServer":
                    string port = "";
                    if (!string.IsNullOrEmpty(tb_Port.Text))
                        port = "," + tb_Port.Text;
                    connstring = "Data Source=" + tb_DataSource.Text + port + ";Initial Catalog=" + tb_DbName.Text + ";User ID=" + tb_UserName.Text + ";Password=" + tb_Password.Text;
                    sql = "select Getdate() as DbTime";
                    try
                    {      
                        //DbCommand cmd=new System.Data.SqlClient.SqlCommand();
                        //DbDataAdapter da =new System.Data.SqlClient.SqlDataAdapter();
                        DbConnection conn=new System.Data.SqlClient.SqlConnection();
                        conn.ConnectionString = connstring;                        
                        dt = DbHelper.ExecuteTableCurrency(conn, CommandType.Text, sql, null);
                        DbTime = dt.Rows[0]["DbTime"].ToString();
                        if (DbTime.Length > 6)
                        {
                            rst = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        rst = -1;
                        rstmsg = ex.Message;
                    }
                    break;
                case "Oracle":
                    string dataSource = "(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST =" + tb_DataSource.Text.Trim() + ")(PORT =" + tb_Port.Text.Trim() + "))) (CONNECT_DATA = (SERVICE_NAME =" + tb_DbName.Text + ")))";
                    connstring = "Data Source=" + dataSource + ";User ID=" + tb_UserName.Text + ";Password=" + tb_Password.Text + "";
                    sql = "select sysdate DbTime from dual";
                    try
                    {
                        //DbCommand cmd = new Devart.Data.Oracle.OracleCommand();
                        //DbDataAdapter da = new Devart.Data.Oracle.OracleDataAdapter();
                        DbConnection conn = new Devart.Data.Oracle.OracleConnection();
                        conn.ConnectionString = connstring;
                        dt = DbHelper.ExecuteTableCurrency(conn, CommandType.Text, sql, null);
                        DbTime = dt.Rows[0]["DbTime"].ToString();
                        if (DbTime.Length > 6)
                        {
                            rst = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        rst = -1;
                        rstmsg = ex.Message;
                    }
                    break;
                case "Access":
                    connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + tb_DataSource.Text + ";Persist Security Info=True;";
                    sql = "select now() as DbTime";
                    try
                    {
                        //DbCommand cmd = new System.Data.OleDb.OleDbCommand();
                        //DbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
                        DbConnection conn = new System.Data.OleDb.OleDbConnection();
                        conn.ConnectionString = connstring;
                        dt = DbHelper.ExecuteTableCurrency(conn, CommandType.Text, sql, null);
                        DbTime = dt.Rows[0]["DbTime"].ToString();
                        if (DbTime.Length > 6)
                        {
                            rst = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        rst = -1;
                        rstmsg = ex.Message;
                    }                   
                    break;
                case "SQLite":
                    connstring = @"Data Source=" + tb_DataSource.Text + ";Version=3;";
                    connstring=connstring+"FailIfMissing=false;";
                    if (tb_Password.Text.Trim() != "")
                    {
                        connstring = connstring + "Password=" + tb_Password.Text;
                    }
                    sql = "select DATETIME('now') as DbTime";
                    try
                    {
                        DbConnection conn = new System.Data.SQLite.SQLiteConnection();
                        conn.ConnectionString = connstring;
                        dt = DbHelper.ExecuteTableCurrency(conn, CommandType.Text, sql, null);
                        DbTime = dt.Rows[0]["DbTime"].ToString();
                        if (DbTime.Length > 6)
                        {
                            rst = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        rst = -1;
                        rstmsg = ex.Message;
                    }
                    break;
                case "MySql":
                    //connstring = "data source=" + tb_DataSource.Text + ";database=" + tb_DbName.Text + ";uid=" + tb_UserName.Text + ";pwd=" + tb_Password.Text + ";";
                    connstring = "server=" + tb_DataSource.Text + ";database=" + tb_DbName.Text + ";port=" + tb_Port.Text + ";user id=" + tb_UserName.Text + ";password=" + tb_Password.Text + ";Charset=" + txt_Charset.Text; //";CharSet=gbk;Allow Zero Datetime=true";
                    //connstring = "Data Source=" + tb_DataSource.Text + ";port=" + tb_Port.Text + ";Initial Catalog=" + tb_DbName.Text + ";user id=" + tb_UserName.Text + ";password=" + tb_Password.Text + ";Charset=" + txt_Charset.Text;
                    sql = "select now() DbTime;";
                    try
                    {
                        DbConnection conn = new MySql.Data.MySqlClient.MySqlConnection();
                        conn.ConnectionString = connstring;
                        dt = DbHelper.ExecuteTableCurrency(conn, CommandType.Text, sql, null);
                        DbTime = dt.Rows[0]["DbTime"].ToString();
                        if (DbTime.Length > 6)
                        {
                            rst = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        rst = -1;
                        rstmsg = ex.Message;
                    }
                    break;
            }

            return rst;
        }

        private void dgv_DbLink_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            int columnindex = e.ColumnIndex;
            string id = dgv_DbLink.Rows[rowindex].Cells["ID"].Value.ToString();
            if (columnindex == 0) //编辑
            {
                var info = dal.DbLinkGetInfo(int.Parse(id));
                tb_DbName.Text = info.DbName;
                tb_UserName.Text = info.UserName;
                tb_Password.Text = info.PassWord;
                tb_DataSource.Text = info.DataSource;
                var DbTypeName = Enum.GetName(typeof(DataBaseTypeEnum), info.DbType);
                cob_DbType.SelectedText = DbTypeName;
                cob_DbType.Text = DbTypeName;
                tb_DbAbbreviation.Text = info.DbAbbreviation;
                tb_Port.Text = info.Port;
                tb_ID.Text = info.ID.ToString();
            }
            else if (columnindex == 1)  //删除
            {

                MessageBoxButtons mess = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("是否删除", "提示", mess);
                if (dr != DialogResult.OK)
                {
                    return;
                }

                var rst = dal.DbLink_Del(int.Parse(id));
                if(rst>0)
                {
                    Initdgv_DbLink();
                    tb_Clear_Click(null, null);
                    MessageBox.Show("删除成功。");
                    return;
                }
                else
                {
                    MessageBox.Show("删除失败。");
                    return;
                }

            }
        }


             
    }
}
