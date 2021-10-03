using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CodeMaker
{
    /// <summary>
    /// 字段信息对象
    /// </summary>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType("Field")]
    public class FieldInfo
    {
        private int id = 0;
        /// <summary>
        /// 字段序号
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        private string strName = null;
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

        private string strDescription = null;
        /// <summary>
        /// 字段说明
        /// </summary>
        public string Description
        {
            get
            {
                string str = "";
                if (strDescription != null)
                {
                    str = strDescription.Replace("\r\n", " ");
                    while (str.IndexOf("  ") > -1)
                    {
                        str = str.Replace("  ", " ");
                    }
                }
                return str;
            }
            set { strDescription = value; }
        }

        private string strFieldType = null;
        /// <summary>
        /// 字段类型
        /// </summary>
        public string FieldType
        {
            get {
                string type = "";
                if (strFieldType == null)
                    return type;
                if (strFieldType.ToLower().IndexOf("char") > -1 || strFieldType.ToLower().IndexOf("binary") > -1)
                {
                    if (FieldWidth != "")
                    {
                        type = strFieldType + "(" + FieldWidth + ")";
                    }
                    else
                    {
                        type = strFieldType;
                    }
                }
                else if (strFieldType.ToLower().IndexOf("decimal") > -1)
                {
                    type = strFieldType + "(" + FieldWidth + ","+Scale+")";
                }
                else
                {
                    type = strFieldType;
                }
                
                return type; 
            }
            set { strFieldType = value; }
        }

        /// <summary>
        /// 字段名称首字母大写
        /// </summary>
        public string NameDx
        {
            get
            {
                string nm = "";
                if (strName == "") return "";
                if (strName.Length == 1) return strName.ToUpper();
                if (strName.Length > 1)
                {
                    nm = strName.Substring(0, 1).ToUpper() + strName.Substring(1, strName.Length - 1);
                }
                return nm;
            }
            set { strName = value; }
        }

        /// <summary>
        /// 字段名称首字母小写
        /// </summary>
        public string NameXx
        {
            get
            {
                string nm = "";
                if (strName == "") return "";
                if (strName.Length == 1) return strName.ToUpper();
                if (strName.Length > 1)
                {
                    nm = strName.Substring(0, 1).ToLower() + strName.Substring(1, strName.Length - 1);
                }
                return nm;
            }
            set { strName = value; }
        }

        private string _defaultvalue;
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue
        {
            get { return _defaultvalue; }
            set { _defaultvalue = value; }
        }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultVal
        {
            get {

                string val = "";
                if (_defaultvalue != "" && _defaultvalue != null)
                {
                    val = _defaultvalue.Replace("(getdate())", "DateTime.Now").Replace("(GETDATE())", "DateTime.Now").Replace("((", "").Replace("))", "").Replace("(N'", "\"").Replace("('", "\"").Replace("')", "\"");

                    if (FieldType == "bit")
                    {
                        if (val == "0")
                            val = "false";
                        else
                            val = "true";
                    }
                    else if (FieldType.ToLower().IndexOf("decimal") > -1 || FieldType.ToLower().IndexOf("money") > -1)
                    {
                        if (val != "" && val != null)
                            val = val + "M";
                    }
                }
                return val; 
            
            }
            set { }

        }

        private bool _isidentity;
        /// <summary>
        /// 字段是否自增
        /// </summary>
        public bool IsIdentity
        {
            get { return _isidentity; }
            set { _isidentity = value; }
        }

        /// <summary>
        /// 判断字段是否是字符串字段
        /// </summary>
        public bool IsString
        {
            get
            {
                return TypeContainStrings(new string[] { "char", "text" });
            }
            set { }
        }

        /// <summary>
        /// 判断字段是否是整数字段
        /// </summary>
        public bool IsInteger
        {
            get
            {
                return TypeContainStrings(new string[] { "int", "bit" });
            }
            set { }
        }

        /// <summary>
        /// 判断字段是否是布尔类型
        /// </summary>
        public bool IsBoolean
        {
            get
            {
                return TypeContainStrings(new string[] { "bit" });
            }
            set { }
        }

        /// <summary>
        /// 判断字段是否是decimal
        /// </summary>
        public bool IsDecimal
        {
            get
            {
                return TypeContainStrings(new string[] { "decimal", "money", "numeric" });
            }
            set { }
        }

        /// <summary>
        /// 字段是否是数值的字段
        /// </summary>
        public bool IsNumberic
        {
            get
            {
                return TypeContainStrings(new string[] { "number", "numberic", "real", "double","numeric" });
            }
            set { }
        }
        /// <summary>
        /// 字段是否是数值的字段
        /// </summary>
        public bool IsDouble
        {
            get
            {
                return TypeContainStrings(new string[] { "double" });
            }
            set { }
        }
        /// <summary>
        /// 字段是否是float的字段
        /// </summary>
        public bool IsFloat
        {
            get
            {
                return TypeContainStrings(new string[] { "float" });
            }
            set { }
        }
        /// <summary>
        /// 是否是日期类型的字段
        /// </summary>
        public bool IsDateTime
        {
            get
            {
                return TypeContainStrings(new string[] { "date", "datetime" });
            }
            set { }
        }

        /// <summary>
        /// 是否是二进制类型的字段
        /// </summary>
        public bool IsBinary
        {
            get
            {
                return TypeContainStrings(new string[] { "binary", "long", "image" });
            }
            set { }
        }

        private bool TypeContainStrings(string[] items)
        {
            string type = this.strFieldType;
            if (type != null)
            {
                type = type.ToLower();
                foreach (string item in items)
                {
                    if (type.IndexOf(item) >= 0)
                        return true;
                }
            }
            return false;
        }

        private string strFieldWidth = "";
        /// <summary>
        /// 字段宽度
        /// </summary>
        public string FieldWidth
        {
            get { return strFieldWidth; }
            set { strFieldWidth = value; }
        }

        private string _scale;
        /// <summary>
        /// 小数位数
        /// </summary>
        public string Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        private bool bolNullable = true;
        /// <summary>
        /// 字段可否为空
        /// </summary>
        //[System.ComponentModel.DefaultValue( true )]
        public bool Nullable
        {
            get { return bolNullable; }
            set { bolNullable = value; }
        }

        private bool bolPrimaryKey = false;
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool PrimaryKey
        {
            get { return bolPrimaryKey; }
            set { bolPrimaryKey = value; }
        }

        private bool bolIndexed = false;
        /// <summary>
        /// 是否索引
        /// </summary>
        public bool Indexed
        {
            get { return bolIndexed; }
            set { bolIndexed = value; }
        }

        private string strRemark = null;
        /// <summary>
        /// 字段说明,一般可以为字段中文名
        /// </summary>
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        /// <summary>
        /// 字段对应的数据类型
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public Type ValueType
        {
            get
            {
                if (this.IsBoolean)
                    return typeof(bool);
                if (this.IsInteger)
                    return typeof(int);
                if (this.IsBinary)
                    return typeof(byte[]);
                if (this.IsDateTime)
                    return typeof(DateTime);
                if (this.IsDecimal)
                    return typeof(decimal);
                if (this.IsDouble)
                    return typeof(double);
                if (this.IsFloat)
                    return typeof(float);
                if (this.IsNumberic)
                    return typeof(float);
                return typeof(string);
            }
        }

        private string _valuetypename;
        /// <summary>
        /// 字段对应的数据类型名称
        /// </summary>
        public string ValueTypeName
        {
            get
            {
                if(_valuetypename==null)
                {
                    string type = "";
                    string FullName = this.ValueType.FullName.Replace("System.", "");
                    switch (FullName.ToLower())
                    {
                        case "int32":
                            type = "int";
                            break;
                        case "string":
                            type = "string";
                            break;
                        case "boolean":
                            type = "bool";
                            break;
                        case "single":
                            type = "float";
                            break;
                        case "double":
                            type = "double";
                            break;
                        case "datetime":
                        case "timestamp": //mysql数据库的数据类型
                            type = "DateTime";
                            break;
                        case "decimal":
                            type = "decimal";
                            break;
                    }
                    if (type == "")
                    {
                        switch (FieldType.ToLower())
                        {
                            case "integer":
                                type = "int";
                                break;
                        }
                    }
                    _valuetypename = type;
                }
                
                return _valuetypename;
            }
            set { _valuetypename = value; }
        }

        /// <summary>
        /// 字段全名
        /// </summary>
        public string FullName
        {
            get
            {
                if (myOwnerTable == null)
                    return strName;
                else
                    return myOwnerTable.Name + "." + strName;
            }
        }

        /// <summary>
        /// 小写字段名称
        /// </summary>
        public string LcName
        {
            get
            {
                string lname = strName.ToLower();
                return lname;
            }
            set { }
        }

        /// <summary>
        /// 返回表示对象的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return FullName;
        }

        private TableInfo myOwnerTable = null;
        /// <summary>
        /// 字段所在的数据表对象
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public TableInfo OwnerTable
        {
            get { return myOwnerTable; }
            set { myOwnerTable = value; }
        }
    }
}
