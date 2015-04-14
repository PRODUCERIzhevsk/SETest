using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Drawing;


namespace SETest.Tests
{
    public abstract class InvoiceTest
    {
        #region Setup and Teardown

        /// <summary>starts a sauce labs sessions</summary>
        /// <param name="browser">name of the browser to request</param>
        /// <param name="version">version of the browser to request</param>
        /// <param name="platform">operating system to request</param>
        public abstract IWebDriver Setup(string browser, string version, string platform);

        /// <summary>called at the end of each test to tear it down</summary>
        public abstract void CleanUp(IWebDriver driver);

        #endregion

        #region Constants

        private static class ConstURI
        {
            public const string URI = "http://setest.plexusdev.ru/";
        }

        private static class ConstForms
        {
            public const string ID_BTN_USERS_FORM = "btnPhotos";

            public static readonly int ID_USER_SENDER = 1;
            public static readonly int ID_USER_RECIPIENT = 3;
            public static readonly string NAME_USER_SENDER = "user" + ID_USER_SENDER.ToString();
            public static readonly string NAME_USER_RECIPIENT = "user" + ID_USER_RECIPIENT.ToString();

            public const string ID_USER_FORM = "thumbnail-{0}";
            //public const string ID_USER_FORM = "";

            public static readonly string ID_USER_FORM_SENDER = string.Format(ID_USER_FORM, ID_USER_SENDER);
            public static readonly string ID_USER_FORM_RECIPIENT = string.Format(ID_USER_FORM, ID_USER_RECIPIENT);

            public const string SELECTOR_USER_PHOTO = "#{0} img";
            //public const string SELECTOR_USER_PHOTO = "";
            public static readonly string SELECTOR_USER_PHOTO_SENDER = string.Format(SELECTOR_USER_PHOTO, ID_USER_FORM_SENDER);
            public static readonly string SELECTOR_USER_PHOTO_RECIPIENT = string.Format(SELECTOR_USER_PHOTO, ID_USER_FORM_RECIPIENT);

            public const string ID_BTN_CREATE_INVOICE_FORM = "btnCreateInvoice";

            public const string INVOICE_FORM_ID_SENDER = "input-sender";
            public const string INVOICE_FORM_ID_RECIPIENT = "input-recipient";
            public const string INVOICE_FORM_ID_COUNT = "input-count";
            public const string INVOICE_FORM_ID_PRICE = "input-price";
            public const string INVOICE_FORM_ID_ITEM_NAME = "input-item-name";
            public const string INVOICE_FORM_ID_BTN_ADD_INVOICE = "btnAddInvoice";

            public static readonly decimal INVOICE_FORM_COUNT = new decimal(5.20);
            public static readonly decimal INVOICE_FORM_PRICE = new decimal(10.50);
            public const string INVOICE_FORM_ITEM_NAME = "item test";

            public const string ID_BTN_ITEMS = "btnItems";
            public const string SELECTOR_ITEMS_TABLE_BODY = "#Items table tbody";
        }

        private static class ConstMsg
        {
            public const string MSG_BTN_USERS_FORM_NOT_FOUND = "button users form not found. id = {0}";

            public const string MSG_PHOTO_SENDER_NOT_FOUND = "photo sender not found. css selector = {0}";
            public const string MSG_PHOTO_RECIPIENT_NOT_FOUND = "photo recipient not found. css selector = {0}";

            public const string MSG_BTN_CREATE_INVOICE_NOT_FOUND = "button create invoice form not found. id = {0}";

            public const string SUMM_ARE_NOT_EQUAL = "Expected Summ is not equal to actual. actual summ = {0}, expexted summ = {1}";
        }

        #endregion


