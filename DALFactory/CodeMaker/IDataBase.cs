using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Model.CodeMaker;

namespace DALFactory.CodeMaker
{
    public interface IDataBase
    {
        DataBaseInfo DataBaseGetInfo(DbLinkInfo info, List<string> tableNameList, out string rstmsg);
        DataBaseInfo GetTableInfo(DbLinkInfo info,out string rstmsg);
        int ImportData(DbLinkInfo info, TableInfo tInfo, string field, DataTable dt, out string rstmsg);
    }
}
