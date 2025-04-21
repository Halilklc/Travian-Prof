using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Travian_Prof
{
    internal class Sayfalar
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly ListBox bilgilertxt;

        public Sayfalar(IWebDriver driver )
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }


        public void NPCSayfasi()
        {
            try
            {
                Thread.Sleep(2000);


                // 1. Adım: /dorf2.php linkine tıklama
                var dorf2Link = wait.Until(driver =>
                    driver.FindElements(By.XPath("//a[contains(@href, '/dorf2.php')]"))
                          .FirstOrDefault(el => el.GetAttribute("href").EndsWith("/dorf2.php")));
                if (dorf2Link != null)
                {
                    SimulateMouseMovementAndClick(dorf2Link);
                    Thread.Sleep(2000);
                }

                var pazaryeri = wait.Until(driver =>
                 driver.FindElements(By.XPath("//a[contains(@href, '/build.php?id=31&gid=17')]"))
                   .FirstOrDefault(el => el.GetAttribute("href").EndsWith("/build.php?id=31&gid=17")));
                if (pazaryeri != null)
                {
                    SimulateMouseMovementAndClick(pazaryeri);
                    Thread.Sleep(2000);
                }

                var pazaryeriNPC = wait.Until(driver =>
                driver.FindElements(By.XPath("//a[contains(@href, '/build.php?id=31&gid=17&t=0')]"))
               .FirstOrDefault(el => el.GetAttribute("href").EndsWith("/build.php?id=31&gid=17&t=0")));
                if (pazaryeri != null)
                {
                    SimulateMouseMovementAndClick(pazaryeriNPC);
                    Thread.Sleep(2000);
                }


            }

            catch (Exception ex)
            {
                BilgiEkle($" Sayfa Açılmadı!! {ex.Message}");
            }
        }
        public void AnasayfaAc()
        {
            try
            {
                Thread.Sleep(2000);

                // 1. Adım: /dorf2.php linkine tıklama
                var dorf2Link = wait.Until(driver =>
                    driver.FindElements(By.XPath("//a[contains(@href, '/dorf1.php')]"))
                          .FirstOrDefault(el => el.GetAttribute("href").EndsWith("/dorf1.php")));
                if (dorf2Link != null)
                {
                    SimulateMouseMovementAndClick(dorf2Link);
                    BilgiEkle("Anasayfaya linkine tıklandı.");
                    Thread.Sleep(2000);
                }
            }
         
             catch (Exception ex)
            {
                BilgiEkle($"Şehir Merkezi Açılmadı!! {ex.Message}");
            }
        }
        
        public void YagmaListesiAc()
        {
            try
            {
                Thread.Sleep(2000);

                // 1. Adım: /dorf2.php linkine tıklama
                var dorf2Link = wait.Until(driver =>
                    driver.FindElements(By.XPath("//a[contains(@href, '/dorf2.php')]"))
                          .FirstOrDefault(el => el.GetAttribute("href").EndsWith("/dorf2.php")));
                if (dorf2Link != null)
                {
                    SimulateMouseMovementAndClick(dorf2Link);
                    Thread.Sleep(2000);
                }

                // 2. Adım: build.php?id=39&gid=16 linkine tıklama
                var buildLink = wait.Until(driver =>
                    driver.FindElements(By.XPath("//a[contains(@href, 'build.php?id=39&gid=16')]"))
                          .FirstOrDefault(el => el.GetAttribute("href").EndsWith("build.php?id=39&gid=16")));
                if (buildLink != null)
                {
                    SimulateMouseMovementAndClick(buildLink);
                    Thread.Sleep(2000);
                }

                // 3. Adım: /build.php?id=39&gid=16&tt=99 linkine tıklama
                var favori99Link = wait.Until(driver =>
                    driver.FindElements(By.XPath("//a[contains(@href, 'build.php?id=39&gid=16&tt=99')]"))
                          .FirstOrDefault(el => el.GetAttribute("href").EndsWith("build.php?id=39&gid=16&tt=99")));
                if (favori99Link != null)
                {
                    SimulateMouseMovementAndClick(favori99Link);
                    Thread.Sleep(2000);
                }
            }
            catch (Exception ex)
            {
                BilgiEkle($"YagmaListesiAc sırasında hata: {ex.Message}");
            }
        }

        private void SimulateMouseMovementAndClick(IWebElement element)
        {
            try
            {
                Random rand = new Random();
                Actions actions = new Actions(driver);

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);
                Thread.Sleep(rand.Next(400, 800));

                actions.MoveToElement(element).Perform();
                Thread.Sleep(rand.Next(500, 1000));

                actions.MoveByOffset(rand.Next(-3, 3), rand.Next(-3, 3)).Perform();
                Thread.Sleep(rand.Next(200, 400));

                actions.Click().Perform();
                Thread.Sleep(rand.Next(800, 1500));
            }
            catch (Exception ex)
            {
                BilgiEkle($"Mouse tıklama simülasyonu sırasında hata: {ex.Message}");
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
