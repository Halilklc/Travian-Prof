using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Travian_Prof
{
    internal class OtoYagma
    {
        private readonly WebDriverWait wait;
        private readonly IWebDriver driver;
        private readonly ListBox bilgilertxt;

        public OtoYagma(IWebDriver driver, ListBox bilgilertxt)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver), "Driver nesnesi null.");
            this.bilgilertxt = bilgilertxt ?? throw new ArgumentNullException(nameof(bilgilertxt), "ListBox nesnesi null.");
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        public void Execute(string url)
        {
            try
            {
                string yagmaUrl = $"{url}/build.php?id=39&gid=16&tt=99";

                if (string.IsNullOrEmpty(yagmaUrl) || !yagmaUrl.StartsWith("http"))
                    yagmaUrl = "http://" + yagmaUrl;

                System.Threading.Thread.Sleep(2000);
                driver.Navigate().GoToUrl(yagmaUrl);
                BilgiEkle($"Sayfa yönlendiriliyor: {yagmaUrl}");
                System.Threading.Thread.Sleep(1000);

                WebDriverWait pageLoadWait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                pageLoadWait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
                BilgiEkle($"Sayfa başarıyla yüklendi: {yagmaUrl}");
                System.Threading.Thread.Sleep(1000);

                var startButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='stickyWrapper']/button[2]/div")));

                SimulateMouseMovementAndClick(startButton);
                System.Threading.Thread.Sleep(3000);

                BilgiEkle("Yağma işlemi başlatıldı.");
            }
            catch (NoSuchElementException ex)
            {
                BilgiEkle($"XPath öğesi bulunamadı: {ex.Message}");
            }
            catch (WebDriverTimeoutException ex)
            {
                BilgiEkle($"Öğe yüklenirken zaman aşımı oluştu: {ex.Message}");
            }
            catch (Exception ex)
            {
                BilgiEkle($"Genel hata oluştu: {ex.Message}");
            }
            finally
            {
                BilgiEkle("Oto Yağma işlemi tamamlandı.");
            }
        }

        private void SimulateMouseMovementAndClick(IWebElement element)
        {
            try
            {
                Random rand = new Random();
                Actions actions = new Actions(driver);

                // Öğeyi görünür alana getir
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);
                System.Threading.Thread.Sleep(rand.Next(400, 800));

                // Öğeye hareket et
                actions.MoveToElement(element).Perform();
                System.Threading.Thread.Sleep(rand.Next(500, 1000));

                // Hafif oynama
                actions.MoveByOffset(rand.Next(-3, 3), rand.Next(-3, 3)).Perform();
                System.Threading.Thread.Sleep(rand.Next(200, 400));

                // Tıklama
                actions.Click().Perform();
                System.Threading.Thread.Sleep(rand.Next(800, 1500));

                BilgiEkle("Tıklama başarıyla gerçekleştirildi.");
            }
            catch (Exception ex)
            {
                BilgiEkle($"Tıklama sırasında hata: {ex.Message}");
            }
        }

        private void BilgiEkle(string mesaj)
        {
            if (bilgilertxt != null)
            {
                bilgilertxt.Items.Add(mesaj);
                bilgilertxt.SelectedIndex = bilgilertxt.Items.Count - 1;
            }
            else
            {
                Console.WriteLine($"Bilgi: {mesaj}");
            }
        }
    }
}
