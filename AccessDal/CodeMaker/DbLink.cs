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
    public class DbLink : DbBase,  IDbLink
    {
        public string constring = DBConfig.GetConString("AccessCodeMaker");
        public int DbLink_Add(DbLinkInfo info)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[7];
                param[0] = new OleDbParameter("@DbName", OleDbType.VarWChar, 50);
                param[0].Value = info.DbName;
                param[1] = new OleDbParameter("@UserName", OleDbType.VarWChar, 50);
                param[1].Value = info.UserName;
                param[2] = new OleDbParameter("@PassWord", OleDbType.VarWChar, 50);
                param[2].Value = info.PassWord;
                param[3] = new OleDbParameter("@DbType", OleDbType.Integer);
                param[3].Value = info.DbType;
                param[4] = new OleDbParameter("@DataSource", OleDbType.VarWChar, 255);
                param[4].Value = info.DataSource;
                param[5] = new OleDbParameter("@Port", OleDbType.VarWChar, 10);
                param[5].Value = info.Port;
                param[6] = new OleDbParameter("@DbAbbreviation", OleDbType.VarWChar, 50);
                param[6].Value = info.DbAbbreviation;

                string sql = "insert into Cm_DbLink (DbName,UserName,[PassWord],DbType,DataSource,CreateTime,Port,DbAbbreviation) values (@DbName,@UserName,@PassWord,@DbType,@DataSource,now(),@Port,@DbAbbreviation)";                
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch(Exception ex)
            {
                rst = -1;
            }

            return rst;
        }

        public int DbLink_Edit(DbLinkInfo info)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[8];
                param[0] = new OleDbParameter("@DbName", OleDbType.VarWChar, 50);
                param[0].Value = info.DbName;
                param[1] = new OleDbParameter("@UserName", OleDbType.VarWChar, 50);
                param[1].Value = info.UserName;
                param[2] = new OleDbParameter("@PassWord", OleDbType.VarWChar, 50);
                param[2].Value = info.PassWord;
                param[3] = new OleDbParameter("@DbType", OleDbType.Integer);
                param[3].Value = info.DbType;
                param[4] = new OleDbParameter("@DataSource", OleDbType.VarWChar, 255);
                param[4].Value = info.DataSource;
                param[5] = new OleDbParameter("@Port", OleDbType.VarWChar, 10);
                param[5].Value = info.Port;
                param[6] = new OleDbParameter("@DbAbbreviation", OleDbType.VarWChar, 50);
                param[6].Value = info.DbAbbreviation;
                param[7] = new OleDbParameter("@ID", OleDbType.VarWChar, 50);
                param[7].Value = info.ID;

                string sql = "update Cm_DbLink set  DbName=@DbName,UserName=@UserName,[PassWord]=@PassWord,DbType=@DbType,DataSource=@DataSource,CreateTime=now(),Port=@Port,DbAbbreviation=@DbAbbreviation where ID=@ID";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }

        public int DbLink_Del(int ID)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[5];
                param[0] = new OleDbParameter("@ID", OleDbType.VarWChar, 20);
                param[0].Value = ID;

                string sql = "delete from Cm_DbLink where ID=@ID";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }
        public DbLinkInfo DbLinkGetInfo(int ID)
        {
            DbLinkInfo info = new DbLinkInfo();
            DataTable dt = new DataTable();
            OleDbParameter[] param = new OleDbParameter[5];
            param[0] = new OleDbParameter("@ID", OleDbType.VarWChar, 20);
            param[0].Value = ID;

            string sql = "select * from Cm_DbLink where ID=@ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, param);
            if (dt.Rows.Count > 0)
            {
                info.ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                info.DbName = dt.Rows[0]["DbName"].ToString();
                info.UserName = dt.Rows[0]["UserName"].ToString();
                info.PassWord = dt.Rows[0]["PassWord"].ToString();
                info.DataSource = dt.Rows[0]["DataSource"].ToString();
                info.DbType = int.Parse(dt.Rows[0]["DbType"].ToString());
                info.CreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                info.Port = dt.Rows[0]["Port"].ToString();
                info.DbAbbreviation = dt.Rows[0]["DbAbbreviation"].ToString();
            }
            return info;
        }

        public IList<DbLinkInfo> DbLinkGetList()
        {
            IList<DbLinkInfo> ilist = new List<DbLinkInfo>();            
            DataTable dt = new DataTable();

            string sql = "select *,iif(DbType=1, 'SQLServer', iif(DbType=2, 'Oracle', iif(DbType=3, 'MySql', iif(DbType=4, 'Access', iif(DbType=5, 'SqLite', iif(DbType=6, 'PDM', iif(DbType=7, 'MongoDB', iif(DbType=8, 'Redis', '')))))))) as DbTypeName from Cm_DbLink";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, null);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DbLinkInfo info = new DbLinkInfo();
                info.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                info.DbName = dt.Rows[i]["DbName"].ToString();
                info.UserName = dt.Rows[i]["UserName"].ToString();
                info.PassWord = dt.Rows[i]["PassWord"].ToString();
                info.DataSource = dt.Rows[i]["DataSource"].ToString();
                info.DbType = int.Parse(dt.Rows[i]["DbType"].ToString());
                info.CreateTime = Convert.ToDateTime(dt.Rows[i]["CreateTime"].ToString());
                info.Port = dt.Rows[i]["Port"].ToString();
                info.DbAbbreviation = dt.Rows[i]["DbAbbreviation"].ToString();
                info.DbTypeName = dt.Rows[i]["DbTypeName"].ToString();
                ilist.Add(info);
            }
            return ilist;
        }


    }
}
