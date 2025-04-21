using System;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Travian_Prof
{
    internal class otomacera
    {
        private ListBox bilgilertxt;

        public otomacera(ListBox bilgilertxt = null)
        {
            this.bilgilertxt = bilgilertxt;
        }

        public void Baslat(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

            try
            {
                // Macera bağlantısını bul
                var adventuresLink = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    By.XPath("//a[contains(@href, '/hero/adventures')]")));
                string adventuresLinkText = adventuresLink.Text;

                if (int.TryParse(adventuresLinkText, out int numericValue) && numericValue >= 1)
                {
                    BilgiEkle("Macera mevcut, sayfaya gidiliyor...");
                    MouseSimulator mouse = new MouseSimulator(driver);
                    mouse.SimulateMouseMovementAndClick(adventuresLink);
                    Thread.Sleep(2000);

                    try
                    {
                        var button = wait2.Until(driver =>
                            driver.FindElement(By.XPath("//*[@id='heroAdventure']/table/tbody/tr/td[5]/button")));

                        if (button.Displayed && button.Enabled)
                        {
                            mouse.SimulateMouseMovementAndClick(button);
                            BilgiEkle("Macera başlatıldı.");
                        }
                    }
                    catch (Exception ex)
                    {
                        BilgiEkle("Macera başlatılamadı: " + ex.Message);
                    }

                    Thread.Sleep(3000);
                }
                else
                {
                    BilgiEkle("Başlatılacak macera yok.");
                }

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            }
            catch (Exception ex)
            {
                BilgiEkle("Macera kontrolünde hata: " + ex.Message);
            }
        }

        private void BilgiEkle(string mesaj)
        {
            if (bilgilertxt != null)
            {
                bilgilertxt.Items.Add(mesaj);
                bilgilertxt.SelectedIndex = bilgilertxt.Items.Count - 1;
            }
        }
    }
}
