using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace DALProfile
{
    public class SqliteHelper2
    {
        private static string pwd = "admin";
        //private static string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\sqliteTest.db";
        private static string path = @"F:\MyProject\代码生成器\CodeFacility\CodeFacility\Data\SqlLiteDb/test.db";
        //private static string connString = string.Format("Data Source ={0}", path, pwd);

        private static string connString = @"F:\MyProject\代码生成器\CodeFacility\CodeFacility\Data\SqlLiteDb/test.db;Version=3;Password=" + pwd;  //Pooling=true;FailIfMissing=false";
        /// <summary>
        /// 返回数据库链接字符串
        /// </summary> 
        public static string ConnString
        {
            get { return connString; }
        }

        /// <summary>
        /// 执行SQL语句,返回受影响的行数
        /// </summary>
        /// <param name="cmdText">需要被执行的SQL语句</param>
        /// <returns>受影响的行数</returns> 
        public static int ExecuteNonQuery(string cmdText)
        {
            return ExecuteNonQuery(ConnString, cmdText);
        }

        /// <summary>
        /// 执行带有事务的SQL语句
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>受影响的行数</returns> 
        public static int ExecuteNonQuery(SQLiteTransaction trans, string cmdText, params SQLiteParameter[] parameters)
        {
            int val = 0;

            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                PrepareCommand(cmd, (SQLiteConnection)trans.Connection, trans, cmdText, parameters);
                val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }

            return val;
        }

        /// <summary>
        /// 执行SQL语句,返回受影响的行数
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="parameters">SQL的参数</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string connString, string cmdText, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                return ExecuteNonQuery(conn, cmdText, parameters);
            }
        }

        /// <summary>
        /// 执行SQL语句,返回受影响的行数
        /// </summary>
        /// <param name="connection">数据库链接</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(SQLiteConnection connection, string cmdText, params SQLiteParameter[] parameters)
        {
            int val = 0;

            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                PrepareCommand(cmd, connection, null, cmdText, parameters);
                val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }

            return val;
        }

        /// <summary>
        /// 执行查询,并返回结果集的第一行的第一列.其他所有的行和列被忽略.
        /// </summary>
        /// <param name="cmdText">SQL 语句</param>
        /// <returns>第一行的第一列的值</returns>
        public static object ExecuteScalar(string cmdText)
        {
            return ExecuteScalar(ConnString, cmdText);
        }

        /// <summary>
        /// 执行查询,并返回结果集的第一行的第一列.其他所有的行和列被忽略.
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdText">SQL 语句</param>
        /// <returns>第一行的第一列的值</returns>
        public static object ExecuteScalar(string connString, string cmdText)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                return ExecuteScalar(conn, cmdText);
            }
        }

        /// <summary>
        /// 执行查询,并返回结果集的第一行的第一列.其他所有的行和列被忽略.
        /// </summary>
        /// <param name="connection">数据库链接</param>
        /// <param name="cmdText">SQL 语句</param>
        /// <returns>第一行的第一列的值</returns>
        public static object ExecuteScalar(SQLiteConnection connection, string cmdText)
        {
            object val;

            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                PrepareCommand(cmd, connection, null, cmdText);
                val = cmd.ExecuteScalar();
            }

            return val;
        }

        /// <summary>
        /// 执行SQL语句,返回结果集的DataReader
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>结果集的DataReader</returns>
        public static SQLiteDataReader ExecuteReader(string cmdText, params SQLiteParameter[] parameters)
        {
            return ExecuteReader(ConnString, cmdText, parameters);
        }

        /// <summary>
        /// 执行SQL语句,返回结果集的DataReader
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>结果集的DataReader</returns>
        public static SQLiteDataReader ExecuteReader(string connString, string cmdText, params SQLiteParameter[] parameters)
        {
            SQLiteConnection conn = new SQLiteConnection(connString);
            SQLiteCommand cmd = new SQLiteCommand();

            try
            {
                PrepareCommand(cmd, conn, null, cmdText, parameters);
                SQLiteDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 预处理Command对象,数据库链接,事务,需要执行的对象,参数等的初始化
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="conn">Connection对象</param>
        /// <param name="trans">Transcation对象</param>
        /// <param name="cmdText">SQL Text</param>
        /// <param name="parameters">参数实例</param>
        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, string cmdText, params SQLiteParameter[] parameters)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;
            if (null != parameters && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }
        }
    }
}
