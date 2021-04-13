using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CodeMaker
{
    public class DbLinkInfo
    {
        private int _id;
        private string _dbname;
        private string _dbabbreviation;
        private string _username;
        private string _password;
        private int _dbtype;
        private string _datasource;
        private DateTime _createTime;
        private string _port;
        private string _dbtypename;
        private string _charset;
        /// <summary>
        /// 序号
        /// </summary>
        public int ID
        {
            set{_id =value;}
            get{return _id;}
        }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DbName
        {
            set{ _dbname=value;}
            get{return _dbname;}
        }
        /// <summary>
        /// 数据库简称
        /// </summary>
        public string DbAbbreviation
        {
            set { _dbabbreviation = value; }
            get { return _dbabbreviation; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set{_username=value;}
            get{return _username;}
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            set{_password=value;}
            get{return _password;}
        }
        /// <summary>
        /// 数据库类型 1 Sqlerver, 2 Oracle, 3 Access,
        /// </summary>
        public int DbType
        {
            set{_dbtype=value;}
            get{return _dbtype;}
        }
        /// <summary>
        /// 连接串
        /// </summary>
        public string DataSource
        {
            set{_datasource=value;}
            get{return _datasource;}
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createTime = value; }
            get { return _createTime; }
        }
        /// <summary>
        /// 端口
        /// </summary>
        public string Port
        {
            set { _port = value; }
            get { return _port; }
        }
        /// <summary>
        /// 数据库类型名称 1 Sqlerver 2 Oracle 3 Access
        /// </summary>
        public string DbTypeName
        {
            set { _dbtypename = value; }
            get { return _dbtypename; }
        }
        /// <summary>
        /// 字符集
        /// </summary>
        public string Charset
        {
            set { _charset = value; }
            get { return _charset; }
        }
        
    }
}
