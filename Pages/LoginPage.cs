using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using AventStack.ExtentReports;
using RestSharp;
using Newtonsoft.Json;
using RestSharp.Serialization.Json;
using System.Threading;

namespace SampleDocker.Pages
{

    class LoginPage
    {
        public static List<string> screenshotList = new List<string>();
        public static JObject jObject;

        static LoginPage()
        {
            jObject = ConfigFile.RetrieveUIMap("LoginPageSelector.json");
        }

        
        /// <summary>
        /// To Verify that user is able to login to the application
        /// </summary>
        /// <param name="inputjson">The input json</param>
        /// <returns>Test reports</returns>
        public static List<TestReportSteps> LoginToApplication(IWebDriver driver, JToken inputjson, ref ExtentTest reporter)
        {
            List<TestReportSteps> listOfReport = new List<TestReportSteps>();
            screenshotList.Clear();
            int step = 0;
            string objective = "To verify that user is able to login to the application";
            
            try
            {

                Thread.Sleep(40000);
                //listOfReport.Add(ReusableComponents.GenerateReportSteps("Click on the My Account button.", "", objective, step));
                //ReusableComponents.JEClick(driver, "XPath", inputjson["myaccount"].ToString());
                //listOfReport[step++].actualResultFail = "";
                //reporter.Log(Status.Pass, ReusableComponents.GenerateExtendReportSteps("Click on the My Account button.", "").ToString());

                ////Click on the 'Login' button
                //listOfReport.Add(ReusableComponents.GenerateReportSteps("Click on the Login button.", "", objective, step));
                //ReusableComponents.Click(driver, "XPath", jObject["loginPage"].ToString());
                //listOfReport[step++].actualResultFail = "";
                //reporter.Log(Status.Pass, ReusableComponents.GenerateExtendReportSteps("Click 'Login' button", "").ToString());

                //Enter username
                listOfReport.Add(ReusableComponents.GenerateReportSteps("Enter username: " + "","", objective, step));
                ReusableComponents.SendKeys(driver, "Id", jObject["username"].ToString(), inputjson["username"].ToString());
                listOfReport[step++].actualResultFail = "";
                reporter.Log(Status.Pass, ReusableComponents.GenerateExtendReportSteps("Enter username: "+ inputjson["username"].ToString(),"").ToString());              

                //Enter password
                listOfReport.Add(ReusableComponents.GenerateReportSteps("Enter password.", "password", objective, step));
                ReusableComponents.SendKeys(driver, "Id", jObject["password"].ToString(), inputjson["password"].ToString());
                listOfReport[step++].actualResultFail = "";
                reporter.Log(Status.Pass, ReusableComponents.GenerateExtendReportSteps("Enter password", "").ToString());

                //Click on the 'Login' button
                listOfReport.Add(ReusableComponents.GenerateReportSteps("Click on the Login button.", "", objective, step));
                ReusableComponents.Click(driver, "XPath", jObject["loginButton"].ToString());
                listOfReport[step++].actualResultFail = "";
                reporter.Log(Status.Pass, ReusableComponents.GenerateExtendReportSteps("Click 'Login' button", "").ToString());



                //Wait for page to load
                Thread.Sleep(10000);                
               

            }
            catch (Exception e)
            {
                Console.WriteLine("LoginToApplication failed: " + e);
                reporter.Fail("LoginToApplication failed");
            }

            return listOfReport;
        }

        /// <summary>
        /// Retrieve list of screenshots captured
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLoginPageScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }
    }
}