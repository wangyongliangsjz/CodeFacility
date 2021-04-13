using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model.CodeMaker;

namespace DALFactory.CodeMaker
{
    public interface IDbLink
    {
        int DbLink_Add(DbLinkInfo info);
        int DbLink_Edit(DbLinkInfo info);
        int DbLink_Del(int ID);
        DbLinkInfo DbLinkGetInfo(int ID);
        IList<DbLinkInfo> DbLinkGetList();
    }
}
