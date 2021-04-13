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
    public class FileType : DbBase, IFileType
    {
        public string constring = DBConfig.GetConString("AccessCodeMaker");
        public int FileType_Add(FileTypeInfo info)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[5];
                param[0] = new OleDbParameter("@ParentID", OleDbType.Integer);
                param[0].Value = info.ParentID;
                param[1] = new OleDbParameter("@Code", OleDbType.VarWChar, 20);
                param[1].Value = info.Code;
                param[2] = new OleDbParameter("@Alias", OleDbType.VarWChar, 20);
                param[2].Value = info.Alias;
                param[3] = new OleDbParameter("@Title", OleDbType.VarWChar, 50);
                param[3].Value = info.Title;
                param[4] = new OleDbParameter("@Remark", OleDbType.VarWChar, 100);
                param[4].Value = info.Remark;

                string sql = "insert into Cm_FileType (ParentID,Code,Alias,Title,Remark) values (@ParentID,@Code,@Alias,@Title,@Remark)";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }

        public int FileType_Edit(FileTypeInfo info)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[5];
                param[0] = new OleDbParameter("@Code", OleDbType.VarWChar, 20);
                param[0].Value = info.Code;
                param[1] = new OleDbParameter("@Alias", OleDbType.VarWChar, 20);
                param[1].Value = info.Alias;
                param[2] = new OleDbParameter("@Title", OleDbType.VarWChar, 50);
                param[2].Value = info.Title;
                param[3] = new OleDbParameter("@Remark", OleDbType.VarWChar, 100);
                param[3].Value = info.Remark;
                param[4] = new OleDbParameter("@ID", OleDbType.VarWChar, 50);
                param[4].Value = info.ID;
                

                string sql = "update Cm_FileType set Code=@Code,Alias=@Alias,Title=@Title,Remark=@Remark where ID=@ID";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }

        public int FileType_Del(int ID)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[1];
                param[0] = new OleDbParameter("@ID", OleDbType.VarWChar, 20);
                param[0].Value = ID;

                string sql = "delete from Cm_FileType where ID=@ID";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }
        public FileTypeInfo FileTypeGetInfo(int ID)
        {
            FileTypeInfo info = new FileTypeInfo();
            DataTable dt = new DataTable();
            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("@ID", OleDbType.VarWChar, 20);
            param[0].Value = ID;

            string sql = "select * from Cm_FileType where ID=@ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, param);
            if (dt.Rows.Count > 0)
            {
                info.ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[0]["ParentID"].ToString());
                info.Code = dt.Rows[0]["Code"].ToString();
                info.Alias = dt.Rows[0]["Alias"].ToString();
                info.Title = dt.Rows[0]["Title"].ToString();
                info.Remark = dt.Rows[0]["Remark"].ToString();
            }
            return info;
        }

        public IList<FileTypeInfo> FileTypeGetList()
        {
            IList<FileTypeInfo> ilist = new List<FileTypeInfo>();
            DataTable dt = new DataTable();

            string sql = "select * from Cm_FileType order by ParentID,ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, null);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FileTypeInfo info = new FileTypeInfo();
                info.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[i]["ParentID"].ToString());
                info.Code = dt.Rows[i]["Code"].ToString();
                info.Alias = dt.Rows[i]["Alias"].ToString();
                info.Title = dt.Rows[i]["Title"].ToString();
                info.Remark = dt.Rows[i]["Remark"].ToString();
                ilist.Add(info);
            }
            return ilist;
        }

        public IList<FileTypeInfo> FileTypeByParentIDGetList(int ParentID)
        {
            IList<FileTypeInfo> ilist = new List<FileTypeInfo>();
            DataTable dt = new DataTable();

            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("@ParentID", OleDbType.VarWChar, 20);
            param[0].Value = ParentID;

            string sql = "select * from Cm_FileType where ParentID=@ParentID order by ParentID,ID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, param);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FileTypeInfo info = new FileTypeInfo();
                info.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                info.ParentID = int.Parse(dt.Rows[i]["ParentID"].ToString());
                info.Code = dt.Rows[i]["Code"].ToString();
                info.Alias = dt.Rows[i]["Alias"].ToString();
                info.Title = dt.Rows[i]["Title"].ToString();
                info.Remark = dt.Rows[i]["Remark"].ToString();
                ilist.Add(info);
            }
            return ilist;
        }

        public string FileType_GetPath(int ID)
        {
            string path = "";
            IList<FileTypeInfo> alist = FileTypeGetList();
            FileTypeInfo info = FileTypeGetInfo(ID);
            IList<FileTypeInfo> ilist = new List<FileTypeInfo>();
            GetFileTypeList(alist, ilist, info);
            int count=ilist.Count;
            for (int i = count; i > 0; i--)
            {
                if (path == "")
                {
                    path = ilist[i - 1].Alias;
                }
                else
                {
                    path = path + "\\" + ilist[i - 1].Alias;
                }
            }
            return path;
        }

        private void GetFileTypeList(IList<FileTypeInfo> alist, IList<FileTypeInfo> ilist, FileTypeInfo ninfo)
        {
            ilist.Add(ninfo);
            var list = from tl in alist
                       where tl.ID == ninfo.ParentID
                       select tl;
            foreach (FileTypeInfo info in list)
            {
                GetFileTypeList(alist,ilist, info);
            }
        }

    }
}
