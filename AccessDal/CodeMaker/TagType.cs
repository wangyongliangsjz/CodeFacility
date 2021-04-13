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
    public class TagType : DbBase, ITagType
    {
        public string constring = DBConfig.GetConString("AccessCodeMaker");
        public int TagType_Add(TagTypeInfo info)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[4];
                param[0] = new OleDbParameter("@ParentID", OleDbType.Integer);
                param[0].Value = info.ParentID;
                param[1] = new OleDbParameter("@Code", OleDbType.VarWChar, 20);
                param[1].Value = info.Code;
                param[2] = new OleDbParameter("@Title", OleDbType.VarWChar, 20);
                param[2].Value = info.Title;
                param[3] = new OleDbParameter("@Remark", OleDbType.VarWChar, 100);
                param[3].Value = info.Remark;
                string sql = "insert into Cm_TagType (ParentID,Code,Title,Remark) values (@ParentID,Code,@Title,@Remark)";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch (Exception ex)
            {
                rst = -1;
            }

            return rst;
        }

        public int TagType_Edit(TagTypeInfo info)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[4];
                param[0] = new OleDbParameter("@Code", OleDbType.VarWChar, 20);
                param[0].Value = info.Code;
                param[1] = new OleDbParameter("@Title", OleDbType.VarWChar, 20);
                param[1].Value = info.Title;
                param[2] = new OleDbParameter("@Remark", OleDbType.VarWChar, 100);
                param[2].Value = info.Remark;
                param[3] = new OleDbParameter("@ID", OleDbType.VarWChar, 50);
                param[3].Value = info.ID;

                string sql = "update Cm_TagType set Code=@Code,Title=@Title,Remark=@Remark where ID=@ID";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }

        public int TagType_Del(int ID)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[1];
                param[0] = new OleDbParameter("@ID", OleDbType.VarWChar, 20);
                param[0].Value = ID;

                string sql = "delete from Cm_TagType where ID=@ID";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }
        public TagTypeInfo TagTypeGetInfo(int ID)
        {
            TagTypeInfo info = new TagTypeInfo();
            DataTable dt = new DataTable();
            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("@ID", OleDbType.VarWChar, 20);
            param[0].Value = ID;

            string sql = "select * from Cm_TagType where ID=@ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, param);
            if (dt.Rows.Count > 0)
            {
                info.ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[0]["ParentID"].ToString());
                info.Code = dt.Rows[0]["Code"].ToString();
                info.Title = dt.Rows[0]["Title"].ToString();
                info.Remark = dt.Rows[0]["Remark"].ToString();
            }
            return info;
        }

        public IList<TagTypeInfo> TagTypeGetList()
        {
            IList<TagTypeInfo> ilist = new List<TagTypeInfo>();
            DataTable dt = new DataTable();

            string sql = "select * from Cm_TagType order by ParentID,ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, null);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TagTypeInfo info = new TagTypeInfo();
                info.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[i]["ParentID"].ToString());
                info.Code = dt.Rows[i]["Code"].ToString();
                info.Title = dt.Rows[i]["Title"].ToString();
                info.Remark = dt.Rows[i]["Remark"].ToString();
                ilist.Add(info);
            }
            return ilist;
        }

        public IList<TagTypeInfo> TagTypeByParentIDGetList(int ParentID)
        {
            IList<TagTypeInfo> ilist = new List<TagTypeInfo>();
            DataTable dt = new DataTable();

            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("@ParentID", OleDbType.VarWChar, 20);
            param[0].Value = ParentID;

            string sql = "select * from Cm_TagType where ParentID=@ParentID order by ParentID,ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, param);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TagTypeInfo info = new TagTypeInfo();
                info.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[i]["ParentID"].ToString());
                info.Code = dt.Rows[i]["Code"].ToString();
                info.Title = dt.Rows[i]["Title"].ToString();
                info.Remark = dt.Rows[i]["Remark"].ToString();
                ilist.Add(info);
            }
            return ilist;
        }
    }
}
