using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

using DALProfile;
using DALFactory.CodeMaker;
using Model.CodeMaker;

namespace AccessDal.CodeMaker
{
    public class Tag : DbBase,  ITag
    {
        public string constring = DBConfig.GetConString("AccessCodeMaker");
        public int Tag_Add(TagInfo info)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[4];
                param[0] = new OleDbParameter("@ParentID", OleDbType.Integer);
                param[0].Value = info.ParentID;
                param[1] = new OleDbParameter("@Title", OleDbType.VarWChar, 20);
                param[1].Value = info.Title;
                param[2] = new OleDbParameter("@Content", OleDbType.VarWChar, 255);
                param[2].Value = info.Content;
                param[3] = new OleDbParameter("@Remark", OleDbType.VarWChar, 100);
                param[3].Value = info.Remark;
                string sql = "insert into Cm_Tag (ParentID,Title,Content,Remark) values (@ParentID,@Title,@Content,@Remark)";
                
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }

        public int Tag_Edit(TagInfo info)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[4];
                param[0] = new OleDbParameter("@Title", OleDbType.VarWChar, 20);
                param[0].Value = info.Title;
                param[1] = new OleDbParameter("@Content", OleDbType.VarWChar, 255);
                param[1].Value = info.Content;
                param[2] = new OleDbParameter("@Remark", OleDbType.VarWChar, 100);
                param[2].Value = info.Remark;
                param[3] = new OleDbParameter("@ID", OleDbType.VarWChar, 50);
                param[3].Value = info.ID;
                
                string sql = "update Cm_Tag set Title=@Title,Content=@Content,Remark=@Remark where ID=@ID";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }

        public int Tag_Del(int ID)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[1];
                param[0] = new OleDbParameter("@ID", OleDbType.VarWChar, 20);
                param[0].Value = ID;

                string sql = "delete from Cm_Tag where ID=@ID";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }
        public TagInfo TagGetInfo(int ID)
        {
            TagInfo info = new TagInfo();
            DataTable dt = new DataTable();
            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("@ID", OleDbType.VarWChar, 20);
            param[0].Value = ID;

            string sql = "select * from Cm_Tag where ID=@ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, param);
            if (dt.Rows.Count > 0)
            {
                info.ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[0]["ParentID"].ToString());
                info.Title = dt.Rows[0]["Title"].ToString();
                info.Content = dt.Rows[0]["Content"].ToString();
                info.Remark = dt.Rows[0]["Remark"].ToString();
            }
            return info;
        }

        public IList<TagInfo> TagGetList()
        {
            IList<TagInfo> ilist = new List<TagInfo>();
            DataTable dt = new DataTable();

            string sql = "select a.*,c.Title as ParentTitle from Cm_Tag a,Cm_TagType c where a.ParentID=c.ID order by a.ParentID,a.ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, null);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TagInfo info = new TagInfo();
                info.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[i]["ParentID"].ToString());
                info.Title = dt.Rows[i]["Title"].ToString();
                info.Content = dt.Rows[i]["Content"].ToString();
                info.Remark = dt.Rows[i]["Remark"].ToString();
                info.ParentTitle = dt.Rows[i]["ParentTitle"].ToString();
                ilist.Add(info);
            }
            return ilist;
        }

        public IList<TagInfo> TagByParentIDGetList(int ParentID)
        {
            IList<TagInfo> ilist = new List<TagInfo>();
            DataTable dt = new DataTable();

            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("@ParentID", OleDbType.VarWChar, 20);
            param[0].Value = ParentID;

            string sql = "select a.*,c.Title as ParentTitle from Cm_Tag a,Cm_TagType c where a.ParentID=c.ID and a.ParentID=@ParentID order by a.ParentID,a.ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, param);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TagInfo info = new TagInfo();
                info.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[i]["ParentID"].ToString());
                info.Title = dt.Rows[i]["Title"].ToString();
                info.Content = dt.Rows[i]["Content"].ToString();
                info.Remark = dt.Rows[i]["Remark"].ToString();
                info.ParentTitle = dt.Rows[i]["ParentTitle"].ToString();
                ilist.Add(info);
            }
            return ilist;
        }
    }
}
