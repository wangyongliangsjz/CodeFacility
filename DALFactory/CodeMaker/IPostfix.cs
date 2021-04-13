using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.CodeMaker;

namespace DALFactory.CodeMaker
{
     public interface IPostfix
    {
         IList<PostfixInfo> PostfixGetList();
    }
}
