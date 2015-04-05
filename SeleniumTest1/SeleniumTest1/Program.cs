using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;

namespace SeleniumTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.plexusdev.ru/setest");
            Thread.Sleep(3000);

            driver.Manage().Window.Position = new Point(0, 0);
            driver.Manage().Window.Size = new Size(640, 360);
            var btn = driver.FindElement(By.Id("btnPhotos"));
            btn.Click();
            Thread.Sleep(3000);

            var screenShot1 = (driver as ITakesScreenshot).GetScreenshot();
            screenShot1.SaveAsFile("screen1.png", ImageFormat.Png);

            //Console.ReadKey();
            driver.Manage().Window.Size = new Size(1920, 1080);
            Thread.Sleep(3000);
            var screenShot2 = (driver as ITakesScreenshot).GetScreenshot();
            screenShot2.SaveAsFile("screen2.png", ImageFormat.Png);

            driver.Close();
        }
    }
}
