using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Data;


namespace Common
{
    public class FileHandle
    {
        #region 文件操作类

        /// <summary>
        /// 写文件新建或替换原文件
        /// </summary>
        /// <param name="info">文件内容</param>
        /// <param name="FilePath">路径</param>
        public static int WirteCreateFile(string Content, string FilePath)
        {
            int rst = 0;
            try
            {
                FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);//创建写入文件 
               
                //使用“另存为”对话框中输入的文件名实例化StreamWriter对象
                //StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("GB2312"));
                StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("utf-8"));
                //向创建的文件中写入内容
                Content = Content.Replace("\n", "\r\n");
                Content = Content.Replace("\r\r\n", "\r\n");
                sw.Write(Content);
                //sw.WriteLine(Content); //换行

                //关闭当前文件写入流
                sw.Close();
                fs.Close();
                rst = 1;
            }
            catch
            {
                rst = -1;
            }
            return rst;
        }

        /// <summary>
        /// 写文件追加
        /// </summary>
        /// <param name="info">文件内容</param>
        /// <param name="FilePath">路径</param>
        public static int WriteLineFile(string Content, string FilePath)
        {
            int rst = 0;
            try
            {
                if (!File.Exists(FilePath))
                {
                    FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);//创建写入文件 
                    fs.Close();
                }

                if (System.IO.File.Exists(FilePath))
                {
                    //使用“另存为”对话框中输入的文件名实例化StreamWriter对象
                    //StreamWriter sw = new StreamWriter(FilePath, true, Encoding.GetEncoding("GB2312"));
                    StreamWriter sw = new StreamWriter(FilePath, true, Encoding.GetEncoding("utf-8"));
                    //向创建的文件中写入内容
                    sw.WriteLine(Content);

                    //关闭当前文件写入流
                    sw.Close();
                    rst = 1;
                }
            }
            catch
            {
                rst = -1;
            }
            return rst;
        }

        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="FilePath">路径</param>
        /// <returns>内容</returns>
        public static string ReadFile(string FilePath)
        {
            string Content = "";
            if (System.IO.File.Exists(FilePath))
            {
                Content = File.ReadAllText(FilePath, Encoding.GetEncoding("GB2312"));
            }
            return Content;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FilePath">路径</param>
        public static void DeleteFile(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
        #endregion

        #region 日志记录

        /// <summary>
        /// 写程序运行日志txt类型
        /// </summary>
        /// <param name="Content"></param>
        public static void WirteLog(string Content)
        {
            DateTime dtime = DateTime.Now;
            string ym = string.Format("{0:yyyyMM}",dtime);
            string ymd = string.Format("{0:yyyyMMdd}", dtime);
            string FolderPath = AppDomain.CurrentDomain.BaseDirectory + @"Log\Log" + ym;
            string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"Log\Log" + ym + @"\Log" + ymd + ".txt";

            //if (File.Exists(FilePath))
            //{
            //    string FilePathNext = AppDomain.CurrentDomain.BaseDirectory + @"Log\Log" + DateTime.Now.ToShortDateString() + ".txt";
            //    FileInfo fi = new FileInfo(FilePath);//file是路径
            //    //1GB=1073741824,1MB=1048576,1KB=1024
            //    int size = Convert.ToInt32(fi.Length / 1048576);
            //    if (size > 10)
            //    {
            //        //Log.txt文件大于10MB备份
            //        File.Move(FilePath, FilePathNext);
            //    }
            //}

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            if (!File.Exists(FilePath))
            {
                FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);//创建写入文件 
                fs.Close();                
            }

            if (System.IO.File.Exists(FilePath))
            {
                //使用“另存为”对话框中输入的文件名实例化StreamWriter对象
                StreamWriter sw = new StreamWriter(FilePath, true);

                //向创建的文件中写入内容
                sw.WriteLine("\r\n" + DateTime.Now.ToString() + "\r\n" + Content);

                //关闭当前文件写入流
                sw.Close();
            }
        }
        
        #endregion

        #region 获取文件或压缩文件路径

        /// <summary>
        /// 压缩文件并返回压缩文件的路径
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <param name="ZipFilePath">压缩文件所在文件夹路径</param>
        /// <param name="ZipFileName">压缩文件路径</param>
        /// <param name="FileSize">压缩文件大小</param>
        /// <param name="rstmsg"></param>
        /// <returns></returns>
        public static string GetFilePath(string FilePath, string ZipFilePath, string ZipFileName, int FileSize, out string rstmsg)
        {
            rstmsg = "";
            string rtPath = "";

            if (File.Exists(FilePath))
            {
                FileInfo fi = new FileInfo(FilePath);
                //1GB=1073741824,1MB=1048576,1KB=1024
                int size =Convert.ToInt32( fi.Length / 1048576);
                if (size < FileSize)
                {
                    rtPath = FilePath;
                }
                else
                {
                    if (File.Exists(ZipFileName))
                    {
                        File.Delete(ZipFileName);
                    }

                    if (ReduceWinrar.Exists())
                    {
                        ReduceWinrar rw = new ReduceWinrar();
                        try
                        {
                            rw.CompressRAR(FilePath, ZipFilePath, ZipFileName);
                            if (File.Exists(ZipFileName))
                            {
                                rtPath = ZipFileName;
                            }
                        }
                        catch (Exception ex)
                        {
                            WirteLog(ex.Message);
                        }
                    }
                }
                

            }

            return rtPath;

        }
        #endregion

        #region DataSet导出到txt文件
        /// <summary> 
        /// DataSet导出到txt文件 
        /// </summary> 
        /// <param name="ds">数据集Dataset</param> 
        ///<param name="filePath">txt文件目录</param>
        public static int DataSetToTxt(DataSet ds, string filePath,out string rstmsg)
        {
            int rst = 0;
            rstmsg = "";
            if (ds == null)
                return rst;

            if (ds.Tables.Count != 0)
            {
                //创建一个.txt文件
                FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter textFile = new StreamWriter(fs, Encoding.GetEncoding("GB2312"));

                //把Dataset中的数据写入.txt文件中 
                for (int l = 0; l < ds.Tables.Count; l++)
                {
                    //统计dataset中当前表的行数 
                    int row = ds.Tables[l].Rows.Count;
                    //统计dataset中当前表的列数 
                    int column = ds.Tables[l].Columns.Count;

                    for (int i = 0; i < column; i++)
                    {
                        string fname = ds.Tables[l].Columns[i].ColumnName;
                        if (i == 0)
                        {
                            textFile.Write(fname);
                        }
                        else
                        {
                            textFile.Write("\t"+fname);
                        }
                    }
                    textFile.WriteLine();
                    //把dataset中当前表的数据写入.txt文件中 
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < column; j++)
                        {
                            string fname = ds.Tables[l].Rows[i][j].ToString();
                            if (j == 0)
                                textFile.Write(fname);
                            else
                                textFile.Write("\t"+fname);

                        }
                        textFile.WriteLine();
                    }
                    textFile.WriteLine();

                }
                //关闭当前的StreamWriter流 
                textFile.Close();
                rst = 1;
                rstmsg = "保存成功。";
            }
            else
            {
                rst = -1;
                rstmsg = "数据为空。";
            }
            return rst;
        }

        #endregion 

        #region txt文件转DataSet
        /// <summary>
        /// 文件加载
        /// </summary>
        /// <param name="FilePath">带文件名的路径</param>
        /// <param name="TableName">自定义的表名</param>
        /// <param name="FieldsInArray">自定义的表字段</param>
        /// <returns>DataSet</returns>
        public static DataSet TxtToDataSet(string FilePath)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            FileStream fs = File.Open(FilePath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("GB2312"));

            string strRead;
            bool flag = true;
            int len = 0;
            while (flag)
            {
                strRead = sr.ReadLine();
                
                if (!string.IsNullOrEmpty(strRead))
                {
                    //string[] rowVale = strRead.Split('\n');
                    //for (int i = 0; i < rowVale.Length; i++)
                    //{
                        string[] aryVale = strRead.Split('\t');
                        DataRow dr = dt.NewRow();
                        if (len == 0)
                        {
                            for (int k = 0; k < aryVale.Length; k++)
                            {
                                dt.Columns.Add(aryVale[k]);
                            }
                        }
                        else
                        {
                            for (int k = 0; k < aryVale.Length; k++)
                            {
                                dr[k] = aryVale[k];
                            }
                            dt.Rows.Add(dr);
                        }
                        
                    //}
                    len++;
                }
                else
                {
                    flag = false;
                }
            }

            ds.Tables.Add(dt);
            return ds;

        }
        #endregion

    }
}
