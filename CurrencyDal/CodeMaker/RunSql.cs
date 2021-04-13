using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.Common;
using System.Data.SQLite;

using DALProfile;
using DALFactory.CodeMaker;
using Model.CodeMaker;

namespace CurrencyDal.CodeMaker
{
    /// <summary>
    /// 查询分析器运行sql语句
    /// </summary>
    public class RunSql
    {
        #region 查询数据库
        public DataSet Run(DbLinkInfo info,string sql,out string sqlmsg, out string rstmsg)
        {
            sqlmsg = "";
            rstmsg = "";
            DataSet ds = null;
            try
            {
                DataBaseTypeEnum fc = DataBaseType.GetDataBaseType(info.DbType);
                System.Data.Common.DbConnection conn = DBConfig.GetDbConnection(info);
                DbHelper.SqlErrorList = null;
                switch (fc)
                {
                    case DataBaseTypeEnum.SQLServer:
                        sql = sql + " print '('+convert(varchar(10), @@rowcount)+'行受影响)'";
                        ds = DbHelper.ExecuteDataSetCurrency(conn, CommandType.Text, sql, null);
                        break;
                    case DataBaseTypeEnum.Oracle:
                        //sql = @"declare n number; begin "+sql+" n:=sql%rowcount; dbms_output.put_line(n); end; ";
                        ds = DbHelper.ExecuteDataSetCurrency(conn, CommandType.Text, sql, null);
                        break;
                    case DataBaseTypeEnum.Access:
                        ds = DbHelper.ExecuteDataSetCurrency(conn, CommandType.Text, sql, null);
                        break;
                    case DataBaseTypeEnum.SQLite:
                        ds = DbHelper.ExecuteDataSetCurrency(conn, CommandType.Text, sql, null);
                        break;
                    case DataBaseTypeEnum.MySql:
                        ds = DbHelper.ExecuteDataSetCurrency(conn, CommandType.Text, sql, null);
                        break;
                }

                StringBuilder ser = new StringBuilder();
                switch (fc)
                {
                    case DataBaseTypeEnum.SQLServer:

                        if (DbHelper.SqlErrorList != null)
                        {
                            for (int i = 0; i < DbHelper.SqlErrorList.Count; i++)
                            {
                                System.Data.SqlClient.SqlError r = DbHelper.SqlErrorList[i];
                                //ser.Append("从 SQL Server 中获取一个数值错误代码，它表示错误、警告或“未找到数据”消息。" + r.State.ToString());
                                //ser.Append("获取生成错误的提供程序的名称。" + r.Source.ToString());
                                //ser.Append("获取生成错误的 SQL Server 实例的名称。" + r.Server.ToString());
                                //ser.Append("获取生成错误的存储过程或远程过程调用 (RPC) 的名称。" + r.Procedure.ToString());
                                //ser.Append("获取一个标识错误类型的数字。" + r.Number.ToString());
                                //ser.Append("获取对错误进行描述的文本。" + r.Message.ToString());
                                //ser.Append("从包含错误的 Transact-SQL 批命令或存储过程中获取行号。" + r.LineNumber.ToString());
                                //ser.Append("获取从 SQL Server 返回的错误的严重程度。" + r.Class.ToString());
                                if (i > 0)
                                {
                                    ser.Append("\r\n");
                                }
                                ser.Append(r.Message);
                            }
                        }
                        break;
                    case DataBaseTypeEnum.Oracle:
                        if (DbHelper.OracleErrorList != null)
                        {
                            for (int i = 0; i < DbHelper.OracleErrorList.Count; i++)
                            {
                                Devart.Data.Oracle.OracleError r = DbHelper.OracleErrorList[i];
                                if (i > 0)
                                {
                                    ser.Append("\r\n");
                                }
                                ser.Append(r.Message);
                            }
                        }
                        break;
                    case DataBaseTypeEnum.Access:
                        if (DbHelper.OleDbErrorList != null)
                        {
                            for (int i = 0; i < DbHelper.OleDbErrorList.Count; i++)
                            {
                                System.Data.OleDb.OleDbError r = DbHelper.OleDbErrorList[i];
                                if (i > 0)
                                {
                                    ser.Append("\r\n");
                                }
                                ser.Append(r.Message);
                            }
                        }
                        break;

                }
                sqlmsg = ser.ToString();
            }
            catch (Exception ex)
            {
                rstmsg = "操作失败。" + ex.Message;
            }
            return ds;
        }

        public int Stop()
        {
            int rst=0;
            return rst;
        }
        #endregion

        #region 执行事务
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sqllist">sql语句列表</param>
        /// <returns></returns>
        public int ExeSqlTran(DbLinkInfo info, List<string> sqllist,out string rstmsg)
        {
            int rst = 0;
            rstmsg = "";
            try
            {
                System.Data.Common.DbConnection conn = DBConfig.GetDbConnection(info);
                rst = DbHelper.ExecuteNonQueryCurrencyTransactions(conn, CommandType.Text, sqllist, null);
                rstmsg = "共" + sqllist.Count + "行数据，成功修改" + rst + "行数据,有" + (sqllist.Count-rst).ToString()+ "行未修改。";
            }
            catch (Exception ex)
            {
                rstmsg = ex.Message;

            }
            return rst;
        }
        #endregion
    }
}
