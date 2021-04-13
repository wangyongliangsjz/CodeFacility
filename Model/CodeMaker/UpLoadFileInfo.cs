using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CodeMaker
{
    public class UpLoadFileInfo
    {
        private int _fileid;
        private string _tablename;
        private string _rootfield;
        private string _rootid;
        private string _parentfield;
        private string _parentid;
        private string _filename;
        private string _filepath;
        private string _expandname;
        private int _filesize;
        private string _remark;
        private DateTime _uploaddate;
        private int _filetypeid;
        private string _uniquename;
        private string _attachmentid;

        public int FileID
        {
            set{_fileid=value;}
            get{return _fileid;}
        }
        public string TableName
        {
            set{_tablename=value;}
            get{return _tablename;}
        }
        public string RootField
        {
            set{_rootfield=value;}
            get{return _rootfield;}
        }
        public string RootId
        {
            set{_rootid=value;}
            get{return _rootid;}
        }
        public string ParentField
        {
            set{_parentfield=value;}
            get{return _parentfield;}
        }
        public string ParentId
        {
            set{_parentid=value;}
            get{return _parentid;}
        }
        public string FileName
        {
            set{_filename=value;}
            get{return _filename;}
        }
        public string FilePath
        {
            set{_filepath=value;}
            get{return _filepath;}
        }
        public string ExpandName
        {
            set{_expandname=value;}
            get{return _expandname;}
        }
        public int FileSize
        {
            set{_filesize=value;}
            get{return _filesize;}
        }
        public string Remark
        {
            set{_remark=value;}
            get{return _remark;}
        }
        public DateTime UploadDate
        {
            set{_uploaddate=value;}
            get{return _uploaddate;}
        }
        public int FileTypeId
        {
            set{_filetypeid =value;}
            get{return _filetypeid;}
        }
        public string UniqueName
        {
            set{_uniquename=value;}
            get{return _uniquename;}
        }
        public string AttachmentId
        {
            set { _attachmentid = value; }
            get { return _attachmentid; }
        }

    }
}
