using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CodeMaker
{
    /// <summary>
    /// 模板类型
    /// </summary>
    public class TempletTypeInfo
    {
        private int _id;
        private int _parentid;
        private string _code;
        private string _title;
        private int _filetypeid;
        private string _alias;
        private DateTime _createtime;
        private DateTime _edittime;
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
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        public int FileTypeID
        {
            set { _filetypeid = value; }
            get { return _filetypeid; }
        }
        /// <summary>
        /// 根目录的文件夹
        /// </summary>
        public string Alias
        {
            set { _alias = value; }
            get { return _alias; }
        }
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        public DateTime EditTime
        {
            set { _edittime = value; }
            get { return _edittime; }
        }
        public string Remark
        {
            set { _remark=value;}
            get { return _remark;}
        }
    }
}
