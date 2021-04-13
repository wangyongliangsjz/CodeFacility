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
using Model;

namespace CodeFacility.CodeMaker
{
    public partial class FormMultiTableQuery : FormWin
    {
        DbDataInfo dinfo = new DbDataInfo();
        List<string> listTable = new List<string>();
        public FormMultiTableQuery()
        {
            InitializeComponent();
            //订阅了信息发送事件，即接受参数值
            Common.MidModule.EventSend += new Common.DataDlg(MidModule_EventSend);
        }

        //接受参数事件
        private void MidModule_EventSend(object sender, object data, object e)
        {
            //if (sender != null)
            //{
            //    Model.EventInfo einfo = e as Model.EventInfo;
            //    if (einfo.Title != "表结构查询")
            //        return;
            //    Form fr = sender as Form;
            //    if (fr.Text == "对象资源管理器")
            //    {
            //        dinfo = data as DbDataInfo;
            //        string msg = "数据库：" + dinfo.DbName;
            //        if (DbDataType.GetDbDataType(dinfo.NameType).ToString() != "数据库")
            //        {
            //            msg = msg + " " + DbDataType.GetDbDataType(dinfo.NameType).ToString() + "：" + dinfo.Name;
            //        }
            //        lb_DbMessage.Text = msg;
            //        DGV1.DataSource = null;
            //    }
            //}
           
        }

        private void FormMultiTableQuery_Load(object sender, EventArgs e)
        {
            Init();
            InitOption();
        }

        private void Init()
        {
            IDbLink idal = new DbLink();
            IList<DbLinkInfo> ilist = idal.DbLinkGetList();
            foreach (DbLinkInfo info in ilist)
            {
                string dbName = string.IsNullOrEmpty(info.DbAbbreviation) ? info.DbName : info.DbName + "(" + info.DbAbbreviation + ")";
                comboBoxDB.Items.Add(new ListItem(info.ID.ToString(), dbName));
            }
            comboBoxDB.SelectedIndex = 0;

            //comboBoxDB.DisplayMember = "DbName";
            //comboBoxDB.ValueMember = "ID";
            //comboBoxDB.DataSource = ilist;


        }

        private void InitOption()
        {
            DGV1.RowsDefaultCellStyle.BackColor = BaseConfigure.ColorTheme;
            DGV2.RowsDefaultCellStyle.BackColor = BaseConfigure.ColorTheme;
            DGV3.RowsDefaultCellStyle.BackColor = BaseConfigure.ColorTheme;
            DGV4.RowsDefaultCellStyle.BackColor = BaseConfigure.ColorTheme;
            DGV5.RowsDefaultCellStyle.BackColor = BaseConfigure.ColorTheme;
            rtb_Message.BackColor = BaseConfigure.ColorTheme;
            panel1.BackColor = BaseConfigure.ColorTheme;
        }

        private void QueryData()
        {
            try
            {
                DataBaseInfo info = GetDbInfo();
                this.tabControl1.SelectTab("tabPage1");
                DGV1.DataSource = null;
                DGV2.DataSource = null;
                DGV3.DataSource = null;
                DGV4.DataSource = null;
                DGV5.DataSource = null;
                int i = 0;
                foreach (var item in listTable)
                {
                    string name = item;
                    var tbv= info.Tables[name] == null ? (info.View[name]==null ? null : info.View[name].Fields) : info.Tables[name].Fields;

                    switch(i)
                    {
                        case 0:

                            DGV1.DataSource = tbv;
                            break;
                        case 1:
                            DGV2.DataSource = tbv;
                            break;
                        case 2:
                            DGV3.DataSource = tbv;
                            break;
                        case 3:
                            DGV4.DataSource = tbv;
                            break;
                        case 4:
                            DGV5.DataSource = tbv;
                            break;
                    }

                    i++;
                }
            }
            catch(Exception ex)
            {

            }
            
        }


        private DataBaseInfo GetDbInfo()
        {
            IDbLink dal = new DbLink();
            int id = 0;
            var lisitem = comboBoxDB.SelectedItem as ListItem;
            id = string.IsNullOrEmpty(lisitem.ID) ? 0 : int.Parse(lisitem.ID);
            //id = comboBoxDB.SelectedValue.ToString() == "" ? 0 : int.Parse(comboBoxDB.SelectedValue.ToString());
            var dlinfo = dal.DbLinkGetInfo(id);

            IDataBase dbDal = new CurrencyDal.CodeMaker.DataBase();
            string rstmsg = "";
            List<string> tableNameList = new List<string>();
            string tableName = lb_DbMessage.Text;
            if (tableName.IndexOf(",") > 0)
            {
                string[] nameitem = tableName.Split(',').ToArray();
                if (listTable.Count > 0)
                    listTable.Clear();
                foreach (var item in nameitem)
                {
                    listTable.Add(item);
                    tableNameList.Add(item);
                }    
            }
            else
            {
                if (listTable.Count > 0)
                    listTable.Clear();
                listTable.Add(tableName);
                tableNameList.Add(tableName);
            }
                

            DataBaseInfo dbinfo = dbDal.DataBaseGetInfo(dlinfo, tableNameList, out rstmsg);

            return dbinfo;
        }

        private void btn_Query_Click(object sender, EventArgs e)
        {
            QueryData();
        }

        private void comboBoxDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string rstmsg = "";
                IDbLink dal = new DbLink();
                int id = 0;
                var lisitem = comboBoxDB.SelectedItem as ListItem;
                id = string.IsNullOrEmpty(lisitem.ID) ? 0 : int.Parse(lisitem.ID);
                //id = comboBoxDB.SelectedValue.ToString() == "" ? 0 : int.Parse(comboBoxDB.SelectedValue.ToString());
                DbLinkInfo dlinfo = dal.DbLinkGetInfo(id);

                IDataBase dbDal = new CurrencyDal.CodeMaker.DataBase();
                DataBaseInfo dbinfo = dbDal.GetTableInfo(dlinfo, out rstmsg);
                comboBoxTable.DataSource = null;
                List<ListItem> list = new List<ListItem>();
                TableInfo model = new TableInfo();
                list.Add(new ListItem("请选择表", "请选择表"));
                foreach (TableInfo item in dbinfo.Tables)
                {
                    TableInfo modeltable = new TableInfo();
                    list.Add(new ListItem(item.xType, item.Name));
                }
                comboBoxTable.DataSource = list;
            }
            catch(Exception ex)
            {
                string msgerror = ex.Message;
            }

            
        }

        private void comboBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxTable.SelectedItem.ToString() != "请选择表")
                {
                    if (lb_DbMessage.Text == "...")
                        lb_DbMessage.Text = comboBoxTable.SelectedItem.ToString();
                    else
                        lb_DbMessage.Text += "," + comboBoxTable.SelectedItem.ToString();
                }
            }
            catch
            { }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lb_DbMessage.Text = "...";
        }

    }
}
