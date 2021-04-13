using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CodeMaker
{
    /// <summary>
    /// 模板
    /// </summary>
    public class TempletInfo
    {
        private int _id;
        private int _parentid;
        private string _code;
        private string _title;
        private string _content;
        private string _path;
        private string _postfix;
        private DateTime _createTime;
        private DateTime _EditTime;
        private string _remark;
        private string _parenttitle;

        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public int ParentID
        {
            set {_parentid=value;}
            get {return _parentid;}
        }
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        public string Title
        {
            set {_title=value;}
            get {return _title;}
        }
        public string Content
        {
            set {_content=value;}
            get {return _content;}
        }
        public string Path
        {
            set {_path=value;}
            get {return _path;}
        }
        public string Postfix
        {
            set { _postfix = value; }
            get { return _postfix; }
        }
        public DateTime CreateTime
        {
            set {_createTime=value;}
            get {return _createTime;}
        }
        public DateTime EditTime
        {
            set {_EditTime=value;}
            get {return _EditTime;}
        }
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        public string ParentTitle
        {
            set { _parenttitle = value; }
            get { return _parenttitle; }
        }

    }
}
