using System;
using System.Net.Http;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;  // ListBox kullanabilmek için gerekli namespace

namespace Travian_Prof
{
    internal class SaldiriVar
    {
        // ListBox BilgiEkle yerine Action kullanacağız.
        public async Task Baslat(IWebDriver driver, long chatId, ListBox bilgilbx)
        {



            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                // Sayfada class'ları "listEntry village attack" olan tüm öğeleri kontrol et
                var attackVillageEntries = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .VisibilityOfAllElementsLocatedBy(By.CssSelector(".listEntry.village.attack")));

                if (attackVillageEntries.Count == 0)
                {
                    bilgilbx.Items.Add("Saldırı Yok");
                    return;
                }

                // Her saldırı öğesi için işlem yap
                foreach (var attackVillageEntry in attackVillageEntries)
                {
                    try
                    {
                        var villageNameElement = attackVillageEntry.FindElement(By.CssSelector("span.name"));
                        string villageName = villageNameElement.Text;

                        for (int i = 0; i < 2; i++)
                        {
                            await SendTelegramMessage("⚔️" + villageName + "⚔ Saldırı altında !!!", chatId, bilgilbx);
                            await Task.Delay(4000); // 3.5 saniye bekle
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        bilgilbx.Items.Add("Bir köy ismi bulunamadı.");
                    }
                }
            }
            catch (WebDriverTimeoutException)
            {
                bilgilbx.Items.Add("Saldırı Yok");
                return;
            }
            catch (Exception ex)
            {
                bilgilbx.Items.Add("Hata oluştu: " + ex.Message);
            }
        }


        // Telegram mesajını gönderecek metot
        private async Task SendTelegramMessage(string message, long chatId, ListBox bilgilbx)
        {
            string token = "8119973550:AAGZ8W2CoCqO8MFI6fNF0jh7CJbVrXrWxpQ";
            string url = $"https://api.telegram.org/bot{token}/sendMessage?chat_id={chatId}&text={Uri.EscapeDataString(message)}";

            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync(url);

                // BilgiEkle metodunu çağırıyoruz, burada ListBox'a mesaj ekliyoruz
                bilgilbx.Items.Add($"Saldırı Tespit edildi\nMesaj gönderildi. Durum: {result.StatusCode}");
            }
        }
    }
}