        #region Tests
        [Test, Parallelizable]
        public void TestCreateInvoice(string browser, string version, string platform)
        {
            var driver = Setup(browser, version, platform);

            driver.Manage().Window.Position = new Point(0, 0);
            driver.Manage().Window.Size = new Size(1280, 768);
            driver.Navigate().GoToUrl(ConstURI.URI);
            Thread.Sleep(1000);

            var btnUserForm = driver.FindElement(By.Id(ConstForms.ID_BTN_USERS_FORM));
            Assert.IsNotNull(btnUserForm, ConstMsg.MSG_BTN_USERS_FORM_NOT_FOUND, new[] { ConstForms.ID_BTN_USERS_FORM });
            btnUserForm.Click();
            Thread.Sleep(1000);

            var photoSender = driver.FindElement(By.CssSelector(ConstForms.SELECTOR_USER_PHOTO_SENDER));
            Assert.IsNotNull(photoSender, ConstMsg.MSG_PHOTO_SENDER_NOT_FOUND, new[] { ConstForms.SELECTOR_USER_PHOTO_SENDER });

            var photoRecipient = driver.FindElement(By.CssSelector(ConstForms.SELECTOR_USER_PHOTO_RECIPIENT));
            Assert.IsNotNull(photoRecipient, ConstMsg.MSG_PHOTO_RECIPIENT_NOT_FOUND, new[] { ConstForms.SELECTOR_USER_PHOTO_RECIPIENT });

            photoSender.Click();
            Thread.Sleep(1000);
            photoRecipient.Click();
            Thread.Sleep(1000);

            var btnCreateInvoiceForm = driver.FindElement(By.Id(ConstForms.ID_BTN_CREATE_INVOICE_FORM));
            Assert.IsNotNull(btnCreateInvoiceForm, ConstMsg.MSG_BTN_CREATE_INVOICE_NOT_FOUND, new[] { ConstForms.ID_BTN_CREATE_INVOICE_FORM });
            btnCreateInvoiceForm.Click();
            Thread.Sleep(1000);

            var invoiceFormSender = driver.FindElement(By.Id(ConstForms.INVOICE_FORM_ID_SENDER));
            Assert.AreEqual(ConstForms.NAME_USER_SENDER, invoiceFormSender.GetAttribute("value"));

            var invoiceFormRecipient = driver.FindElement(By.Id(ConstForms.INVOICE_FORM_ID_RECIPIENT));
            Assert.AreEqual(ConstForms.NAME_USER_RECIPIENT, invoiceFormRecipient.GetAttribute("value"));

            var invoiceFormCount = driver.FindElement(By.Id(ConstForms.INVOICE_FORM_ID_COUNT));
            invoiceFormCount.SendKeys(ConstForms.INVOICE_FORM_COUNT.ToString());

            var invoiceFormPrice = driver.FindElement(By.Id(ConstForms.INVOICE_FORM_ID_PRICE));
            invoiceFormPrice.SendKeys(ConstForms.INVOICE_FORM_PRICE.ToString());

            var invoiceFormItemName = driver.FindElement(By.Id(ConstForms.INVOICE_FORM_ID_ITEM_NAME));
            invoiceFormItemName.SendKeys(ConstForms.INVOICE_FORM_ITEM_NAME);

            var invoiceFormBtnAddInvoice = driver.FindElement(By.Id(ConstForms.INVOICE_FORM_ID_BTN_ADD_INVOICE));
            invoiceFormBtnAddInvoice.Click();
            Thread.Sleep(1000);


            //Items
            var btnItemsForm = driver.FindElement(By.Id(ConstForms.ID_BTN_ITEMS));
            btnItemsForm.Click();
            Thread.Sleep(1000);

            var itemsTableBody = driver.FindElement(By.CssSelector(ConstForms.SELECTOR_ITEMS_TABLE_BODY));
            var itemRows = itemsTableBody.FindElements(By.CssSelector("tr"));
            var lastItemRow = itemRows.Last().FindElements(By.CssSelector("td"));
            var xmlURIElement = lastItemRow.Last().FindElement(By.CssSelector("a"));
            var xmlURI = xmlURIElement.GetAttribute("href");

            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.DownloadFile(xmlURI, "data.xml");
            SETest.Data.Invoice invoice = SETest.Data.Invoice.LoadFromXML("data.xml");
            try
            {
                System.IO.File.Delete("data.xml");
            }
            catch
            {

            }

            Assert.AreEqual(ConstForms.INVOICE_FORM_COUNT, invoice.DCount);
            Assert.AreEqual(ConstForms.INVOICE_FORM_PRICE, invoice.DPrice);

            Assert.AreEqual(
                decimal.Multiply(ConstForms.INVOICE_FORM_COUNT, ConstForms.INVOICE_FORM_PRICE),
                invoice.Summ,
                string.Format(ConstMsg.SUMM_ARE_NOT_EQUAL, invoice.Summ, decimal.Multiply(ConstForms.INVOICE_FORM_COUNT, ConstForms.INVOICE_FORM_PRICE)));

            CleanUp(driver);
        }

        #endregion
    }
}
