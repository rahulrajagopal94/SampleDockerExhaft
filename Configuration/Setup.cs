using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using OpenQA.Selenium;
using Ex_haft.Configuration;
using Ex_haft.Utilities;
using System;

namespace SampleDocker.Configuration
{
    public class Setup
    {

        public IWebDriver driver;
        public ExtentReports extent;
        public ExtentHtmlReporter htmlReporter;
        public string testObjective;
        public string scriptName;
        public List<TestReportSteps> report = null;
        public (List<TestReportSteps>, List<string>) artifacts;
        public JArray testData;
        public ExtentTest reporter;
        public List<string> screenshotList = new List<string>();

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
        public void BeforeEachTest()
        {
            ConfigFile configFile = new ConfigFile();
            driver = configFile.Init("Configuration//AppSettings.json");
            Constant.SetConfig("Configuration//AppSettings.json");
        }

        [TearDown]
        public void Exit()
        {
            //Generate test report
            driver.Quit();
            Console.WriteLine(scriptName + "-" + screenshotList.Count + "-" + report.Count);
            screenshotList.ForEach(Console.WriteLine);
            Report.WriteResultToHtml(driver, report, screenshotList, testObjective, scriptName);
        }

        [OneTimeTearDown]
        public void GenerateExtentReport()
        {
            extent.Flush();
        }
    }
}
