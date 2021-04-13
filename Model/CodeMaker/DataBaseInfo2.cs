using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.OleDb;

namespace Model.CodeMaker
{
    /// <summary>
    /// 分析数据库表结构的对象
    /// </summary>
    /// <remarks>
    /// 本对象能分析Access2000,Oracle,SQLServer 的数据库,并加载其表结构定义.
    /// 也可从PDM文件中加载表结构定义
    /// </remarks>
    [System.Serializable()]
    public class DataBaseInfo2 : System.ICloneable
    {
        /// <summary>
        /// 无作为的初始化对象
        /// </summary>
        public DataBaseInfo2()
        {
        }

        private string strName = null;
        /// <summary>
        /// 对象名称
        /// </summary>
        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }
        private string strDescription = null;
        /// <summary>
        /// 对象说明
        /// </summary>
        public string Description
        {
            get { return strDescription; }
            set { strDescription = value; }
        }
        /// <summary>
        /// 总共包含的字段个数
        /// </summary>
        public int FieldCount
        {
            get
            {
                int iCount = 0;
                foreach (TableInfo table in myTables)
                {
                    iCount += table.Fields.Count;
                }
                return iCount;
            }
        }

        private TableInfoCollection myTables = new TableInfoCollection();
        /// <summary>
        /// 数据库表信息列表
        /// </summary>
        public TableInfoCollection Tables
        {
            get { return myTables; }
        }

        /// <summary>
        /// 数据表信息列表类型
        /// </summary>
        public class TableInfoCollection : System.Collections.CollectionBase
        {
            /// <summary>
            /// 返回指定序号的表信息对象
            /// </summary>
            public TableInfo this[int index]
            {
                get { return (TableInfo)this.List[index]; }
            }
            /// <summary>
            /// 返回指定名称的表信息对象
            /// </summary>
            public TableInfo this[string strTableName]
            {
                get
                {
                    foreach (TableInfo t in this)
                    {
                        if (string.Compare(t.Name, strTableName, true) == 0)
                            return t;
                    }
                    return null;
                }
            }
            /// <summary>
            /// 向列表添加表对象
            /// </summary>
            /// <param name="table">表对象</param>
            /// <returns>新增对象在列表中的序号</returns>
            public int Add(TableInfo table)
            {
                return this.List.Add(table);
            }
            public void Remove(TableInfo table)
            {
                this.List.Remove(table);
            }
        }

        /// <summary>
        /// 获得指定表名和字段名的字段对象
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="FieldName">字段名</param>
        /// <returns>获得的字段对象,若未找到则返回空引用</returns>
        public FieldInfo GetField(string TableName, string FieldName)
        {
            TableInfo table = myTables[TableName];
            if (table != null)
                return table.Fields[FieldName];
            return null;
        }

        /// <summary>
        /// 获得指定全名称的字段对象
        /// </summary>
        /// <param name="FullName">字段名称,格式为 表名.字段名</param>
        /// <returns>获得的字段对象,若为找到怎返回空引用</returns>
        public FieldInfo GetField(string FullName)
        {
            if (FullName == null)
                return null;
            int index = FullName.IndexOf(".");
            if (index <= 0)
                return null;
            return GetField(
                FullName.Substring(0, index).Trim(),
                FullName.Substring(index + 1).Trim());
        }


        /// <summary>
        /// 对象填充类型
        /// </summary>
        public enum FillStyleConst
        {
            /// <summary>
            /// 无样式
            /// </summary>
            None,
            /// <summary>
            /// 从SQLSERVER数据库填充对象
            /// </summary>
            SQLServer,
            /// <summary>
            /// 从ORACLE数据库填充对象
            /// </summary>
            Oracle,
            /// <summary>
            /// 从Access2000数据库填充对象
            /// </summary>
            Access2000,
            /// <summary>
            /// 从PDM文件填充对象 PowerDesigner
            /// </summary>
            PDM

            //			/// <summary>
            //			/// 从XML文档填充对象
            //			/// </summary>
            //			XMLDocument
        }

        /// <summary>
        /// 对象填充样式
        /// </summary>
        protected FillStyleConst intFillStyle = FillStyleConst.None;
        /// <summary>
        /// 对象填充样式
        /// </summary>
        public FillStyleConst FillStyle
        {
            get { return intFillStyle; }
            set { intFillStyle = value; }
        }

        #region 从PDM文档加载对象数据 *****************************************

