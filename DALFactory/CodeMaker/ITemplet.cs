using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.CodeMaker;

namespace DALFactory.CodeMaker
{
    public interface ITemplet
    {
        int Templet_Add(TempletInfo info, out int ID);
        int Templet_Edit(TempletInfo info);
        int Templet_Del(int ID);
        TempletInfo TempletGetInfo(int ID);
        IList<TempletInfo> TempletGetList();
        IList<TempletInfo> TempletByParentIDGetList(int ParentID);
    }
}
