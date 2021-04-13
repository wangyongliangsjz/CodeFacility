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
using Common;
using CurrencyDal.CodeMaker;

namespace CodeFacility.CodeMaker
{
    public partial class FormFieldData : FormWin
    {
        DbDataInfo dinfo;
        DataSet ds;
        bool key = false;
        string extend = "";
        RunSql dalRunSql = new RunSql();
        public FormFieldData()
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
                if (einfo.Title != "修改字段数据")
                    return;
                Form fr = sender as Form;
                if (fr.Text == "对象资源管理器")
                {
                    dinfo = data as DbDataInfo;
                    DbDataTypeEnum ddt = DbDataType.GetDbDataType(dinfo.NameType);
                    if (ddt != DbDataTypeEnum.表)
                    {
                        MessageBox.Show("请选择表。");
                        return;
                    }
                    string msg = "数据库：" + dinfo.DbName;
                    if (DbDataType.GetDbDataType(dinfo.NameType).ToString() != "数据库")
                    {
                        msg = msg + " " + DbDataType.GetDbDataType(dinfo.NameType).ToString() + "：" + dinfo.Name;
                    }
                    lb_DbMessage.Text = msg;
                    QueryData();
                }
            }

        }

        private void FormImportData_Load(object sender, EventArgs e)
        {
            InitOption();
        }

        private void InitOption()
        {
            tv_table.BackColor = BaseConfigure.ColorTheme;
            panel1.BackColor = BaseConfigure.ColorTheme;
        }

        private void QueryData()
        {
            //获取菜单数据
            DataBaseInfo dbinfo = GetDbInfo(dinfo.DbLinkID);
            string tablename=dinfo.Name;
            TableInfo tinfo = dbinfo.Tables[tablename];

            if (tv_table.Nodes.Count > 0)
                tv_table.Nodes.Clear();
            TreeNode tn = new TreeNode();
            tn.Text = "表("+dinfo.Name+")";
            TreeNode fnode = null;
            foreach (FieldInfo info in tinfo.Fields)
            {
                fnode = new TreeNode();
                fnode.Text = info.Name;
                tn.Nodes.Add(fnode);
            }
            tn.ExpandAll();
            tv_table.Nodes.Add(tn);
        }

        private DataBaseInfo GetDbInfo(int DbLinkId)
        {
            IDbLink dal = new DbLink();
            DbLinkInfo dlinfo = dal.DbLinkGetInfo(DbLinkId);

            IDataBase dbDal = new CurrencyDal.CodeMaker.DataBase();
            string rstmsg = "";
            string tableName = "";
            List<string> tableNameList = new List<string>();
            DbDataTypeEnum dtype = DbDataType.GetDbDataType(dinfo.NameType);
            if (dtype == DbDataTypeEnum.表)
            {
                tableName = dinfo.Name;
                tableNameList.Add(tableName);
            }
            DataBaseInfo dbinfo = dbDal.DataBaseGetInfo(dlinfo, tableNameList, out rstmsg);
            return dbinfo;
        }


        private void btb_scan_Click(object sender, EventArgs e)
        {
            //获取导入文件数据
            dgv1.DataSource = null;
            dgv2.DataSource = null;
            dgv3.DataSource = null;
            dgv4.DataSource = null;
            dgv5.DataSource = null;
            dgv6.DataSource = null;
            string rstmsg = "";

            #region 打开文件
            OpenFileDialog openFileDialog1 = new OpenFileDialog();     //显示选择文件对话框
            openFileDialog1.InitialDirectory = "c:\\";
            //openFileDialog1.Filter = "Excel 工作簿(*.xlsx)|*.xlsx|Miscrosoft Office Excel 97-2003 工作表|*.xls|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.Filter = "Miscrosoft Office Excel 97-2003 工作表|*.xls|excel07文件(*.xlsx)|*.xlsx|txt 文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            extend = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_file.Text = openFileDialog1.FileName;          //显示文件路径
                extend = System.IO.Path.GetExtension(openFileDialog1.FileName);
                //System.IO.FileInfo f = new System.IO.FileInfo(openFileDialog1.FileName);
                //extend = f.Extension.ToLower();
                extend = extend.Replace(".", "");
            }
            #endregion
            int rst= GetDataSet(out rstmsg);
            if (rst != 1)
            {
                MessageBox.Show(rstmsg);
                return;
            }
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                string dgvname = "dgv" + (i + 1).ToString();
                DataGridView dgv = (DataGridView)this.Controls.Find(dgvname, true)[0];
                dgv.DataSource = ds.Tables[i];
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            int rst = 0;
            string rstmsg = "";


            if (string.IsNullOrEmpty(tb_Where.Text.Trim()))
            {
                string msgbox = "未设置查询条件，是否查询？";
                if (cb_Coordinate.Checked || cb_Coordinate1.Checked)
                {
                    msgbox = "未设置查询条件将更新字段所有行数据，是否查询？";
                }
                DialogResult dr = MessageBox.Show(msgbox, "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.Cancel)
                    return;
            }
            DataSet ds= new DataSet();
            rst = Coordinate(1, out rstmsg, out ds);
            if(ds != null && ds.Tables[0] != null )
                dgv1.DataSource = ds.Tables[0];
        }

        /// <summary>
        /// 复制sql更新语句
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CopySql_Click(object sender, EventArgs e)
        {
            int rst = 0;
            string rstmsg = "";

            if (string.IsNullOrEmpty(tb_Key.Text.Trim()))
            {
                MessageBox.Show("请输入主键");
                return;
            }

            if (string.IsNullOrEmpty(tb_Where.Text.Trim()))
            {
                string msgbox = "未设置查询条件，复制？";
                if (cb_Coordinate.Checked || cb_Coordinate1.Checked)
                {
                    msgbox = "未设置查询条件将更新字段所有行数据，是否复制？";
                }
                DialogResult dr = MessageBox.Show(msgbox, "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.Cancel)
                    return;
            }
            DataSet ds = new DataSet();
            if (cb_File.Checked)
            {
                rst = UpdateFilde(3, out rstmsg);
            }
            else
            {
                rst = Coordinate(3, out rstmsg, out ds);
            }
            MessageBox.Show(rstmsg);
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_import_Click(object sender, EventArgs e)
        {
            int rst = 0;
            string rstmsg = "";

            if(string.IsNullOrEmpty(tb_Key.Text.Trim()))
            {
                MessageBox.Show("请输入主键");
                return;
            }

            if (string.IsNullOrEmpty(tb_Where.Text.Trim()))
            {
                string msgbox = "未设置查询条件，是否执行？";
                if (cb_Coordinate.Checked || cb_Coordinate1.Checked)
                {
                    msgbox = "未设置查询条件将更新字段所有行数据，是否执行？";
                }
                DialogResult dr = MessageBox.Show(msgbox, "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.Cancel)
                    return;
            }
            DataSet ds = new DataSet();
            if (cb_Coordinate.Checked || cb_Coordinate1.Checked)
            {
                rst = Coordinate(2,out rstmsg,out ds);
            }
            else if(cb_File.Checked)
            {
                rst = UpdateFilde(2,out rstmsg);
            }
            else
            {
                rstmsg = "请选择执行方式";
            }

            MessageBox.Show(rstmsg);
            
        }

        /// <summary>
        /// 转换坐标/更新数据
        /// </summary>
        /// <param name="type">操作类型：1 查询，2 更新表字段，3 复制sql</param>
        /// <param name="rstmsg"></param>
        /// <returns></returns>
        private int Coordinate(int type,out string rstmsg,out DataSet rtds)
        {
            int rst = 0;
            rstmsg = "";
            string sqlmsg = "";
            rtds = new DataSet();
            try
            {

                string keyField = tb_Key.Text.Trim(); //主键
                string mapXField = ""; //经度
                string mapYField = ""; //纬度
                string mapXYField = ""; //坐标集合

                CurrencyDal.CodeMaker.DataBase dal = new CurrencyDal.CodeMaker.DataBase();
                IDbLink dldal = new DbLink();
                DbLinkInfo dlinfo = dldal.DbLinkGetInfo(dinfo.DbLinkID);
                string tableName = dinfo.Name;
                string targetTableName = "[]";
                List<string> fieldlist = new List<string>();
                string field = tb_field.Text.Trim();
                field = field.Replace(" ", "");

                string[] fieldItem = field.Split(',');
                for (int i = 0; i < fieldItem.Length; i++)
                {
                    if (!string.IsNullOrEmpty(fieldItem[i]))
                    {
                        string strfield = fieldItem[i];
                        string strFieldLast = "";
                        if (!string.IsNullOrEmpty(strfield))
                            strFieldLast = strfield.Substring(strfield.Length - 1, 1);
                        if (strFieldLast.ToLower().IndexOf("x") >= 0 && strfield.ToLower().IndexOf("xy") < 0)
                            mapXField = strfield;
                        else if (strFieldLast.ToLower().IndexOf("y") >= 0 && strfield.ToLower().IndexOf("xy") < 0)
                            mapYField = strfield;
                        else if (strfield.ToLower() == txt_Coordinate.Text.Trim().ToLower() && !string.IsNullOrEmpty(txt_Coordinate.Text.Trim()))
                            mapXYField = txt_Coordinate.Text.Trim();
                        fieldlist.Add(strfield);
                    }

                }

                DataBaseInfo dbinfo = GetDbInfo(dinfo.DbLinkID);
                string tablename = dinfo.Name;
                TableInfo tinfo = dbinfo.Tables[dinfo.Name];

                if (!string.IsNullOrEmpty(txt_TargetTable.Text.Trim()) && type==3)
                    targetTableName = txt_TargetTable.Text.Trim();
                //查询数据
                string sqlQuery = tb_Sql.Text.Trim();

                if (string.IsNullOrEmpty(sqlQuery))
                {
                    sqlQuery = "Select ";
                    if (!string.IsNullOrEmpty(tb_field.Text.Trim()))
                    {
                        sqlQuery += tb_field.Text.Trim() + " ";
                    }
                    else
                    {
                        rst = -1;
                        rstmsg = "请输入sql语句";
                        return rst;
                    }
                    sqlQuery += " From " + tablename + " ";
                    if (!string.IsNullOrEmpty(tb_Where.Text.Trim()))
                    {
                        sqlQuery += " Where " + tb_Where.Text.Trim();
                    }
                }

                DataSet dsr = dalRunSql.Run(dlinfo, sqlQuery, out sqlmsg, out rstmsg);
                rtds = dsr;
                if (type == 1) return 1; //查询数据

                List<string> listSql = new List<string>();

                int coordinateType = 0; //1 百度地图转天地图,2天地图转百度地图
                char sourceDelimiter = new char(); //源分割符 “_”、“|”都转为“|”
                string targetDelimiter = ""; //目标分割符 “|”天地图
                if (cb_Coordinate1.Checked)
                {
                    sourceDelimiter = '|';
                    targetDelimiter = "|";
                    coordinateType = 1;
                }
                else if (cb_Coordinate.Checked)
                {
                    sourceDelimiter = '|';
                    targetDelimiter = "|";
                    coordinateType = 2;
                }
                    
                string MapX = "";
                string MapY = "";
                
                if (dsr == null && dsr.Tables[0] == null && dsr.Tables[0].Rows.Count == 0)
                {
                    rstmsg = "未查询到数据";
                    return 1;
                }
 
                    foreach (DataRow dr in dsr.Tables[0].Rows)
                    {
                        string MapListOneLevel = "";
                        string MapList = "";
                        if (!string.IsNullOrEmpty(mapXField) && !string.IsNullOrEmpty(mapYField))
                        {
                            MapX = dr[mapXField].ToString().Trim();
                            MapY = dr[mapYField].ToString().Trim();
                            GetMapXAndMapY(coordinateType, MapX, MapY, out MapX, out MapY);
                            #region 坐标转换 已不用

                            //if (!string.IsNullOrEmpty(MapX) && !string.IsNullOrEmpty(MapY))
                            //{
                            //    double mapx = double.Parse(MapX); double mapy = double.Parse(MapY);
                            //    if (mapx > 0 && mapy > 0)
                            //    {
                            //        if (coordinateType == 1)
                            //        {
                            //            //百度地图转天地图
                            //            double[] gcjarr = MapTransform.BD09ToGCJ02(mapx, mapy);
                            //            double[] wgsarr = MapTransform.GCJ02ToWGS84(gcjarr[0], gcjarr[1]);
                            //            MapX = Math.Round(wgsarr[0], 6).ToString();
                            //            MapY = Math.Round(wgsarr[1], 6).ToString();

                            //        }
                            //        else if (coordinateType == 2)
                            //        {
                            //            //天地图转百度地图
                            //            double[] GCJ02 = MapTransform.WGS84ToGCJ02(mapx, mapy);
                            //            double[] BD09 = MapTransform.GCJ02ToBD09(GCJ02[0], GCJ02[1]);
                            //            MapX = Math.Round(BD09[0], 6).ToString();
                            //            MapY = Math.Round(BD09[1], 6).ToString();
                            //        }
                            //    }
                            //}
                            #endregion

                        }

                        if (!string.IsNullOrEmpty(mapXYField))
                        {
                            #region 坐标集合转换
                            string mapxy = dr[mapXYField].ToString();
                            mapxy = mapxy.Replace("_", "|"); //源分割符 “_”、“|”都转为“|”
                            List<string> mapListOneLevel = new List<string>();
                            List<string> mapList = new List<string>();
                            string[] mapOneLevel = new string[0];
                            if (!string.IsNullOrEmpty(mapxy.Trim())) mapOneLevel = mapxy.Split(sourceDelimiter);
                            foreach(var item in mapOneLevel)
                            {

                                string[] maps = new string[0];
                                if (!string.IsNullOrEmpty(item.Trim())) maps = item.Split(';');
                                foreach (string map in maps)
                                {
                                    if (string.IsNullOrEmpty(map.Trim())) continue;
                                    string s_mapx = "", s_mapy = "";
                                    try
                                    {
                                        s_mapx = map.Split(',')[0];
                                        s_mapy = map.Split(',')[1];
                                    }
                                    catch (Exception ex) { continue; }
                                    GetMapXAndMapY(coordinateType, s_mapx, s_mapy, out s_mapx, out s_mapy);
                                    #region 坐标转换 已不用
                                    //double smapx = 0, smapy = 0;
                                    //double.TryParse(s_mapx, out smapx); double.TryParse(s_mapy, out smapy);
                                    //if (smapx > 0 && smapy > 0)
                                    //{
                                    //    if (coordinateType == 1)
                                    //    {
                                    //        //百度地图转天地图
                                    //        double[] gcjarr = MapTransform.BD09ToGCJ02(smapx, smapy);
                                    //        double[] wgsarr = MapTransform.GCJ02ToWGS84(gcjarr[0], gcjarr[1]);
                                    //        s_mapx = Math.Round(wgsarr[0], 6).ToString();
                                    //        s_mapy = Math.Round(wgsarr[1], 6).ToString();
                                    //    }
                                    //    else if (coordinateType == 2)
                                    //    {
                                    //        //天地图转百度地图
                                    //        double[] GCJ02 = MapTransform.WGS84ToGCJ02(smapx, smapy);
                                    //        double[] BD09 = MapTransform.GCJ02ToBD09(GCJ02[0], GCJ02[1]);
                                    //        s_mapx = Math.Round(BD09[0], 6).ToString();
                                    //        s_mapy = Math.Round(BD09[1], 6).ToString();
                                    //    }
                                    //}
                                    #endregion

                                    mapList.Add(s_mapx + "," + s_mapy);
                                }
                                //MapList = string.Join(";", mapList);
                                foreach (var mapItem in mapList)
                                {
                                    if (!string.IsNullOrEmpty(MapList)) MapList = MapList + ";";
                                    MapList = MapList + mapItem;
                                }
                                if (mapListOneLevel.Count > 0 && !string.IsNullOrEmpty(MapList))
                                    mapListOneLevel.Add(targetDelimiter);

                                if (!string.IsNullOrEmpty(MapList))
                                    mapListOneLevel.Add(MapList);
                                MapList = "";
                                mapList.Clear();
                            }

                            foreach (var mapItemOneLevel in mapListOneLevel)
                            {
                                MapListOneLevel = MapListOneLevel + mapItemOneLevel;
                            }
                            #endregion
                        }

                        //需要更新的字段
                        string filedData = "";
                        if (!string.IsNullOrEmpty(mapXField) && !string.IsNullOrEmpty(mapYField) && !string.IsNullOrEmpty(MapX) && !string.IsNullOrEmpty(MapY))
                        {
                            if (filedData != "") filedData = ",";
                            filedData = mapXField + "=" + MapX + " ," + mapYField + "=" + MapY;
                        }
                        if (!string.IsNullOrEmpty(mapXYField) && !string.IsNullOrEmpty(MapListOneLevel))
                        {
                            if (filedData != "") filedData = filedData + ",";
                            filedData = filedData + mapXYField + "='" + MapListOneLevel + "'";
                        }
                        if(!string.IsNullOrEmpty(filedData))
                        {
                            StringBuilder strBuilder = new StringBuilder();
                            if (type == 3)
                                strBuilder.Append(" Update " + targetTableName);
                            else
                                strBuilder.Append(" Update " + tablename);
                            strBuilder.Append(" Set ");
                            strBuilder.Append(filedData);
                            strBuilder.Append(" Where " + keyField + "='" + dr[keyField].ToString().Trim() + "'");
                            listSql.Add(strBuilder.ToString());
                        }
                        MapListOneLevel = "";
                        
                    }

                    if(type==2)
                    {
                        rst = dalRunSql.ExeSqlTran(dlinfo, listSql, out rstmsg);  // 更新地图坐标
                    }
                    else if(type==3)
                    {
                        //导出查询语句
                        string strData = "";
                        if (listSql != null && listSql.Count > 0)
                        {
                            foreach (var item in listSql)
                            {
                                if (!string.IsNullOrEmpty(strData)) strData = strData + ";" + Environment.NewLine;
                                strData = strData + item;
                            }
                            Clipboard.SetDataObject(strData);
                            rstmsg = string.Format("复制Sql更新语句{0}行", listSql.Count);
                        }
                        else
                            rstmsg = "复制Sql更新语句0行";
                    }

            }
            catch (Exception ex)
            {
                rst = -1;
                rstmsg = ex.Message;
            }

            return rst;
        }

        /// <summary>
        /// 坐标转换
        /// </summary>
        /// <param name="coordinateType">坐标转换方式：1 百度地图转天地图,2天地图转百度地图</param>
        /// <param name="inMapX">经度</param>
        /// <param name="inMapY">纬度</param>
        /// <param name="MapX">经度</param>
        /// <param name="MapY">纬度</param>
        /// <returns></returns>
        private int GetMapXAndMapY(int coordinateType,string inMapX,string inMapY,out string MapX,out string MapY)
        {
            MapX = inMapX;
            MapY = inMapY;
            try
            {
                double mapx = double.Parse(MapX); double mapy = double.Parse(MapY);
                if (mapx > 0 && mapy > 0)
                {
                    if (coordinateType == 1)
                    {
                        //百度地图转天地图
                        double[] gcjarr = MapTransform.BD09ToGCJ02(mapx, mapy);
                        double[] wgsarr = MapTransform.GCJ02ToWGS84(gcjarr[0], gcjarr[1]);
                        MapX = Math.Round(wgsarr[0], 6).ToString();
                        MapY = Math.Round(wgsarr[1], 6).ToString();

                    }
                    else if (coordinateType == 2)
                    {
                        //天地图转百度地图
                        double[] GCJ02 = MapTransform.WGS84ToGCJ02(mapx, mapy);
                        double[] BD09 = MapTransform.GCJ02ToBD09(GCJ02[0], GCJ02[1]);
                        MapX = Math.Round(BD09[0], 6).ToString();
                        MapY = Math.Round(BD09[1], 6).ToString();
                    }
                }
                return 1;
            }
            catch(Exception ex)
            {
            }

            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">操作类型：2 更新表字段，3 复制sql</param>
        /// <param name="rstmsg"></param>
        /// <returns></returns>
        private int UpdateFilde(int type,out string rstmsg)
        {
            int rst = 0;
            rstmsg = "";
            btnRefresh_Click(null, null);
            //导入文件数据
            if (ds == null)
            {
                MessageBox.Show("导入数据为空。");
            }
            if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("导入数据为空。");
            }

            CurrencyDal.CodeMaker.DataBase dal = new CurrencyDal.CodeMaker.DataBase();
            IDbLink dldal = new DbLink();
            DbLinkInfo dlinfo = dldal.DbLinkGetInfo(dinfo.DbLinkID);
            string tableName = dinfo.Name;
            List<string> fieldlist = new List<string>();
            string field = tb_field.Text.Trim();
            field = field.Replace(" ", "");

            int index = tabControl1.SelectedIndex;
            DataTable dt = ds.Tables[index];
            DataBaseInfo dbinfo = GetDbInfo(dinfo.DbLinkID);
            string tablename = dinfo.Name;
            TableInfo tinfo = dbinfo.Tables[dinfo.Name];
            if (!string.IsNullOrEmpty(txt_TargetTable.Text.Trim()) && type == 3)
                tablename = txt_TargetTable.Text.Trim();
            string keyField = tb_Key.Text.Trim(); //主键
            var listSql = dal.GetUpdateSql(dlinfo, tinfo, field, keyField,tb_Where.Text.Trim(), dt, out rstmsg);

            if(type==2)
            {
                try
                {
                    rst = dalRunSql.ExeSqlTran(dlinfo, listSql, out rstmsg);
                }
                catch (Exception ex)
                {
                    rst = -1;
                    rstmsg = "更新失败。" + ex.Message;
                    return rst;
                }

                if (rst >= 1)
                {
                    rstmsg = "总共" + dt.Rows.Count + "行数据，更新成功" + rst + "行数据。";
                }
            }
            else if (type == 3)
            {
                string strData = "";
                if (listSql != null && listSql.Count>0)
                {
                    foreach (var item in listSql)
                    {
                        if (!string.IsNullOrEmpty(strData)) strData = strData + ";" + Environment.NewLine;
                        strData = strData + item;
                    }
                    Clipboard.SetDataObject(strData);
                    rstmsg = string.Format("复制Sql更新语句{0}行", listSql.Count);
                }
                else
                    rstmsg = "复制Sql更新语句0行";

            }

            return rst;

        }

        /// <summary>
        /// 导入文件
        /// </summary>
        /// <param name="rstmsg"></param>
        /// <returns></returns>
        private int ImportData(out string rstmsg)
        {
            int rst = 0;
            rstmsg = "";
            //获取文件数据
            //rst = GetDataSet(out rstmsg);
            //if (rst != 1)
            //{
            //    MessageBox.Show(rstmsg);
            //    return;
            //}
            btnRefresh_Click(null, null);
            //导入文件数据
            if (ds == null)
            {
                MessageBox.Show("导入数据为空。");
            }
            if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("导入数据为空。");
            }

            CurrencyDal.CodeMaker.DataBase dal = new CurrencyDal.CodeMaker.DataBase();
            IDbLink dldal = new DbLink();
            DbLinkInfo dlinfo = dldal.DbLinkGetInfo(dinfo.DbLinkID);
            string tableName = dinfo.Name;
            List<string> fieldlist = new List<string>();
            string field = tb_field.Text.Trim();
            field = field.Replace(" ", "");

            int index = tabControl1.SelectedIndex;
            DataTable dt = ds.Tables[index];
            DataBaseInfo dbinfo = GetDbInfo(dinfo.DbLinkID);
            string tablename = dinfo.Name;
            TableInfo tinfo = dbinfo.Tables[tablename];
            rst = dal.ImportData(dlinfo, tinfo, field, dt, out rstmsg);

            return rst;
        }

        /// <summary>
        /// 获取文件数据
        /// </summary>
        /// <param name="rstmsg"></param>
        /// <returns></returns>
        private int GetDataSet(out string rstmsg)
        {
            int rst = 0;
            rstmsg = "";
            try
            {
                string filepath = tb_file.Text.Trim();
                if (filepath == "")
                {
                    rstmsg = "请选择文件。";
                    return -1;
                }
                ds = null;
                if (filepath != "")
                {
                    if (extend.ToLower() == "xls" || extend.ToLower() == "xlsx")
                    {
                        //ds = Common.Excel.ExcelToDataSet(filepath);
                        ds = Common.Excel.ExcelToDS(filepath);
                    }
                    else if (extend.ToLower() == "txt")
                    {
                        ds = Common.FileHandle.TxtToDataSet(filepath);
                    }
                }
                if (ds == null)
                {
                    rstmsg = "请选择文件失败。";
                    return -1;
                }
                if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    rstmsg = "请选择文件失败。";
                    return -1;
                }
                rst = 1;
                return rst;
            }
            catch(Exception ex)
            {
                rst = -1;
                rstmsg = ex.Message;
                return rst;
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            tb_field.Text = "";
            tb_file.Text = "";
            tb_Key.Text = "";
            tb_Sql.Text = "";
            tb_Where.Text = "";
            txt_Coordinate.Text = "";
            cb_Coordinate.Checked = false;
            cb_Coordinate1.Checked = false;
            cb_File.Checked = false;
            key = true;
            tv_table.Nodes[0].Checked = false;
            TreeNodeCollection tnc = tv_table.Nodes[0].Nodes;
            foreach (TreeNode tn in tnc)
            {
                tn.Checked = false;
            }
            key = false;
        }

        #region
        /// <summary>
        /// 设置导入字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tv_table_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            //设置导入字段
            if (e.Node.Text.Trim() == "")
                return;
            if (key)
            {
                return;
            }
            string field = e.Node.Text;
            if (field.IndexOf("表") > -1)
            {
                key = true;
                if (e.Node.Checked)
                {
                    tb_field.Text = "";
                    TreeNodeCollection tnc = tv_table.Nodes[0].Nodes;
                    foreach (TreeNode tn in tnc)
                    {
                        tn.Checked = false;
                    }
                }
                else
                {
                    tb_field.Text = "";
                    TreeNodeCollection tnc = tv_table.Nodes[0].Nodes;
                    foreach (TreeNode tn in tnc)
                    {
                        if (tn.Text != null)
                        {
                            tn.Checked = true;
                            //增加字段
                            if (tb_field.Text.Trim() == "")
                            {
                                tb_field.Text = tn.Text;
                            }
                            else
                            {
                                tb_field.Text = tb_field.Text + "," + tn.Text;
                            }
                        }
                    }
                }
                key = false;
                return;
            }

            if (key)
            {
                return;
            }

            if (e.Node.Checked)
            {
                //删除字段
                if (tb_field.Text.IndexOf(",") > -1)
                {
                    //if (tb_field.Text.EndsWith(field))
                    //{
                    //    tb_field.Text = tb_field.Text.Replace(field, "");
                    //}
                    string[] fieldItem = tb_field.Text.Trim().Split(',');
                    string strf = "";
                    for (int i = 0; i < fieldItem.Length; i++)
                    {
                        string fi = fieldItem[i];
                        if (fieldItem[i].ToString() == field)
                        {
                            fieldItem[i] = "";
                        }
                        if (fieldItem[i] != "")
                        {
                            if (strf == "")
                            {
                                strf = fieldItem[i];
                            }
                            else
                            {
                                strf = strf +","+ fieldItem[i];
                            }
                        }
                    }
                    tb_field.Text = strf;
                }
                else
                {
                    tb_field.Text = tb_field.Text.Replace(field, "");
                }
            }
            else
            {
                //增加字段
                if (tb_field.Text.Trim() == "")
                {
                    if (tb_field.Text.IndexOf(field) > -1)
                    {
                        MessageBox.Show("已选择此字段。");
                        return;
                    }
                    tb_field.Text = field;
                }
                else
                {
                    if (tb_field.Text.IndexOf(field) > -1)
                    {
                        MessageBox.Show("已选择此字段。");
                        return;
                    }
                    tb_field.Text = tb_field.Text + "," + field;
                }
            }

            if (tb_field.Text.IndexOf(" ") > -1)
            {
                tb_field.Text = tb_field.Text.Replace(" ", "");
            }
            if (tb_field.Text.IndexOf(",,") > -1)
            {
                tb_field.Text = tb_field.Text.Replace(",,", ",");
            }
            if (tb_field.Text.StartsWith(","))
            {
                tb_field.Text = tb_field.Text.Substring(1, tb_field.Text.Length - 1);
            }
            if (tb_field.Text.EndsWith(","))
            {
                tb_field.Text = tb_field.Text.Substring(0, tb_field.Text.Length - 1);
            }

        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            int rst = 0;
            string rstmsg = "";
            //获取文件数据
            rst = GetDataSet(out rstmsg);
            if (rst != 1)
            {
                MessageBox.Show(rstmsg);
                return;
            }

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                string dgvname = "dgv" + (i + 1).ToString();
                DataGridView dgv = (DataGridView)this.Controls.Find(dgvname, true)[0];
                dgv.DataSource = ds.Tables[i];
            }
        }

        private void FormFieldData_FormClosed(object sender, FormClosedEventArgs e)
        {
            tv_table = new TreeView();
        }

        private void cb_Coordinate_CheckedChanged(object sender, EventArgs e)
        {
            cb_Coordinate1.Checked = false;
            cb_File.Checked = false;
        }

        private void cb_Coordinate1_CheckedChanged(object sender, EventArgs e)
        {
            cb_Coordinate.Checked = false;
            cb_File.Checked = false;
        }

        private void cb_File_CheckedChanged(object sender, EventArgs e)
        { 
            cb_Coordinate1.Checked = false;
            cb_Coordinate.Checked = false;
        }

    }
}
