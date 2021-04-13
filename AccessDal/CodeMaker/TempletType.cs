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
    public class TempletType : DbBase, ITempletType
    {
        public string constring = DBConfig.GetConString("AccessCodeMaker");
        public int TempletType_Add(TempletTypeInfo info)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[5];
                param[0] = new OleDbParameter("@ParentID", OleDbType.Integer);
                param[0].Value = info.ParentID;
                param[1] = new OleDbParameter("@Code", OleDbType.VarWChar, 50);
                param[1].Value = info.Code;
                param[2] = new OleDbParameter("@Title", OleDbType.VarWChar, 50);
                param[2].Value = info.Title;
                param[3] = new OleDbParameter("@Alias", OleDbType.VarWChar, 50);
                param[3].Value = info.Alias;
                param[4] = new OleDbParameter("@Remark", OleDbType.VarWChar, 100);
                param[4].Value = info.Remark;
                string sql = "insert into Cm_TempletType (ParentID,Code,Title,Alias,CreateTime,EditTime,Remark) values (@ParentID,@Code,@Title,@Alias,now(),now(),@Remark)";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }

        public int TempletType_Edit(TempletTypeInfo info)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[5];
                //param[0] = new OleDbParameter("@Code", OleDbType.VarWChar, 50);
                //param[0].Value = info.Code;
                param[0] = new OleDbParameter("@Title", OleDbType.VarWChar, 50);
                param[0].Value = info.Title;
                param[1] = new OleDbParameter("@Alias", OleDbType.VarWChar, 50);
                param[1].Value = info.Alias;
                param[2] = new OleDbParameter("@Remark", OleDbType.VarWChar, 100);
                param[2].Value = info.Remark;
                param[3] = new OleDbParameter("@ID", OleDbType.Integer);
                param[3].Value = info.ID;
                string sql = "update Cm_TempletType set Title=@Title,Alias=@Alias,EditTime=now(),Remark=@Remark where ID=@ID";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }

        public int TempletTypeByFileTypeID_Edit(int FileTypeID,int ID)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[2];
                param[0] = new OleDbParameter("@FileTypeID", OleDbType.VarWChar, 50);
                param[0].Value = FileTypeID;
                param[1] = new OleDbParameter("@ID", OleDbType.Integer);
                param[1].Value = ID;
                string sql = "update Cm_TempletType set FileTypeID=@FileTypeID,EditTime=now() where ID=@ID";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }


        public int TempletType_Del(int ID)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[1];
                param[0] = new OleDbParameter("@ID", OleDbType.Integer);
                param[0].Value = ID;

                string sql = "delete from Cm_TempletType where ID=@ID";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }
        public TempletTypeInfo TempletTypeGetInfo(int ID)
        {
            TempletTypeInfo info = new TempletTypeInfo();
            DataTable dt = new DataTable();
            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("@ID", OleDbType.Integer);
            param[0].Value = ID;

            string sql = "select * from Cm_TempletType where ID=@ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, param);
            if (dt.Rows.Count > 0)
            {
                info.ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[0]["ParentID"].ToString());
                info.Code = dt.Rows[0]["Code"].ToString();
                info.Title = dt.Rows[0]["Title"].ToString();
                info.FileTypeID = int.Parse(dt.Rows[0]["FileTypeID"].ToString());
                info.Alias = dt.Rows[0]["Alias"].ToString();
                info.CreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                info.EditTime = Convert.ToDateTime(dt.Rows[0]["EditTime"].ToString());
                info.Remark = dt.Rows[0]["Remark"].ToString();
            }
            return info;
        }

        public TempletTypeInfo TempletTypeByCodeGetList(string Code)
        {
            TempletTypeInfo info = new TempletTypeInfo();
            DataTable dt = new DataTable();
            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("@Code", OleDbType.VarWChar, 50);
            param[0].Value = Code;

            string sql = "select * from Cm_TempletType where Code=@Code order by ParentID,ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, param);
            if (dt.Rows.Count > 0)
            {
                info.ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[0]["ParentID"].ToString());
                info.Code = dt.Rows[0]["Code"].ToString();
                info.Title = dt.Rows[0]["Title"].ToString();
                info.FileTypeID = int.Parse(dt.Rows[0]["FileTypeID"].ToString());
                info.Alias = dt.Rows[0]["Alias"].ToString();
                info.CreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                info.EditTime = Convert.ToDateTime(dt.Rows[0]["EditTime"].ToString());
                info.Remark = dt.Rows[0]["Remark"].ToString();
            }
            return info;
        }

        public IList<TempletTypeInfo> TempletTypeGetList()
        {
            IList<TempletTypeInfo> ilist = new List<TempletTypeInfo>();
            DataTable dt = new DataTable();

            string sql = "select * from Cm_TempletType order by ParentID,ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, null);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TempletTypeInfo info = new TempletTypeInfo();
                info.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[i]["ParentID"].ToString());
                info.Code = dt.Rows[i]["Code"].ToString();
                info.Title = dt.Rows[i]["Title"].ToString();
                info.FileTypeID = int.Parse(dt.Rows[i]["FileTypeID"].ToString());
                info.Alias = dt.Rows[i]["Alias"].ToString();
                info.CreateTime = Convert.ToDateTime(dt.Rows[i]["CreateTime"].ToString());
                info.EditTime = Convert.ToDateTime(dt.Rows[i]["EditTime"].ToString());
                info.Remark = dt.Rows[i]["Remark"].ToString();
                ilist.Add(info);
            }
            return ilist;
        }

        public IList<TempletTypeInfo> TempletTypeByParentIDGetList(int ParentID)
        {
            IList<TempletTypeInfo> ilist = new List<TempletTypeInfo>();
            DataTable dt = new DataTable();
            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("@ParentID", OleDbType.Integer);
            param[0].Value = ParentID;

            string sql = "select * from Cm_TempletType where ParentID=@ParentID order by ParentID,ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, param);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TempletTypeInfo info = new TempletTypeInfo();
                info.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[i]["ParentID"].ToString());
                info.Code = dt.Rows[i]["Code"].ToString();
                info.Title = dt.Rows[i]["Title"].ToString();
                info.FileTypeID = int.Parse(dt.Rows[i]["FileTypeID"].ToString());
                info.Alias = dt.Rows[i]["Alias"].ToString();
                info.CreateTime = Convert.ToDateTime(dt.Rows[i]["CreateTime"].ToString());
                info.EditTime = Convert.ToDateTime(dt.Rows[i]["EditTime"].ToString());
                info.Remark = dt.Rows[i]["Remark"].ToString();
                ilist.Add(info);
            }
            return ilist;
        }

    }
}
