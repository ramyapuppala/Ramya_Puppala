using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blinds
{
    public class AppKeywords : GenericKeywords
    {
        ExtentTest test;
        private float a;

        public AppKeywords(ExtentTest test) : base(test)
        {
            this.test = test;
        }

        

        

        public String verify()
        {
          

            List<String> NewList1 = new List<String>();
            NewList1 = search_sort();

            List<float> floatList = NewList1.Select(x => float.Parse(x)).ToList();
         
            List<float> check = floatList;
            check.Sort();
          
            Assert.AreEqual(floatList, check, "The list of elements under Price-to-low  catergory are sorted in Ascending order");
            return Constants.PASS;


        }



        public List<String> search_sort()
        {
            IList<IWebElement> price = getElements("PriceLowtoHigh_elements_xpath");
         
           List<String> NewList = new List<String>();
            List<String> NewList2 = new List<String>();

            foreach (var item in price)
            {

                if (item.Text == "N/A" ||item.Text == ""|| item.Text == "  ")
                    continue;

                NewList.Add(item.Text);

                var match = Regex.Match(item.Text, @"([-+]?[0-9]*\.?[0-9]+)");
                if (match.Success)
                a = Convert.ToSingle(match.Groups[1].Value);
                NewList2.Add(a.ToString());

            //    string[] numbers = Regex.Split(item.Text, @"\D+");
             //   foreach (string value in numbers)
             //   {
               //     int i=0;
                 //   if (!string.IsNullOrEmpty(value))
                   // {
                     //   i = int.Parse(value);
                       // Console.WriteLine("Number: {0}", i);
                   // }
                   // NewList.Add(i.ToString());
               // }
            }
            return NewList2;
        }
            
          }

        }
      





 