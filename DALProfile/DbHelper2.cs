using System;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Collections.Generic;


namespace DALProfile
{
    public abstract class DbHelper2
    {
        public static void SetTimeoutDefault()
        {
            Timeout = 30;
        }
        public static int Timeout = 30;

        public static IDbBase Provider = null;

        public static DbConnection Conn = null;

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {
            return ExecuteNonQuery(DBConfig.CmsConString, cmdType, cmdText, commandParameters);
        }

        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {

            DbCommand cmd = Provider.CreateCommand();

            using (DbConnection conn = Provider.CreateConnection())
            {
                try
                {
                    conn.ConnectionString = connectionString;
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    //DbHelper.Conn = conn;
                    return val;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }
        }

        public static int ExecuteNonQuery(DbConnection connection, CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {

            DbCommand cmd = Provider.CreateCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            DbHelper.Conn = connection;
            return val;
        }

        public static int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            DbHelper.Conn = trans.Connection;
            return val;
        }

        public static int ExecuteNonQueryCurrency(DbConnection connection, CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {
            DbCommand cmd = GetDbCommand(connection);
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            DbHelper.Conn = connection;
            return val;
        }

        public static DbDataReader ExecuteReader(CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {
            return ExecuteReader(DBConfig.CmsConString, cmdType, cmdText, commandParameters);
        }

        public static DbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();
            DbConnection conn = Provider.CreateConnection();
            conn.ConnectionString = connectionString;
            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                DbHelper.Conn = conn;
                return rdr;
            }
            catch
            {

                throw;
            }
            finally
            {
                //conn.Close();
                //conn.Dispose();
                //conn = null;
            }
        }

        /// <summary>
        /// 执行对默认数据库有自定义排序的分页的查询
        /// </summary>
        /// <param name="connectionString">连接字符串
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        public static DbDataReader ExecuteReaderPage(string connectionString, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string GroupClause, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, ParameterCollection commandParameters)
        {
            DbConnection conn = Provider.CreateConnection();
            conn.ConnectionString = connectionString;
            try
            {
                conn.Open();
                DbCommand cmd = Provider.CreateCommand();
                PrepareCommand(cmd, conn, null, CommandType.Text, "", commandParameters);
                string Sql = GetPageSql(conn, cmd, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out  RecordCount, out  PageCount);
                if (GroupClause != null && GroupClause.Trim() != "")
                {
                    int n = Sql.ToLower().LastIndexOf(" order by ");
                    Sql = Sql.Substring(0, n) + " " + GroupClause + " " + Sql.Substring(n);
                }
                cmd.CommandText = Sql;
                DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                //DbHelper.Conn = conn;
                return rdr;
            }
            catch
            {
                //if (conn.State == ConnectionState.Open)
                //    conn.Close();
                throw;
            }
            finally
            {
                //if (conn.State == ConnectionState.Open)
                //    conn.Close();
                //conn.Dispose();
                //conn = null;
            }
        }

        public static DbDataReader ExecuteReader(DbConnection connection, CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();
            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            //DbHelper.Conn = connection;
            return rdr;
        }
        public static object ExecuteScalar(CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {
            return ExecuteScalar(DBConfig.CmsConString, cmdType, cmdText, commandParameters);
        }

        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();

            using (DbConnection connection = Provider.CreateConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    //DbHelper.Conn = connection;
                    return val;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Dispose();
                }
            }
        }

