using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using Devart.Data.Oracle;
using System.Data.SQLite;
using MySql.Data.MySqlClient;

namespace DALProfile
{
    /// <summary>
    ///功能描述：支持远程跨处理应用程序域边界访问对象的查询参数，支持克隆
    /// </summary>
    public sealed class QueryParameter : MarshalByRefObject, IDbDataParameter, ICloneable
    {
        #region 定义私有变量

        // 查询的参数类型
        private ParameterDirection _direction;
        // 是否受影响
        private bool _forceSize;
        private bool _isNullable;
        private string _name;
        private int _offset;
        private byte _precision;
        private byte _scale;
        private int _size;
        private string _sourceColumn;
        private bool _suppress;
        private object _value;
        private DataRowVersion _version;
        private DbType _dbType;
        private IDbDataParameter _realParameter;
        private string _parameterType;

        

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryParameter()
        {
            _value = null;
            _direction = ParameterDirection.Input;
            _size = -1;
            _version = DataRowVersion.Current;
            _forceSize = false;
            _offset = 0;
            _suppress = false;
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="Value">参数值</param>
        public QueryParameter(string parameterName, object Value)
            : this()
        {
            _name = parameterName;
            _value = Value;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="Value">参数值</param>
        public QueryParameter(string parameterName, object Value, string ParameterType)
            : this()
        {
            _name = parameterName;
            _value = Value;
            _parameterType = ParameterType;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="Value">参数值</param>
        /// <param name="dbType">参数类型</param>
        public QueryParameter(string parameterName, object Value, DbType dbType)
            : this()
        {
            _name = parameterName;
            _dbType = dbType;
            _value = Value;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">参数类型</param>
        public QueryParameter(string parameterName, DbType dbType)
            : this()
        {
            _name = parameterName;
            _dbType = dbType;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size">大小</param>
        public QueryParameter(string parameterName, DbType dbType, int size)
            : this()
        {
            _name = parameterName;
            _dbType = dbType;
            _size = size;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size">大小</param>
        /// <param name="sourceColumn">源字段列</param>
        public QueryParameter(string parameterName, DbType dbType, int size, string sourceColumn)
            : this()
        {
            _name = parameterName;
            _dbType = dbType;
            _size = size;
            _sourceColumn = sourceColumn;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size">大小</param>
        /// <param name="direction">DataSet的参数类型</param>
        /// <param name="isNullable">是否为空</param>
        /// <param name="precision">精确度</param>
        /// <param name="scale">小数位数</param>
        /// <param name="sourceColumn">源字段列</param>
        /// <param name="sourceVersion">DataRow版本</param>
        /// <param name="Value"></param>
        public QueryParameter(string parameterName, DbType dbType, int size, ParameterDirection direction,
                              bool isNullable, byte precision, byte scale, string sourceColumn,
                              DataRowVersion sourceVersion, object Value)
            : this()
        {
            _name = parameterName;
            _dbType = dbType;
            _size = size;
            _direction = direction;
            _isNullable = isNullable;
            _precision = precision;
            _scale = scale;
            _sourceColumn = sourceColumn;
            _version = sourceVersion;
            _value = Value;
        }

        public QueryParameter(string parameterName, DbType dbType, ParameterDirection direction, object Value)
        {
            _name = parameterName;
            _dbType = dbType;
            _direction = direction;
            //_value = Value;
        }
        #endregion

        /// <summary>
        /// 要扩展或修改继承的方法、属性、索引器或事件的抽象实现或虚实现
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ParameterName;
        }

        /// <summary>
        /// 表示参数名
        /// </summary>
        public string ParameterName
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 包括实参的值
        /// </summary>
        public object Value
        {
            get
            {
                if (Object.Equals(_value, null)) // 比较两个值是否相等
                    return DBNull.Value;
                else
                    return _value;
            }
            set { _value = value; }
        }



        public byte Precision
        {
            get { return _precision; }
            set { _precision = value; }

        }


        public int Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }



        public byte Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }


        public int Size
        {
            get
            {
                if (_forceSize)
                {
                    return _size;
                }
                return 0;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception(value.ToString());
                }
                if (value != 0)
                {
                    _forceSize = true;
                    _size = value;
                    return;
                }
                _forceSize = false;
                _size = -1;
            }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示参数是只可输入、只可输出、双向还是存储过程返回值参数
        /// </summary>
        public ParameterDirection Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        /// <summary>
        /// 表示该参数的数据库类型，它的值是DbType枚举中的元素
        /// </summary>
        public DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        /// <summary>
        /// 表示该参数是否可以包含空值
        /// </summary>
        public bool IsNullable
        {
            get { return _isNullable; }
            set { _isNullable = value; }
        }

        /// <summary>
        /// 在获取参数值时表示DataRowVersion的版本
        /// </summary>
        public DataRowVersion SourceVersion
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// 表示映射到参数值的数据源的列，用于行的更新
        /// </summary>
        public string SourceColumn
        {
            get
            {
                if (_sourceColumn == null)
                {
                    return string.Empty;
                }
                return _sourceColumn;
            }
            set
            {
                _sourceColumn = value;
            }
        }

        internal void InitRealParameter()
        {
            if (Object.Equals(_realParameter, null))
            {
                if (_parameterType == "" || _parameterType == "SqlParameter")
                {
                    _realParameter = new SqlParameter();
                }
                else if (_parameterType == "OleDbParameter")
                {
                    _realParameter = new OleDbParameter();
                }
                else if (_parameterType == "OracleParameter")
                {
                    _realParameter = new OracleParameter();
                }
                else if (_parameterType == "SQLiteParameter")
                {
                    _realParameter = new SQLiteParameter();
                }
                else if (_parameterType == "MySqlParameter")
                {
                    _realParameter = new MySqlParameter();
                }              
                
            }

            RealParameter.DbType = DbType;
            RealParameter.Direction = Direction;
            RealParameter.ParameterName = ParameterName;
            RealParameter.Precision = Precision;
            RealParameter.Scale = Scale;
            RealParameter.Size = Size;
            RealParameter.SourceColumn = SourceColumn;
            RealParameter.SourceVersion = SourceVersion;
            RealParameter.Value = Value;
        }

        internal bool Suppress
        {
            get { return _suppress; }
            set { _suppress = value; }
        }


        /// <summary>
        /// 用来向 Command 对象表示一个参数，以及向该对象的 DataSet 列映射表示参数
        /// </summary>
        internal IDbDataParameter RealParameter
        {
            get { return _realParameter; }

        }

        /// <summary>
        /// 同步参数集合
        /// 类或结构定义可以添加 internal 关键字，使其访问级别成为显式的
        /// 只有在同一程序集的文件中，内部类型或成员才是可访问的
        /// </summary>
        internal void SyncParameter()
        {
            if (Object.Equals(_realParameter, null))
                return;
            SetProperties(RealParameter.ParameterName, RealParameter.SourceColumn, RealParameter.SourceVersion, RealParameter.Precision,

                RealParameter.Scale, RealParameter.Size, _forceSize, _offset, RealParameter.Direction, RealParameter.Value,

                RealParameter.DbType, Suppress);
        }

        /// <summary>
        /// 设置参数属性
        /// </summary>
        /// <param name="name"></param>
        /// <param name="column"></param>
        /// <param name="version"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <param name="size"></param>
        /// <param name="forceSize"></param>
        /// <param name="offset"></param>
        /// <param name="direction"></param>
        /// <param name="Value"></param>
        /// <param name="type"></param>
        /// <param name="suppress"></param>
        internal void SetProperties(string name, string column, DataRowVersion version, byte precision, byte scale,

            int size, bool forceSize, int offset, ParameterDirection direction, object Value, DbType type, bool suppress)
        {
            ParameterName = name;
            _sourceColumn = column;
            SourceVersion = version;
            Precision = precision;
            _scale = scale;
            _size = size;
            _forceSize = forceSize;
            _offset = offset;
            Direction = direction;

            if ((Value as ICloneable) != null)
            {
                Value = ((ICloneable)Value).Clone();

            }
            _value = Value;
            Suppress = suppress;
        }

        #region ICloneable

        public object Clone()
        {
            QueryParameter parameter1;
            parameter1 = new QueryParameter();
            parameter1.SetProperties(_name, _sourceColumn, _version, _precision, _scale,
                _size, _forceSize, _offset, _direction, _value, _dbType, _suppress);
            return parameter1;
        }

        #endregion
    }
}
