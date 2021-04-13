using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CodeMaker
{
    /// <summary>
    /// 模板标签
    /// </summary>
    public class LabelInfo
    {
        private int _id;
        private int _parentid;
        private string _title;
        private string _content;
        private string _remark;
        private string _parenttitle;

        public int ID
        {
            set{_id=value;;}
            get{return _id;}
        }
        public int ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        public string Content
        {
            set{_content=value;}
            get{return _content;}
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
