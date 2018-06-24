using AventStack.ExtentReports;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blinds
{
    public class BaseTest
    {
        public ExtentReports rep;
        public Keywords app = null;

        
        [TearDown]
        public void quit()
        {
            if (rep != null)
                rep.Flush();
            if (app != null)
                app.getGenericKeywords().closeBrowser();

        }
    }
}
