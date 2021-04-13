using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net;

namespace Common
{
    public class Security
    {

        #region 检验黑客SQL注入函数 [='/<>-*]
        public static bool CheckSqlImmitParams(params object[] args)
        {
            string[] Lawlesses = { "=", "'", "<", ">" };
            if (Lawlesses == null || Lawlesses.Length <= 0)
                return true;
            // 构造正则表达式,例:Lawlesses是=号和'号,则正则表达式为 .*[=}'].*  
            string str_Regex = ".*[";
            for (int i = 0; i < Lawlesses.Length - 1; i++)
                str_Regex += Lawlesses[i] + "|";
            str_Regex += Lawlesses[Lawlesses.Length - 1] + "].*";
            foreach (object arg in args)
            {
                if (arg is string)//如果是字符串,直接检查        
                {
                    if (Regex.Matches(arg.ToString(), str_Regex).Count > 0)
                        return false;
                }
                else if (arg is ICollection)//如果是一个集合,则检查集合内元素是否字符串,是字符串,就进行检查       
                {
                    foreach (object obj in (ICollection)arg)
                    {
                        if (obj is string)
                        {
                            if (Regex.Matches(obj.ToString(), str_Regex).Count > 0)
                                return false;
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        #region 密码加密解密

        #region base64位加密
        /// <summary>
        /// base64位加密
        /// </summary>
        /// <param name="strInput">字符串</param>
        /// <returns>STRING</returns>
        public static string ToBase64Encrypt(string strInput)
        {
            if (strInput.Trim() == string.Empty)
                return null;
            //加密
            byte[] msg = Encoding.Default.GetBytes(strInput);
            string strSend = Convert.ToBase64String(msg);
            byte[] SMS = Encoding.Default.GetBytes(strSend);
            return Encoding.Default.GetString(SMS); //返回信息
        }
        #endregion

        #region base64位解密
        /// <summary>
        /// base64位解密
        /// </summary>
        /// <param name="strInput">字符串</param>
        /// <returns>STRING</returns>
        public static string FormBase64Encrypt(string strInput)
        {
            if (strInput.Trim() == string.Empty)
                return null;
            byte[] by = Convert.FromBase64String(strInput);
            return Encoding.Default.GetString(by); //返回信息
        }
        #endregion

        #endregion

        #region 获取服务器名称
        public static string GetServerName()
        {
            string ServerName = "";
            ServerName = Dns.GetHostName();//获得本机名
            return ServerName;
        }
        #endregion

        #region 获取服务器名称
        public static string GetServerIP()
        {
            string ServerName = "";
            ServerName = Dns.GetHostName();//获得本机名
            IPHostEntry myhost = Dns.GetHostByName(ServerName);
            string ServerIP = myhost.AddressList[0].ToString();//显示IP地址
            return ServerIP;
        }
        #endregion

    }
}