        /// <summary>
        /// 从一个PDM数据结构定义文件中加载数据结构信息 PowerDesigner
        /// </summary>
        /// <param name="strFileName">PDM文件名</param>
        /// <returns>加载的字段信息个数</returns>
        public int LoadFromPDMXMLFile(string strFileName)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(strFileName);
            return LoadFromPDMXMLDocument(doc);
        }
        /// <summary>
        /// 从PDM数据结构定义XML文件中加载数据结构信息
        /// </summary>
        /// <param name="doc">XML文档对象</param>
        /// <returns>加载的字段信息个数</returns>
        public int LoadFromPDMXMLDocument(XmlDocument doc)
        {
            intFillStyle = FillStyleConst.PDM;
            int RecordCount = 0;
            myTables.Clear();
            XmlNamespaceManager nsm = new XmlNamespaceManager(doc.NameTable);
            nsm.AddNamespace("a", "attribute");
            nsm.AddNamespace("c", "collection");
            nsm.AddNamespace("o", "object");
            XmlNode RootNode = doc.SelectSingleNode("/Model/o:RootObject/c:Children/o:Model", nsm);
            if (RootNode == null)
                return 0;
            strName = ReadXMLValue(RootNode, "a:Name", nsm);
            strDescription = strName;
            // 数据表
            foreach (XmlNode TableNode in RootNode.SelectNodes("c:Tables/o:Table", nsm))
            {
                TableInfo table = new TableInfo();
                myTables.Add(table);
                table.Name = ReadXMLValue(TableNode, "a:Code", nsm);
                table.Remark = ReadXMLValue(TableNode, "a:Name", nsm);
                string keyid = ReadXMLValue(TableNode, "c:PrimaryKey/o:Key/@Ref", nsm);
                System.Collections.Specialized.StringCollection Keys =
                    new System.Collections.Specialized.StringCollection();
                if (keyid != null)
                {
                    foreach (XmlNode KeyNode in TableNode.SelectNodes(
                        "c:Keys/o:Key[@Id = '" + keyid + "']/c:Key.Columns/o:Column/@Ref", nsm))
                    {
                        Keys.Add(KeyNode.Value);
                    }
                }
                foreach (XmlNode FieldNode in TableNode.SelectNodes("c:Columns/o:Column", nsm))
                {
                    RecordCount++;
                    string id = ((XmlElement)FieldNode).GetAttribute("Id");
                    FieldInfo field = new FieldInfo();
                    table.Fields.Add(field);
                    field.Name = ReadXMLValue(FieldNode, "a:Code", nsm);
                    field.Remark = ReadXMLValue(FieldNode, "a:Name", nsm);
                    field.Description = ReadXMLValue(FieldNode, "a:Comment", nsm);
                    string FieldType = ReadXMLValue(FieldNode, "a:DataType", nsm);
                    if (FieldType != null)
                    {
                        int index = FieldType.IndexOf("(");
                        if (index > 0)
                            FieldType = FieldType.Substring(0, index);
                    }
                    field.FieldType = FieldType;

                    field.FieldWidth = ReadXMLValue(FieldNode, "a:Length", nsm);
                    if (Keys.Contains(id))
                        field.PrimaryKey = true;
                }
            }
            return RecordCount;
        }

        private string ReadXMLValue(
            System.Xml.XmlNode node,
            string path,
            System.Xml.XmlNamespaceManager nsm)
        {
            System.Xml.XmlNode node2 = node.SelectSingleNode(path, nsm);
            if (node2 == null)
                return null;
            else
            {
                if (node2 is System.Xml.XmlElement)
                    return ((System.Xml.XmlElement)node2).InnerText;
                else
                    return node2.Value;
            }
        }

        #endregion

        #region 分析数据库加载对象数据 ****************************************

        /// <summary>
        /// 从指定名称的Access2000数据库中加载数据库结构信息
        /// </summary>
        /// <param name="strFileName">数据库文件名</param>
        /// <returns>加载的字段信息个数</returns>
        public int LoadFromAccess2000(string strFileName)
        {
            using (OleDbConnection conn = new OleDbConnection())
            {
                conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFileName;
                int result = LoadFromAccess(conn);
                return result;
            }
        }

        /// <summary>
        /// 从 Jet40( Access2000 ) 的数据库中加载数据结构信息
        /// </summary>
        /// <param name="myConn">数据库连接对象</param>
        /// <returns>加载的字段信息个数</returns>
        public int LoadFromAccess(OleDbConnection myConn)
        {
            intFillStyle = FillStyleConst.Access2000;
            int RecordCount = 0;
            myTables.Clear();
            string dbName = myConn.DataSource;
            if (dbName != null)
                strName = System.IO.Path.GetFileName(dbName);

            try
            {
                if (myConn.State != ConnectionState.Open)
                    myConn.Open();

                using (System.Data.DataTable myDataTable =
                          myConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Columns, null))
                {
                    
                    foreach (System.Data.DataRow myRow in myDataTable.Rows)
                    {
                        string strTable = Convert.ToString(myRow["TABLE_NAME"]);
                        if (!strTable.StartsWith("MSys"))
                        {
                            TableInfo myTable = myTables[strTable];
                            if (myTable == null)
                            {
                                myTable = new TableInfo();
                                myTable.Name = strTable;
                                myTables.Add(myTable);
                            }
                            FieldInfo myField = new FieldInfo();
                            myTable.Fields.Add(myField);
                            myField.Name = Convert.ToString(myRow["COLUMN_NAME"]);
                            myField.Nullable = Convert.ToBoolean(myRow["IS_NULLABLE"]);
                            System.Data.OleDb.OleDbType intType = (System.Data.OleDb.OleDbType)
                                Convert.ToInt32(myRow["DATA_TYPE"]);
                            if (System.DBNull.Value.Equals(myRow["DESCRIPTION"]) == false)
                            {
                                myField.Remark = Convert.ToString(myRow["DESCRIPTION"]);
                            }
                            if (intType == System.Data.OleDb.OleDbType.WChar)
                            {
                                myField.FieldType = "Char";
                            }
                            else
                            {
                                myField.FieldType = intType.ToString();
                            }
                            myField.FieldWidth = Convert.ToString(myRow["CHARACTER_MAXIMUM_LENGTH"]);
                            RecordCount++;
                        }
                    }//foreach
                }//using
                using (System.Data.DataTable myDataTable =
                           myConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Indexes, null))
                {
                    foreach (System.Data.DataRow myRow in myDataTable.Rows)
                    {
                        string strTable = Convert.ToString(myRow["TABLE_NAME"]);
                        TableInfo myTable = myTables[strTable];
                        if (myTable != null)
                        {
                            FieldInfo myField = myTable.Fields[Convert.ToString(myRow["COLUMN_NAME"])];
                            if (myField != null)
                            {
                                myField.Indexed = true;
                                myField.PrimaryKey = (Convert.ToBoolean(myRow["PRIMARY_KEY"]));
                            }
                        }
                    }//foreach
                }//using
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
                myConn.Dispose();
            }
            return RecordCount;
        }//public int LoadFromAccess2000( OleDbConnection myConn )

        /// <summary>
        /// 从 Oracle 加载数据库结构信息
        /// </summary>
        /// <param name="myConn">数据库连接对象</param>
        /// <returns>加载的字段信息个数</returns>
        public int LoadFromOracle(IDbConnection myConn)
        {
            intFillStyle = FillStyleConst.Oracle;
            int RecordCount = 0;
            string strSQL = null;
            strSQL = "Select TName,CName,coltype,width  From Col Order by TName,CName";
            myTables.Clear();
            if (myConn is OleDbConnection)
            {
                strName = ((System.Data.OleDb.OleDbConnection)myConn).DataSource
                    + " - " + myConn.Database;
            }
            else
                strName = myConn.Database;

            try
            {
                using (System.Data.IDbCommand myCmd = myConn.CreateCommand())
                {
                    if (myConn.State != ConnectionState.Open)
                        myConn.Open();
                    myCmd.CommandText = strSQL;
                    IDataReader myReader = myCmd.ExecuteReader(CommandBehavior.SingleResult);
                    TableInfo LastTable = null;
                    while (myReader.Read())
                    {
                        string TableName = myReader.GetString(0).Trim();
                        if (LastTable == null || LastTable.Name != TableName)
                        {
                            LastTable = new TableInfo();
                            myTables.Add(LastTable);
                            LastTable.Name = TableName;
                        }
                        FieldInfo NewField = new FieldInfo();
                        LastTable.Fields.Add(NewField);
                        NewField.Name = myReader.GetString(1);
                        NewField.FieldType = myReader.GetString(2);
                        NewField.FieldWidth = myReader[3].ToString();
                        RecordCount++;
                    }//while
                    myReader.Close();

                    myCmd.CommandText = @"
select table_name , 
	column_name , 
	index_name 
from user_ind_columns 
order by table_name , column_name ";
                    myReader = myCmd.ExecuteReader(CommandBehavior.SingleResult);
                    TableInfo myTable = null;
                    while (myReader.Read())
                    {
                        myTable = myTables[myReader.GetString(0)];
                        if (myTable != null)
                        {
                            string IDName = myReader.GetString(2);
                            string FieldName = myReader.GetString(1);
                            FieldInfo myField = myTable.Fields[FieldName];
                            if (myField != null)
                            {
                                myField.Indexed = true;
                                if (IDName.StartsWith("PK"))
                                {
                                    myField.PrimaryKey = true;
                                }
                            }
                        }
                    }//while
                    myReader.Close();
                }//using
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
                myConn.Dispose();
            }
            return RecordCount;
        }//public int LoadFromOracle( System.Data.IDbConnection myConn )

        /// <summary>
        /// 从 Oracle 加载数据库结构信息
        /// </summary>
        /// <param name="myConn">数据库连接对象</param>
        /// <returns>加载的字段信息个数</returns>
        public int GetOracleDb(IDbConnection myConn)
        {
            intFillStyle = FillStyleConst.Oracle;
            int RecordCount = 0;
            string strSQL = null;
            strSQL = "Select TName,CName,coltype,width  From Col Order by TName,CName";
            myTables.Clear();
            if (myConn is Devart.Data.Oracle.OracleConnection)
            {
                strName = ((Devart.Data.Oracle.OracleConnection)myConn).DataSource
                    + "" + myConn.Database;
            }
            else
                strName = myConn.Database;

            try
            {
                using (System.Data.IDbCommand myCmd = myConn.CreateCommand())
                {
                    if (myConn.State != ConnectionState.Open)
                        myConn.Open();
                    myCmd.CommandText = strSQL;                                        
                    IDataReader myReader = myCmd.ExecuteReader(CommandBehavior.SingleResult);
                    TableInfo LastTable = null;
                    while (myReader.Read())
                    {
                        string TableName = myReader.GetString(0).Trim();
                        if (LastTable == null || LastTable.Name != TableName)
                        {
                            LastTable = new TableInfo();
                            myTables.Add(LastTable);
                            LastTable.Name = TableName;
                        }
                        FieldInfo NewField = new FieldInfo();
                        LastTable.Fields.Add(NewField);
                        NewField.Name = myReader.GetString(1);
                        NewField.FieldType = myReader.GetString(2);
                        NewField.FieldWidth = myReader[3].ToString();
                        RecordCount++;
                    }//while
                    myReader.Close();

                    myCmd.CommandText = @"
select table_name , 
	column_name , 
	index_name 
from user_ind_columns 
order by table_name , column_name ";
                    myReader = myCmd.ExecuteReader(CommandBehavior.SingleResult);
                    TableInfo myTable = null;
                    while (myReader.Read())
                    {
                        myTable = myTables[myReader.GetString(0)];
                        if (myTable != null)
                        {
                            string IDName = myReader.GetString(2);
                            string FieldName = myReader.GetString(1);
                            FieldInfo myField = myTable.Fields[FieldName];
                            if (myField != null)
                            {
                                myField.Indexed = true;
                                if (IDName.StartsWith("PK"))
                                {
                                    myField.PrimaryKey = true;
                                }
                            }
                        }
                    }//while
                    myReader.Close();
                }//using
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
                myConn.Dispose();
            }
            return RecordCount;
        }//public int LoadFromOracle( System.Data.IDbConnection myConn )

        /// <summary>
        /// 从 SQLServer 中加载数据库结构信息
        /// </summary>
        /// <param name="myConn">数据库连接对象</param>
        /// <returns>加载的字段信息个数</returns>
        public int LoadFromSQLServer(IDbConnection myConn)
        {
            intFillStyle = FillStyleConst.SQLServer;
            int RecordCount = 0;

            if (myConn is OleDbConnection)
                strName = ((OleDbConnection)myConn).DataSource;
            else if (myConn is System.Data.SqlClient.SqlConnection)
                strName = ((System.Data.SqlClient.SqlConnection)myConn).DataSource;
            strName = strName + " - " + myConn.Database;

            string strSQL = null;
            strSQL = @"
select
	sysobjects.name ,
	syscolumns.name  ,
	systypes.name ,
	syscolumns.length , 
	syscolumns.isnullable ,
	sysobjects.type
from 
	syscolumns,
	sysobjects,
	systypes 
where 
	syscolumns.id=sysobjects.id 
	and syscolumns.xusertype=systypes.xusertype 
	and (sysobjects.type='U' or sysobjects.type='V' )
	and systypes.name <>'_default_' 
	and systypes.name<>'sysname' 
order by 
	sysobjects.name,
	syscolumns.name";
            myTables.Clear();
            try
            {
                using (System.Data.IDbCommand myCmd = myConn.CreateCommand())
                {
                    if (myConn.State != ConnectionState.Open)
                        myConn.Open();
                    myCmd.CommandText = strSQL;
                    IDataReader myReader = myCmd.ExecuteReader(CommandBehavior.SingleResult);
                    TableInfo LastTable = null;
                    while (myReader.Read())
                    {
                        string TableName = myReader.GetString(0).Trim();
                        if (LastTable == null || LastTable.Name != TableName)
                        {
                            LastTable = new TableInfo();
                            myTables.Add(LastTable);
                            LastTable.Name = TableName;
                            LastTable.Tag = Convert.ToString(myReader.GetValue(5));
                        }
                        FieldInfo NewField = new FieldInfo();
                        LastTable.Fields.Add(NewField);
                        NewField.Name = myReader.GetString(1);
                        NewField.FieldType = myReader.GetString(2);
                        NewField.FieldWidth = myReader[3].ToString();
                        if (myReader.IsDBNull(4) == false)
                            NewField.Nullable = (myReader.GetInt32(4) == 1);
                        RecordCount++;
                    }//while
                    myReader.Close();
                    // 加载主键信息
                    for (int iCount = myTables.Count - 1; iCount >= 0; iCount--)
                    {
                        TableInfo myTable = myTables[iCount];

                        int aa = string.Compare((string)myTable.Tag, "U", true);
                        if (string.Compare((string)myTable.Tag, "U", true) == 1)
                        {
                            try
                            {
                                myCmd.CommandText = "sp_helpindex \"" + myTable.Name + "\"";
                                //myCmd.CommandType = System.Data.CommandType.Text ;
                                myReader = myCmd.ExecuteReader();
                                while (myReader.Read())
                                {
                                    string strKeyName = myReader.GetString(0);
                                    string strDesc = myReader.GetString(1);
                                    string strFields = myReader.GetString(2);
                                    bool bolPrimary = (strDesc.ToLower().IndexOf("primary") >= 0);
                                    foreach (string strField in strFields.Split(','))
                                    {
                                        FieldInfo myField = myTable.Fields[strField.Trim()];
                                        if (myField != null)
                                        {
                                            myField.Indexed = true;
                                            myField.PrimaryKey = bolPrimary;
                                        }
                                    }//foreach
                                }//while
                                myReader.Close();
                            }
                            catch (Exception ext)
                            {
                                //this.List.Remove( myTable );
                                myTable.Name = myTable.Name + " " + ext.Message;
                            }
                        }
                    }//foreach
                }//using
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
                myConn.Dispose();
            }

            return RecordCount;
        }//public int LoadFromSQLServer( System.Data.IDbConnection myConn )

        #endregion

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public object Clone()
        {
            DataBaseInfo2 info = new DataBaseInfo2();
            info.intFillStyle = this.intFillStyle;
            info.strDescription = this.strDescription;
            info.Name = this.strName;
            foreach (TableInfo table in myTables)
            {
                TableInfo NewTable = new TableInfo();
                NewTable.Name = table.Name;
                NewTable.Remark = table.Remark;
                NewTable.Description = table.Description;
                NewTable.Tag = table.Tag;
                info.myTables.Add(NewTable);
                foreach (FieldInfo field in table.Fields)
                {
                    FieldInfo NewField = new FieldInfo();
                    NewField.Name = field.Name;
                    NewField.Remark = field.Remark;
                    NewField.Description = field.Description;
                    NewField.FieldType = field.FieldType;
                    NewField.FieldWidth = field.FieldWidth;
                    NewField.Indexed = field.Indexed;
                    NewField.Nullable = field.Nullable;
                    NewField.PrimaryKey = field.PrimaryKey;
                    NewTable.Fields.Add(NewField);
                }
            }
            return info;
        }
    }

}
