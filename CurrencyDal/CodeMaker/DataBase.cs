using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.IO;

using DALProfile;
using DALFactory.CodeMaker;
using Model.CodeMaker;

namespace CurrencyDal.CodeMaker
{
    public class DataBase : IDataBase
    {
        #region 查询数据库
        public DataBaseInfo DataBaseGetInfo(DbLinkInfo info,List<string> tableNameList,out string rstmsg)
        {
            rstmsg = "";
            DataBaseInfo dbinfo = new DataBaseInfo();
            try
            {
                DataBaseTypeEnum fc = DataBaseType.GetDataBaseType(info.DbType);
                System.Data.Common.DbConnection conn = DBConfig.GetDbConnection(info);
                switch (fc)
                {
                    case DataBaseTypeEnum.SQLServer:
                        dbinfo = GetDataBaseInfoBySQLserver(conn, tableNameList);
                        break;
                    case DataBaseTypeEnum.Oracle:
                        dbinfo = GetDataBaseInfoByOracle(conn, tableNameList);
                        break;
                    case DataBaseTypeEnum.Access:
                        dbinfo = GetDataBaseInfoByAccess(conn, tableNameList);
                        break;
                    case DataBaseTypeEnum.SQLite:
                        dbinfo = GetDataBaseInfoBySQLite(conn, tableNameList);
                        break;
                    case DataBaseTypeEnum.MySql:
                        dbinfo = GetDataBaseInfoByMySql(conn, tableNameList);
                        break;
                    //case DataBaseTypeEnum.PDM:

                    //    break;
                    //case DataBaseTypeEnum.MongoDB:
                    //    string ConnectionString = "server=" + info.DataSource + ";database=" + info.DbName + ";port=" + info.Port + ";user id=" + info.UserName + ";password=" + info.PassWord + ";Allow Zero Datetime=true";
                    //    dbinfo = GetDataBaseInfoByMyMongoDB(ConnectionString, tableNameList);
                    //    break;
                }
            }
            catch (Exception ex)
            {
                rstmsg="操作失败。" + ex.Message;
            }
            return dbinfo;
        }

        #region 查询SQLserver数据库结构
        /// <summary>
        /// 查询SQLserver数据库结构
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataBaseInfo GetDataBaseInfoBySQLserver(System.Data.Common.DbConnection conn, List<string> tableNameList)
        {
            string tableName = "";
            for (int i=0;i<tableNameList.Count;i++)
            {
                if (i == 0)
                {
                    tableName = "'" + tableNameList[i] + "'";
                }
                else
                {
                    tableName = tableName + ",'" + tableNameList[i]+"'";
                }
            }
            DataBaseInfo dbinfo = new DataBaseInfo();
            
            dbinfo.DBType = DataBaseTypeEnum.SQLServer;

            string sql="";
//            sql = @"select ta.*,tb.PrimaryKey from 
//(select sysobjects.name TableName,syscolumns.colid,syscolumns.name ColumnsName,systypes.name  FieldType, 
//syscolumns.length FieldWidth, syscolumns.isnullable , sysobjects.type
//from  syscolumns, sysobjects, systypes 
//where  syscolumns.id=sysobjects.id and syscolumns.xusertype=systypes.xusertype 
//and (sysobjects.type='U' or sysobjects.type='V' ) and systypes.name <>'_default_' 
//and systypes.name<>'sysname') ta full join
//(select a.table_name, b.column_name,a.constraint_type PrimaryKey 
//from information_schema.table_constraints a inner join information_schema.constraint_column_usage b 
//on a.constraint_name = b.constraint_name where a.constraint_type = 'PRIMARY KEY') tb
//on ta.TableName=tb.table_name and ta.ColumnsName=tb.column_name
//order by ta.TableName,ta.colid";
            //查询表、视图、存储过程
            sql = @"SELECT 
d.name TableName, 
a.colorder ColumnsId, 
a.name ColumnsName, 
(case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '1' else '' end) IsIdentity, 
(case when (SELECT count(*) 
FROM sysobjects 
WHERE (name in 
           (SELECT name 
          FROM sysindexes 
          WHERE (id = a.id) AND (indid in 
                    (SELECT indid 
                   FROM sysindexkeys 
                   WHERE (id = a.id) AND (colid in 
                             (SELECT colid 
                            FROM syscolumns 
                            WHERE (id = a.id) AND (name = a.name))))))) AND 
        (xtype = 'PK'))>0 then '1' else '' end) PrimaryKey, 
b.name ColumnsType, 
a.length Length, 
COLUMNPROPERTY(a.id,a.name,'PRECISION') as Longness, 
isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0) as Scale, 
(case when a.isnullable=1 then '1' else '' end) isnullable, 
isnull(e.text,'')  DefaultValue, 
isnull(g.[value],'') AS Description,
d.xtype
FROM syscolumns a 
left join systypes b 
on a.xtype=b.xusertype 
inner join sysobjects d 
on a.id=d.id and d.xtype in ('U','V','P') 
and d.name<>'dtproperties' 
left join syscomments e 
on a.cdefault=e.id 
left join sys.extended_properties g 
on a.id=g.major_id AND a.colid = g.minor_id";
if(tableName != null && tableName!="")
{
sql =sql+" where d.name in ("+tableName+")";
}
sql =sql+" order by d.xtype,d.name,a.colorder";

            DataTable dt = DbHelper.ExecuteTableCurrency(conn, CommandType.Text, sql, null);
            TableInfo LastInfo = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string TableName = dt.Rows[i]["TableName"].ToString();
                
