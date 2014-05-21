Test123
test123
Test123
Tewst123

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumExam
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathToExecutionReport = @"C:\Users\xd\Documents\Visual Studio 2012\Projects\SeleniumExam\SeleniumExam\Reports\ExecutionReport.pdf";
            PdfDocument executionReport = new PdfDocument();
            
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5)); // Set delay between operations
            driver.Manage().Window.Maximize(); // Maximize browser window

            // 1. Navigate to 'http://www.microfocus.com'
            driver.Navigate().GoToUrl("http://www.microfocus.com");
            TakeScreenshot(driver, @"C:\Users\xd\Documents\Visual Studio 2012\Projects\SeleniumExam\SeleniumExam\Images\Step1.png");
            PdfReport("TEST: Verify that link to customer is displayed on the \"Customer Success Stories\" page",
                "Step 1: Navigate to 'http://www.microfocus.com'",
                "",
                @"C:\Users\xd\Documents\Visual Studio 2012\Projects\SeleniumExam\SeleniumExam\Images\Step1.png",
                executionReport);

            // 2. Click on the Customers link
            driver.FindElement(By.XPath("/html/body/form/div[2]/div/div/div/ul/li[4]/a")).Click();
            TakeScreenshot(driver, @"C:\Users\xd\Documents\Visual Studio 2012\Projects\SeleniumExam\SeleniumExam\Images\Step2.png");
            PdfReport("TEST: Verify that link to customer is displayed on the \"Customer Success Stories\" page",
                "Step 2: Click on the Customers link",
                "",
                @"C:\Users\xd\Documents\Visual Studio 2012\Projects\SeleniumExam\SeleniumExam\Images\Step2.png",
                executionReport);

            // 3. On the "Select Customer Successes" section, select a category: "Country" option
            var categories = driver.FindElements(By.Name("ctl00$MainContentAreas$MainContent$ctlCaseStudyList$rblPrimary"));

            foreach (var category in categories)
            {
                if (category.GetAttribute("value") == "tcm:6-54140-512")
                {
                    category.Click();
                    TakeScreenshot(driver, @"C:\Users\xd\Documents\Visual Studio 2012\Projects\SeleniumExam\SeleniumExam\Images\Step3.png");
                    PdfReport("TEST: Verify that link to customer is displayed on the \"Customer Success Stories\" page",
                        "Step 3: On the \"Select Customer Successes\" section, select a category: \"Country\" option",
                        "",
                        @"C:\Users\xd\Documents\Visual Studio 2012\Projects\SeleniumExam\SeleniumExam\Images\Step3.png",
                        executionReport);
                    break;
                }
            }
            
            // 4. On the Country listbox section, select a country: "Brazil"
            var countryListBox = driver.FindElement(By.Id("ctl00_MainContentAreas_MainContent_ctlCaseStudyList_lbSecondary"));
            var countryListBoxSelector = new SelectElement(countryListBox);

            countryListBoxSelector.SelectByText("Brazil");
            TakeScreenshot(driver, @"C:\Users\xd\Documents\Visual Studio 2012\Projects\SeleniumExam\SeleniumExam\Images\Step4.png");
            PdfReport("TEST: Verify that link to customer is displayed on the \"Customer Success Stories\" page",
                "Step 4: On the Country listbox section, select a country: \"Brazil\"",
                "",
                @"C:\Users\xd\Documents\Visual Studio 2012\Projects\SeleniumExam\SeleniumExam\Images\Step4.png",
                executionReport);

            // 5. On the "customer success stories" verify if the "Romao Calcados" is displayed

            if(IsElementPresent(driver, By.LinkText("Romao Calcados")))
            {
                Console.WriteLine("The \"Romao Calcados\" link is displayed on the \"Customer Success Stories\". Status: PASSED");
                
                TakeScreenshot(driver, @"C:\Users\xd\Documents\Visual Studio 2012\Projects\SeleniumExam\SeleniumExam\Images\Step5.png");
                PdfReport("TEST: Verify that link to customer is displayed on the \"Customer Success Stories\" page",
                    "Step 5: The \"Romao Calcados\" link is displayed on the \"Customer Success Stories\". Status: PASSED",
                    "green",
                    @"C:\Users\xd\Documents\Visual Studio 2012\Projects\SeleniumExam\SeleniumExam\Images\Step5.png",
                executionReport);
            }
            else
            {
                Console.WriteLine("The \"Romao Calcados\" link is NOT displayed on the \"Customer Success Stories\". Status: FAILED");
                TakeScreenshot(driver, @"C:\Users\xd\Documents\Visual Studio 2012\Projects\SeleniumExam\SeleniumExam\Images\Step5.png");
                PdfReport("TEST: Verify that link to customer is displayed on the \"Customer Success Stories\" page",
                    "Step 5: The \"Romao Calcados\" link is NOT displayed on the \"Customer Success Stories\". Status: FAILED",
                    "red",
                    @"C:\Users\xd\Documents\Visual Studio 2012\Projects\SeleniumExam\SeleniumExam\Images\Step5.png",
                executionReport);
            }

            driver.Close(); // Close driver
            executionReport.Save(pathToExecutionReport);
            executionReport.Close();

        }

        private static bool IsElementPresent(IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private static void TakeScreenshot(IWebDriver driver, string img)
        {
            ITakesScreenshot ssDriver = driver as ITakesScreenshot;
            Screenshot screen = ssDriver.GetScreenshot();
            screen.SaveAsFile(img, ImageFormat.Png);
        }

        public static void Logs(string FilePath, string logMessage)
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    File.Create(FilePath).Close();
                }

                StreamWriter sw = File.AppendText(FilePath);
                sw.WriteLine("[" + DateTime.Now + "] " + logMessage + Environment.NewLine);
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void PdfReport(string title, string result, string color, string imagePath, PdfDocument doc)
        {
            // Create an empty page
            PdfPage page = doc.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont titleFont = new XFont("Verdana", 12, XFontStyle.BoldItalic);
            XFont bodyFont = new XFont("Verdana", 10, XFontStyle.BoldItalic);

            // Draw the text
            gfx.DrawString(title,
                titleFont,
                XBrushes.Black,
                new XRect(10, 50, 0, 0),
                XStringFormats.Default);

            gfx.DrawString(result, bodyFont, SetColor(color), 10, 80, XStringFormats.Default);

            XImage image = XImage.FromFile(imagePath);
            gfx.DrawImage(image, 10, 100, 500, 500);
        }

        private static XSolidBrush SetColor(string color)
        {
            switch (color.ToLower())
            {
                case "green":
                    return XBrushes.Green;
                case "red":
                    return XBrushes.Red;
                default:
                    return XBrushes.Black;
            }

        }

        private static void ManageCheckbox(IWebDriver driver)
        {
            var xPathDoc = new System.Xml.XPath.XPathDocument(new StringReader(driver.PageSource));
            var navigator = xPathDoc.CreateNavigator();
            var iterator = navigator.Select("//*[@id=\"check1\"]/following-sibling::text()[1]");

            while (iterator.MoveNext())
            {
                string val = iterator.Current.Value.Trim();
                Console.WriteLine(val);
            }
        }
    }
}
