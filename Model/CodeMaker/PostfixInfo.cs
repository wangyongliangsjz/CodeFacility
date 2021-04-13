using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CodeMaker
{
    public class PostfixInfo
    {
        private int _id;
        private string _postfix;
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public string Postfix
        {
            set { _postfix = value; }
            get { return _postfix; }
        }
    }
}