                if (LastInfo == null || LastInfo.Name != TableName)
                {
                    string xtype = dt.Rows[i]["xtype"].ToString().Trim();
                    LastInfo = new TableInfo();
                    if (xtype == "U")
                    {
                        dbinfo.Tables.Add(LastInfo);
                    }
                    else if (xtype == "V")
                    {
                        dbinfo.View.Add(LastInfo);
                    }
                    else if (xtype == "P")
                    {
                        dbinfo.Procedure.Add(LastInfo);
                    }
                    LastInfo.Name = TableName;
                    LastInfo.xType = dt.Rows[i]["xtype"].ToString();
                }
                FieldInfo fInfo = new FieldInfo();
                LastInfo.Fields.Add(fInfo);
                fInfo.ID =int.Parse(dt.Rows[i]["ColumnsId"].ToString());
                fInfo.Name = dt.Rows[i]["ColumnsName"].ToString();
                fInfo.FieldType = dt.Rows[i]["ColumnsType"].ToString();
                //fInfo.FieldWidth = dt.Rows[i]["Length"].ToString();
                fInfo.FieldWidth = dt.Rows[i]["Longness"].ToString();
                fInfo.Scale = dt.Rows[i]["Scale"].ToString();
                fInfo.Description = dt.Rows[i]["Description"].ToString();
                fInfo.DefaultValue = dt.Rows[i]["DefaultValue"].ToString(); 
                if (dt.Rows[i]["IsIdentity"].ToString() == "1")
                    fInfo.IsIdentity = true;
                else
                    fInfo.IsIdentity = false;

                if (dt.Rows[i]["PrimaryKey"].ToString() == "1")
                    fInfo.PrimaryKey = true;
                else
                    fInfo.PrimaryKey = false;

                if (dt.Rows[i]["isnullable"].ToString() == "1")
                    fInfo.Nullable = true;
                else
                    fInfo.Nullable = false;
            }

