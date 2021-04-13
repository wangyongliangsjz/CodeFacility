using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.CodeMaker;

namespace DALFactory.CodeMaker
{
    public interface IUpLoadFile
    {
        int UpLoadFile_Add(UpLoadFileInfo info);
        int UpLoadFile_Edit(UpLoadFileInfo info);
        int UpLoadFile_Del(int FileID);
        UpLoadFileInfo UpLoadFileGetInfo(int FileID);
        UpLoadFileInfo UpLoadFileGetInfo(string TableName, int ParentId);
        IList<UpLoadFileInfo> UpLoadFileGetList();
    }
}
