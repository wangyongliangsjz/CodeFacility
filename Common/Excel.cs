using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Web;
using System.Xml;
using System.Data.OleDb;
using System.Data.Common;
using System.Reflection;
using Newtonsoft.Json;

using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;



namespace Common
{
    /// <summary>
    /// Excel导入导出类
    /// </summary>
    public class Excel
    {
        #region 下载Excel文件
        
        /// <summary>
        /// 下载Excel文件 DataRow
        /// </summary>
        /// <param name="dr">需要下载的数据 DataRow</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="colBodys">文件字段</param>
        /// <param name="colHeaders">文件字段标题</param>
        /// <param name="colSplit">分隔符</param>
        public static void DataRowToExcel(DataRow[] dr,string filePath, string fileName, string colBodys, string colHeaders, string colSplit)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(fileName);

            //填充表头
            IRow dataRow = sheet.CreateRow(0);
            int i = 0;
            foreach (string aStr in colHeaders.Split(colSplit.ToCharArray()))
            {
                dataRow.CreateCell(i).SetCellValue(aStr);
                i++;
            }

            //填充内容
            for (int k = 0; k < dr.Length; k++)
            {
                int j = 0;
                DataRow row = dr[k];
                dataRow = sheet.CreateRow(k + 1);
                foreach (string aStr in colBodys.Split(colSplit.ToCharArray()))
                {
                    ICell newCell = dataRow.CreateCell(j);
                    if (aStr == "NO")
                    {
                        newCell.SetCellValue(k + 1);
                    }
                    else
                    {
                        string drValue = row[aStr].ToString();
                        switch (row[aStr].GetType().ToString())
                        {
                            case "System.String"://字符串类型
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);
                                //newCell.CellStyle = "yyyy-mm-dd";//格式化显示
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }
                    }

                    j++;
                }
            }

