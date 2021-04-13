using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.CodeMaker;

namespace DALFactory.CodeMaker
{
    public interface ILabel
    {
        int Label_Add(LabelInfo info);
        int Label_Edit(LabelInfo info);
        int Label_Del(int ID);
        LabelInfo LabelGetInfo(int ID);
        LabelInfo LabelGetInfo(string Title);
        IList<LabelInfo> LabelGetList();
        IList<LabelInfo> LabelByParentIDGetList(int ParentID);

    }
}
