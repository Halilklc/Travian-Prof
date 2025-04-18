using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Travian_Prof
{
    public class AnaGiris
    {
        private Random random = new Random(); // Rastgele süreler eklemek için
        private WebDriverWait wait;          // Dinamik bekleme
        private IWebDriver driver;          // WebDriver nesnesi

        public IWebDriver GirisYap(
            string urladresi,
            string username,
            string passwrd,
            ListBox bilgilertxt,
            ComboBox npckoycbx,
            ComboBox buildkoycbx,
            ComboBox askerkoycmbx,
            IWebDriver driver
        )
        {
            try
            {
                BilgiEkle("Driver kontrol ediliyor...", bilgilertxt);

                // Driver nesnesi sıfırsa, mevcut yeni tarayıcı başlatma mantığına dokunmadan devam ediliyor
                if (driver == null)
                {
                    BilgiEkle("Driver başlatılıyor...", bilgilertxt);

                    // Tarayıcı mantığı korunuyor (mevcut koddan alınmış)
                    driver = new ChromeDriver(); // WebDriver başlatılıyor
                }

                BilgiEkle("Travian sayfası açılıyor...", bilgilertxt);
                driver.Navigate().GoToUrl(urladresi);
                Thread.Sleep(random.Next(1000, 3000)); // Rastgele bekleme süresi

                BilgiEkle("Giriş alanları bekleniyor...", bilgilertxt);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                var usernameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("name")));
                var passwordField = driver.FindElement(By.Name("password"));

                BilgiEkle("Kullanıcı adı ve şifre giriliyor...", bilgilertxt);
                usernameField.SendKeys(username);
                passwordField.SendKeys(passwrd);

                Thread.Sleep(random.Next(2000, 5000));

                BilgiEkle("Giriş butonuna tıklanıyor...", bilgilertxt);
                var loginButton = driver.FindElement(By.XPath("//*[@id='dialogContent']/div/div[2]/button/div"));
                loginButton.Click();

                BilgiEkle("Başarıyla giriş yapıldı!", bilgilertxt);
                Thread.Sleep(random.Next(5000, 10000));

                // Köyler tespit edilip ComboBox'lara ekleniyor
                Dictionary<string, string> villages = TespitEtKoyler(driver, bilgilertxt);
                GuncelleComboBoxlar(npckoycbx, buildkoycbx, askerkoycmbx, villages, bilgilertxt);

                return driver;
            }
            catch (Exception ex)
            {
                BilgiEkle($"Hata oluştu: {ex.Message}", bilgilertxt);
                return driver;
            }
        }

        private Dictionary<string, string> TespitEtKoyler(IWebDriver driver, ListBox bilgilertxt)
        {
            Dictionary<string, string> villages = new Dictionary<string, string>();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            int i = 2; // XPath başlangıç index
            int villageIndex = 1; // Köy1, Köy2...

            // Masaüstü -> Travian klasörü -> villages.txt dosya yolu
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folderPath = Path.Combine(desktopPath, "Travian");
            string filePath = Path.Combine(folderPath, "villages.txt");

            // Klasör yoksa oluştur
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // StreamWriter ile dosyayı sıfırdan oluştur (overwrite mod)
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                try
                {
                    var villageElements = wait.Until(
                        SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(
                            By.XPath("//*[@id='sidebarBoxVillageList']/div[2]/div[2]/div/div/a/div/span[2]")
                        ));

                    foreach (var element in villageElements)
                    {
                        try
                        {
                            string xpath = $"//*[@id='sidebarBoxVillageList']/div[2]/div[2]/div[{i}]/div/a/div/span[2]";
                            var villageElement = driver.FindElement(By.XPath(xpath));
                            string villageName = villageElement.Text;

                            // Dictionary'ye ekle
                            villages.Add(villageName, xpath);

                            // TXT dosyasına ekle
                            writer.WriteLine($"Köy{villageIndex}* {villageName}");
                            writer.WriteLine($"Köy{villageIndex} Xpath Yolu:* {xpath}");

                            // Bilgi ListBox'a yaz
                            BilgiEkle($"Köy Ekleniyor: Köy{villageIndex} - {villageName}, XPath: {xpath}", bilgilertxt);

                            i++;
                            villageIndex++;
                        }
                        catch (NoSuchElementException)
                        {
                            break;
                        }
                        catch (Exception ex)
                        {
                            BilgiEkle($"Köy tespit edilirken hata: {ex.Message}", bilgilertxt);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    BilgiEkle($"Köyler bulunurken genel bir hata oluştu: {ex.Message}", bilgilertxt);
                }
            }

            return villages;
        }



        private void GuncelleComboBoxlar(
             ComboBox npckoycbx,
             ComboBox buildkoycbx,
             ComboBox askerkoycmbx,
             Dictionary<string, string> villages,
             ListBox bilgilertxt)
        {
            // ComboBox'ları temizle
            npckoycbx.Items.Clear();
            buildkoycbx.Items.Clear();
            askerkoycmbx.Items.Clear();

            foreach (var village in villages)
            {
                try
                {
                    npckoycbx.Items.Add(new ComboBoxItem(village.Key, village.Value));
                    buildkoycbx.Items.Add(new ComboBoxItem(village.Key, village.Value));
                    askerkoycmbx.Items.Add(new ComboBoxItem(village.Key, village.Value));


                    BilgiEkle($"Köy Ekleniyor: {village.Key}, XPath: {village.Value}", bilgilertxt);
     
                }
                catch (Exception ex)
                {
                    BilgiEkle($"ComboBox eklenirken hata oluştu: {ex.Message}", bilgilertxt);
                }
            }
        }

        public void BilgiEkle(string mesaj, ListBox bilgilertxt)
        {
            if (bilgilertxt.Items.Count > 50)
            {
                bilgilertxt.Items.RemoveAt(0); // Eski mesajları kaldır
            }
            bilgilertxt.Items.Add(mesaj);
            bilgilertxt.SelectedIndex = bilgilertxt.Items.Count - 1; // Son eklenen mesajı seç
        }
    }
}
