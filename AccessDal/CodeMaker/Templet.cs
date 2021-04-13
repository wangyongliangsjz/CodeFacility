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
    public class Templet : DbBase, ITemplet
    {
        public string constring = DBConfig.GetConString("AccessCodeMaker");
        public int Templet_Add(TempletInfo info,out int ID)
        {
            ID = 0;
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[7];
                param[0] = new OleDbParameter("@ParentID", OleDbType.Integer);
                param[0].Value = info.ParentID;
                param[1] = new OleDbParameter("@Code", OleDbType.VarWChar, 30);
                param[1].Value = info.Code;
                param[2] = new OleDbParameter("@Title", OleDbType.VarWChar, 50);
                param[2].Value = info.Title;
                param[3] = new OleDbParameter("@Content", OleDbType.VarWChar, 200);
                param[3].Value = info.Content;
                param[4] = new OleDbParameter("@Path", OleDbType.VarWChar, 200);
                param[4].Value = info.Path;
                param[5] = new OleDbParameter("@Postfix", OleDbType.VarWChar, 200);
                param[5].Value = info.Postfix;   
                param[6] = new OleDbParameter("@Remark", OleDbType.VarWChar, 100);
                param[6].Value = info.Remark;
                string sql = "insert into Cm_Templet (ParentID,Code,Title,Content,Path,Postfix,CreateTime,EditTime,Remark) values (@ParentID,@Code,@Title,@Content,@Path,@Postfix,now(),now(),@Remark)";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            if (rst == 1)
            {
                DataTable dt = new DataTable();
                string sql = "select max(ID) as ID from Cm_Templet";
                dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, null);
                ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
            }

            return rst;
        }

        public int Templet_Edit(TempletInfo info)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[7];
                param[0] = new OleDbParameter("@Code", OleDbType.VarWChar, 30);
                param[0].Value = info.Code;
                param[1] = new OleDbParameter("@Title", OleDbType.VarWChar, 50);
                param[1].Value = info.Title;
                param[2] = new OleDbParameter("@Content", OleDbType.VarWChar, 200);
                param[2].Value = info.Content;
                param[3] = new OleDbParameter("@Path", OleDbType.VarWChar, 200);
                param[3].Value = info.Path;
                param[4] = new OleDbParameter("@Postfix", OleDbType.VarWChar, 200);
                param[4].Value = info.Postfix;
                param[5] = new OleDbParameter("@Remark", OleDbType.VarWChar, 100);
                param[5].Value = info.Remark;
                param[6] = new OleDbParameter("@ID", OleDbType.VarWChar, 50);
                param[6].Value = info.ID;

                string sql = "update Cm_Templet set Code=@Code,Title=@Title,Content=@Content,Path=@Path,Postfix=@Postfix,EditTime=now(),Remark=@Remark where ID=@ID";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }
            return rst;
        }

        public int Templet_Del(int ID)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[1];
                param[0] = new OleDbParameter("@ID", OleDbType.VarWChar, 20);
                param[0].Value = ID;

                string sql = "delete from Cm_Templet where ID=@ID";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }
        public TempletInfo TempletGetInfo(int ID)
        {
            TempletInfo info = new TempletInfo();
            DataTable dt = new DataTable();
            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("@ID", OleDbType.VarWChar, 20);
            param[0].Value = ID;

            string sql = "select * from Cm_Templet where ID=@ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, param);
            if (dt.Rows.Count > 0)
            {
                info.ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[0]["ParentID"].ToString());
                info.Code = dt.Rows[0]["Code"].ToString();
                info.Title = dt.Rows[0]["Title"].ToString();
                info.Content = dt.Rows[0]["Content"].ToString();
                info.Path = dt.Rows[0]["Path"].ToString();
                info.Postfix = dt.Rows[0]["Postfix"].ToString();
                info.CreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                info.EditTime = Convert.ToDateTime(dt.Rows[0]["EditTime"].ToString());
                info.Remark = dt.Rows[0]["Remark"].ToString();
            }
            return info;
        }

        public IList<TempletInfo> TempletGetList()
        {
            IList<TempletInfo> ilist = new List<TempletInfo>();            
            DataTable dt = new DataTable();

            string sql = "select a.*,c.Title as ParentTitle from Cm_Templet a,cm_TempletType c where a.ParentID=c.ID order by a.ParentID,a.ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, null);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TempletInfo info = new TempletInfo();
                info.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[i]["ParentID"].ToString());
                info.Code = dt.Rows[i]["Code"].ToString();
                info.Title = dt.Rows[i]["Title"].ToString();
                info.Content = dt.Rows[i]["Content"].ToString();
                info.Path = dt.Rows[i]["Path"].ToString();
                info.Postfix = dt.Rows[i]["Postfix"].ToString();
                info.CreateTime = Convert.ToDateTime(dt.Rows[i]["CreateTime"].ToString());
                info.EditTime = Convert.ToDateTime(dt.Rows[i]["EditTime"].ToString());
                info.Remark = dt.Rows[i]["Remark"].ToString();
                info.ParentTitle = dt.Rows[i]["ParentTitle"].ToString();
                ilist.Add(info);
            }
            return ilist;
        }

        public IList<TempletInfo> TempletByParentIDGetList(int ParentID)
        {
            IList<TempletInfo> ilist = new List<TempletInfo>();
            DataTable dt = new DataTable();

            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("@ParentID", OleDbType.Integer);
            param[0].Value = ParentID;

            string sql = "select a.*,c.Title as ParentTitle from Cm_Templet a,cm_TempletType c where a.ParentID=c.ID and a.ParentID=@ParentID order by a.ParentID,a.ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, param);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TempletInfo info = new TempletInfo();
                info.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[i]["ParentID"].ToString());
                info.Code = dt.Rows[0]["Code"].ToString();
                info.Title = dt.Rows[i]["Title"].ToString();
                info.Content = dt.Rows[i]["Content"].ToString();
                info.Path = dt.Rows[i]["Path"].ToString();
                info.Postfix = dt.Rows[i]["Postfix"].ToString();
                info.CreateTime = Convert.ToDateTime(dt.Rows[i]["CreateTime"].ToString());
                info.EditTime = Convert.ToDateTime(dt.Rows[i]["EditTime"].ToString());
                info.Remark = dt.Rows[i]["Remark"].ToString();
                ilist.Add(info);
            }
            return ilist;
        }
    }
}
