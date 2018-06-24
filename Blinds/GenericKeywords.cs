using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blinds
{
    public class GenericKeywords
    {
        ExtentTest test;
       
        public IWebDriver driver = null;
        public GenericKeywords(ExtentTest test)
        {
            this.test = test;
        }
        public string openBrowser(string browserType)
            {
                string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
                filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;

                if (browserType.Equals("Mozilla"))
                    //driver = new FirefoxDriver(filePath + "\\Drivers");
                    driver = new FirefoxDriver();
                else if (browserType.Equals("Chrome"))
                    //driver = new ChromeDriver(filePath + "\\Drivers");
                    driver = new ChromeDriver();
                else if (browserType.Equals("IE"))
                    //driver = new InternetExplorerDriver(filePath + "\\Drivers");
                    driver = new InternetExplorerDriver();
           
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            return Constants.PASS;
        }

      
        public string navigate(string urlKey)
        {
            test.Log(Status.Info, "Navigating to " + ConfigurationManager.AppSettings[urlKey]);
            driver.Url = ConfigurationManager.AppSettings[urlKey];
            return Constants.PASS;
        }
        public string input(string locatorKey, string data)
        {
            test.Log(Status.Info, "Typing in " + ConfigurationManager.AppSettings[locatorKey]);
            IWebElement e = getElement(locatorKey);
            e.SendKeys(data);
            return Constants.PASS;
        }
        public string click(string locatorKey)
        {
            test.Log(Status.Info, "Clicking on " + ConfigurationManager.AppSettings[locatorKey]);
            IWebElement e = getElement(locatorKey);
            e.Click();
            return Constants.PASS;
        }

        public string takeScreenshot()
        {
            string screenshotFile = DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(" ", "_") + ".png";
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            string filePath = @"C:\Selenium\Screenshots\";
            string screenshotPath = filePath + screenshotFile;
            screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
            test.Log(Status.Info, "Screenshot - ", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
            test.AddScreenCaptureFromPath(screenshotPath);

            return Constants.PASS;
        }


        public string reportFailure(string failureMsg)
        {
            takeScreenshot();
            test.Log(Status.Fail, failureMsg);
            takeScreenshot();

            return Constants.PASS;
        }
        public String list_of_elements(string locatorKey)
        {
            test.Log(Status.Info, "getting Elements " + ConfigurationManager.AppSettings[locatorKey]);
            getElements(locatorKey);
            return Constants.PASS;
        }
        public string getText(string locatorKey)
        {
            return getElement(locatorKey).Text;
        }
        public IWebElement getElement(string locatorKey)
        {
            IWebElement e = null;

            try
            {
                if (locatorKey.EndsWith("_id"))
                    e = driver.FindElement(By.Id(ConfigurationManager.AppSettings[locatorKey]));
                else if (locatorKey.EndsWith("_name"))
                    e = driver.FindElement(By.Name(ConfigurationManager.AppSettings[locatorKey]));
                else if (locatorKey.EndsWith("_xpath"))
                    e = driver.FindElement(By.XPath(ConfigurationManager.AppSettings[locatorKey]));
                
            }
            catch (Exception ex)
            {
                Assert.Fail("Failure in element extraction " + locatorKey);
                test.Log(Status.Fail, "Failure in element extraction " + locatorKey);
            }

            return e;
        }



        public IList<IWebElement> getElements(string locatorKey)
        {
            IList<IWebElement> List_of_elements = null;
            try
            {
                if (locatorKey.EndsWith("_id"))
                    List_of_elements = driver.FindElements(By.Id(ConfigurationManager.AppSettings[locatorKey]));
                else if (locatorKey.EndsWith("_name"))
                    List_of_elements = driver.FindElements(By.Name(ConfigurationManager.AppSettings[locatorKey]));
                else if (locatorKey.EndsWith("_xpath"))
                    List_of_elements = driver.FindElements(By.XPath(ConfigurationManager.AppSettings[locatorKey]));
                
               // foreach (var a in List_of_elements)
               // {
                    
               // }
            }

            catch (Exception ex)
            {
                Assert.Fail("Failure in element extraction " + locatorKey);
                test.Log(Status.Fail, "Failure in element extraction " + locatorKey);
            }

            return List_of_elements;


        }

        
            /* *********************** validation Functions ************* */
      
        public string verifyElementPresent(string locatorKey)
        {
            // true - Present
            // false - Not
            bool result = isElementPresent(locatorKey);
            if (result)
                return Constants.PASS;
            else
                return Constants.FAIL;
        }
        public string verifyElementNotPresent(string locatorKey)
        {
            bool result = isElementPresent(locatorKey);
            if (result)
                return Constants.FAIL;
            else
                return Constants.PASS;
        }
        /* ******************** Utility Functions ********************* */
        public bool isElementPresent(string locatorKey)
        {
            IList<IWebElement> e = null;
            try
            {
                if (locatorKey.EndsWith("_id"))
                    e = driver.FindElements(By.Id(ConfigurationManager.AppSettings[locatorKey]));
                else if (locatorKey.EndsWith("_name"))
                    e = driver.FindElements(By.Name(ConfigurationManager.AppSettings[locatorKey]));
                else if (locatorKey.EndsWith("_xpath"))
                    e = driver.FindElements(By.XPath(ConfigurationManager.AppSettings[locatorKey]));
            }
            catch (Exception ex)
            {
                Assert.Fail("Failure in element extraction " + locatorKey);
                test.Log(Status.Fail, "Failure in element extraction " + locatorKey);
            }
            if (e.Count == 0)
                return false;
            else
                return true;
        }
        public string closeBrowser()
        {
            test.Log(Status.Info, "Closing browser");
            driver.Quit();
            //driver.Close();
            return Constants.PASS;
        }
        public string wait(String timeToWait)
        {
            try
            {
                Thread.Sleep(Convert.ToInt32(timeToWait)*1000);
            }
            catch (Exception ex)
            {

            }
            return Constants.PASS;
        }
       
        
      
       

      
        
          
    }
}
