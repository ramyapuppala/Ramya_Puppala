using AventStack.ExtentReports;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blinds
{
    [TestFixture]
   
    public class SearchTest : BaseTest
    {
       static ExcelReaderFile xls = new ExcelReaderFile(Constants.FA_Xls);

        static string testCaseName = "SearchTest";

        [Test, TestCaseSource("getData")]
        public void searchTest(Dictionary<string, string> data)
        {
            rep = ExtentManager.getInstance();
            ExtentTest test = rep.CreateTest("SearchTest", "This test will describe my Login Test!");

            //if (DataUtil.isSkip(xls, testCaseName))
            //{
            //    test.Log(Status.Skip, "Skipping the Test as Runmode is No");
            //    Assert.Ignore("Skipping the Test as Runmode is No");
            //}
            app = new Keywords(test);

          
            test.Log(Status.Info, "Executing Keywords");
            app.executeKeywords(testCaseName, xls, data);
           
        }
        // Data Source
        public static object[] getData()
        {
            return DataUtil.getData(xls, testCaseName);
        }
    }
}
