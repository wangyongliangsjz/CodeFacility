using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CodeMaker
{
    /// <summary>
    /// 数据表信息对象
    /// </summary>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType("Table")]
    public class TableInfo
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public TableInfo()
        {
            myFields.myOwnerTable = this;
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
        /// <summary>
        /// 字段名称首字母大写
        /// </summary>
        public string NameDx
        {
            get
            {
                //return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strName);
                string nm="";
                if(strName=="") return "";
                if(strName.Length==1) return strName.ToUpper();
                if(strName.Length>1)
                {
                  nm=  strName.Substring(0,1).ToUpper()+strName.Substring(1,strName.Length-1); 
                }
                return nm;
            }
            set { strName = value; }
        }
        /// <summary>
        /// 字段名称首字母大写
        /// </summary>
        public string NameXx
        {
            get
            {
                //return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strName);
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
        private string _xtype = null;
        public string xType
        {
            get { return _xtype; }
            set { _xtype = value; }
        }
        private string strRemark = null;
        /// <summary>
        /// 对象说明,一般可以为对象中文名
        /// </summary>
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
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

        private object objTag = null;
        /// <summary>
        /// 对象附加数据
        /// </summary>
        public object Tag
        {
            get { return objTag; }
            set { objTag = value; }
        }
        private FieldInfoCollection myFields = new FieldInfoCollection();
        /// <summary>
        /// 字段对象列表
        /// </summary>
        public FieldInfoCollection Fields
        {
            get { return myFields; }
        }

        /// <summary>
        /// 字段对象列表类型
        /// </summary>
        public class FieldInfoCollection : System.Collections.CollectionBase
        {
            internal TableInfo myOwnerTable = null;
            /// <summary>
            /// 返回指定序号的字段信息
            /// </summary>
            public FieldInfo this[int index]
            {
                get { return (FieldInfo)this.InnerList[index]; }
            }
            /// <summary>
            /// 返回指定名称的字段信息
            /// </summary>
            public FieldInfo this[string strFieldName]
            {
                get
                {
                    foreach (FieldInfo f in this.InnerList)
                    {
                        if (string.Compare(f.Name, strFieldName, true) == 0)
                            return f;
                    }
                    return null;
                }
            }
            /// <summary>
            /// 添加字段信息对象
            /// </summary>
            /// <param name="info">字段信息对象</param>
            /// <returns>字段对象在列表中的序号</returns>
            public int Add(FieldInfo info)
            {
                info.OwnerTable = myOwnerTable;
                return this.List.Add(info);
            }
            public void Remove(FieldInfo info)
            {
                this.List.Remove(info);
            }

            public IList<FieldInfo> GetList()
            {
                IList<FieldInfo> flist = new List<FieldInfo>();
                foreach (FieldInfo finfo in this.List)
                {
                    flist.Add(finfo);
                }
                return flist;
            }
        }
    }
}
