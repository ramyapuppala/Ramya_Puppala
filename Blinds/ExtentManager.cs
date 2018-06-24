using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blinds
{
    public class ExtentManager
    {
        public static ExtentHtmlReporter htmlReporter;
        private static ExtentReports extent;

        private ExtentManager()
        {

        }
        // comment
        // 2nd comment
        public static ExtentReports getInstance()
        {
            if (extent == null)
            {
                string reportFile = DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(" ", "_") + ".html";
                string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
                filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
                
                htmlReporter = new ExtentHtmlReporter(filePath + "\\Reports\\" + reportFile);
                
                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);

                extent.AddSystemInfo("OS", "Windows");
                extent.AddSystemInfo("Environment", "Automation Testing");
                extent.AddSystemInfo("User Name", "Sruthi"); 
              



                htmlReporter.LoadConfig(filePath + "\\Util\\extent-config.xml");

            }
            return extent;
        }

    }
}
