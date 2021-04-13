using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Model.CodeMaker;

namespace DALProfile
{
    public class DBConfig
    {
        /// <summary>
        /// 取得连接字符串
        /// </summary>
        /// <param name="TypeDal">连接字符串类型</param>
        /// <returns>返回字符串</returns>
        public static string GetConString(string TypeDal)
        {
            string constring = "";
            if(TypeDal=="SqlServer")
            {
                constring = DALProfile.DBSecurity.Decrypt(ConfigurationManager.AppSettings["SQLConnString_grasp_crm"]);
            }
            else if (TypeDal == "AccessCodeMaker")
            {
                string DataSource = "";
                if (ConfigurationManager.AppSettings["AccessCodeMaker"] != null)
                    DataSource = ConfigurationManager.AppSettings["AccessCodeMaker"];
                else
                    DataSource = AppDomain.CurrentDomain.BaseDirectory + "Data\\" + ConfigurationManager.AppSettings["AccessCodeMakerDBName"];

//连接失败。混合模式程序集是针对“v2.0.50727”版的运行时生成的，在没有配置其他信息的情况下，无法在 4.0 运行时中加载该程序集。 配置文件设置问题

//#if DEBUG
//                //测试代码
//                DataSource = ConfigurationManager.AppSettings["AccessCodeMaker"];

//#endif
//#if !DEBUG
//                //正式代码   
//                DataSource = AppDomain.CurrentDomain.BaseDirectory +"Data\\"+ ConfigurationManager.AppSettings["AccessCodeMakerDBName"];
//#endif
                constring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataSource + ";Persist Security Info=True;";
            }
            else if (TypeDal == "AccessFamilyFinance")
            {
                //string DataSource = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["AccessCodeMaker"];
                string DataSource = ConfigurationManager.AppSettings["AccessFamilyFinance"];
                constring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataSource + ";Persist Security Info=True;";
            }
            return constring;
        }

        public static string CmsConString
        {
            get
            {
                string constring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory + @"Data\IISLog.mdb" + ";Persist Security Info=True;";
                return constring;
            }
        }

        public static System.Data.Common.DbConnection GetDbConnection(Model.CodeMaker.DbLinkInfo info)
        {
            System.Data.Common.DbConnection conn=null;
            try
            {
                DataBaseTypeEnum fc = DataBaseType.GetDataBaseType(info.DbType);
                string characterSet = ConfigurationManager.AppSettings["CharacterSet"] == null ? "" : ConfigurationManager.AppSettings["CharacterSet"];
                switch (fc)
                {
                    case DataBaseTypeEnum.SQLServer:
                        conn = new System.Data.SqlClient.SqlConnection();
                        string port = "";
                        if (!string.IsNullOrEmpty(info.Port))
                            port = ","+info.Port;
                        conn.ConnectionString = "Data Source=" + info.DataSource + port + ";Initial Catalog=" + info.DbName + ";User ID=" + info.UserName + ";Password=" + info.PassWord;
                        break;
                    case DataBaseTypeEnum.Oracle:
                        conn = new Devart.Data.Oracle.OracleConnection();
                        string dataSource = "(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST =" + info.DataSource + ")(PORT =" + info.Port + "))) (CONNECT_DATA = (SERVICE_NAME =" + info.DbName + ")))";
                        conn.ConnectionString = "Data Source=" + dataSource + ";User ID=" + info.UserName + ";Password=" + info.PassWord + "";
                        break;
                    case DataBaseTypeEnum.Access:
                        conn = new System.Data.OleDb.OleDbConnection();
                        string AccessDataSource = info.DataSource;
                        if (AccessDataSource.IndexOf("CodeMaker.mdb") > 0)
                            AccessDataSource = DBConfig.GetConString("AccessCodeMaker");
                        conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + AccessDataSource;
                        break;
                    case DataBaseTypeEnum.SQLite:
                        conn = new System.Data.SQLite.SQLiteConnection();
                        string connstring = @"Data Source=" + info.DataSource + ";Version=3;";
                        if (info.PassWord.Trim() != "")
                        {
                            connstring = connstring + "Password=" + info.PassWord;
                        }
                        conn.ConnectionString = connstring;
                        break;
                    case DataBaseTypeEnum.MySql:
                        //配置文件字符集
                        string charset = "";
                        //if (!string.IsNullOrEmpty(characterSet))
                        //    charset = ";Charset=" + characterSet;

                        conn = new MySql.Data.MySqlClient.MySqlConnection();
                        conn.ConnectionString = "server=" + info.DataSource + ";database=" + info.DbName + ";port=" + info.Port + ";user id=" + info.UserName + ";password=" + info.PassWord + ";Allow Zero Datetime=true" + charset;
                        //conn.ConnectionString = "Data Source=" + info.DataSource + ";port=" + info.Port+";Initial Catalog=" + info.DbName + ";User ID=" + info.UserName + ";Password=" + info.PassWord+charset;
                        break;
                    //case DataBaseTypeEnum.PDM:

                    //    break;
                    //case DataBaseTypeEnum.MongoDB:
                    //    string ConnectionString = "server=" + info.DataSource + ";database=" + info.DbName + ";port=" + info.Port + ";user id=" + info.UserName + ";password=" + info.PassWord + ";Allow Zero Datetime=true";
                    //    break;
                }
                return conn;
            }
            catch (Exception ex)
            {
                //rstmsg = "操作失败。" + ex.Message;
            }

            return conn;

        }

        public static int SqlTimeout
        {
            get { return ConfigurationManager.AppSettings["SqlTimeout"] == null ? 30 : int.Parse(ConfigurationManager.AppSettings["SqlTimeout"].ToString()); }
            
        }

    }
}
