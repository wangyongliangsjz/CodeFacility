using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CodeMaker
{
    /// <summary>
    /// 菜单节点数据
    /// </summary>
    public class DbDataInfo
    {
        private int _dblinkid;
        private string _dbname;
        private string _table;
        private string _view;
        private string _procedure;
        private string _name;
        private int _nametype;

        /// <summary>
        /// DbLink表序号
        /// </summary>
        public int DbLinkID
        {
            set { _dblinkid = value; }
            get { return _dblinkid; }
        }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DbName
        {
            set { _dbname = value; }
            get { return _dbname; }
        }
        /// <summary>
        /// 表
        /// </summary>
        public string Table
        {
            set { _table = value; }
            get { return _table; }
        }
        /// <summary>
        /// 视图
        /// </summary>
        public string View
        {
            set { _view = value; }
            get { return _view; }
        }
        /// <summary>
        /// 存储过程
        /// </summary>
        public string Procedure
        {
            set { _procedure = value; }
            get { return _procedure; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 名称类型
        /// </summary>
        public int NameType
        {
            set { _nametype = value; }
            get { return _nametype; }
        }
    }
}