            //保存
            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
            }

        }

        /// <summary>
        /// 下载Excel文件 DataTable
        /// </summary>
        /// <param name="dr">需要下载的数据 DataSet</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="colBodys">文件字段</param>
        /// <param name="colHeaders">文件字段标题</param>
        /// <param name="colSplit">分隔符</param>
        public static int DataSetToExcel(DataSet ds, string filePath,out string rstmsg)
        {
            int rst = 0;
            rstmsg = "";
            IWorkbook workbook = new HSSFWorkbook();
            try
            {
                for (int l = 0; l < ds.Tables.Count; l++)
                {
                    DataTable dt = ds.Tables[l];
                    string fileName = "Sheet" + (l + 1).ToString();
                    ISheet sheet = workbook.CreateSheet(fileName);

                    //填充表头
                    IRow dataRow = sheet.CreateRow(0);
                    int i = 0;
                    List<string> filedNameList = new List<string>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        string filed = string.Format("列名：{0} ,数据类型：{1}", dc.ColumnName, dc.DataType);
                        string filedName = dc.ColumnName;
                        filedNameList.Add(filedName);
                        dataRow.CreateCell(i).SetCellValue(filedName);
                        i++;
                    }

                    //填充内容
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        int j = 0;
                        DataRow row = dt.Rows[k];
                        dataRow = sheet.CreateRow(k + 1);
                        foreach (string filedName in filedNameList)
                        {
                            ICell newCell = dataRow.CreateCell(j);
                            //ICellStyle dateStyle = workbook.CreateCellStyle();
                            //IDataFormat format = workbook.CreateDataFormat();

                            string drValue = row[filedName].ToString();
                            string type = row[filedName].GetType().ToString();
                            switch (row[filedName].GetType().ToString())
                            {
                                case "System.String"://字符串类型
                                    newCell.SetCellValue(drValue);
                                    break;
                                case "System.DateTime"://日期类型
                                case "MySql.Data.Types.MySqlDateTime"://日期类型
                                    //DateTime dateV;
                                    //DateTime.TryParse(drValue, out dateV);
                                    //IDataFormat datastyle = workbook.CreateDataFormat();
                                    //newCell.CellStyle.DataFormat = datastyle.GetFormat("yyyy/mm/dd");
                                    //newCell.SetCellValue(dateV);
                                    if (!string.IsNullOrEmpty(drValue))
                                        drValue = DateTime.Parse(drValue).ToString("yyyy-MM-dd HH:mm:ss");
                                    newCell.SetCellValue(drValue);
                                    //dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd HH:mm:ss");
                                    //newCell.CellStyle.DataFormat = dateStyle.DataFormat;
                                    break;
                                case "System.Boolean"://布尔型
                                    bool boolV = false;
                                    bool.TryParse(drValue, out boolV);
                                    newCell.SetCellValue(boolV);
                                    break;
                                case "System.Int16"://整型
                                case "System.Int32":
                                case "System.Int64":
                                case "System.Byte":
                                    int intV = 0;
                                    int.TryParse(drValue, out intV);
                                    newCell.SetCellValue(intV);
                                    break;
                                case "System.Decimal"://浮点型
                                case "System.Double":
                                    double doubV = 0;
                                    double.TryParse(drValue, out doubV);
                                    newCell.SetCellValue(doubV);
                                    break;
                                case "System.DBNull"://空值处理
                                    newCell.SetCellValue("");
                                    break;
                                default:
                                    newCell.SetCellValue("");
                                    break;
                            }

                            j++;
                        }
                    }
                }

                //保存
                using (MemoryStream ms = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(fs);
                    }
                }
                rst = 1;
                rstmsg = "导出成功。";
            }
            catch (Exception ex)
            {
                rstmsg = ex.Message;
            }
            return rst;
        }

        /// <summary>
        /// 下载Excel文件 List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="filePath"></param>
        /// <param name="rstmsg"></param>
        /// <returns></returns>
        public static int ListToExcel<T>(List<T> list, string filePath, out string rstmsg)
        {
            int rst = 0;
            rstmsg = "";
            DataSet ds = new DataSet();
            IWorkbook workbook = new HSSFWorkbook();
            try
            {
                var PropTypeof = typeof(T);
                System.Reflection.PropertyInfo[] PropItem = PropTypeof.GetProperties();
                //foreach (var item in PropItem)
                //{
                //    var FindName = item.Name;
                //    var FindType = item.PropertyType.FullName;
                //}

                PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in props)
                {
                    //Type t = GetCoreType(prop.PropertyType);
                }

                string SheetName = "Sheet1";
                ISheet sheet = workbook.CreateSheet(SheetName);

                //填充表头
                IRow dataRow = sheet.CreateRow(0);
                int i = 0;
                List<string> filedNameList = new List<string>();
                foreach (var item in PropItem)
                {
                    string filed = string.Format("列名：{0} ,数据类型：{1}", item.Name, item.PropertyType.FullName);
                    string filedName = item.Name;
                    filedNameList.Add(filedName);
                    dataRow.CreateCell(i).SetCellValue(filedName);
                    i++;
                }

                object[] indexArgs = { PropItem.Length };

                //填充内容
                //for (int k = 0; k < dt.Rows.Count; k++)
                int index = 1;
                foreach (var item in list)
                {
                    int j = 0;
                    //DataRow row =index dt.Rows[k];
                    dataRow = sheet.CreateRow(index);
                    index++;
                    //var values = new object[props.Length];

                    foreach (var pitem in PropItem)
                    {
                        string filedName = pitem.Name;
                        ICell newCell = dataRow.CreateCell(j);
                        //ICellStyle dateStyle = workbook.CreateCellStyle();
                        //IDataFormat format = workbook.CreateDataFormat();

                        var objValue = item.GetType().GetProperty(filedName).GetValue(item, indexArgs);
                        string drValue = "";
                        string fullName = "";
                        if (objValue != null)
                        {
                            drValue = JsonConvert.SerializeObject(item.GetType().GetProperty(filedName).GetValue(item, indexArgs)).Replace("\"", "").Replace("null", "");
                            fullName = objValue.GetType().FullName; //空值无法获取数据类型
                        }
                        //string fullName = pitem.PropertyType.UnderlyingSystemType.FullName;
                        switch (fullName)
                        {
                            case "System.String"://字符串类型
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型
                            case "MySql.Data.Types.MySqlDateTime"://日期类型
                                if (!string.IsNullOrEmpty(drValue))
                                    drValue = DateTime.Parse(drValue).ToString("yyyy-MM-dd HH:mm:ss");
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }

                        j++;
                    }
                }

                //保存
                using (MemoryStream ms = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(fs);
                    }
                }
                rst = 1;
                rstmsg = "导出成功。";
            }
            catch (Exception ex)
            {
                rstmsg = ex.Message;
            }
            return rst;
        }

        #region Excel2007
        ///// <summary>
        ///// Datable导出成Excel
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <param name="file">导出路径(包括文件名与扩展名)</param>
        //public static void TableToExcel(DataTable dt, string file)
        //{
        //    IWorkbook workbook;
        //    string fileExt = Path.GetExtension(file).ToLower();
        //    if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(); } else { workbook = null; }
        //    if (workbook == null) { return; }
        //    ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

        //    //表头  
        //    IRow row = sheet.CreateRow(0);
        //    for (int i = 0; i < dt.Columns.Count; i++)
        //    {
        //        ICell cell = row.CreateCell(i);
        //        cell.SetCellValue(dt.Columns[i].ColumnName);
        //    }

        //    //数据  
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        IRow row1 = sheet.CreateRow(i + 1);
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //        {
        //            ICell cell = row1.CreateCell(j);
        //            cell.SetCellValue(dt.Rows[i][j].ToString());
        //        }
        //    }

        //    //转为字节数组  
        //    MemoryStream stream = new MemoryStream();
        //    workbook.Write(stream);
        //    var buf = stream.ToArray();

        //    //保存为Excel文件  
        //    using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
        //    {
        //        fs.Write(buf, 0, buf.Length);
        //        fs.Flush();
        //    }
        //}

        ///// <summary>
        ///// 获取单元格类型
        ///// </summary>
        ///// <param name="cell"></param>
        ///// <returns></returns>
        //private static object GetValueType(ICell cell)
        //{
        //    if (cell == null)
        //        return null;
        //    switch (cell.CellType)
        //    {
        //        case CellType.Blank: //BLANK:  
        //            return null;
        //        case CellType.Boolean: //BOOLEAN:  
        //            return cell.BooleanCellValue;
        //        case CellType.Numeric: //NUMERIC:  
        //            return cell.NumericCellValue;
        //        case CellType.String: //STRING:  
        //            return cell.StringCellValue;
        //        case CellType.Error: //ERROR:  
        //            return cell.ErrorCellValue;
        //        case CellType.Formula: //FORMULA:  
        //        default:
        //            return "=" + cell.CellFormula;
        //    }
        //}
        #endregion

        /// <summary>
        /// 下载Excel文件 XML
        /// </summary>
        /// <param name="dr">需要下载的数据 XML</param>
        /// <param name="fileName">文件名称</param>
        public static void XmlToExcel(string xmlFile,string filePath, string fileName)
        {
            if (xmlFile == "")
                return;

            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(fileName);

            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xmlFile);
            XmlNodeList xdNodeList = xd.SelectNodes("/note");

            int i = 0;
            IRow dataRow = null;
            if (xdNodeList != null)
            {
                foreach (XmlNode xd1 in xdNodeList)
                {
                    foreach (XmlNode xd2 in xd1)
                    {
                        dataRow = sheet.CreateRow(i);
                        int j = 0;
                        foreach (XmlNode xnd in xd2)
                        {
                            string value = xnd.InnerText;
                            dataRow.CreateCell(j).SetCellValue(value);
                            j++;
                        }
                        i++;
                    }
                }
            }

            //保存
            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
            }

        }

       #endregion


        #region 读取Excel
        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string filePath)
        {
            DataSet ds = new DataSet();
            IWorkbook workbook;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(file);
            }
            for (int k = 0; k < 6; k++)
            {
                ISheet sheet = null;
                try
                {
                    sheet = workbook.GetSheetAt(k);
                }
                catch{}

                if (sheet != null)
                {
                    DataTable dt = new DataTable();
                    System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                    IRow headerRow = sheet.GetRow(0);
                    if (headerRow != null)
                    {
                        int cellCount = headerRow.LastCellNum;

                        for (int j = 0; j < cellCount; j++)
                        {
                            ICell cell = headerRow.GetCell(j);
                            dt.Columns.Add(cell.ToString());
                        }

                        for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                        {
                            IRow row = sheet.GetRow(i);
                            DataRow dataRow = dt.NewRow();

                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                    dataRow[j] = row.GetCell(j).ToString();
                            }

                            dt.Rows.Add(dataRow);
                        }
                        ds.Tables.Add(dt);
                    }
                }

            }
            return ds;
        }

        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string filePath)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(file);
            }

            ISheet sheet = workbook.GetSheetAt(0);
            if (sheet != null)
            {
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;

                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    dt.Columns.Add(cell.ToString());
                }

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = dt.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();
                    }

                    dt.Rows.Add(dataRow);
                }
            }
            return dt;
        }

        public static DataSet ExcelToDS(string Path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [Sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            return ds;
        }

        #endregion
    }
}
