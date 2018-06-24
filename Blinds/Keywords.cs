using AventStack.ExtentReports;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blinds
{
    public class Keywords
    {
        
        ExtentTest test;
        AppKeywords app;
        public Keywords(ExtentTest test)
        {
            this.test = test;
        }

        public AppKeywords getGenericKeywords()
        {
            return app;
        }
        public void executeKeywords(string testUnderExecution, ExcelReaderFile xls, Dictionary<string, string> testData)
        {

            app = new AppKeywords(test);

            int rows = xls.getRowCount(Constants.SHEETNAME);
            for (int rNum = 2; rNum <= rows; rNum++)
            {
                //app.reportFailure("Error Message");
                string TCID = xls.getCellData(Constants.SHEETNAME, Constants.TCID_COL, rNum);
                if (TCID.Equals(testUnderExecution))
                {
                    string data = null;
                    string keyword = xls.getCellData(Constants.SHEETNAME, Constants.KEYWORD_COL, rNum);
                    string objct = xls.getCellData(Constants.SHEETNAME, Constants.OBJECT_COL, rNum);
                    string key = xls.getCellData(Constants.SHEETNAME, Constants.DATA_COL, rNum);
                    if (!key.Equals(""))
                    {
                        data = testData[key];
                    }

                    test.Log(Status.Info, TCID + "-----" + keyword + "-----" + objct + "-----" + data);

                    string result = "";
                    if (keyword.Equals("openBrowser"))
                        result = app.openBrowser(data);
                    else if (keyword.Equals("navigate"))
                        result = app.navigate(objct);
                    else if (keyword.Equals("input"))
                        result = app.input(objct, data);
                    else if (keyword.Equals("input"))
                        result = app.input(objct, data);
                    else if (keyword.Equals("click"))
                        result = app.click(objct);
                    else if (keyword.Equals("closeBrowser"))
                        result = app.closeBrowser();
                    else if (keyword.Equals("wait"))
                        result = app.wait(objct);
                    else if (keyword.Equals("verifyElementPresent"))
                        result = app.verifyElementPresent(objct);
                    else if (keyword.Equals("verifyElementNotPresent"))
                        result = app.verifyElementNotPresent(objct);
                    else if (keyword.Equals("list_of_elements"))
                        result = app.list_of_elements(objct);
                    else if (keyword.Equals("verify"))
                        result = app.verify();
                   





                    if (!result.Equals(Constants.PASS))
                    {
                        
                        Assert.Fail(result);
                    }

                }
            }
        }
    }
}
