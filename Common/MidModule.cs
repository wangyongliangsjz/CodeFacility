using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 定义一个信息委托
    /// </summary>
    /// <param name="sender">发布者</param>
    /// <param name="msg">发送内容</param>
    public delegate void DataDlg(object sender, object data, object e);

    /// <summary>
    /// winform通过事件传参数
    /// </summary>
    public class MidModule
    {
        /// <summary>
        /// 消息发送事件
        /// </summary>
        public static event DataDlg EventSend;
        public static void SendData(object sender, object data, object e)
        {
            if (EventSend != null)
            {
                EventSend(sender, data, e);
            }
        }
    }
}
