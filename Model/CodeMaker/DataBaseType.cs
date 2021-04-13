using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CodeMaker
{

        /// <summary>
        /// 对象填充类型
        /// </summary>
        public enum DataBaseTypeEnum
        {
            /// <summary>
            /// 无样式
            /// </summary>
            None=0,
            /// <summary>
            /// 从SQLSERVER数据库填充对象
            /// </summary>
            SQLServer=1,
            /// <summary>
            /// 从ORACLE数据库填充对象
            /// </summary>
            Oracle=2,
            /// <summary>
            /// 从MySql数据库填充对象
            /// </summary>
            MySql = 3,
            /// <summary>
            /// 从Access数据库填充对象
            /// </summary>
            Access=4,
            /// <summary>
            /// 从SqlLite数据库填充对象
            /// </summary>
            SQLite = 5,
            /// <summary>
            /// 从PDM文件填充对象 PowerDesigner
            /// </summary>
            //PDM=6,
            /// <summary>
            /// MongoDB数据库
            /// </summary>
            //MongoDB=7,
            /// <summary>
            /// Redis数据库
            /// </summary>
            //Redis=8
        }

    public class DataBaseType
    {
        public static Dictionary<int, string> GetDicDataBaseType()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (DataBaseTypeEnum item in Enum.GetValues(typeof(DataBaseTypeEnum)))
            {
                dic.Add((int)item, item.ToString());
            }
            return dic.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value);
        }

        public static DataBaseTypeEnum GetDataBaseType(int id)
        {
            DataBaseTypeEnum fc = DataBaseTypeEnum.None;
            switch (id)
            {
                case (int)DataBaseTypeEnum.None:
                    fc = DataBaseTypeEnum.None;
                    break;
                case (int)DataBaseTypeEnum.SQLServer:
                    fc = DataBaseTypeEnum.SQLServer;
                    break;
                case (int)DataBaseTypeEnum.Oracle:
                    fc = DataBaseTypeEnum.Oracle;
                    break;
                case (int)DataBaseTypeEnum.Access:
                    fc = DataBaseTypeEnum.Access;
                    break;
                case (int)DataBaseTypeEnum.SQLite:
                    fc = DataBaseTypeEnum.SQLite;
                    break;
                case (int)DataBaseTypeEnum.MySql:
                    fc = DataBaseTypeEnum.MySql;
                    break;
                //case (int)DataBaseTypeEnum.PDM:
                //    fc = DataBaseTypeEnum.PDM;
                //    break;
                //case (int)DataBaseTypeEnum.MongoDB:
                //    fc = DataBaseTypeEnum.MongoDB;
                //    break;
                //case (int)DataBaseTypeEnum.Redis:
                //    fc = DataBaseTypeEnum.Redis;
                //    break;
            }
            return fc;
        }
    }
}
