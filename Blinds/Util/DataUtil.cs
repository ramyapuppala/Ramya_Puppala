using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blinds

    { 
    public class DataUtil
    {
        public static object[] getData(ExcelReaderFile xls, string testCaseName)
        {

            string sheetName = "Data";

            // Read data for only testCaseName
            int testStartRowNum = 1;
            //int colStartRowNum = 1;

            while (!xls.getCellData(sheetName, 0, testStartRowNum).Equals(testCaseName))
            {
                testStartRowNum++;
            }
            Console.WriteLine("Test starts from row : " + testStartRowNum);

             int colStartRowNum = 1 + testStartRowNum;
            int dataStartRowNum = 2 + testStartRowNum;

            // Calculate rows of data
            int rows = 0;
            while (!xls.getCellData(sheetName, 0, dataStartRowNum + rows).Equals(""))
            {
                rows++;
            }
            Console.WriteLine("Total number of rows : " + rows);

            // Calculate total number of cols
            int cols = 0;
            while (!xls.getCellData(sheetName, cols, colStartRowNum).Equals(""))
            {
                cols++;
            }
            Console.WriteLine("Total number of cols : " + cols);

            // Read data
            object[][] data = new object[rows][];
            int dataRow = 0;
            Dictionary<string, string> table = null;

            for (int rNum = dataStartRowNum; rNum < dataStartRowNum + rows; rNum++)
            {
                data[rNum - dataStartRowNum] = new object[1];
                table = new Dictionary<string, string>();
                for (int cNum = 0; cNum < cols; cNum++)
                {
                    string key = xls.getCellData(sheetName, cNum, colStartRowNum);
                    string value = xls.getCellData(sheetName, cNum, rNum);
                    table.Add(key, value);
                }
                data[dataRow][0] = table;
                dataRow++;
            }
            return data;
        }
        public static bool isSkip(ExcelReaderFile xls, string testCaseName)
        {
            // true - N
            // false - Y
            int rows = xls.getRowCount("TestCases");
            for (int rNum = 2; rNum <= rows; rNum++)
            {
                string tcid = xls.getCellData("TestCases", "TCID", rNum);
                if (tcid.Equals(testCaseName))
                {
               
                        return true;

                }
            }
            return true;
        }
    }
}
