using System;
using System.Net.Http;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Windows.Forms;  // ListBox kullanabilmek için gerekli namespace

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
                // Sayfada class'ları "listEntry village attack" olan öğeyi kontrol et
                var attackVillageEntry = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    By.CssSelector(".listEntry.village.attack")));

                // Eğer bulunduysa mesaj gönder
                await SendTelegramMessage("⚔️ Saldırı altında olan bir köy listede görünüyor!", chatId, bilgilbx);
            }
            catch (WebDriverTimeoutException)
            {
                // Eğer öğe bulunamazsa kullanıcıya bildir ve işlemi durdur
                bilgilbx.Items.Add("Saldırı Yok");
                return;
            }
            catch (Exception ex)
            {
                // Diğer olası hatalar
                Console.WriteLine("Hata: " + ex.Message);
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
