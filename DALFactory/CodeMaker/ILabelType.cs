using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.CodeMaker;

namespace DALFactory.CodeMaker
{
    public interface ILabelType
    {
        int LabelType_Add(LabelTypeInfo info);
        int LabelType_Edit(LabelTypeInfo info);
        int LabelType_Del(int ID);
        LabelTypeInfo LabelTypeGetInfo(int ID);
        IList<LabelTypeInfo> LabelTypeGetList();
        IList<LabelTypeInfo> LabelTypeByParentIDGetList(int ParentID);
    }
}
