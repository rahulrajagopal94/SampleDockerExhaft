using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using OpenQA.Selenium;
using Ex_haft.Configuration;
using Ex_haft.Utilities;

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
            string reportPath = ConfigFile.GetAbsoluteFilePath("Results\\Report\\ExtentReport\\") + testName + ConfigFile.GetCurrentDateTime() + "\\TestReport.html";

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
            driver = ConfigFile.Init("Configuration\\appsettings.json");
            Constant.SetConfig("Configuration\\appsettings.json");
        }

        [TearDown]
        public static void Exit()
        {
            //Generate test report
            driver.Quit();
            Report.WriteResultToHtml(driver, report, screenshotList, testObjective, scriptName);
        }

        [OneTimeTearDown]
        public static void GenerateExtentReport()
        {
            extent.Flush();
        }
    }
}
