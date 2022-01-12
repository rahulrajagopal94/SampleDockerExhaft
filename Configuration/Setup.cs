using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using OpenQA.Selenium;
using Ex_haft.Configuration;
using Ex_haft.Utilities;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace SampleDocker.Configuration
{
    class Setup
    {

        public static IWebDriver driver;
        public static ExtentReports extent;
        public static ExtentHtmlReporter htmlReporter;
        public static string testObjective;
        public static string scriptName;
        public static List<TestReportSteps> report = null;
        public static JArray testData;
        public static ExtentTest reporter;
        public static List<string> screenshotList = new List<string>();

        public Setup()
        {
            if (ConfigFile.IsRunFromDriverFile())
            {
                ConfigureExtentReport();
                BeforeEachTest();

            }
        }

        [OneTimeSetUp]
        public void ConfigureExtentReport()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            string reportPath = ConfigFile.GetAbsoluteFilePath("Results/Report/ExtentReport/") + testName + "/TestReport.html";
            extent = new ExtentReports();
            htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.Config.DocumentTitle = "Automation Testing Report";
            htmlReporter.Config.ReportName = "Automation Testing";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            extent.AttachReporter(htmlReporter);
        }

        [SetUp]
        public static void BeforeEachTest()
        {
            //var options = new ChromeOptions();
            //var remoteUrl = "http://localhost:4444/wd/hub";
            //driver = new RemoteWebDriver(new Uri(remoteUrl), options);
            driver = ConfigFile.Init("Configuration/AppSettings.json");
            Constant.SetConfig("Configuration/AppSettings.json");
        }

        [TearDown]
        public static void Exit()
        {
            //Generate test report
   //         driver.Quit();

            Thread.Sleep(3000);

            Report.WriteResultToHtml(driver, report, screenshotList, testObjective, scriptName);
        }

        [OneTimeTearDown]
        public static void GenerateExtentReport()
        {
            extent.Flush();
        }
    }
}
