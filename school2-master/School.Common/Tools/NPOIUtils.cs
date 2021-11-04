using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace school.Common.Tools
{
  public  class NPOIUtils
    {
        private HSSFWorkbook hssfworkbook;
        private ISheet sheet1;

        public void BuildExcel()
        {
            hssfworkbook = new HSSFWorkbook();
            // 新建一个Excel页签
            sheet1 = hssfworkbook.CreateSheet("Sheet1");

            // 创建新增行
            for (var i = 0; i < 10; i++)
            {
                IRow row1 = sheet1.CreateRow(i);
                for (var j = 0; j < 10; j++)
                {
                    //新建单元格
                    ICell cell = row1.CreateCell(j);
                    // 单元格赋值
                    cell.SetCellValue("单元格" + j.ToString());
                }
            }
            // 设置行宽度
            sheet1.SetColumnWidth(2, 10 * 256);
            // 获取单元格 并设置样式
            ICellStyle styleCell = hssfworkbook.CreateCellStyle();
            //居中
            styleCell.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            //垂直居中
            styleCell.VerticalAlignment = VerticalAlignment.Top;
            ICellStyle cellStyle = hssfworkbook.CreateCellStyle();
            //设置字体
            IFont fontColorRed = hssfworkbook.CreateFont();
            fontColorRed.Color = HSSFColor.OliveGreen.Red.Index;
            styleCell.SetFont(fontColorRed);
            sheet1.GetRow(2).GetCell(2).CellStyle = styleCell;
            // 合并单元格
            sheet1.AddMergedRegion(new CellRangeAddress(2, 4, 2, 5));
            // 输出Excel
            string filename = "patient" + DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + ".xls";
            var context = HttpContext.Current;
            context.Response.ContentType = "application/vnd.ms-excel";
            context.Response.AddHeader("Content-Disposition",
                string.Format("attachment;filename={0}", context.Server.UrlEncode(filename)));
            context.Response.Clear();

            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);
            context.Response.BinaryWrite(file.GetBuffer());
            context.Response.End();
        }

        public static string getUrl(DataTable dtsource, List<string> rowName, string fileName)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("block");
            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            if (dtsource != null)
            {
                int rowCount = dtsource.Rows.Count + 1;
                int cellCount = dtsource.Columns.Count;
                Create(sheet, rowCount, cellCount);

                //for (int i = 0; i < dtsource.Columns.Count; i++)
                //{
                //    string cellValue = dtsource.Columns[i].ColumnName.ToString();
                //    sheet.GetRow(0).GetCell(i).SetCellValue(cellValue);
                //}
                for (int i = 0; i < rowName.Count; i++)
                {
                    string cellValue = rowName[i].ToString();
                    sheet.GetRow(0).GetCell(i).SetCellValue(cellValue);
                }

                for (int i = 0; i < dtsource.Rows.Count; i++)
                {
                    int row = i + 1;
                    for (int j = 0; j < dtsource.Columns.Count; j++)
                    {
                        string cellValue = dtsource.Rows[i][j].ToString();
                        sheet.GetRow(row).GetCell(j).SetCellValue(cellValue);
                    }
                }
            }
            var patUrl = HttpContext.Current.Server.MapPath("/ExportFile/");
            var partName = RandomNumUtils.GetDtRng_RandomNo();
            FileHelper.CreateDirectory(patUrl);
            using (FileStream fs = File.OpenWrite(@"" + patUrl + fileName + "-" + partName + ".xls"))
            //打开一个xls文件，如果没有则自行创建，如果存在myxls.xls文件则在创建是不要打开该文件！
            {
                workbook.Write(fs); //向打开的这个xls文件中写入mySheet表并保存。
            }

            //fileName + "-" + partName + ".xls"
            return "/ExportFile/" + fileName + "-" + partName + ".xls";
        }


        public static byte[] getFileStream(DataTable dtsource, List<string> rowName, string fileName)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("block");
            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();

            int rowCount = dtsource.Rows.Count + 1;
            int cellCount = dtsource.Columns.Count;
            Create(sheet, rowCount, cellCount);

            //for (int i = 0; i < dtsource.Columns.Count; i++)
            //{
            //    string cellValue = dtsource.Columns[i].ColumnName.ToString();
            //    sheet.GetRow(0).GetCell(i).SetCellValue(cellValue);
            //}
            for (int i = 0; i < rowName.Count; i++)
            {
                string cellValue = rowName[i].ToString();
                sheet.GetRow(0).GetCell(i).SetCellValue(cellValue);
            }

            for (int i = 0; i < dtsource.Rows.Count; i++)
            {
                int row = i + 1;
                for (int j = 0; j < dtsource.Columns.Count; j++)
                {
                    string cellValue = dtsource.Rows[i][j].ToString();
                    sheet.GetRow(row).GetCell(j).SetCellValue(cellValue);
                }
            } 
            var patUrl = HttpContext.Current.Server.MapPath("/ExportFile/");
            var partName = RandomNumUtils.GetDtRng_RandomNo(); 
            FileHelper.CreateDirectory(patUrl);
            using (FileStream fs = File.OpenWrite(@"" + patUrl + fileName + "-" + partName + ".xls"))
            {
                workbook.Write(fs); 
            }
            using (FileStream fs = new FileStream(@"" + patUrl + fileName + "-" + partName + ".xls", FileMode.Open, FileAccess.Read))
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);
                return buffur;
            } 
          
        }

        /// <summary>
        /// 创建单元格
        /// </summary>
        /// <param name="sheet">表</param>
        /// <param name="rowcount">行数</param>
        /// <param name="cellcount">列数</param>
        /// <returns>返回表</returns>
        public static ISheet Create(ISheet sheet, int rowcount, int cellcount)
        {
            for (int i = 0; i < rowcount; i++)
            {
                NPOI.SS.UserModel.IRow targetRow = null;
                targetRow = sheet.CreateRow(i);
                for (int j = 0; j < cellcount; j++)
                {
                    NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                    NPOI.SS.UserModel.ICell cell = row.CreateCell(j);
                }
            }
            return sheet;
        }

        public static MemoryStream CommonBaseExcel(DataTable dtsource)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("test_01");
            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();

            int rowCount = dtsource.Rows.Count + 1;
            int cellCount = dtsource.Columns.Count;
            Create(sheet, rowCount, cellCount);

            for (int i = 0; i < dtsource.Columns.Count; i++)
            {
                string cellValue = dtsource.Columns[i].ColumnName.ToString();
                sheet.GetRow(0).GetCell(i).SetCellValue(cellValue);
            }
            for (int i = 0; i < dtsource.Rows.Count; i++)
            {
                int row = i + 1;
                for (int j = 0; j < dtsource.Columns.Count; j++)
                {
                    string cellValue = dtsource.Rows[i][j].ToString();
                    sheet.GetRow(row).GetCell(j).SetCellValue(cellValue);
                }

            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                workbook = null;
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }

        public static DataSet GetImportExcel(string filePath)
        {
            string fileExt = Path.GetExtension(filePath);
            IWorkbook workbook;

            #region 初始化信息(兼容Excel2003、Excel2007)

            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (fileExt == ".xls")
                {
                    workbook = new HSSFWorkbook(file);
                }
                else if (fileExt == ".xlsx")
                {
                    workbook = new NPOI.XSSF.UserModel.XSSFWorkbook(file);
                }
                else
                {
                    return null;
                }
            }

            #endregion

            DataSet ds = new DataSet();


            int sheetLenth = workbook.NumberOfSheets;

            for (int m = 0; m < sheetLenth; m++)
            {
                ISheet sheet = workbook.GetSheetAt(m);
                string tableName = workbook.GetSheetName(m);
                int colCount = 0; //最大列数

                for (int i = 0; i < sheet.PhysicalNumberOfRows; i++)
                {
                    var rowObj = sheet.GetRow(i);
                    if (rowObj != null)
                        if (rowObj.LastCellNum > colCount)
                            colCount = rowObj.LastCellNum;
                }

                #region 将数据加载到内容表里

                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                DataTable dt = new DataTable();
                int index = 0;
                while (rows.MoveNext())
                {
                    IRow row;
                    if (fileExt == ".xls") //兼容Excel2003
                    {
                        row = (HSSFRow)rows.Current;
                    }
                    else if (fileExt == ".xlsx") //兼容Excel2007
                    {
                        row = (NPOI.XSSF.UserModel.XSSFRow)rows.Current;
                    }
                    else
                    {
                        row = null;
                    }
                    if (row != null)
                    {
                        index++;
                        if (index.Equals(1))
                        {
                            for (int j = 0; j < colCount; j++)
                            {
                                dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
                            }
                        }

                    }

                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        ICell cell = row.GetCell(i);


                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString().Trim();
                        }
                    }
                    dt.Rows.Add(dr);
                }
                dt.TableName = tableName;
                ds.Tables.Add(dt);

                #endregion
            }
            return ds;
        }
        public static DataSet GetImportExcelNew(Stream fs, string fileExt)
        {

            IWorkbook workbook;

            #region 初始化信息(兼容Excel2003、Excel2007)

            if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook(fs);
            }
            else if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook(fs);
            }
            else
            {
                return null;
            }

            #endregion

            DataSet ds = new DataSet();


            int sheetLenth = workbook.NumberOfSheets;

            for (int m = 0; m < sheetLenth; m++)
            {
                ISheet sheet = workbook.GetSheetAt(m);
                string tableName = workbook.GetSheetName(m);
                int colCount = 0; //最大列数

                for (int i = 0; i < sheet.PhysicalNumberOfRows; i++)
                {
                    var rowObj = sheet.GetRow(i);
                    if (rowObj != null)
                        if (rowObj.LastCellNum > colCount)
                            colCount = rowObj.LastCellNum;
                }

                #region 将数据加载到内容表里

                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                DataTable dt = new DataTable();
                int index = 0;
                while (rows.MoveNext())
                {
                    IRow row;
                    if (fileExt == ".xls") //兼容Excel2003
                    {
                        row = (HSSFRow)rows.Current;
                    }
                    else if (fileExt == ".xlsx") //兼容Excel2007
                    {
                        row = (NPOI.XSSF.UserModel.XSSFRow)rows.Current;
                    }
                    else
                    {
                        row = null;
                    }
                    if (row != null)
                    {
                        index++;
                        if (index.Equals(1))
                        {
                            for (int j = 0; j < colCount; j++)
                            {
                                dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
                            }
                        }

                    }

                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        ICell cell = row.GetCell(i);


                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString().Trim();
                        }
                    }
                    dt.Rows.Add(dr);
                }
                dt.TableName = tableName;
                ds.Tables.Add(dt);

                #endregion
            }
            return ds;
        }
        public static DataTable ReadHead(string path)
        {
            var patUrl2 = HttpContext.Current.Server.MapPath("");
            var patUrl = HttpContext.Current.Server.MapPath("/");
            var patUrl3 = HttpContext.Current.Server.MapPath("/upload/");
            string fileName = patUrl + path;  //路径
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate); //读取文件流
            HSSFWorkbook workbook = new HSSFWorkbook(fs);  //根据EXCEL文件流初始化工作簿
            var sheet1 = workbook.GetSheetAt(0); //获取第一个sheet
            DataTable table = new DataTable();//
            var row1 = sheet1.GetRow(0);//获取第一行即标头
            int cellCount = row1.LastCellNum; //第一行的列数
            //把第一行的数据添加到datatable的列名
            for (int i = row1.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(row1.GetCell(i).StringCellValue);
                if (!string.IsNullOrEmpty(row1.GetCell(i).StringCellValue))
                    table.Columns.Add(column);
            }
            workbook = null; //清空工作簿--释放资源
            sheet1 = null;  //清空sheet</pre><br>
            return table;
        }
        public static DataTable ReadExcelAll(string path)
        {
            //string fileName = @"e:\myxls.xls";  //路径
            var patUrl = HttpContext.Current.Server.MapPath("/");
            string fileName = patUrl + path;
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate); //读取文件流
            HSSFWorkbook workbook = new HSSFWorkbook(fs);  //根据EXCEL文件流初始化工作簿
            var sheet1 = workbook.GetSheetAt(0); //获取第一个sheet
            DataTable table = new DataTable();//
            var row1 = sheet1.GetRow(0);//获取第一行即标头
            int cellCount = row1.LastCellNum; //第一行的列数
            //把第一行的数据添加到datatable的列名
            for (int i = row1.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(row1.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            int rowCount = sheet1.LastRowNum; //总行数
            //把每行数据添加到datatable中
            for (int i = (sheet1.FirstRowNum + 1); i <= sheet1.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet1.GetRow(i);
                if (row.GetCell(0) != null)
                {
                    DataRow dataRow = table.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();
                    }

                    if (!string.IsNullOrEmpty(row.GetCell(0).ToString()))
                        table.Rows.Add(dataRow);
                }
            }
            //到这里 table 已经可以用来做数据源使用了
            workbook = null; //清空工作簿--释放资源
            sheet1 = null;  //清空sheet</pre><br>
            return table;
        }
    }
} 