        public static object ExecuteScalar(DbConnection connection, CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {

            DbCommand cmd = Provider.CreateCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            DbHelper.Conn = connection;
            return val;
        }

        public static object ExecuteScalar(DbTransaction trans, CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();

            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            DbHelper.Conn = trans.Connection;
            return val;
        }

        public static DataTable ExecuteTable(CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteTable(DBConfig.CmsConString, cmdType, cmdText, commandParameters);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public static DataTable ExecuteTable(string connectionString, CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();

            using (DbConnection connection = Provider.CreateConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                    DbDataAdapter ap = Provider.CreateDataAdapter();
                    ap.SelectCommand = cmd;
                    DataSet st = new DataSet();
                    ap.Fill(st, "Result");
                    cmd.Parameters.Clear();
                    //DbHelper.Conn = connection;
                    return st.Tables["Result"];
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Dispose();
                }
            }
        }

        public static DbCommand GetDbCommand(DbConnection conn)
        {
            DbCommand cmd = null;
            if (conn is System.Data.SqlClient.SqlConnection)
            {
                cmd = new System.Data.SqlClient.SqlCommand();
            }
            if (conn is System.Data.OleDb.OleDbConnection)
            {
                cmd = new System.Data.OleDb.OleDbCommand();
            }
            if (conn is Devart.Data.Oracle.OracleConnection)
            {
                cmd = new Devart.Data.Oracle.OracleCommand();
            }
            if (conn is System.Data.SQLite.SQLiteConnection)
            {
                cmd = new System.Data.SQLite.SQLiteCommand();
            }
            if (conn is MySql.Data.MySqlClient.MySqlConnection)
            {
                cmd = new MySql.Data.MySqlClient.MySqlCommand();
            }
            return cmd;
        }

        public static DbDataAdapter GetDbDataAdapter(DbConnection conn)
        {
            DbDataAdapter da = null;
            if (conn is System.Data.SqlClient.SqlConnection)
            {
                da = new System.Data.SqlClient.SqlDataAdapter();
            }
            if (conn is System.Data.OleDb.OleDbConnection)
            {
                da = new System.Data.OleDb.OleDbDataAdapter();
            }
            if (conn is Devart.Data.Oracle.OracleConnection)
            {
                da = new Devart.Data.Oracle.OracleDataAdapter();
            }
            if (conn is System.Data.SQLite.SQLiteConnection)
            {
                da = new System.Data.SQLite.SQLiteDataAdapter();
            }
            if (conn is MySql.Data.MySqlClient.MySqlConnection)
            {
                da = new MySql.Data.MySqlClient.MySqlDataAdapter();
            }
            return da;
        }

        public static DataTable ExecuteTableCurrency(DbConnection conn, CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {
            DbCommand cmd = GetDbCommand(conn);
            DbDataAdapter da = GetDbDataAdapter(conn);
            using (DbConnection connection = conn)
            {
                try
                {
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                    DbDataAdapter ap = da;
                    ap.SelectCommand = cmd;
                    DataSet st = new DataSet();
                    ap.Fill(st);
                    cmd.Parameters.Clear();
                    //DbHelper.Conn = connection;
                    return st.Tables[0];
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Dispose();
                    cmd.Parameters.Clear();
                }
            }
        }

        public static DataSet ExecuteDataSetCurrency(DbConnection conn, CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {
            DbCommand cmd = GetDbCommand(conn);
            DbDataAdapter da = GetDbDataAdapter(conn);
            DataSet ds = new DataSet();
            using (DbConnection connection = conn)
            {
                try
                {
                    PrepareCommandCurrency(cmd, connection, null, cmdType, cmdText, commandParameters);
                    DbDataAdapter ap = da;
                    ap.SelectCommand = cmd;
                    ap.Fill(ds);
                    cmd.Parameters.Clear();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Dispose();
                }
            }
            return ds;
        }

        private static void PrepareCommandCurrency(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, ParameterCollection cmdParms)
        {
            if (conn is System.Data.SqlClient.SqlConnection)
            {
                System.Data.SqlClient.SqlConnection sqlconn = new System.Data.SqlClient.SqlConnection();
                sqlconn.ConnectionString = conn.ConnectionString;
                sqlconn.FireInfoMessageEventOnUserErrors = true;
                sqlconn.InfoMessage += new System.Data.SqlClient.SqlInfoMessageEventHandler(SqlConnection_InfoMessage);
                conn = sqlconn;
            }

            if (conn is Devart.Data.Oracle.OracleConnection)
            {
                Devart.Data.Oracle.OracleConnection oraconn = new Devart.Data.Oracle.OracleConnection();
                oraconn.ConnectionString = conn.ConnectionString;
                
                oraconn.InfoMessage += new Devart.Data.Oracle.OracleInfoMessageEventHandler(OracleConnection_InfoMessage);
                conn = oraconn;
            }

            if (conn is System.Data.OleDb.OleDbConnection)
            {
                System.Data.OleDb.OleDbConnection oledbconn = new System.Data.OleDb.OleDbConnection();
                oledbconn.ConnectionString = conn.ConnectionString;
                oledbconn.InfoMessage += new System.Data.OleDb.OleDbInfoMessageEventHandler(OleDbConnection_InfoMessage);
                conn = null;
                conn = oledbconn;
            }

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;
            cmd.CommandTimeout = Timeout;
            //if (cmdParms != null)
            //{
            //    foreach (DbParameter parm in cmdParms)
            //        if (parm != null)
            //            cmd.Parameters.Add(parm);
            //}

            if ((cmdParms != null) && (cmdParms.Count > 0))
            {
                for (int i = 0; i < cmdParms.Count; i++)
                {
                    cmdParms[i].InitRealParameter();
                    cmd.Parameters.Add(cmdParms[i].RealParameter as DbParameter);
                }
            }
        }

        public static IList<System.Data.SqlClient.SqlError> SqlErrorList = new List<System.Data.SqlClient.SqlError>();
        private static void SqlConnection_InfoMessage(object sender, System.Data.SqlClient.SqlInfoMessageEventArgs e)
        {
            foreach (System.Data.SqlClient.SqlError r in e.Errors)
            {
                if (SqlErrorList == null)
                {
                    SqlErrorList = new List<System.Data.SqlClient.SqlError>();
                }
                SqlErrorList.Add(r);
            }
        }

        public static IList<Devart.Data.Oracle.OracleError> OracleErrorList = new List<Devart.Data.Oracle.OracleError>();
        private static void OracleConnection_InfoMessage(object sender, Devart.Data.Oracle.OracleInfoMessageEventArgs e)
        {

            string msg = e.Message;
            foreach (Devart.Data.Oracle.OracleError r in e.Errors)
            {
                if (OracleErrorList == null)
                {
                    OracleErrorList = new List<Devart.Data.Oracle.OracleError>();
                }
                OracleErrorList.Add(r);
            }
        }

        public static IList<System.Data.OleDb.OleDbError> OleDbErrorList = new List<System.Data.OleDb.OleDbError>();
        private static void OleDbConnection_InfoMessage(object sender, System.Data.OleDb.OleDbInfoMessageEventArgs e)
        {
            foreach (System.Data.OleDb.OleDbError r in e.Errors)
            {
                if (OleDbErrorList == null)
                {
                    OleDbErrorList = new List<System.Data.OleDb.OleDbError>();
                }
                OleDbErrorList.Add(r);
            }
        }

        public static int ExecuteTableAdd(string connectionString, CommandType cmdType, string cmdText, DataTable dt)
        {
            int rst = 1;
            DbCommand cmd = Provider.CreateCommand();

            using (DbConnection connection = Provider.CreateConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, null);
                    DbDataAdapter ap = Provider.CreateDataAdapter();
                    ap.SelectCommand = cmd;

                    //DbCommandBuilder dbcmd = new System.Data.OleDb.OleDbCommandBuilder();
                    DbCommandBuilder dbcmd = Provider.CreateDbCommandBuilder();
                    dbcmd.DataAdapter = ap;
                    ap.Update(dt);
                    cmd.Parameters.Clear();

                }
                catch (Exception ex)
                {
                    rst = -1;
                    throw;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Dispose();
                }
            }

            return rst;
        }

        public static DataTable ExecuteTable(DbConnection connection, CommandType cmdType, string cmdText, ParameterCollection commandParameters)
        {

            DbCommand cmd = Provider.CreateCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            DbDataAdapter ap = Provider.CreateDataAdapter();
            ap.SelectCommand = cmd;
            DataSet st = new DataSet();
            ap.Fill(st, "Result");
            cmd.Parameters.Clear();
            DbHelper.Conn = connection;
            return st.Tables["Result"];
        }

        /// <summary>
        /// 执行对默认数据库有自定义排序的分页的查询
        /// </summary>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        public static DataTable ExecutePage(string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, ParameterCollection commandParameters)
        {
            return ExecutePage(DBConfig.CmsConString, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out  RecordCount, out  PageCount, commandParameters);
        }

        /// <summary>
        /// 执行有自定义排序的分页的查询
        /// </summary>
        /// <param name="connectionString">SQL数据库连接字符串</param>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        public static DataTable ExecutePage(string connectionString, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, ParameterCollection commandParameters)
        {
            using (DbConnection connection = Provider.CreateConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    //DbHelper.Conn = connection;
                    return ExecutePage(connection, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, commandParameters);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 执行有自定义排序的分页的查询
        /// </summary>
        /// <param name="connection">SQL数据库连接对象</param>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        public static DataTable ExecutePage(DbConnection connection, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, ParameterCollection commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();
            PrepareCommand(cmd, connection, null, CommandType.Text, "", commandParameters);
            string Sql = GetPageSql(connection, cmd, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out  RecordCount, out  PageCount);
            cmd.CommandText = Sql;
            DbDataAdapter ap = Provider.CreateDataAdapter();
            ap.SelectCommand = cmd;
            DataSet st = new DataSet();
            ap.Fill(st, "PageResult");
            cmd.Parameters.Clear();
            DbHelper.Conn = connection;
            return st.Tables["PageResult"];
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public static void CloseConn()
        {

            if (DbHelper.Conn != null)
            {
                if (DbHelper.Conn.State == ConnectionState.Open)
                {
                    DbHelper.Conn.Close();
                }
            }
        }
        /// <summary>
        /// 取得分页的SQL语句
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmd"></param>
        /// <param name="SqlAllFields"></param>
        /// <param name="SqlTablesAndWhere"></param>
        /// <param name="IndexField"></param>
        /// <param name="OrderFields"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="RecordCount"></param>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        private static string GetPageSql(DbConnection connection, DbCommand cmd, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            RecordCount = 0;
            PageCount = 0;
            if (PageSize <= 0)
            {
                PageSize = 10;
            }
            string SqlCount = "select count(" + IndexField + ") from " + SqlTablesAndWhere;
            cmd.CommandText = SqlCount;
            RecordCount = (int)cmd.ExecuteScalar();
            if (RecordCount % PageSize == 0)
            {
                PageCount = RecordCount / PageSize;
            }
            else
            {
                PageCount = RecordCount / PageSize + 1;
            }
            if (PageIndex > PageCount)
                PageIndex = PageCount;
            if (PageIndex < 1)
                PageIndex = 1;
            string Sql = null;
            if (PageIndex == 1)
            {
                Sql = "select top " + PageSize + " " + SqlAllFields + " from " + SqlTablesAndWhere + " " + OrderFields;
            }
            else
            {
                Sql = "select top " + PageSize + " " + SqlAllFields + " from ";
                if (SqlTablesAndWhere.ToLower().IndexOf(" where ") > 0)
                {
                    string _where = Regex.Replace(SqlTablesAndWhere, @"\ where\ ", " where (", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    Sql += _where + ") and (";
                }
                else
                {
                    Sql += SqlTablesAndWhere + " where (";
                }
                Sql += IndexField + " not in (select top " + (PageIndex - 1) * PageSize + " " + IndexField + " from " + SqlTablesAndWhere + " " + OrderFields;
                Sql += ")) " + OrderFields;
            }
            return Sql;
        }
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, ParameterCollection cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;
            cmd.CommandTimeout = Timeout;
            //if (cmdParms != null)
            //{
            //    foreach (DbParameter parm in cmdParms)
            //        if (parm != null)
            //            cmd.Parameters.Add(parm);
            //}
            if ((cmdParms != null) && (cmdParms.Count > 0))
            {
                for (int i = 0; i < cmdParms.Count; i++)
                {
                    cmdParms[i].InitRealParameter();
                    cmd.Parameters.Add(cmdParms[i].RealParameter as DbParameter);
                }
            }
        }

    }


    public class DBSecurity2
    {
        private const string sKey = "23,24,82,41,40,28,53,34";
        private const string sIV = "21,49,73,15,86,15,24,14";
        # region 加密解密
        //方法 
        //加密方法 
        public static string Encrypt(string pToEncrypt)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                //把字符串放到byte数组中 			
                byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

                //建立加密对象的密钥和偏移量


                byte[] b_key = new byte[8];
                string[] s_keys = new string[8];
                s_keys = sKey.Split(',');


                for (int i = 0; i <= 7; i++)
                {
                    b_key[i] = Convert.ToByte(s_keys[i].ToString());
                }

                des.Key = b_key;

                byte[] b_iv = new byte[8];
                string[] s_ivs = new string[8];
                s_ivs = sIV.Split(',');


                for (int i = 0; i <= 7; i++)
                {
                    b_iv[i] = Convert.ToByte(s_ivs[i].ToString());
                }

                des.IV = b_iv;


                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                //Write the byte array into the crypto stream 
                //(It will end up in the memory stream) 
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                //Get the data back from the memory stream, and into a string 
                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    //Format as hex 
                    ret.AppendFormat("{0:X2}", b);
                }
                ret.ToString();
                return ret.ToString();
            }
            catch
            {
                return "";
            }

        }


        //解密方法 
        public static string Decrypt(string pToDecrypt)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                //Put the input string into the byte array 
                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                //建立加密对象的密钥和偏移量，此值重要，不能修改 
                byte[] b_key = new byte[8];
                string[] s_keys = new string[8];
                s_keys = sKey.Split(',');


                for (int i = 0; i <= 7; i++)
                {
                    b_key[i] = Convert.ToByte(s_keys[i].ToString());
                }

                des.Key = b_key;

                byte[] b_iv = new byte[8];
                string[] s_ivs = new string[8];
                s_ivs = sIV.Split(',');


                for (int i = 0; i <= 7; i++)
                {
                    b_iv[i] = Convert.ToByte(s_ivs[i].ToString());
                }

                des.IV = b_iv;


                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                //Flush the data through the crypto stream into the memory stream 
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                //Get the decrypted data back from the memory stream 
                //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象 
                StringBuilder ret = new StringBuilder();

                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception exp)
            {
                string s = exp.Message.ToString();
                return "";
            }
        }


        #endregion
    }
}