using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using DALProfile;
using DALFactory.CodeMaker;
using Model.CodeMaker;

namespace AccessDal.CodeMaker
{
    public class Postfix : DbBase, IPostfix
    {
        public string constring = DBConfig.GetConString("AccessCodeMaker");
        public IList<PostfixInfo> PostfixGetList()
        {
            IList<PostfixInfo> ilist = new List<PostfixInfo>();
            DataTable dt = new DataTable();

            string sql = "select * from Cm_Postfix order by ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, null);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PostfixInfo info = new PostfixInfo();
                info.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                info.Postfix = dt.Rows[i]["Postfix"].ToString();
                ilist.Add(info);
            }
            return ilist;
        }
    }
}
