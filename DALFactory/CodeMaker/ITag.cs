using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.CodeMaker;

namespace DALFactory.CodeMaker
{
    public interface ITag
    {
        int Tag_Add(TagInfo info);
        int Tag_Edit(TagInfo info);
        int Tag_Del(int ID);
        TagInfo TagGetInfo(int ID);
        IList<TagInfo> TagGetList();
        IList<TagInfo> TagByParentIDGetList(int ParentID);
    }
}
