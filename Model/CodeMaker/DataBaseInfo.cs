using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public class DataBaseInfo
    {
        /// <summary>
        /// 无作为的初始化对象
        /// </summary>
        public DataBaseInfo()
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

        #region 表
        private TableInfoCollection myTables = new TableInfoCollection();
        /// <summary>
        /// 数据库表信息列表
        /// </summary>
        public TableInfoCollection Tables
        {
            set { myTables = value; }
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
        #endregion

        #region 视图
        private ViewInfoCollection _view = new ViewInfoCollection();
        /// <summary>
        /// 数据库视图信息列表
        /// </summary>
        public ViewInfoCollection View
        {
            set { _view = value; }
            get { return _view; }
        }

        /// <summary>
        /// 数据视图信息列表类型
        /// </summary>
        public class ViewInfoCollection : System.Collections.CollectionBase
        {
            /// <summary>
            /// 返回指定序号的视图信息对象
            /// </summary>
            public TableInfo this[int index]
            {
                get { return (TableInfo)this.List[index]; }
            }
            /// <summary>
            /// 返回指定名称的视图信息对象
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
            /// 向列表添加视图对象
            /// </summary>
            /// <param name="table">视图对象</param>
            /// <returns>新增对象在视图中的序号</returns>
            public int Add(TableInfo table)
            {
                return this.List.Add(table);
            }
            public void Remove(TableInfo table)
            {
                this.List.Remove(table);
            }
        }
        #endregion

        #region 存储过程
        private ProcedureInfoCollection _procedure = new ProcedureInfoCollection();
        /// <summary>
        /// 数据库存储过程信息列表
        /// </summary>
        public ProcedureInfoCollection Procedure
        {
            set { _procedure = value; }
            get { return _procedure; }
        }

        /// <summary>
        /// 数据存储过程类型
        /// </summary>
        public class ProcedureInfoCollection : System.Collections.CollectionBase
        {
            /// <summary>
            /// 返回指定序号的存储过程对象
            /// </summary>
            public TableInfo this[int index]
            {
                get { return (TableInfo)this.List[index]; }
            }
            /// <summary>
            /// 返回指定名称的存储过程对象
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
            /// 向列表添加存储过程对象
            /// </summary>
            /// <param name="table">存储过程对象</param>
            /// <returns>新增对象在存储过程中的序号</returns>
            public int Add(TableInfo table)
            {
                return this.List.Add(table);
            }
            public void Remove(TableInfo table)
            {
                this.List.Remove(table);
            }
        }
        #endregion

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
        /// 对象填充样式
        /// </summary>
        protected DataBaseTypeEnum _dbtype = DataBaseTypeEnum.None;
        /// <summary>
        /// 对象填充样式
        /// </summary>
        public DataBaseTypeEnum DBType
        {
            get { return _dbtype; }
            set { _dbtype = value; }
        }
    }
}
