using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.CodeMaker;

namespace DALFactory.CodeMaker
{
    public interface ITagType
    {
        int TagType_Add(TagTypeInfo info);
        int TagType_Edit(TagTypeInfo info);
        int TagType_Del(int ID);
        TagTypeInfo TagTypeGetInfo(int ID);
        IList<TagTypeInfo> TagTypeGetList();
        IList<TagTypeInfo> TagTypeByParentIDGetList(int ParentID);
    }
}
