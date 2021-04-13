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
    public class UpLoadFile : DbBase, IUpLoadFile
    {
        public string constring = DBConfig.GetConString("AccessCodeMaker");
        public int UpLoadFile_Add(UpLoadFileInfo info)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[13];
                param[0] = new OleDbParameter("@TableName", OleDbType.VarWChar, 30);
                param[0].Value = info.TableName;
                param[1] = new OleDbParameter("@RootField", OleDbType.VarWChar, 20);
                param[1].Value = info.RootField;
                param[2] = new OleDbParameter("@RootId", OleDbType.VarWChar, 20);
                param[2].Value = info.RootId;
                param[3] = new OleDbParameter("@ParentField", OleDbType.VarWChar, 20);
                param[3].Value = info.ParentField;
                param[4] = new OleDbParameter("@ParentId", OleDbType.VarWChar, 20);
                param[4].Value = info.ParentId;
                param[5] = new OleDbParameter("@FileName", OleDbType.VarWChar, 255);
                param[5].Value = info.FileName;
                param[6] = new OleDbParameter("@FilePath", OleDbType.VarWChar, 50);
                param[6].Value = info.FilePath;
                param[7] = new OleDbParameter("@ExpandName", OleDbType.VarWChar, 20);
                param[7].Value = info.ExpandName;
                param[8] = new OleDbParameter("@FileSize", OleDbType.Integer);
                param[8].Value = info.FileSize;
                param[9] = new OleDbParameter("@Remark", OleDbType.VarWChar, 100);
                param[9].Value = info.Remark;
                param[10] = new OleDbParameter("@FileTypeId", OleDbType.Integer);
                param[10].Value = info.FileTypeId;
                param[11] = new OleDbParameter("@UniqueName", OleDbType.VarWChar, 100);
                param[11].Value = info.UniqueName;
                param[12] = new OleDbParameter("@AttachmentId", OleDbType.VarWChar, 50);
                param[12].Value = info.AttachmentId;

                string sql = "insert into Cm_UpLoadFile (TableName,RootField,RootId,ParentField,ParentId,FileName,FilePath,ExpandName,FileSize,Remark,FileTypeId,UniqueName,AttachmentId,UploadDate) values (@TableName,@RootField,@RootId,@ParentField,@ParentId,@FileName,@FilePath,@ExpandName,@FileSize,@Remark,@FileTypeId,@UniqueName,@AttachmentId,now())";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch (Exception ex)
            {
                rst = -1;
            }

            return rst;
        }

        public int UpLoadFile_Edit(UpLoadFileInfo info)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[14];
                param[0] = new OleDbParameter("@TableName", OleDbType.VarWChar, 30);
                param[0].Value = info.TableName;
                param[1] = new OleDbParameter("@RootField", OleDbType.VarWChar, 20);
                param[1].Value = info.RootField;
                param[2] = new OleDbParameter("@RootId", OleDbType.VarWChar, 20);
                param[2].Value = info.RootId;
                param[3] = new OleDbParameter("@ParentField", OleDbType.VarWChar, 20);
                param[3].Value = info.ParentField;
                param[4] = new OleDbParameter("@ParentId", OleDbType.VarWChar, 20);
                param[4].Value = info.ParentId;
                param[5] = new OleDbParameter("@FileName", OleDbType.VarWChar, 255);
                param[5].Value = info.FileName;
                param[6] = new OleDbParameter("@FilePath", OleDbType.VarWChar, 50);
                param[6].Value = info.FilePath;
                param[7] = new OleDbParameter("@ExpandName", OleDbType.VarWChar, 20);
                param[7].Value = info.ExpandName;
                param[8] = new OleDbParameter("@FileSize", OleDbType.Integer);
                param[8].Value = info.FileSize;
                param[9] = new OleDbParameter("@Remark", OleDbType.VarWChar, 100);
                param[9].Value = info.Remark;
                param[10] = new OleDbParameter("@FileTypeId", OleDbType.Integer);
                param[10].Value = info.FileTypeId;
                param[11] = new OleDbParameter("@UniqueName", OleDbType.VarWChar, 100);
                param[11].Value = info.UniqueName;
                param[12] = new OleDbParameter("@AttachmentId", OleDbType.VarWChar, 50);
                param[12].Value = info.AttachmentId;
                param[0] = new OleDbParameter("@FileID", OleDbType.VarWChar, 50);
                param[0].Value = info.FileID;
                
                string sql = "update Cm_UpLoadFile set TableName=@TableName,RootField=@RootField,RootId=@RootId,ParentField=@ParentField,ParentId=@ParentId,FileName=@FileName,FilePath=@FilePath,ExpandName=@ExpandName,FileSize=@FileSize,Remark=@Remark,FileTypeId=@FileTypeId,UniqueName=@UniqueName,AttachmentId=@AttachmentId,UploadDate=now() where FileID=@FileID";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }

        public int UpLoadFile_Del(int FileID)
        {
            int rst = 0;
            try
            {
                OleDbParameter[] param = new OleDbParameter[1];
                param[0] = new OleDbParameter("@FileID", OleDbType.VarWChar, 20);
                param[0].Value = FileID;


                string sql = "delete from Cm_UpLoadFile where FileID=@FileID";
                rst = DbHelper.ExecuteNonQuery(constring, CommandType.Text, sql, param);
            }
            catch
            {
                rst = -1;
            }

            return rst;
        }
        public UpLoadFileInfo UpLoadFileGetInfo(int FileID)
        {
            UpLoadFileInfo info = new UpLoadFileInfo();
            DataTable dt = new DataTable();
            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("@FileID", OleDbType.VarWChar, 20);
            param[0].Value = FileID;

            string sql = "select * from Cm_UpLoadFile where FileID=@FileID";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, param);
            if (dt.Rows.Count > 0)
            {
                info.FileID = Convert.ToInt32(dt.Rows[0]["FileID"].ToString());
                info.TableName = dt.Rows[0]["TableName"].ToString();
                info.RootField = dt.Rows[0]["RootField"].ToString();
                info.RootId = dt.Rows[0]["RootId"].ToString();
                info.ParentField = dt.Rows[0]["ParentField"].ToString();
                info.ParentId = dt.Rows[0]["ParentId"].ToString();
                info.FileName = dt.Rows[0]["FileName"].ToString();
                info.FilePath = dt.Rows[0]["FilePath"].ToString();
                info.ExpandName = dt.Rows[0]["ExpandName"].ToString();
                info.FileSize = int.Parse(dt.Rows[0]["FileSize"].ToString());
                info.Remark = dt.Rows[0]["Remark"].ToString();
                info.FileTypeId = int.Parse(dt.Rows[0]["FileTypeId"].ToString());
                info.UniqueName = dt.Rows[0]["UniqueName"].ToString();
                info.AttachmentId = dt.Rows[0]["AttachmentId"].ToString();
                info.UploadDate = Convert.ToDateTime(dt.Rows[0]["UploadDate"].ToString());
            }
            return info;
        }

        public UpLoadFileInfo UpLoadFileGetInfo(string TableName, int ParentId)
        {
            UpLoadFileInfo info = new UpLoadFileInfo();
            DataTable dt = new DataTable();
            OleDbParameter[] param = new OleDbParameter[2];
            param[0] = new OleDbParameter("@TableName", OleDbType.VarWChar, 30);
            param[0].Value = TableName;
            param[1] = new OleDbParameter("@ParentId", OleDbType.VarWChar, 100);
            param[1].Value = ParentId;

            string sql = "select * from Cm_UpLoadFile where TableName=@TableName and ParentId=@ParentId";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, param);
            if (dt.Rows.Count > 0)
            {
                info.FileID = Convert.ToInt32(dt.Rows[0]["FileID"].ToString());
                info.TableName = dt.Rows[0]["TableName"].ToString();
                info.RootField = dt.Rows[0]["RootField"].ToString();
                info.RootId = dt.Rows[0]["RootId"].ToString();
                info.ParentField = dt.Rows[0]["ParentField"].ToString();
                info.ParentId = dt.Rows[0]["ParentId"].ToString();
                info.FileName = dt.Rows[0]["FileName"].ToString();
                info.FilePath = dt.Rows[0]["FilePath"].ToString();
                info.ExpandName = dt.Rows[0]["ExpandName"].ToString();
                info.FileSize = int.Parse(dt.Rows[0]["FileSize"].ToString());
                info.Remark = dt.Rows[0]["Remark"].ToString();
                info.FileTypeId = int.Parse(dt.Rows[0]["FileTypeId"].ToString());
                info.UniqueName = dt.Rows[0]["UniqueName"].ToString();
                info.AttachmentId = dt.Rows[0]["AttachmentId"].ToString();
                info.UploadDate = Convert.ToDateTime(dt.Rows[0]["UploadDate"].ToString());
            }
            return info;
        }

        public IList<UpLoadFileInfo> UpLoadFileGetList()
        {
            IList<UpLoadFileInfo> ilist = new List<UpLoadFileInfo>();            
            DataTable dt = new DataTable();

            string sql = "select * from Cm_UpLoadFile";
            dt = DbHelper.ExecuteTable(constring, CommandType.Text, sql, null);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UpLoadFileInfo info = new UpLoadFileInfo();
                info.FileID = Convert.ToInt32(dt.Rows[i]["FileID"].ToString());
                info.TableName = dt.Rows[i]["TableName"].ToString();
                info.RootField = dt.Rows[i]["RootField"].ToString();
                info.RootId = dt.Rows[i]["RootId"].ToString();
                info.ParentField = dt.Rows[i]["ParentField"].ToString();
                info.ParentId = dt.Rows[i]["ParentId"].ToString();
                info.FileName = dt.Rows[i]["FileName"].ToString();
                info.FilePath = dt.Rows[i]["FilePath"].ToString();
                info.ExpandName = dt.Rows[i]["ExpandName"].ToString();
                info.FileSize = int.Parse(dt.Rows[i]["FileSize"].ToString());
                info.Remark = dt.Rows[i]["Remark"].ToString();
                info.FileTypeId = int.Parse(dt.Rows[i]["FileTypeId"].ToString());
                info.UniqueName = dt.Rows[i]["UniqueName"].ToString();
                info.AttachmentId = dt.Rows[i]["AttachmentId"].ToString();
                info.UploadDate = Convert.ToDateTime(dt.Rows[i]["UploadDate"].ToString());
                ilist.Add(info);
            }
            return ilist;
        }
    }
}
