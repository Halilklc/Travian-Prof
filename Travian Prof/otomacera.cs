using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Travian_Prof
{
    internal class otomacera
    {
        public void Baslat(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

            // Macera bağlantısını bul
            var adventuresLink = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                By.XPath("//a[contains(@href, '/hero/adventures')]")));
            string adventuresLinkText = adventuresLink.Text;

            if (int.TryParse(adventuresLinkText, out int numericValue) && numericValue >= 1)
            {
                // Linke tıkla
                adventuresLink.Click();
                Thread.Sleep(2000);

                try
                {
                    var button = wait2.Until(driver =>
                        driver.FindElement(By.XPath("//*[@id='heroAdventure']/table/tbody/tr/td[5]/button")));

                    if (button.Displayed && button.Enabled)
                    {
                        // MouseSimulator kullanarak tıkla
                        MouseSimulator simulator = new MouseSimulator(driver);
                        simulator.SimulateMouseMovementAndClick(button);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Butona tıklanamadı: " + ex.Message);
                }

                Thread.Sleep(3000);
            }

            // Kısa gecikme
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }
    }
}
