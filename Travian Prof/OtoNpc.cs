using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Travian_Prof
{
    internal class OtoNpc
    {
        private WebDriverWait wait;
        private IWebDriver driver;
        private ListBox bilgilertxt;
        private string npckoyst;

        public OtoNpc(IWebDriver driver, ListBox bilgilertxt, string npckoyst)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver), "Driver referansı null.");
            this.bilgilertxt = bilgilertxt ?? throw new ArgumentNullException(nameof(bilgilertxt), "ListBox referansı null.");
            this.npckoyst = npckoyst ?? throw new ArgumentNullException(nameof(npckoyst), "ComboBox referansı null.");
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void Execute(string url)
        {
            try
            {
                string selectedXPath = npckoyst.ToString();
                BilgiEkle($"Seçilen XPath: {selectedXPath}");

                if (string.IsNullOrEmpty(selectedXPath))
                {
                    BilgiEkle("Lütfen geçerli bir XPath seçin.");
                    return;
                }
                var sayfalar = new Sayfalar(driver);
                sayfalar.AnasayfaAc();
                Random random = new Random();
                Thread.Sleep(random.Next(3000, 7000));


                try
                {
                    var selectedElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(selectedXPath)));
                    new MouseSimulator(driver).SimulateMouseMovementAndClick(selectedElement);
               
                }
                catch (Exception ex)
                {
                    return;
                }

                var stockBarLink = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='stockBar']/div[2]/a[1]")));
                double mevcutDepo = ParseToDouble(stockBarLink.Text, "Mevcut depo");

                var stockBarNumericValueElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='stockBar']/div[2]/div/div")));
                double toplamDepo = ParseToDouble(stockBarNumericValueElement.Text, "Toplam depo");

                var productionElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='production']/tbody/tr[4]/td[3]")));
                double saatlikUretim = ParseToDouble(productionElement.Text, "Saatlik üretim");

                double npcdepo = saatlikUretim / 2 + mevcutDepo;


                if (npcdepo >= toplamDepo)
                {
                    BilgiEkle("Yarım Saatten az süre sonra Tahıl taşacak!");
                    HandleNpcTrade(url);
                }
            }
            finally
            {
                BilgiEkle("Oto NPC tamamlandı.");
            }
        }

        private void HandleNpcTrade(string url)
        {
            MouseSimulator simulator = new MouseSimulator(driver);

            var sayfalar = new Sayfalar(driver);
            sayfalar.NPCSayfasi();
            Random random = new Random();
            Thread.Sleep(random.Next(3000, 7000));


            var takasLink = wait.Until(ExpectedConditions.ElementIsVisible(
        By.XPath("//*[contains(@class, 'textButtonV1') and contains(@class, 'gold')]")));
            simulator.SimulateMouseMovementAndClick(takasLink);
            Thread.Sleep(1000);


            string[] inputXpaths = {
                "//*[@id='npc']/tbody/tr[1]/td[1]/input",
                "//*[@id='npc']/tbody/tr[1]/td[2]/input",
                "//*[@id='npc']/tbody/tr[1]/td[3]/input"
            };

            foreach (string xpath in inputXpaths)
            {
                var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                element.Clear();
                Thread.Sleep(2000);
            }

            var hammaddeLink = wait.Until(ExpectedConditions.ElementIsVisible(
        By.XPath("//*[contains(@onclick, 'exchangeResources.distribute')]")));
            simulator.SimulateMouseMovementAndClick(hammaddeLink);
            Thread.Sleep(2000);


            var altinHarcamButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='npc_market_button']")));
            simulator.SimulateMouseMovementAndClick(altinHarcamButton);
            Thread.Sleep(new Random().Next(2000, 5000));
        }

        private double ParseToDouble(string value, string fieldName)
        {
            string cleanedValue = Regex.Replace(value, @"[^\d]", "");
            if (double.TryParse(cleanedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double numericValue))
            {
                BilgiEkle($"{fieldName}: {numericValue}");
                return numericValue;
            }
            else
            {
                BilgiEkle($"{fieldName} değeri sayısal bir değere dönüştürülemedi.");
                return 0;
            }
        }

        private void BilgiEkle(string mesaj)
        {
            bilgilertxt.Items.Add(mesaj);
            bilgilertxt.SelectedIndex = bilgilertxt.Items.Count - 1;
            Console.WriteLine(mesaj);
        }
    }

    internal class MouseSimulator
    {
        private readonly IWebDriver driver;

        public MouseSimulator(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver), "Driver null olamaz.");
        }

        public void SimulateMouseMovementAndClick(IWebElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element), "Element null olamaz.");

            try
            {
                var actions = new OpenQA.Selenium.Interactions.Actions(driver);
                actions.MoveToElement(element).Click().Build().Perform();
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fare simülasyonu sırasında hata: {ex.Message}");
            }
        }

        public void SimulateMouseMovementAndClick(int x, int y)
        {
            try
            {
                var actions = new OpenQA.Selenium.Interactions.Actions(driver);
                actions.MoveByOffset(x, y).Click().Build().Perform();
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Koordinata tıklarken hata: {ex.Message}");
            }
        }

        public void SimulateRightClick(IWebElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element), "Element null olamaz.");

            try
            {
                var actions = new OpenQA.Selenium.Interactions.Actions(driver);
                actions.ContextClick(element).Build().Perform();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sağ tıklama sırasında hata: {ex.Message}");
            }
        }

        public void MoveMouseToPosition(int x, int y)
        {
            try
            {
                var actions = new OpenQA.Selenium.Interactions.Actions(driver);
                actions.MoveByOffset(x, y).Build().Perform();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fare taşınamadı: {ex.Message}");
            }
        }
    }
}
