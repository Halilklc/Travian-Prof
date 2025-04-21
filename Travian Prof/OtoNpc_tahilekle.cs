using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using Telegram.Bot.Types;

namespace Travian_Prof
{
    internal class OtoNpc_tahilekle
    {
        private readonly WebDriverWait wait;
        private readonly IWebDriver driver; // WebDriver burada saklanıyor
        private readonly MouseSimulator mouseSimulator;

        public OtoNpc_tahilekle(IWebDriver driver, string link)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver), "Driver referansı null.");
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // WebDriverWait başlatıyoruz
            this.mouseSimulator = new MouseSimulator(driver); // MouseSimulator nesnesini oluşturduk
        }

        public void Execute(string link)
        {
            Random random = new Random();
            try

            {
                var sayfalar = new Sayfalar(driver);
                sayfalar.NPCSayfasi();



                var takasLink = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath("//*[contains(@class, 'textButtonV1') and contains(@class, 'gold')]")));
                mouseSimulator.SimulateMouseMovementAndClick(takasLink);
                Thread.Sleep(1000);



                // Yeni XPath'lere tıklayıp içeriğini temizleme
                string[] inputXpaths = new string[] {
                    "//*[@id='npc']/tbody/tr[1]/td[1]/input",
                    "//*[@id='npc']/tbody/tr[1]/td[2]/input",
                    "//*[@id='npc']/tbody/tr[1]/td[3]/input"
                };

                foreach (string xpath in inputXpaths)
                {
                    var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                    element.Clear(); // İçeriği temizle
                    element.SendKeys("0"); // 0 yazdır
                }

                string xpath2 = "//*[@id='npc']/tbody/tr[1]/td[4]/input";
                var element2 = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath2)));
                element2.Clear();  // İçeriği temizle
                Thread.Sleep(new Random().Next(1500, 3000)); // 1.5 - 3 saniye rastgele bekleme


                var hammaddeLink = wait.Until(ExpectedConditions.ElementIsVisible(
                 By.XPath("//*[contains(@onclick, 'exchangeResources.distribute')]")));
                mouseSimulator.SimulateMouseMovementAndClick(hammaddeLink);
                Thread.Sleep(2000);


               var altinHarcamButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='npc_market_button']")));
               mouseSimulator.SimulateMouseMovementAndClick(altinHarcamButton);
                Thread.Sleep(new Random().Next(2000, 5000));

            }
            catch (Exception ex)
            {
                // Hata durumunda loglama yapalım
                Console.WriteLine($"Hata oluştu: {ex.Message}");
            }
            finally
            {
                var sayfalar = new Sayfalar(driver);
                sayfalar.AnasayfaAc();
                Thread.Sleep(new Random().Next(1500, 3000)); // 1.5 - 3 saniye rastgele bekleme

            }
        }

    }    
}
