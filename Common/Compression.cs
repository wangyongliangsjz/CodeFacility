using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Common
{
    public class Compression
    {
        /// <summary>
        /// 压缩ACCESS数据库
        /// </summary>
        /// <param name="DBPath">ACCESS数据库</param>
        /// <param name="TempPath">ACCESS临时数据库</param>
        public static int CompactAccessDB(string DBPath, int DBSize, string TempPath, out string rstmsg)
        {
            int rst = 0;
            rstmsg = "";

            FileInfo fi = new FileInfo(DBPath);//file是路径；
            //1GB=1073741824,1MB=1048576,1KB=1024
            int size = Convert.ToInt32(fi.Length / 1048576);
            if (size < DBSize)
            {
                rstmsg = "ACCESS数据库小于" + DBSize.ToString() + "MB，不需要压缩！";
                return rst;
            }

            //string DBPath = AppDomain.CurrentDomain.BaseDirectory + @"Data\ECTRTXMsg.mdb";
            //string TempPath = AppDomain.CurrentDomain.BaseDirectory + @"Data\Temp.mdb";

            string config1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBPath + ";Persist Security Info=True;";
            string config2 = @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=0;Jet OLEDB:Database Password=;Data Source=" + TempPath + ";Password=;Jet OLEDB:Engine Type=5;Jet OLEDB:Global Bulk Transactions=1;Provider=\"Microsoft.Jet.OLEDB.4.0\";Jet OLEDB:System database=;Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False";
            //创建Jet引擎对象
            object objJetEngine = Activator.CreateInstance(Type.GetTypeFromProgID("JRO.JetEngine"));

            //设置参数数组
            //根据你所使用的Access版本修改"JetOLEDB:EngineType=5"中的数字.
            //5对应JET4X格式(access2000,2002)

            object[] objParams = new object[]{
            String.Format(config1),//输入连接字符串
            String.Format(config2)//输出连接字符串
            };

            if (File.Exists(TempPath))
            {
                System.IO.File.Delete(TempPath);
            }

            try
            {
                //通过反射调用CompactDatabase方法
                objJetEngine.GetType().InvokeMember("CompactDatabase", System.Reflection.BindingFlags.InvokeMethod, null, objJetEngine, objParams);

                //删除原数据库文件
                System.IO.File.Delete(DBPath);
                //重命名压缩后的数据库文件
                System.IO.File.Move(TempPath, DBPath);
                //释放Com组件
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objJetEngine);
                objJetEngine = null;
                rst = 1;
                rstmsg = "压缩ACCESS数据库成功！";
            }
            catch (Exception ex)
            {
                rst = -1;
                rstmsg = "压缩ACCESS数据库失败！"+ex.Message;
            }

            return rst;

        }


    }
}
