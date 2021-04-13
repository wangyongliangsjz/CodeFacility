using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.Common;
using DALProfile;


namespace CodeFacility.CodeMaker
{
    public partial class FormCopyData : FormWin
    {
        public FormCopyData()
        {
            InitializeComponent();
        }

        //测试连接源数据库
        private void btn_Link_Click(object sender, EventArgs e)
        {
            int rst = 0;
            string DbTime = "";
            string rstmsg = "";
            if (tb_DbName.Text.Trim() == "")
            {
                MessageBox.Show("请输入数据库名称。");
                return;
            }
            string connstring = "Data Source=" + tb_DataSource.Text + ";Initial Catalog=" + tb_DbName.Text + ";User ID=" + tb_UserName.Text + ";Password=" + tb_Password.Text;
            rst = GetDbLink(connstring,out DbTime, out rstmsg);
            if (rst == 1)
            {
                MessageBox.Show("连接成功。数据库当前时间：" + DbTime);
            }
            else
            {
                MessageBox.Show("连接失败。" + rstmsg);
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            tb_DbName.Text = "";
            tb_DataSource.Text = "";
            tb_DbName.Text = "";
            tb_UserName.Text = "";
            tb_Password.Text = "";
        }

        //测试连接目标数据库
        private void btn_Link2_Click(object sender, EventArgs e)
        {
            int rst = 0;
            string DbTime = "";
            string rstmsg = "";
            if (tb_DbName2.Text.Trim() == "")
            {
                MessageBox.Show("请输入数据库名称。");
                return;
            }
            string connstring = "Data Source=" + tb_DataSource2.Text + ";Initial Catalog=" + tb_DbName2.Text + ";User ID=" + tb_UserName2.Text + ";Password=" + tb_Password2.Text;
            rst = GetDbLink(connstring,out DbTime, out rstmsg);
            if (rst == 1)
            {
                MessageBox.Show("连接成功。数据库当前时间：" + DbTime);
            }
            else
            {
                MessageBox.Show("连接失败。" + rstmsg);
            }
        }

        private void btn_clear2_Click(object sender, EventArgs e)
        {
            tb_DbName2.Text = "";
            tb_DataSource2.Text = "";
            tb_DbName2.Text = "";
            tb_UserName2.Text = "";
            tb_Password2.Text = "";
        }

        private int GetDbLink(string connstring, out string DbTime, out string rstmsg)
        {
            int rst = 0;
            DbTime = "";
            rstmsg = "";
            //string connstring = "";
            DataTable dt = new DataTable();
            string sql = "";
            switch ("SQLServer")
            {
                case "SQLServer":
                    
                    sql = "select Getdate() as DbTime";
                    try
                    {
                        DbConnection conn = new System.Data.SqlClient.SqlConnection();
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

        private void btb_forbid_Click(object sender, EventArgs e)
        {

        }

        private void btn_permission_Click(object sender, EventArgs e)
        {

        }





       
    }
}
