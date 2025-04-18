using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Travian_Prof
{
    internal class Askerolmesin
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly ListBox _bilgilertxt;
        private readonly Form1 _form;
        private readonly string _urladres;

        public Askerolmesin(IWebDriver driver, ListBox bilgilertxt, Form1 form, string urladres)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            _bilgilertxt = bilgilertxt ?? throw new ArgumentNullException(nameof(bilgilertxt));
            _form = form ?? throw new ArgumentNullException(nameof(form));
            _urladres = urladres ?? throw new ArgumentNullException(nameof(urladres));
        }

        public void TumKoylereTikla()
        {
            try
            {
                BilgiEkle("Navigasyon başlıyor.");
                _driver.Navigate().GoToUrl(_urladres + "/dorf1.php");
                BilgiEkle("Navigasyon tamamlandı.");

                var villageElements = _wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(
                        By.XPath("//*[@id='sidebarBoxVillageList']/div[2]/div[2]/div/div/a/div/span[2]")
                    )
                );

                BilgiEkle($"Toplam köy sayısı: {villageElements.Count}");

                List<string> villageXpaths = new List<string>();
                for (int i = 1; i <= villageElements.Count+1; i++)
                {
                    string xpath = $"//*[@id='sidebarBoxVillageList']/div[2]/div[2]/div[{i}]/div/a/div/span[2]";
                    villageXpaths.Add(xpath);
                    BilgiEkle($"Köy {i} XPath'i: {xpath}");
                }

                int tiklanan = 1;
                foreach (var villageXPath in villageXpaths)
                {
                    try
                    {
                        IWebElement villageButton = _wait.Until(
                            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(villageXPath))
                        );
                        villageButton.Click();
                        BilgiEkle($"Köy {tiklanan} tıklandı.");
                        Thread.Sleep(3000); // Veya daha akıllı bir bekleme uygulanabilir
                        KontrolEt();
                        tiklanan++;
                    }
                    catch (Exception ex)
                    {
                        BilgiEkle($"Köy tıklanırken hata oluştu: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                BilgiEkle($"Genel hata oluştu: {ex.Message}");
            }
            finally
            {
                BilgiEkle("Tüm köyler tıklama işlemi tamamlandı.");
            }
        }


        private void KontrolEt()
        {
            try
            {
                var stockBarNumericValueElement = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='stockBar']/div[2]/div/div")));
                string numericValueText = stockBarNumericValueElement.Text.Trim();

                string cleanedNumericValueText = Regex.Replace(numericValueText, @"[^\d-]", "");
                double ToplamDepo = 0;
                if (double.TryParse(cleanedNumericValueText, NumberStyles.Any, CultureInfo.InvariantCulture, out ToplamDepo))
                {
                    BilgiEkle($"Tahıl Ambarı Kapasite: {ToplamDepo}");
                }
                else
                {
                    BilgiEkle("Toplam depo değeri sayısal bir değere dönüştürülemedi.");
                }

                var updatedStockBarNumericValueElement1 = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='stockBar']/div[2]/a[1]")));
                string updatedNumericValueText1 = updatedStockBarNumericValueElement1.Text.Trim();
                string cleanedUpdatedNumericValueText1 = Regex.Replace(updatedNumericValueText1, @"[^\d-]", "");
                double mevcutDepo = 0;
                if (double.TryParse(cleanedUpdatedNumericValueText1, NumberStyles.Any, CultureInfo.InvariantCulture, out mevcutDepo))
                {
                    BilgiEkle($"Tahıl Ambarı: {mevcutDepo}");
                }
                else
                {
                    BilgiEkle("Güncellenmiş depo değeri sayısal bir değere dönüştürülemedi.");
                }

                Thread.Sleep(10000);

                var updatedStockBarNumericValueElement = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='stockBar']/div[2]/a[1]")));
                string updatedNumericValueText = updatedStockBarNumericValueElement.Text.Trim();
                string cleanedUpdatedNumericValueText = Regex.Replace(updatedNumericValueText, @"[^\d-]", "");
                double mevcutDepo2 = 0;
                if (double.TryParse(cleanedUpdatedNumericValueText, NumberStyles.Any, CultureInfo.InvariantCulture, out mevcutDepo2))
                {
                    if (mevcutDepo > mevcutDepo2)
                    {
                        BilgiEkle("Üretim Negatif..");
                    }
                }
                else
                {
                    BilgiEkle("Güncellenmiş depo değeri sayısal bir değere dönüştürülemedi.");
                }

                var productionElement = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='production']/tbody/tr[4]/td[3]")));
                string productionValue = productionElement.Text.Trim();

                string cleanedProductionValue = Regex.Replace(productionValue, @"[^\d-]", "");
                double saatlikUretim = 0;
                if (double.TryParse(cleanedProductionValue, NumberStyles.Any, CultureInfo.InvariantCulture, out saatlikUretim))
                {
                    BilgiEkle($"Saatlik Tahıl üretim: {saatlikUretim}");
                }
                else
                {
                    BilgiEkle("Saatlik üretim değeri sayısal bir değere dönüştürülemedi.");
                }



                if ((mevcutDepo > mevcutDepo2 && mevcutDepo <= saatlikUretim) || mevcutDepo2 == 0)
                {
                    // Koşullar sağlandığında yapılacak işlemler

                    BilgiEkle("Ölüm Tehlikesi!!! OTO NPC Aktif edildi.");
                    OtoNpc_tahilekle otoNpcSade = new OtoNpc_tahilekle(_driver, _urladres);
                    otoNpcSade.Execute(_urladres);
                    BilgiEkle($"NPC tamamlandı. Diğer köylere bakılıyor.");
                }
            }
            catch (Exception ex)
            {
                BilgiEkle($"Koşul kontrolü sırasında hata oluştu: {ex.Message}");
            }
        }

        private void BilgiEkle(string mesaj)
        {
            _bilgilertxt.Items.Add(mesaj);
            _bilgilertxt.SelectedIndex = _bilgilertxt.Items.Count - 1;
            Console.WriteLine(mesaj);

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folderPath = Path.Combine(desktopPath, "Travian");
            string filePath = Path.Combine(folderPath, "Log.txt");

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine($"{DateTime.Now}: {mesaj}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Log kaydedilirken hata oluştu: {ex.Message}");
            }
        }
    }
}
