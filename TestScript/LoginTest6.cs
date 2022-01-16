using Ex_haft.Configuration;
using NUnit.Framework;
using SampleDocker.Configuration;
using SampleDocker.Pages;


namespace SampleDocker.TestScript
{
    [TestFixture]
    [Parallelizable]
    class LoginTest6 : Setup
    {
        
        public LoginTest6() : base()
        {
            testObjective = "To Verify that user is able to login to the web application.";
            scriptName = "TestScript6";
            testData = ConfigFile.RetrieveInputTestData("LoginTest.json");

            if (ConfigFile.IsRunFromDriverFile())
                VerifyLogin();
        }


       [Test, Category("Smoke")]
        public void VerifyLogin()
        {
            screenshotList.Clear();
            if (testData != null)
            {
                foreach (var input in testData)
                {
                    screenshotList.Clear();

                    reporter = extent.CreateTest("LoginTest").Info("Login test started");

                    artifacts = LoginPage.LoginToApplication(scriptName, driver, input, ref reporter);
                    report = artifacts.Item1;
                    foreach (string screenshot in artifacts.Item2)
                        screenshotList.Add(screenshot);

                    artifacts = AddressPage.OpenAddressBook(scriptName, driver, ref reporter);
                    report.AddRange(artifacts.Item1);
                    foreach (string screenshot in artifacts.Item2)
                        screenshotList.Add(screenshot);

                    reporter.Info("Login test finished");
                }
            }

            //Exit for driver file
            if (ConfigFile.IsRunFromDriverFile())
            {
                Exit();
                GenerateExtentReport();
            }
        }

    }
}
