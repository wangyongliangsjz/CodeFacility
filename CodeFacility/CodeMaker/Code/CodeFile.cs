using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using DALFactory.CodeMaker;
using AccessDal.CodeMaker;
using Model.CodeMaker;

namespace CodeFacility.CodeMaker
{
    public class CodeFile
    {
        static string rootFolder = GetFolder(Model.Folder.代码生成器);
        public static string GetFilePath(TempletTypeInfo info)
        {
            string filepath = "";
            IFileType ftdal = new AccessDal.CodeMaker.FileType();
            ITempletType tdal = new AccessDal.CodeMaker.TempletType();

            string code = info.Code.Substring(0, 3);
            TempletTypeInfo tyinfo = tdal.TempletTypeByCodeGetList(code);

            string path = ftdal.FileType_GetPath(tyinfo.FileTypeID);
            filepath = rootFolder+"\\"+path + "\\" + info.Code;
            string fpath = AppDomain.CurrentDomain.BaseDirectory + filepath;
            if (!Directory.Exists(fpath))
            {
                Directory.CreateDirectory(fpath);
            }

            return filepath;
        }

        public static string GetFilePath(int ID)
        {
            string filepath = "";
            IFileType ftdal = new AccessDal.CodeMaker.FileType();
            ITempletType tdal = new AccessDal.CodeMaker.TempletType();
            ITemplet dal = new AccessDal.CodeMaker.Templet();
            TempletInfo tinfo = dal.TempletGetInfo(ID);
            TempletTypeInfo info = tdal.TempletTypeGetInfo(tinfo.ParentID);

            string code = info.Code.Substring(0, 3);
            TempletTypeInfo tyinfo = tdal.TempletTypeByCodeGetList(code);

            string path = ftdal.FileType_GetPath(tyinfo.FileTypeID);
            filepath = rootFolder+"\\"+path + "\\" + info.Code;
            return filepath;
        }

        public static string GetFolder(Model.Folder fd)
        {
            string rootFolder = "";
            if (fd == Model.Folder.代码生成器) rootFolder = "CodeMaker";
            return rootFolder;
        }


    }
}
