using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V126.Overlay;
using OpenQA.Selenium.Interactions;


namespace AgData_Assesment_Part2
{
    public class Tests : IDisposable
    {
        private IWebDriver driver;
       
        [SetUp]
        public void Setup()
        {
            // Create a new instance of Selenium Web Driver
            driver = new ChromeDriver();
            // 1. Navigate to URL
            driver.Navigate().GoToUrl("https://www.agdata.com/");
            driver.Manage().Window.Maximize();
        }

        public void HoverAndClickSubMenu()
        {
            // 2. On the top navigation menu click on "Company" > "Overview"
            IWebElement menu = driver.FindElement(By.Id("menu-item-992"));

            // 2a. Locate submenu item to click
            IWebElement submenu = driver.FindElement(By.Id("menu-item-829"));

            // 2b. Initialize Actions class
            Actions action = new Actions(driver);

            // 2d. Hover over menu and then click on submenu
            action.MoveToElement(menu).Perform(); 
            action.MoveToElement(submenu).Click().Perform();            
        }


        public void GetCompanyPageValues()   
        {
            // 3. On the page, get back all the Values on the page in a LIST
            // List to hold the extracted values
            List<string> valuesList = new List<string>();

            // Find all <li> elements on the page
            var lielements = driver.FindElements(By.TagName("li"));

            // Loop through all <li> elements and get its text
            foreach (var li in lielements)
            {
               valuesList.Add(li.Text); // Add the text inside the <li> to the list
            }

            // Print the values
            foreach (var value in valuesList)
            {
                Console.WriteLine(value);
            }
        }
        
        public void ClickLetsGetStartedBtn() 
        {
            // 4. Click on the "Let's Get Started" button at the bottom
            // 4a. Scroll to the bottom of the page to ensure the button is visible
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            bool buttonExists = driver.FindElements(By.XPath("//a[contains(@href,'contact')]")).Count > 0;
            if (buttonExists)
            {
                IWebElement letsGetStartedBtn = driver.FindElement(By.XPath("//a[contains(@href,'contact')]"));
                letsGetStartedBtn.Click();
            }
            // 5. Validate that the 'Contact' page is displayed/loaded
            // Validate by Page Title
            Assert.IsTrue(driver.Title.Contains("Contact - AGDATA"), "The Contact page title was not found.");
        }

        [Test]
        public void AgDataUITest()
        {
            HoverAndClickSubMenu();
            GetCompanyPageValues();
            ClickLetsGetStartedBtn();
        }
        
        [TearDown]
        public void CloseBrowser()
        {
            // Close the browser
            if (driver != null)
            {
                driver.Quit();
            }            
        }
       

        public void Dispose()
        {
            // Dispose WebDriver
            if (driver != null)
            {
                driver.Dispose();
            }            
        }
    }
}