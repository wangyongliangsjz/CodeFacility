using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class EventInfo
    {
        private string _code;
        private string _title;
        private string _eventtype;

        /// <summary>
        /// 事件编码
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 事件
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 事件类型
        /// </summary>
        public string EventType
        {
            set { _eventtype = value; }
            get { return _eventtype; }
        }
    }
}
