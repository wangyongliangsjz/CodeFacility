using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.CodeMaker;

namespace DALFactory.CodeMaker
{
    public interface ITempletType
    {
        int TempletType_Add(TempletTypeInfo info);
        int TempletType_Edit(TempletTypeInfo info);
        int TempletTypeByFileTypeID_Edit(int FileTypeID, int ID);
        int TempletType_Del(int ID);
        TempletTypeInfo TempletTypeGetInfo(int ID);
        TempletTypeInfo TempletTypeByCodeGetList(string Code);
        IList<TempletTypeInfo> TempletTypeGetList();
        IList<TempletTypeInfo> TempletTypeByParentIDGetList(int ParentID);
    }
}
