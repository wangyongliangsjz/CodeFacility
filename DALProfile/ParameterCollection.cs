using System;
using System.Data;
using System.Collections;

namespace DALProfile
{
    /// <summary>
    ///功能描述：支持远程跨处理应用程序域边界访问对象的查询参数集合
    /// </summary>
    public sealed class ParameterCollection : MarshalByRefObject
    {
        /// <summary>
        /// 初始接受能力数量，默认为10个
        /// </summary>
        int intitialCapacity = 10;

        /// <summary>
        /// 定义大小可动态改变的数据接口,该类的索引器属性
        /// </summary>
        private ArrayList items;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ParameterCollection()
        {
            //
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="initCapacity">容量个数</param>
        public ParameterCollection(int initCapacity)
        {
            intitialCapacity = initCapacity;
        }
        #endregion


        #region 定义属性
        /// <summary>
        /// 总数量
        /// </summary>
        public int Count
        {
            get
            {
                if (this.items == null)
                {
                    return 0;
                }
                return this.items.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public QueryParameter this[int index]
        {
            get
            {
                this.RangeCheck(index);
                return ((QueryParameter)this.items[index]);
            }
            set
            {
                this.RangeCheck(index);
                this.Replace(index, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParameterName"></param>
        /// <returns></returns>
        public QueryParameter this[string ParameterName]
        {
            get
            {
                int num1 = this.RangeCheck(ParameterName);
                return ((QueryParameter)this.items[num1]);
            }
            set
            {
                int num1 = this.RangeCheck(ParameterName);
                this.Replace(num1, value);
            }
        }
        #endregion

        private static string _parameterType;

        public string SetParameterType
        {
            set { _parameterType = value; }
        }

        /// <summary>
        /// 定义大小可动态改变的数据接口
        /// </summary>
        /// <returns></returns>
        private ArrayList ArrayList()
        {
            if (this.items == null)
            {
                // 数组大小个数等于容量个数
                this.items = new ArrayList(intitialCapacity);
            }
            return this.items;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns>返回参数集合</returns>
        public QueryParameter Add(QueryParameter param)
        {
            this.ArrayList().Add(param);
            return param;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="Value">参数值</param>
        /// <returns>返回参数集合</returns>
        public QueryParameter Add(string ParameterName, object Value)
        {
            return this.Add(new QueryParameter(ParameterName, Value, _parameterType));
        }

        public QueryParameter Add(string ParameterName, DbType dbType, ParameterDirection direction, object Value)
        {
            return this.Add(new QueryParameter(ParameterName, dbType, direction, Value));
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="Value">参数值</param>
        /// <param name="dbType">参数类型</param>
        /// <returns>返回参数集合</returns>
        public QueryParameter Add(string ParameterName, object Value, DbType dbType)
        {
            return this.Add(new QueryParameter(ParameterName, Value, dbType));
        }

        /// <summary>
        /// 替换参数
        /// </summary>
        /// <param name="index">索引开始位置</param>
        /// <param name="newValue">新的集合参数</param>
        private void Replace(int index, QueryParameter newValue)
        {
            this.Validate(index, newValue);
            this.items[index] = newValue;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        private void ValidateType(object Value)
        {
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="Value"></param>
        private void Validate(int index, QueryParameter Value)
        {
            //
        }

        /// <summary>
        /// 数据跃界检测
        /// </summary>
        /// <param name="index"></param>
        private void RangeCheck(int index)
        {
            if ((index < 0) || (this.Count <= index))
            {
                throw new IndexOutOfRangeException("Number " + index.ToString() + " is out of Range");
            }
        }

        /// <summary>
        /// 数据跃界检测
        /// </summary>
        /// <param name="ParameterName"></param>
        /// <returns></returns>
        private int RangeCheck(string ParameterName)
        {
            int num1;
            num1 = this.IndexOf(ParameterName);
            if (num1 < 0)
            {
                throw new IndexOutOfRangeException("ParameterName " + ParameterName + " dose not exist");
            }
            return num1;
        }

        /// <summary>
        /// 第一个匹配项的索引
        /// </summary>
        /// <param name="ParameterName"></param>
        /// <returns></returns>
        public int IndexOf(string ParameterName)
        {
            int index = -1;
            if (this.items != null)
            {
                for (int i = 0; i < this.items.Count; i++)
                {
                    if (((QueryParameter)items[i]).ParameterName.Equals(ParameterName))
                    {
                        index = i;
                        break;
                    }
                }
            }
            return index;
        }

        /// <summary>
        /// 清空数组数据
        /// </summary>
        public void Clear()
        {
            this.ArrayList().Clear();
        }

    }
}
