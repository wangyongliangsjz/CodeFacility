using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CodeFacility.CodeMaker
{
    /// <summary>
    /// 基础配置
    /// </summary>
    public class BaseConfigure
    {
        #region 工具
        #region 选项

        private static Color _colortheme = Color.Gainsboro;
        /// <summary>
        /// 颜色主题
        /// </summary>
        public static Color ColorTheme
        {
            get { return _colortheme; }
            set { _colortheme = value; }
        }
        private static int _fontsize = 12;
        /// <summary>
        /// 字体大小
        /// </summary>
        public static int FontSize
        {
            get { return _fontsize; }
            set { _fontsize = value; }
        }
        private static string _fonttypeface;
        public static string FontTypeface
        {
            get { return "宋体"; }
            set { _fonttypeface = value; }
        } 
        #endregion
        #endregion
    }
}
