using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using MongoDB.Driver.Core.Servers;

namespace Blinds
{
    public class ExcelReaderFile
    {
        public string path;
        public FileStream fs = null;
        private XSSFWorkbook workbook = null;
        private ISheet sheet = null;
        private IRow row = null;
        private ICell cell = null;

        public ExcelReaderFile(string path)
        {

            this.path = path;
            //this.path = Server.MapPath("Data.xlsx");
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                int k = 0;
                workbook = new XSSFWorkbook(fs);
                sheet = workbook.GetSheetAt(0);
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public int getRowCount(string sheetName)
        {
            int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
                return 0;
            else
            {
                sheet = workbook.GetSheetAt(index);
                int number = sheet.LastRowNum + 1;
                return number;
            }
        }

        public int getColumnCount(string sheetName)
        {
            if (!isSheetExist(sheetName))
                return -1;
            sheet = workbook.GetSheet(sheetName);
            row = sheet.GetRow(0);
            if (row == null)
                return -1;
            return row.LastCellNum;
        }

        public int getRowNumber(string sheetName, int colNum, string value)
        {
            fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            workbook = new XSSFWorkbook(fs);
            int d = 0;
            int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
                return 0;

            sheet = workbook.GetSheetAt(index);
            for (int rw = 0; rw < sheet.LastRowNum; rw++)
            {
                if (sheet.GetRow(rw) != null)
                {
                    row = sheet.GetRow(rw);
                }

            }

            return row.RowNum;


        }

        public string getCellData(string sheetName, int colNum, int rowNum)
        {
         
           // fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
           // workbook = new XSSFWorkbook(fs);

            if (rowNum <= 0)
                return "";
            int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
                return "";
            sheet = workbook.GetSheetAt(index);
            row = sheet.GetRow(rowNum - 1);
            if (row == null)
                return "";
            cell = row.GetCell(colNum);
            if (cell == null)
                return "";
            if (cell.CellType == CellType.String)
                return cell.StringCellValue;
            else if (cell.CellType == CellType.Numeric || cell.CellType == CellType.Formula)
            {
                string cellText = Convert.ToString(cell.NumericCellValue);
                return cellText;
            }
            else if (cell.CellType == CellType.Blank)
                return "";
            else
                return cell.BooleanCellValue.ToString();

        }

        public string getCellData(string sheetName, string colName, int rowNum)
        {
           
            if (rowNum <= 0)
                return "";
            int colNum = -1;
           int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
                return "";
            sheet = workbook.GetSheetAt(index);
            row = sheet.GetRow(0);
            for (int i = 0; i < row.LastCellNum; i++)
            {
                if (row.GetCell(i).StringCellValue.Trim().Equals(colName.Trim()))
                {
                    colNum = i;
                }
            }
            if (colNum == -1)
                return "";
           // sheet = workbook.GetSheetAt(index);
            row = sheet.GetRow(rowNum - 1);
            if (row == null)
                return "";
            cell = row.GetCell(colNum);
            if (cell == null)
                return "";
            if (cell.CellType == CellType.String)
                return cell.StringCellValue;
            else if (cell.CellType == CellType.Numeric || cell.CellType == CellType.Formula)
            {
                string cellText = Convert.ToString(cell.NumericCellValue);
                return cellText;
            }
            else if (cell.CellType == CellType.Blank)
                return "";
            else
                return cell.BooleanCellValue.ToString();
        }

        public bool isSheetExist(string sheetName)
        {
            int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
            {
                index = workbook.GetSheetIndex(sheetName.ToUpper());
                if (index == -1)
                    return false;
                else
                    return true;
            }
            else
                return true;
        }

        public int getCellRowNum(string sheetName, string colName, string cellValue)
        {
            for (int i = 0; i < getRowCount(sheetName); i++)
            {
                if (getCellData(sheetName, colName, i).Equals(cellValue))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
