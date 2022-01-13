using Ex_haft.Configuration;
using NUnit.Framework;
using SampleDocker.Configuration;
using SampleDocker.Pages;


namespace SampleDocker.TestScript
{
     //[TestFixture]
 //   [Parallelizable]
    class LoginTest15 : Setup
    {
        
        public LoginTest15() : base()
        {
            testObjective = "To Verify that user is able to login to the web application.";
            scriptName = "15Login to Application";
            testData = ConfigFile.RetrieveInputTestData("LoginTest5.json");

            if (ConfigFile.IsRunFromDriverFile())
                VerifyLogin15();
        }


       [Test, Category("Smoke")]
        public void VerifyLogin15()
        {
            if (testData != null)
            {
                foreach (var input in testData)
                {
                    reporter = extent.CreateTest("LoginTest").Info("Login test started");

                    report = LoginPage.LoginToApplication(driver, input, ref reporter);
                    foreach (string screenshot in LoginPage.GetLoginPageScreenshots())
                        screenshotList.Add(screenshot);

                    report.AddRange(AddressPage.OpenAddressBook(driver, ref reporter));
                    foreach (string screenshot in AddressPage.GetHomePageScreenshots())
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