            return dbinfo;
        }
        #endregion

        #region 查询Oracle数据库结构
        /// <summary>
        /// 查询Oracle数据库结构
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataBaseInfo GetDataBaseInfoByOracle(System.Data.Common.DbConnection dbconn, List<string> tableNameList)
        {
            string tableName = "";
            for (int i = 0; i < tableNameList.Count; i++)
            {
                if (i == 0)
                {
                    tableName = "'" + tableNameList[i] + "'";
                }
                else
                {
                    tableName = tableName + ",'" + tableNameList[i] + "'";
                }
            }
            DataBaseInfo dbinfo = new DataBaseInfo();
            dbinfo.DBType = DataBaseTypeEnum.Oracle;
            System.Data.Common.DbConnection conn = new Devart.Data.Oracle.OracleConnection();
            conn.ConnectionString = dbconn.ConnectionString;
            //查询表
            string sql = @"select A.Table_Name,A.column_id,A.column_name,A.data_type,A.data_length,A.data_precision,
    A.Data_Scale,A.nullable,A.Data_default,B.comments,C.index_name,E.constraint_type
from user_tab_columns A,user_col_comments B,user_ind_columns C,user_tables D,user_constraints E
where   A.Table_Name = B.Table_Name and A.Column_Name = B.Column_Name
    and A.Table_Name = C.Table_Name(+) and A.Column_Name =C.Column_Name(+)
    and A.Table_Name=D.Table_Name and C.table_name=E.table_name(+) and C.index_name=E.constraint_name(+)";
if(tableName != null && tableName!="")
{
    sql = sql + " and D.Table_Name in (" + tableName + ")";
}
sql =sql+" order by A.Table_Name,A.column_id";

            DataTable dt = DbHelper.ExecuteTableCurrency(conn, CommandType.Text, sql, null);
            TableInfo LastInfo = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string TableName = dt.Rows[i]["Table_Name"].ToString();
                if (LastInfo == null || LastInfo.Name != TableName)
                {
                    LastInfo = new TableInfo();
                    dbinfo.Tables.Add(LastInfo);
                    LastInfo.Name = TableName;
                }
                FieldInfo fInfo = new FieldInfo();
                LastInfo.Fields.Add(fInfo);
                fInfo.ID = int.Parse(dt.Rows[i]["column_id"].ToString());
                fInfo.Name = dt.Rows[i]["column_name"].ToString();
                fInfo.FieldType = dt.Rows[i]["data_type"].ToString();
                fInfo.FieldWidth = dt.Rows[i]["data_length"].ToString();
                fInfo.Scale = dt.Rows[i]["Data_Scale"].ToString();
                fInfo.Description = dt.Rows[i]["comments"].ToString();
                //if (dt.Rows[i]["IsIdentity"].ToString() == "1")
                //{
                //    fInfo.IsIdentity = true;
                //}
                if (dt.Rows[i]["constraint_type"].ToString() == "P")
                    fInfo.PrimaryKey = true;
                else 
                    fInfo.PrimaryKey = false;
                if (dt.Rows[i]["nullable"].ToString() == "N")
                    fInfo.Nullable = false;
                else if (dt.Rows[i]["nullable"].ToString() == "Y")
                    fInfo.Nullable = true;
            }

            //查询视图
            sql = @" select A.Table_Name,A.column_name,A.data_type,A.data_length,A.data_precision, A.Data_Scale,A.nullable
 from   dba_tab_columns A where A.table_name in (select view_name from user_views) ";
            if (tableName != null && tableName != "")
            {
                sql = sql + " and A.Table_Name in (" + tableName + ")";
            }
            sql = sql + "order by A.Table_Name,A.column_name";

            conn = new Devart.Data.Oracle.OracleConnection();
            conn.ConnectionString = dbconn.ConnectionString;
            DataTable vdt = DbHelper.ExecuteTableCurrency(conn, CommandType.Text, sql, null);
            TableInfo vInfo = null;
            for (int i = 0; i < vdt.Rows.Count; i++)
            {
                string Name = vdt.Rows[i]["Table_Name"].ToString();
                if (vInfo == null || vInfo.Name != Name)
                {
                    vInfo = new TableInfo();
                    dbinfo.View.Add(vInfo);
                    vInfo.Name = Name;
                }
                FieldInfo fInfo = new FieldInfo();
                vInfo.Fields.Add(fInfo);
                fInfo.Name = dt.Rows[i]["column_name"].ToString();
                fInfo.FieldType = dt.Rows[i]["data_type"].ToString();
                fInfo.FieldWidth = dt.Rows[i]["data_length"].ToString();
                if (dt.Rows[i]["nullable"].ToString() == "N")
                    fInfo.Nullable = true;
            }

            
            if (tableName != null && tableName != "")
            {
                return dbinfo;
            }
            //查询存储过程
            sql = "select Object_Name from user_procedures";
            conn = new Devart.Data.Oracle.OracleConnection();
            conn.ConnectionString = dbconn.ConnectionString;
            DataTable pdt = DbHelper.ExecuteTableCurrency(conn, CommandType.Text, sql, null);
            TableInfo proInfo = null;
            for (int i = 0; i < pdt.Rows.Count; i++)
            {
                string Name = pdt.Rows[i]["Object_Name"].ToString();
                if (proInfo == null || proInfo.Name != Name)
                {
                    proInfo = new TableInfo();
                    dbinfo.Procedure.Add(proInfo);
                    proInfo.Name = Name;
                }
            }
            
            return dbinfo;
        }
        #endregion

        #region 查询Access数据库结构
        /// <summary>
        /// 查询Access数据库结构
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataBaseInfo GetDataBaseInfoByAccess(System.Data.Common.DbConnection dbconn, List<string> tableNameList)
        {
            string tableName = "";
            for (int i = 0; i < tableNameList.Count; i++)
            {
                if (i == 0)
                {
                    tableName = "TABLE_NAME='"+tableNameList[i]+"'";
                }
                else
                {
                    tableName += " Or TABLE_NAME='" + tableNameList[i] + "'";
                }
            }
            DataBaseInfo dbinfo = new DataBaseInfo();
            System.Data.OleDb.OleDbConnection conn=new System.Data.OleDb.OleDbConnection();
            if (dbconn is System.Data.OleDb.OleDbConnection)
            {
                conn = dbconn as System.Data.OleDb.OleDbConnection;
            }
            else
            {
                return dbinfo;
            }

            dbinfo.DBType = DataBaseTypeEnum.Access;
            int RecordCount = 0;
            dbinfo.Tables.Clear();
            string dbName = conn.DataSource;
            if (dbName != null)
                dbinfo.Name = System.IO.Path.GetFileName(dbName);

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                

                using (System.Data.DataTable dt = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Columns, null))
                {
                    DataView dv = dt.DefaultView;
                    if (tableName != null && tableName != "")
                    {
                        dv.RowFilter = tableName;
                    }
                    dv.Sort = "TABLE_NAME Asc,ORDINAL_POSITION Asc";
                    DataTable myDataTable = dv.ToTable();

                    foreach (System.Data.DataRow myRow in myDataTable.Rows)
                    {
                        string strTable = Convert.ToString(myRow["TABLE_NAME"]);
                        if (!strTable.StartsWith("MSys"))
                        {
                            TableInfo myTable = dbinfo.Tables[strTable];
                            if (myTable == null)
                            {
                                myTable = new TableInfo();
                                myTable.Name = strTable;
                                dbinfo.Tables.Add(myTable);
                            }
                            FieldInfo myField = new FieldInfo();
                            myTable.Fields.Add(myField);
                            myField.ID = int.Parse(myRow["ORDINAL_POSITION"].ToString());
                            myField.Name = Convert.ToString(myRow["COLUMN_NAME"]);
                            myField.Nullable = Convert.ToBoolean(myRow["IS_NULLABLE"]);
                            myField.Description = "";
                            System.Data.OleDb.OleDbType intType = (System.Data.OleDb.OleDbType)
                                Convert.ToInt32(myRow["DATA_TYPE"]);
                            if (System.DBNull.Value.Equals(myRow["DESCRIPTION"]) == false)
                            {
                                myField.Description = Convert.ToString(myRow["DESCRIPTION"]);
                            }
                            if (intType == System.Data.OleDb.OleDbType.WChar)
                            {
                                myField.FieldType = "Char";
                            }
                            else
                            {
                                myField.FieldType = intType.ToString();
                            }
                            myField.Scale = "";
                            myField.FieldWidth = Convert.ToString(myRow["CHARACTER_MAXIMUM_LENGTH"]);
                            string aa = myRow["IS_NULLABLE"].ToString();

                            if (System.DBNull.Value.Equals(myRow["IS_NULLABLE"]) == false)
                            {
                                myField.Nullable =Convert.ToBoolean(myRow["IS_NULLABLE"]);
                            }

                            RecordCount++;
                        }
                    }
                }
                using (System.Data.DataTable dt = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Indexes, null))
                {
                    DataView dv = dt.DefaultView;
                    if (tableName != null && tableName != "")
                    {
                        dv.RowFilter = tableName;
                    }
                    DataTable myDataTable = dv.ToTable();
                    foreach (System.Data.DataRow myRow in myDataTable.Rows)
                    {
                        string strTable = Convert.ToString(myRow["TABLE_NAME"]);
                        TableInfo myTable = dbinfo.Tables[strTable];
                        if (myTable != null)
                        {
                            FieldInfo myField = myTable.Fields[Convert.ToString(myRow["COLUMN_NAME"])];
                            if (myField != null)
                            {
                                if (myRow["PRIMARY_KEY"].ToString() == "True")
                                {
                                    myField.PrimaryKey = (Convert.ToBoolean(myRow["PRIMARY_KEY"]));
                                }
                                else if (myRow["PRIMARY_KEY"].ToString() == "False")
                                {
                                    myField.Indexed = true;
                                }
                            }
                        }
                    }
                }

                //表自增字段查询？
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
            }

            return dbinfo;
        }
        #endregion
        #region 未用
        public static DataSet GetOleDbColumns(string tableName)
        {
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\Project\OrgCertificate\OrgCertificate\bin\Debug\OrgCertificateDB.mdb;User ID=;Password=;";
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
            conn.ConnectionString = connectionString;
            //DBHelperOleDb.connectionString = dbLink.linkConnStr;
            //DataTable dtKey = DBHelperOleDb.GetPrimaryInfo(tableName);//获取主键信息
            //DataTable result = DBHelperOleDb.GetColumnInfo(tableName);//获取列信息
            //System.Data.DataSetExtension
            DataTable dtKey = new DataTable(); // DBHelperOleDb.GetPrimaryInfo(tableName);//获取主键信息
            DataTable result = new DataTable(); //DBHelperOleDb.GetColumnInfo(tableName);//获取列信息
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("tableName", typeof(string)));
            dt.Columns.Add(new DataColumn("tableDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("colOrder", typeof(string)));
            dt.Columns.Add(new DataColumn("columnName", typeof(string)));
            dt.Columns.Add(new DataColumn("IsIdentity", typeof(string)));
            dt.Columns.Add(new DataColumn("IsPrimaryKey", typeof(string)));
            dt.Columns.Add(new DataColumn("TypeName", typeof(string)));
            dt.Columns.Add(new DataColumn("Length", typeof(string)));
            dt.Columns.Add(new DataColumn("Precision", typeof(string)));
            dt.Columns.Add(new DataColumn("Scale", typeof(string)));
            dt.Columns.Add(new DataColumn("Nullable", typeof(string)));
            dt.Columns.Add(new DataColumn("DefaultVal", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();
                r["tableName"] = row["TABLE_NAME"].ToString();
                r["tableDescription"] = row["TABLE_CATALOG"].ToString();
                r["colOrder"] = row["ORDINAL_POSITION"].ToString();//
                r["columnName"] = row["COLUMN_NAME"].ToString();
                r["IsIdentity"] = false;//还未找到对应项
                r["IsPrimaryKey"] = dtKey.Select(string.Format("COLUMN_NAME='{0}'", row["COLUMN_NAME"].ToString())).Length > 0 ? true : false;//是否是主键
                r["TypeName"] = row["DATA_TYPE"].ToString();
                r["Length"] = row["CHARACTER_MAXIMUM_LENGTH"].ToString();
                r["Precision"] = row["NUMERIC_PRECISION"].ToString();
                r["Scale"] = row["NUMERIC_SCALE"].ToString();
                r["Nullable"] = bool.Parse(row["IS_NULLABLE"].ToString());
                r["DefaultVal"] = row["COLUMN_DEFAULT"].ToString();
                r["Description"] = row["DESCRIPTION"].ToString();
                dt.Rows.Add(r);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// 获取Access表列信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetColumnInfo(string tableName)
        {
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\Project\OrgCertificate\OrgCertificate\bin\Debug\OrgCertificateDB.mdb;User ID=;Password=;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null });
                DataView view = new DataView();
                view.Table = dt;
                view.RowFilter = string.Format("table_name='{0}'", tableName);
                return view.ToTable();
            }
        }

        /// <summary>
        /// 获取Access表主键信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetPrimaryInfo(string tableName)
        {
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\Project\OrgCertificate\OrgCertificate\bin\Debug\OrgCertificateDB.mdb;User ID=;Password=;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, new object[] { null, null });
                DataView view = new DataView();
                view.Table = dt;
                view.RowFilter = string.Format("table_name='{0}'", tableName);
                return view.ToTable();
            }
        }
        #endregion

        #region 查询SQLite数据库结构
        /// <summary>
        /// 查询SQLite数据库结构
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataBaseInfo GetDataBaseInfoBySQLite(System.Data.Common.DbConnection conn, List<string> tableNameList)
        {
            string tableName = "";
            for (int i = 0; i < tableNameList.Count; i++)
            {
                if (i == 0)
                {
                    tableName = "'" + tableNameList[i] + "'";
                }
                else
                {
                    tableName = tableName + ",'" + tableNameList[i] + "'";
                }
            }
            DataBaseInfo dbinfo = new DataBaseInfo();

            dbinfo.DBType = DataBaseTypeEnum.SQLServer;

            string sql = @"select type,name,tbl_name,rootpage,sql from sqlite_master where type in ('table','view') and name not like 'sqlite%' and tbl_name not like 'sqlite%'";
            if (tableName != null && tableName != "")
            {
                sql = sql + " and name in (" + tableName + ") and tbl_name in (" + tableName + ") ";
            }
            sql = sql + " order by type,name";

            DataTable dt = DbHelper.ExecuteTableCurrency(conn, CommandType.Text, sql, null);
            TableInfo LastInfo = null;
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Name = dt.Rows[i]["name"].ToString();
                    string type = dt.Rows[i]["type"].ToString().Trim();

                    if (LastInfo == null || LastInfo.Name != Name)
                    {

                        LastInfo = new TableInfo();
                        if (type == "table")
                        {
                            dbinfo.Tables.Add(LastInfo);
                        }
                        else if (type == "view")
                        {
                            dbinfo.View.Add(LastInfo);
                        }

                        LastInfo.Name = Name;
                        LastInfo.xType = type;
                    }

                    if (type == "table")
                    {
                        

                        string createsql = dt.Rows[i]["sql"].ToString().Trim();
                        createsql = createsql.Replace("\r\n", "");
                        int iof = createsql.IndexOf("(") + 1;
                        createsql = createsql.Substring(iof, createsql.Length - iof - 1);
                        while (createsql.IndexOf("  ") > -1)
                        {
                            createsql = createsql.Replace("  ", " ");
                        }

                        //字段说明中的","替换为";"
                        string[] val = System.Text.RegularExpressions.Regex.Split(createsql, @"default");
                        for (int j = 0; j < val.Length; j++)
                        {
                            string strval = val[j].Trim();

                            string[] fv = strval.Split('\'');
                            if (fv.Length > 1)
                            {
                                string str = fv[1];
                                if (str.IndexOf(",") > -1)
                                {
                                    string str1 = str.Replace(",", ";");
                                    createsql=createsql.Replace(str, str1);
                                }
                            }
                        }

                        string[] field = createsql.Split(',');
                        for (int j = 0; j < field.Length; j++)
                        {
                            FieldInfo fInfo = new FieldInfo();
                            LastInfo.Fields.Add(fInfo);
                            string strfield = field[j].Trim();
                            while (strfield.IndexOf("  ") > -1)
                            {
                                strfield = strfield.Replace("  ", " ");
                            }
                            string[] flist = strfield.Split(' ');

                            fInfo.ID = j + 1;
                            fInfo.Name = flist[0];

                            if (flist[1].IndexOf("(") > -1)
                            {
                                string[] fieldType = flist[1].Split('(');
                                string ftype = fieldType[0];
                                fInfo.FieldType = ftype;
                                string len = fieldType[1].Substring(0, fieldType[1].Length - 1);
                                if (fieldType[0] == "decimal")
                                {
                                    string[] lenArray = len.Split(',');
                                    fInfo.FieldWidth = lenArray[0];
                                    fInfo.Scale = lenArray[1];
                                }
                                else
                                {
                                    fInfo.FieldWidth = len;
                                    fInfo.Scale = "";
                                }
                            }
                            else
                            {
                                fInfo.FieldType = flist[1];

                                fInfo.FieldWidth = "";
                                fInfo.Scale = "";
                            }

                            string fieldLower = field[j].ToLower();
                            if (fieldLower.IndexOf("default") > -1)
                            {
                                for (int l = 0; l < flist.Length; l++)
                                {
                                    if (flist[l].ToLower().IndexOf("default")>-1)
                                    {
                                        if(flist[l].Trim().Length==7)
                                        {
                                            fInfo.DefaultValue = flist[l + 1].Replace("'", "");
                                        }
                                        else
                                        {
                                            fInfo.DefaultValue = flist[l].Trim().Substring(7, flist[l].Trim().Length - 7).Replace("'", ""); ;
                                        }
                                       
                                    }
                                }
                            }
                            else
                            {
                                fInfo.DefaultValue = "";
                            }

                            fInfo.Description = "";

                            if (fieldLower.IndexOf("integer") > -1 && fieldLower.IndexOf("autoincrement") > -1)
                            {
                                fInfo.IsIdentity = true;
                            }
                            else fInfo.IsIdentity = false;
                            if (fieldLower.IndexOf("integer") > -1 && fieldLower.IndexOf("primary") > -1 && fieldLower.IndexOf("key") > -1 && fieldLower.IndexOf("autoincrement") > -1)
                            {
                                fInfo.PrimaryKey = true;
                            }
                            else fInfo.PrimaryKey = false;
                            if (fieldLower.IndexOf("not") > -1 && fieldLower.IndexOf("null") > -1)
                                fInfo.Nullable = true;
                            else fInfo.Nullable = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return dbinfo;
        }
        #endregion

        #region 查询MySql数据结构
        public DataBaseInfo GetDataBaseInfoByMySql(System.Data.Common.DbConnection conn, List<string> tableNameList)
        {
            string tableName = "";
            for (int i = 0; i < tableNameList.Count; i++)
            {
                if (i == 0)
                {
                    tableName = "'" + tableNameList[i] + "'";
                }
                else
                {
                    tableName = tableName + ",'" + tableNameList[i] + "'";
                }
            }
            DataBaseInfo dbinfo = new DataBaseInfo();

            dbinfo.DBType = DataBaseTypeEnum.SQLServer;

            string dbName = conn.Database;

            //查询表和视图
            string sql = "select t.Table_Type,c.Table_Name,c.Column_Name,c.Ordinal_Position,c.Is_Nullable,c.Data_Type,c.Column_Default,c.Character_Maximum_Length,c.Numeric_Precision,c.NumerIc_Scale,c.Column_Type,c.Column_Key,c.Column_Comment,Extra  from information_schema.columns c,information_schema.tables t where c.table_schema=t.table_schema and c.table_name=t.table_name and t.table_schema='" + dbName + "'";
            if (tableName != null && tableName != "")
            {
                sql = sql + " and c.Table_Name in (" + tableName + ")";
            }
            sql = sql + " order by t.Table_Type,c.Table_Name,c.Ordinal_Position";
            DataTable dt = DbHelper.ExecuteTableCurrency(conn, CommandType.Text, sql, null);
            TableInfo LastInfo = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string TableName = dt.Rows[i]["Table_Name"].ToString();

                if (LastInfo == null || LastInfo.Name != TableName)
                {
                    string xtype = dt.Rows[i]["Table_Type"].ToString().Trim();
                    LastInfo = new TableInfo();
                    if (xtype == "BASE TABLE")
                    {
                        dbinfo.Tables.Add(LastInfo);
                    }
                    else if (xtype == "VIEW")
                    {
                        dbinfo.View.Add(LastInfo);
                    }
                    LastInfo.Name = TableName;
                    LastInfo.xType = dt.Rows[i]["Table_Type"].ToString();
                }
                FieldInfo fInfo = new FieldInfo();
                LastInfo.Fields.Add(fInfo);
                fInfo.ID = int.Parse(dt.Rows[i]["Ordinal_Position"].ToString());
                fInfo.Name = dt.Rows[i]["Column_Name"].ToString();
                fInfo.FieldType = dt.Rows[i]["Data_Type"].ToString();
                if (dt.Rows[i]["Character_Maximum_Length"].ToString() != "")
                {
                    fInfo.FieldWidth = dt.Rows[i]["Character_Maximum_Length"].ToString();
                }
                else if (dt.Rows[i]["Numeric_Precision"].ToString() != "")
                {
                    fInfo.FieldWidth = dt.Rows[i]["Numeric_Precision"].ToString();
                }

                fInfo.Scale = dt.Rows[i]["NumerIc_Scale"].ToString();
                fInfo.Description = dt.Rows[i]["Column_Comment"].ToString();
                if (dt.Rows[i]["Extra"].ToString().ToLower() == "auto_increment")
                    fInfo.IsIdentity = true;
                else fInfo.IsIdentity = false;
                if (dt.Rows[i]["Column_Key"].ToString() == "PRI")
                    fInfo.PrimaryKey = true;
                else fInfo.PrimaryKey = false;
                if (dt.Rows[i]["Is_Nullable"].ToString() == "NO")
                    fInfo.Nullable = false;
                else fInfo.Nullable = true;

                fInfo.DefaultValue = dt.Rows[i]["Column_Default"].ToString();
            }

            if (tableNameList.Count > 0)
                return dbinfo;

            //查询存储过程
            sql = "select Specific_Name,Parameter_Name,Ordinal_Position,Data_Type,Character_Maximum_Length,Numeric_Precision,NumerIc_Scale,Dtd_Identifier from information_schema.parameters where Routine_Type='PROCEDURE' and Specific_Schema='" + dbName + "'";
            DataTable pdt = DbHelper.ExecuteTableCurrency(conn, CommandType.Text, sql, null);
            TableInfo proInfo = null;
            for (int i = 0; i < pdt.Rows.Count; i++)
            {
                string Name = pdt.Rows[i]["Specific_Name"].ToString();
                if (proInfo == null || proInfo.Name != Name)
                {
                    proInfo = new TableInfo();
                    dbinfo.Procedure.Add(proInfo);
                    proInfo.Name = Name;
                }
                FieldInfo pInfo = new FieldInfo();
                proInfo.Fields.Add(pInfo);
                pInfo.ID = int.Parse(dt.Rows[i]["Ordinal_Position"].ToString());
                //pInfo.Name = dt.Rows[i]["Parameter_Name"].ToString();
                pInfo.FieldType = dt.Rows[i]["Data_Type"].ToString();
                if (dt.Rows[i]["Character_Maximum_Length"].ToString() != "")
                {
                    pInfo.FieldWidth = dt.Rows[i]["Character_Maximum_Length"].ToString();
                }
                else if (dt.Rows[i]["Numeric_Precision"].ToString() != "")
                {
                    pInfo.FieldWidth = dt.Rows[i]["Numeric_Precision"].ToString();
                }

                pInfo.Scale = dt.Rows[i]["NumerIc_Scale"].ToString();

            }

            return dbinfo;
        }
        #endregion

        #region 
        public DataBaseInfo GetDataBaseInfoByMyMongoDB(string ConnectionString, List<string> tableNameList)
        {
            DataBaseInfo dbinfo = new DataBaseInfo();

            return dbinfo;
        }
        #endregion

        #endregion

        #region 查询数据库的表
        public DataBaseInfo GetTableInfo(DbLinkInfo info, out string rstmsg)
        {
            rstmsg = "";
            DataBaseInfo dbinfo = new DataBaseInfo();
            string sql = "";
            DataTable dt = new DataTable();

            try
            {
                DataBaseTypeEnum fc = DataBaseType.GetDataBaseType(info.DbType);
                System.Data.Common.DbConnection conn = DBConfig.GetDbConnection(info);
                switch (fc)
                {
                    case DataBaseTypeEnum.SQLServer:
                        dbinfo.DBType = DataBaseTypeEnum.SQLServer;
                        sql = "select name TableName from sysobjects where xtype='U' or xtype='V' order by name";
                        dt = DbHelper.ExecuteTableCurrency(conn, CommandType.Text, sql, null);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TableInfo LastInfo = new TableInfo();
                            string TableName = dt.Rows[i]["TableName"].ToString();
                            LastInfo.Name = TableName;
                            dbinfo.Tables.Add(LastInfo);
                        }
                        break;
                    case DataBaseTypeEnum.Oracle:
                        dbinfo.DBType = DataBaseTypeEnum.MySql;
                        sql = "select * from (select Table_Name from user_tables union all select view_name from user_views) A order by A.Table_Name";

                        dt = DbHelper.ExecuteTableCurrency(conn, CommandType.Text, sql, null);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TableInfo LastInfo = new TableInfo();
                            LastInfo.Name = dt.Rows[i]["Table_Name"].ToString();
                            dbinfo.Tables.Add(LastInfo);
                        }
                        break;
                    case DataBaseTypeEnum.Access:
                        dbinfo = GetDataBaseInfoByAccessTable(conn);
                    
                        break;
                    case DataBaseTypeEnum.SQLite:

                        break;
                    case DataBaseTypeEnum.MySql:
                        dbinfo.DBType = DataBaseTypeEnum.MySql;
                        sql = "select Table_Name,Table_Type from information_schema.tables where table_schema='" + info.DbName + "' order by table_name";
                        dt = DbHelper.ExecuteTableCurrency(conn, CommandType.Text, sql, null);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TableInfo LastInfo = new TableInfo();
                            LastInfo.Name = dt.Rows[i]["Table_Name"].ToString();
                            LastInfo.xType = dt.Rows[i]["Table_Type"].ToString();
                            dbinfo.Tables.Add(LastInfo);
                        }

                        break;
                    //case DataBaseTypeEnum.PDM:

                    //    break;
                    //case DataBaseTypeEnum.MongoDB:

                    //    break;
                }
            }
            catch (Exception ex)
            {
                rstmsg = "操作失败。" + ex.Message;
            }
            return dbinfo;
        }


        public DataBaseInfo GetDataBaseInfoByAccessTable(System.Data.Common.DbConnection dbconn)
        {
            DataBaseInfo dbinfo = new DataBaseInfo();
            System.Data.OleDb.OleDbConnection conn=new System.Data.OleDb.OleDbConnection();
            if (dbconn is System.Data.OleDb.OleDbConnection)
            {
                conn = dbconn as System.Data.OleDb.OleDbConnection;
            }
            else
            {
                return dbinfo;
            }
            dbinfo.DBType = DataBaseTypeEnum.Access;
            dbinfo.Tables.Clear();
            string dbName = conn.DataSource;
            if (dbName != null)
                dbinfo.Name = System.IO.Path.GetFileName(dbName);

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();


                using (System.Data.DataTable dtAccess = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Columns, null))
                {
                    DataView dv = dtAccess.DefaultView;
                    dv.Sort = "TABLE_NAME Asc,ORDINAL_POSITION Asc";
                    DataTable myDataTable = dv.ToTable();

                    foreach (System.Data.DataRow myRow in myDataTable.Rows)
                    {
                        string strTable = Convert.ToString(myRow["TABLE_NAME"]);
                        if (!strTable.StartsWith("MSys"))
                        {
                            TableInfo myTable = dbinfo.Tables[strTable];
                            if (myTable == null)
                            {
                                myTable = new TableInfo();
                                myTable.Name = strTable;
                                dbinfo.Tables.Add(myTable);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return dbinfo;
        }
        
        #endregion

        #region 导入数据
        public int ImportData(DbLinkInfo info, TableInfo tInfo, string field, DataTable dt, out string rstmsg)
        {
            int rst = 0;
            rstmsg = "";
            try
            {
                DataBaseTypeEnum fc = DataBaseType.GetDataBaseType(info.DbType);
                System.Data.Common.DbConnection conn = DBConfig.GetDbConnection(info);
                switch (fc)
                {
                    case DataBaseTypeEnum.SQLServer:
                        rst = ImportDataBySQLserver(conn, tInfo, field, dt, out rstmsg);
                        break;
                    case DataBaseTypeEnum.Oracle:
                        rst = ImportData(conn, tInfo, field, dt, fc, out rstmsg);
                        break;
                    case DataBaseTypeEnum.Access:
                        rst = ImportData(conn, tInfo, field, dt, fc, out rstmsg);
                        break;
                    case DataBaseTypeEnum.SQLite:
                        rst = ImportData(conn, tInfo, field, dt, fc, out rstmsg);
                        break;
                    case DataBaseTypeEnum.MySql:
                        rst = ImportData(conn, tInfo, field, dt, fc, out rstmsg);
                        break;
                    //case DataBaseTypeEnum.PDM:

                    //    break;
                }
            }
            catch (Exception ex)
            {
                rstmsg = "操作失败。" + ex.Message;
            }
            return rst;
        }

        private int ImportDataBySQLserver(DbConnection conn, TableInfo tInfo, string field, DataTable dt, out string rstmsg)
        {
            int rst = 0;
            rstmsg = "";
            try
            {
                System.Data.SqlClient.SqlBulkCopy sqlBulkCopy = new System.Data.SqlClient.SqlBulkCopy(conn.ConnectionString);
                sqlBulkCopy.DestinationTableName = tInfo.Name;
                string[] fieldlist = field.Split(',');
                for (int i = 0; i < fieldlist.Length; i++)
                {
                    string tname1 = dt.Columns[i].ColumnName;
                    string tname2 = fieldlist[i];
                    sqlBulkCopy.ColumnMappings.Add(new System.Data.SqlClient.SqlBulkCopyColumnMapping(tname1, tname2));
                }
                sqlBulkCopy.BatchSize = 500;
                sqlBulkCopy.WriteToServer(dt);
                sqlBulkCopy.Close();
                rst = 1;
                rstmsg = "导入成功。";
            }
            catch (Exception ex)
            {
                rstmsg=ex.Message;
            }
            return rst;
        }

        private int ImportData(DbConnection conn, TableInfo tInfo, string field, DataTable dt,DataBaseTypeEnum dbType, out string rstmsg)
        {
            int rst = 0;
            rstmsg = "";
            string sql = "";
            List<string> listSql = new List<string>();
            string[] fieldItem=field.Split(',');
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string fvalue = "";
                string fvalueAll = "";
                for(int j=0;j<fieldItem.Length;j++)
                {
                    string fname=fieldItem[j];
                    FieldInfo finfo = tInfo.Fields[fname];

                    if (finfo.ValueTypeName.ToLower() == "string")
                    {
                        fvalue = "'" + dr[fname].ToString().Trim() + "'";
                    }
                    else if (finfo.ValueTypeName.ToLower() == "datetime")
                    {
                        if (dr[j].ToString() == "" || dr[fname].ToString().ToLower() == "null")
                            fvalue = "NULL";
                        else
                            fvalue = GetTimeData(dr[fname].ToString().Trim(), dbType);
                    }
                    else
                    {
                        if (dr[j].ToString() == "" || dr[fname].ToString().ToLower() == "null")
                            fvalue = "NULL";
                        else
                            fvalue = dr[fname].ToString();
                    }

                    if (fvalueAll == "")
                    {
                        fvalueAll = fvalue;
                    }
                    else
                    {
                        fvalueAll += "," + fvalue;

                    }
                }
                sql = "insert into " + tInfo.Name + " (" + field + ")" + " values(" + fvalueAll + ")";
                listSql.Add(sql);
            }

            try
            {
                rst = DbHelper.ExecuteNonQueryCurrencyTransactions(conn, CommandType.Text, listSql, null);
            }
            catch (Exception ex)
            {
                rst = -1;
                rstmsg = "导入失败。" + ex.Message;
                return rst;
            }

            if (rst >= 1)
            {
                rstmsg = "总共" + dt.Rows.Count + "行数据，导入成功" + rst + "行数据。";
            }
            return rst;
        }


        #endregion

        #region 更新字段数据
        public List<string> GetUpdateSql(DbLinkInfo info, TableInfo tInfo, string field, string keyField, string sqlWhere, DataTable dt, out string rstmsg)
        {
            int rst = 0;
            rstmsg = "";
            try
            {
                DataBaseTypeEnum fc = DataBaseType.GetDataBaseType(info.DbType);
                System.Data.Common.DbConnection conn = DBConfig.GetDbConnection(info);
                string sql = "";
                List<string> listSql = new List<string>();
                string[] fieldItem = field.Split(',');
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string fvalue = "";
                    string fvalueAll = "";
                    string fvalueWhere = "";
                    for (int j = 0; j < fieldItem.Length; j++)
                    {
                        string fname = fieldItem[j];
                        FieldInfo finfo = tInfo.Fields[fname];

                        if (finfo.ValueTypeName.ToLower() == "string")
                        {
                            fvalue = fname + "='" + dr[fname].ToString().Trim() + "'";
                        }
                        else if (finfo.ValueTypeName.ToLower() == "datetime")
                        {
                            if (dr[fname].ToString() == "" || dr[fname].ToString().ToLower() == "null")
                                fvalue = fname + "=NULL";
                            else
                                fvalue = fname + "='" + dr[fname].ToString().Trim() + "'";
                        }
                        else
                        {
                            if (dr[fname].ToString() == "" || dr[fname].ToString().ToLower() == "null")
                                fvalue = fname + "=NULL";
                            else
                                fvalue = fname + "=" + dr[fname].ToString();
                        }

                        if (fname == keyField)
                        {
                            fvalueWhere = fvalue;
                        }
                        else
                        {
                            if (fvalueAll == "")
                            {
                                fvalueAll = fvalue;
                            }
                            else
                            {
                                fvalueAll += "," + fvalue;

                            }
                        }
                    }
                    sql = "update " + tInfo.Name + " set " + fvalueAll + " where " + fvalueWhere + (string.IsNullOrEmpty(sqlWhere) ? "" : " and " + sqlWhere);
                    listSql.Add(sql);
                }
                //string strData = "";
                //foreach (var item in listSql)
                //{
                //    if (!string.IsNullOrEmpty(strData)) strData = strData + ";" + Environment.NewLine;
                //    strData = strData + item;
                //}
                return listSql;
            }
            catch (Exception ex)
            {
                rstmsg = "操作失败。" + ex.Message;
            }
            return null;
        }
        public int UpdateFieldData(DbLinkInfo info, TableInfo tInfo, string field, string keyField,string sqlWhere, DataTable dt, out string rstmsg)
        {
            int rst = 0;
            rstmsg = "";
            try
            {
                DataBaseTypeEnum fc = DataBaseType.GetDataBaseType(info.DbType);
                System.Data.Common.DbConnection conn = DBConfig.GetDbConnection(info);
                var listSql = GetUpdateSql(info, tInfo, field, keyField, sqlWhere, dt, out rstmsg);

                try
                {
                    rst = DbHelper.ExecuteNonQueryCurrencyTransactions(conn, CommandType.Text, listSql, null);
                }
                catch (Exception ex)
                {
                    rst = -1;
                    rstmsg = "更新失败。" + ex.Message;
                    return rst;
                }

                if (rst >= 1)
                {
                    rstmsg = "总共" + dt.Rows.Count + "行数据，更新成功" + rst + "行数据。";
                }

            }
            catch (Exception ex)
            {
                rstmsg = "操作失败。" + ex.Message;
            }
            return rst;
        }

        #endregion

        private string GetTimeData(string variable, DataBaseTypeEnum dbType)
        {
            variable = DateTime.Parse(variable).ToString("yyyy-MM-dd hh:mm:ss");
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
                    variable = "date_format('" + variable + "', '%Y-%m-%d %H:%I:%S')";
                    break;
            }
            return variable;
        }

    }
}
