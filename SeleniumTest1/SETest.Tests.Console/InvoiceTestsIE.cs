using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using MbUnit.Framework;


namespace SETest.Tests.Console
{
    public class InvoiceTestsIE : SETest.Tests.InvoiceTest
    {
        public override OpenQA.Selenium.IWebDriver Setup(string browser, string version, string platform)
        {
            return new InternetExplorerDriver();
        }

        public override void CleanUp(OpenQA.Selenium.IWebDriver driver)
        {
            driver.Close();
        }

        public void StartTests()
        {
            this.TestCreateInvoice(null, null, null);
        }
    }
}
