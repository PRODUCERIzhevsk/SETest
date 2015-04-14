using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SETest.Tests.Saucelabs
{
    [TestFixture]
    [Header("browser", "version", "platform")] 
    [Row("internet explorer", "11", "Windows 7")]
    [Row("internet explorer", "6", "Windows XP")]
    [Row("internet explorer", "7", "Windows XP")]
    [Row("firefox", "32", "OS X 10.6")]
    public class InvoiceTest : SETest.Tests.InvoiceTest
    {
        private static class SauselabConstatnts
        {
            public const string SAUCE_LABS_ACCOUNT_NAME = "producerizhevsk";
            public const string SAUCE_LABS_ACCOUNT_KEY = "564d1f0f-ab59-401c-b3ae-fa4d2e2de496";
        }

        public override IWebDriver Setup(string browser, string version, string platform)
        {
            // construct the url to sauce labs
            Uri commandExecutorUri = new Uri("http://ondemand.saucelabs.com/wd/hub");

            // set up the desired capabilities
            DesiredCapabilities desiredCapabilites = new DesiredCapabilities(browser, version, Platform.CurrentPlatform); // set the desired browser
            desiredCapabilites.SetCapability("platform", platform); // operating system to use
            desiredCapabilites.SetCapability("username", SauselabConstatnts.SAUCE_LABS_ACCOUNT_NAME); // supply sauce labs username
            desiredCapabilites.SetCapability("accessKey", SauselabConstatnts.SAUCE_LABS_ACCOUNT_KEY);  // supply sauce labs account key
            desiredCapabilites.SetCapability("name", TestContext.CurrentContext.Test.Name); // give the test a name

            // start a new remote web driver session on sauce labs
            var driver = new RemoteWebDriver(commandExecutorUri, desiredCapabilites);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));

            return driver;
        }

        public override void CleanUp(IWebDriver driver)
        {
            // get the status of the current test
            bool passed = TestContext.CurrentContext.Outcome.Status == TestStatus.Passed;
            try
            {
                // log the result to sauce labs
                ((IJavaScriptExecutor)driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            }
            finally
            {
                // terminate the remote webdriver session
                driver.Quit();
            }
        }
    }
}
