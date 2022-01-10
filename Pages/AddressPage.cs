using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using AventStack.ExtentReports;
using SampleDocker.Utilities;
using System.Linq;
using RestSharp.Serialization.Json;
using RestSharp;
using Newtonsoft.Json;
using System.Threading;

namespace SampleDocker.Pages
{
    class AddressPage
    {
        public static List<string> screenshotList = new List<string>();
        public static JObject jObject;
        public static int importId = 0;

        static AddressPage()
        {
            jObject = ConfigFile.RetrieveUIMap("AddressPageSelector.json");
        }

        /// <summary>
        /// To Verify that user is able to view the homepage of the application
        /// </summary>
        /// <param name="inputjson">The input json</param>
        /// <returns>Test reports</returns>
        public static List<TestReportSteps> OpenAddressBook(IWebDriver driver, ref ExtentTest reporter)
        {
            List<TestReportSteps> listOfReport = new List<TestReportSteps>();
            screenshotList.Clear();
            int step = 0;
            string objective = "To verify that Address Book page is loaded.";

            try
            {

                //Click on the 'Login' button
                listOfReport.Add(ReusableComponents.GenerateReportSteps("Click on the Address Book button.", "", objective, step));
                ReusableComponents.Click(driver, "XPath", jObject["address"].ToString());
                listOfReport[step++].actualResultFail = "";
                reporter.Log(Status.Pass, ReusableComponents.GenerateExtendReportSteps("Click 'Address Book' button", "").ToString());

                listOfReport.Add(ReusableComponents.GenerateReportSteps("Verify that the address book page is loaded. Capture Screenshot.", "", objective, step));
                string expectedUrl = "route=account/addressing";
                string actualUrl = driver.Url;
                Console.WriteLine(expectedUrl);
                if(actualUrl.Contains(expectedUrl))
                {
                    listOfReport[step++].actualResultFail = "";
                    reporter.Log(Status.Pass, ReusableComponents.GenerateExtendReportSteps("Verify that the homepage is loaded.", "")).ToString();
                }
                else
                {
                    step++;
                }
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Homepage" + ConfigFile.GetCurrentDateTime())); ;
            }
            catch (Exception e)
            {
                Console.WriteLine("Homepage load failed: " + e);
                reporter.Fail("Verify Home page failed");
                if (!listOfReport[step].GetStepDescription().Contains("Capture Screenshot") == true)
                {
                    listOfReport[step].stepDescription = listOfReport[step].stepDescription + ", Capture Screenshot.";
                }
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "VerifyThatHomePageIsLoaded" + ConfigFile.GetCurrentDateTime()));
            }
            return listOfReport;
        }


   

        /// <summary>
        /// Retrieve list of screenshots captured
        /// </summary>
        /// <returns></returns>
        public static List<string> GetHomePageScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }
    }
}
