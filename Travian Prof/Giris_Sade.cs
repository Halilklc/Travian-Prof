using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using System;
using System.Windows.Forms;

namespace Travian_Prof
{
    internal class Giris_Sade
    {
        private WebDriverWait wait;
        private IWebDriver driver;

        public Giris_Sade()
        {
            // Constructor: Driver ve bekleme süreleri başlatılıyor.
        }

        public IWebDriver GirisYapSade(string url, string username, string password, ListBox bilgilertxt, IWebDriver existingDriver)
        {
            try
            {
                // Mevcut driver kullanılıyor veya yenisi başlatılıyor
                driver = existingDriver ?? BaslatChromeDriver();

                BilgiEkle("URL'ye gidiliyor...", bilgilertxt);
                driver.Navigate().GoToUrl(url);
                Thread.Sleep(new Random().Next(1500, 3000)); // 1.5 - 3 saniye rastgele bekleme

                BilgiEkle("Giriş alanları bekleniyor...", bilgilertxt);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                var usernameField = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name("name")));
                var passwordField = driver.FindElement(By.Name("password"));
                Thread.Sleep(new Random().Next(1500, 2500)); // 1.5 - 2.5 saniye rastgele bekleme

                BilgiEkle("Kullanıcı adı ve şifre giriliyor...", bilgilertxt);
                usernameField.SendKeys(username);
                Thread.Sleep(new Random().Next(1000, 3500)); // 1 - 2 saniye rastgele bekleme

                passwordField.SendKeys(password);
                Thread.Sleep(new Random().Next(1500, 3000)); // 1 - 2 saniye rastgele bekleme

                BilgiEkle("Giriş butonuna tıklanıyor...", bilgilertxt);
                var loginButton = driver.FindElement(By.XPath("//*[@id='dialogContent']/div/div[2]/button/div"));
                Thread.Sleep(new Random().Next(500, 1500)); // 0.5 - 1.5 saniye rastgele bekleme

                // MouseSimulator kullanımı
                MouseSimulator mouseSim = new MouseSimulator(driver);
                mouseSim.SimulateMouseMovementAndClick(loginButton);

                Thread.Sleep(new Random().Next(1000, 4000)); // 1.5 - 3 saniye rastgele bekleme

                return driver;
            }
            catch (Exception ex)
            {
                BilgiEkle($"Giriş sırasında hata oluştu: {ex.Message}", bilgilertxt);
                driver?.Quit();
                return null;
            }
        }

        private IWebDriver BaslatChromeDriver()
        {
            BilgiEkle("ChromeDriver başlatılıyor...", null);

            // WebDriverManager kullanılarak ChromeDriver sürümü otomatik ayarlanıyor
            new DriverManager().SetUpDriver(new ChromeConfig());

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddExcludedArgument("enable-automation");
            options.AddArgument("--start-maximized");

            return new ChromeDriver(options); // Yeni driver başlatılıyor
        }


        private void BilgiEkle(string mesaj, ListBox bilgilertxt)
        {
            if (bilgilertxt != null)
            {
                bilgilertxt.Items.Add(mesaj);
                bilgilertxt.SelectedIndex = bilgilertxt.Items.Count - 1; // Yeni eklenen mesaja odaklanılıyor
            }
            else
            {
                Console.WriteLine($"Bilgi: {mesaj}");
            }
        }
    }
}
