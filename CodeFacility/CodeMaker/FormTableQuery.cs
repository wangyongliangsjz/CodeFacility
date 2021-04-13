using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DALFactory.CodeMaker;
using AccessDal.CodeMaker;
using Model.CodeMaker;

namespace CodeFacility.CodeMaker
{
    public partial class FormTableQuery : FormWin
    {
        DbDataInfo dinfo;
        public FormTableQuery()
        {
            InitializeComponent();
            //订阅了信息发送事件，即接受参数值
            Common.MidModule.EventSend += new Common.DataDlg(MidModule_EventSend);
        }

        //接受参数事件
        private void MidModule_EventSend(object sender, object data, object e)
        {
            if (sender != null)
            {
                Model.EventInfo einfo = e as Model.EventInfo;
                if (einfo.Title != "表结构查询")
                    return;
                Form fr = sender as Form;
                if (fr.Text == "对象资源管理器")
                {
                    dinfo = data as DbDataInfo;
                    string msg = "数据库：" + dinfo.DbName;
                    if (DbDataType.GetDbDataType(dinfo.NameType).ToString() != "数据库")
                    {
                        msg = msg + " " + DbDataType.GetDbDataType(dinfo.NameType).ToString() + "：" + dinfo.Name;
                    }
                    lb_DbMessage.Text = msg;
                    dataGridView1.DataSource = null;
                }
            }
           
        }

        private void FormTableQuery_Load(object sender, EventArgs e)
        {
            InitOption();
        }

        private void InitOption()
        {
            panel1.BackColor = BaseConfigure.ColorTheme;
            dataGridView1.RowsDefaultCellStyle.BackColor = BaseConfigure.ColorTheme;
        }

        private void QueryData()
        {

            DataBaseInfo info = null; GetDbInfo();
            TableInfo table = null;
            string tablename = dinfo.Name;
            DbDataTypeEnum ddt = DbDataType.GetDbDataType(dinfo.NameType);
            switch (ddt)
            {
                case DbDataTypeEnum.表:
                    info = GetDbInfo();
                    table = info.Tables[tablename];
                    break;
                case DbDataTypeEnum.视图:
                    info = GetDbInfo();
                    table = info.View[tablename];
                    break;
            }
            if (table == null)
            {
                MessageBox.Show("请选择一个表或视图");
                return;
            }
            dataGridView1.DataSource = table.Fields;
        }

        private DataBaseInfo GetDbInfo()
        {
            IDbLink dal = new DbLink();
            DbLinkInfo dlinfo = dal.DbLinkGetInfo(dinfo.DbLinkID);

            IDataBase dbDal = new CurrencyDal.CodeMaker.DataBase();
            string rstmsg = "";
            List<string> tableNameList = new List<string>();
            tableNameList.Add(dinfo.Name);
            DataBaseInfo dbinfo = dbDal.DataBaseGetInfo(dlinfo, tableNameList, out rstmsg);

            return dbinfo;
        }

        private void btn_Query_Click(object sender, EventArgs e)
        {
            QueryData();
        }

    }
}
