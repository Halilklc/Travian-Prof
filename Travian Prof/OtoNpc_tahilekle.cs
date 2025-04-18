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
                // Saatlik üretimi al
                var productionElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='production']/tbody/tr[4]/td[3]")));
                string productionValue = productionElement.Text.Trim(); // Öğenin metnini al

                // Saatlik üretimi sayısal değere çevir
                double saatlikUretim = ParseToDouble(productionValue);

                // 1. Adım: Belirtilen linke git
                driver.Navigate().GoToUrl(link + "/build.php?id=31&gid=17&t=0");
                Thread.Sleep(new Random().Next(1500, 3000)); // 1.5 - 3 saniye rastgele bekleme

                // 2. Adım: 'aria/Hammadde takası' metnini bul ve tıkla
                var takasLink = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@aria-label, 'Hammadde takası') or contains(text(), 'Hammadde takası')]")));
                mouseSimulator.SimulateMouseMovementAndClick(takasLink); // Simulate mouse movement and click
                Thread.Sleep(new Random().Next(1500, 3000)); // 1.5 - 3 saniye rastgele bekleme

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

                element2.SendKeys("0"); // 0 yazdır
                Thread.Sleep(new Random().Next(1500, 3000)); // 1.5 - 3 saniye rastgele bekleme

                // 'aria/Hammadde dağıt' XPath'ini bul ve tıkla
                var hammaddeLink = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@aria-label, 'Hammadde dağıt') or contains(text(), 'Hammadde dağıt')]")));
                mouseSimulator.SimulateMouseMovementAndClick(hammaddeLink); // Simulate mouse movement and click
                Thread.Sleep(new Random().Next(1500, 3000)); // 1.5 - 3 saniye rastgele bekleme


                // '//*[@id="npc_market_button"]' XPath'ini bul ve altın harcamayı başlat
                var altinHarcamButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='npc_market_button']")));
                mouseSimulator.SimulateMouseMovementAndClick(altinHarcamButton); // Simulate mouse movement and click
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama yapalım
                Console.WriteLine($"Hata oluştu: {ex.Message}");
            }
            finally
            {
                driver.Navigate().GoToUrl(link + "/dorf1.php");
            }
        }

        // Parse to double helper function
        private double ParseToDouble(string value)
        {
            string cleanedValue = Regex.Replace(value, @"[^\d.-]", ""); // Sayı ve negatif işaretini bırak
            double parsedValue = 0;

            if (double.TryParse(cleanedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out parsedValue))
            {
                return parsedValue;
            }
            else
            {
                Console.WriteLine($"'{value}' değeri sayısal bir değere dönüştürülemedi.");
                return 0;
            }
        }
    }

    
}
