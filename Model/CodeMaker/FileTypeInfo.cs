using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CodeMaker
{

    /// <summary>
    /// 文件类型
    /// </summary>
    public class FileTypeInfo
    {
        private int _id;
        private int _parentid;
        private string _code;
        private string _alias;
        private string _title;
        private string _remark;

        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public int ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        public string Alias
        {
            set { _alias = value; }
            get { return _alias; }
        }
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

    }
}
