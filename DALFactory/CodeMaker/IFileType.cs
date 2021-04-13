using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.CodeMaker;

namespace DALFactory.CodeMaker
{
    public interface IFileType
    {
        int FileType_Add(FileTypeInfo info);
        int FileType_Edit(FileTypeInfo info);
        int FileType_Del(int ID);
        FileTypeInfo FileTypeGetInfo(int ID);
        IList<FileTypeInfo> FileTypeGetList();
        IList<FileTypeInfo> FileTypeByParentIDGetList(int ParentID);
        string FileType_GetPath(int ID);
    }
}
