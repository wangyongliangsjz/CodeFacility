using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CodeMaker
{
    public enum DbDataTypeEnum
    {
        /// <summary>
        /// 服务器
        /// </summary>
        服务器=1,
        /// <summary>
        /// 数据库
        /// </summary>
        数据库=2,
        /// <summary>
        /// 表
        /// </summary>
        表=3,
        /// <summary>
        /// 视图
        /// </summary>
        视图=4,
        /// <summary>
        /// 存储过程
        /// </summary>
        存储过程=5,
        /// <summary>
        /// 表列名
        /// </summary>
        列=6,
        /// <summary>
        /// 存储过程参数
        /// </summary>
        参数=7
    }

    public class DbDataType
    {
        public static DbDataTypeEnum GetDbDataType(int id)
        {
            DbDataTypeEnum ddt = DbDataTypeEnum.服务器;
            switch (id)
            {
                case (int)DbDataTypeEnum.服务器:
                    ddt = DbDataTypeEnum.服务器;
                    break;
                case (int)DbDataTypeEnum.数据库:
                    ddt = DbDataTypeEnum.数据库;
                    break;
                case (int)DbDataTypeEnum.表:
                    ddt = DbDataTypeEnum.表;
                    break;
                case (int)DbDataTypeEnum.视图:
                    ddt = DbDataTypeEnum.视图;
                    break;
                case (int)DbDataTypeEnum.存储过程:
                    ddt = DbDataTypeEnum.存储过程;
                    break;
                case (int)DbDataTypeEnum.列:
                    ddt = DbDataTypeEnum.列;
                    break;
                case (int)DbDataTypeEnum.参数:
                    ddt = DbDataTypeEnum.参数;
                    break;
            }
            return ddt;
        }
    }
}
