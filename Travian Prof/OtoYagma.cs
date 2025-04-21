using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Threading;
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
                var sayfalar = new Sayfalar(driver);
                sayfalar.YagmaListesiAc();

                var wrappers = wait.Until(driver =>
                {
                    var elements = driver.FindElements(By.CssSelector(".farmListWrapper"));
                    return elements.Count > 0 ? elements : null;
                });

                if (wrappers == null || wrappers.Count == 0)
                {
                    BilgiEkle("Herhangi bir farm listesi bulunamadı. İşlem yapılmadı.");
                    return;
                }

                foreach (var wrapper in wrappers)
                {
                    string wrapperClass = wrapper.GetAttribute("class");

                    if (wrapperClass.Contains("collapsed"))
                    {
                        try
                        {
                            // ExpandCollapse butonunu bul ve tıkla
                            var expandCollapseButton = wrapper.FindElement(By.CssSelector(".expandCollapse"));
                            if (expandCollapseButton.Displayed && expandCollapseButton.Enabled)
                            {
                                SimulateMouseMovementAndClick(expandCollapseButton);
                                BilgiEkle("ExpandCollapse butonuna ilk tıklama yapıldı.");
                            }

                            // ZararKontrol işlemini gerçekleştir
                            ZararKontrol(driver);
                            BilgiEkle("Zarar kontrol işlemi tamamlandı.");

                            // Tekrar ExpandCollapse butonuna tıkla
                            if (expandCollapseButton.Displayed && expandCollapseButton.Enabled)
                            {
                                SimulateMouseMovementAndClick(expandCollapseButton);
                                BilgiEkle("ExpandCollapse butonuna ikinci tıklama yapıldı.");
                            }

                            // StartFarmList butonunu bul ve tıkla
                            var startButton = wrapper.FindElement(By.CssSelector(".textButtonV2.buttonFramed.startFarmList.rectangle.withText.green"));
                            if (startButton.Displayed && startButton.Enabled)
                            {
                                SimulateMouseMovementAndClick(startButton);
                                BilgiEkle("Collapsed için StartFarmList butonuna tıklandı.");
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            BilgiEkle("Collapsed içinde bir öğe bulunamadı veya işlem yapılamadı.");
                        }
                    }
                    else if (wrapperClass.Contains("expanded"))
                    {
                        // expanded durumunda mevcut checkleme işlemi devam etsin
                        var slots = wrapper.FindElements(By.CssSelector(".slot"));
                        bool checkboxIsChecked = false;

                        foreach (var slot in slots)
                        {
                            try
                            {
                                if (slot.GetAttribute("class").Contains("disabled"))
                                {
                                    continue;
                                }
                                ZararKontrol(driver);
                                BilgiEkle("Zarar kontrol işlemi tamamlandı.");

                                var attackIcon = slot.FindElements(By.CssSelector(".attack_small"));
                                if (attackIcon.Count == 0)
                                {
                                    var checkbox = slot.FindElement(By.CssSelector("input[type='checkbox']"));

                                    // Öğeyi kontrol et ve görünür değilse kaydırma yap
                                    ScrollToElementIfNotVisible(driver, checkbox);

                                    if (!checkbox.Selected)
                                    {
                                        // Fare hareketi ile checkbox'ı işaretle
                                        SimulateMouseMovementGradual(checkbox);

                                        checkboxIsChecked = true;
                                        Thread.Sleep(new Random().Next(1, 20)); // Doğal bekleme süresi
                                    }
                                }
                            }
                            catch (NoSuchElementException)
                            {
                                BilgiEkle("Checkbox bulunamadı, geçildi.");
                            }
                        }

                        if (checkboxIsChecked)
                        {
                            try
                            {
                                var startButton = wrapper.FindElement(By.CssSelector(".textButtonV2.buttonFramed.startFarmList.rectangle.withText.green"));
                                if (startButton.Displayed && startButton.Enabled)
                                {
                                    // StartFarmList butonuna tıklama
                                    SimulateMouseMovementAndClick(startButton);
                                    BilgiEkle("Farmlist tıklandı.");
                                }
                            }
                            catch (NoSuchElementException)
                            {
                                BilgiEkle("StartFarmList butonu bulunamadı.");
                            }
                        }
                        else
                        {
                            BilgiEkle("Hiçbir checkbox işaretlenmedi, StartFarmList butonu tıklanmadı.");
                        }
                    }

                    // Her wrapper işleminden sonra küçük bir bekleme süresi
                    Thread.Sleep(new Random().Next(1000, 2000));
                }
            }
            catch (WebDriverTimeoutException)
            {
                BilgiEkle("Wrapper zaman aşımına uğradı. İşlem iptal.");
            }
            catch (Exception ex)
            {
                BilgiEkle("Genel hata: " + ex.Message);
            }
            finally
            {
                BilgiEkle("Oto Yağma işlemi tamamlandı.");
            }
        }








        // Scroll işlemi yapacak metod
        public void ScrollToElementIfNotVisible(IWebDriver driver, IWebElement element)
        {
            // Görünürlük kontrolü yap, öğe görünürse kaydırma yapma
            var isVisible = (bool)((IJavaScriptExecutor)driver).ExecuteScript(
                "return (arguments[0].getBoundingClientRect().top >= 0 && arguments[0].getBoundingClientRect().bottom <= window.innerHeight);",
                element);

            // Eğer öğe görünür değilse, kaydırma yap
            if (!isVisible)
            {
                IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
                jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", element);

                // Kaydırma işlemi doğal görünsün diye rastgele bekleme ekle
                Thread.Sleep(new Random().Next(300, 600));  // Doğal bir kaydırma için rastgele bekleme
            }
        }

        // Fareyi hedef öğeye yavaşça taşıma ve tıklama işlemi
        public void SimulateMouseMovementGradual(IWebElement element)
        {
            try
            {
                var actions = new Actions(driver);

                // Random nesnesi oluşturuluyor
                var random = new Random();

                // JavaScript kullanarak öğenin boyutlarını ve konumunu al
                var rect = (Dictionary<string, object>)((IJavaScriptExecutor)driver).ExecuteScript(@"
            var element = arguments[0];
            var rect = element.getBoundingClientRect();
            return { top: rect.top, left: rect.left, width: rect.width, height: rect.height };
        ", element);

                // Rect değerleri ile işlem yap
                double elementTop = Convert.ToDouble(rect["top"]);
                double elementLeft = Convert.ToDouble(rect["left"]);
                double elementWidth = Convert.ToDouble(rect["width"]);
                double elementHeight = Convert.ToDouble(rect["height"]);

                // Hareketi simüle et
                actions.MoveToElement(element, random.Next(-20, 20), random.Next(-10, 10)).Perform();
                Thread.Sleep(random.Next(150, 300)); // Daha küçük bekleme süreleri ile doğal hareketler simüle ediliyor

                // Hedef öğenin etrafında biraz dolaşarak daha doğal bir hareket elde et
                actions.MoveToElement(element, random.Next(-5, 5), random.Next(-5, 5)).Perform();
                Thread.Sleep(random.Next(100, 200)); // Yavaş hareket ekle

                // Hedef öğeye tıkla
                actions.Click().Perform();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fare simülasyonu sırasında bir hata oluştu: {ex.Message}");
            }
        }

        private void ZararKontrol(IWebDriver driver)
        {
            try
            {
                var slots = driver.FindElements(By.CssSelector(".slot"));

                foreach (var slot in slots)
                {
                    if (slot.GetAttribute("class").Contains("disabled"))
                        continue;

                    var lastRaidState = slot.FindElements(By.CssSelector(".lastRaidState.attack_lost_small"));
                    if (lastRaidState.Count > 0)
                    {
                        var openContextMenu = slot.FindElements(By.CssSelector(".openContextMenu"));
                        if (openContextMenu.Count > 0)
                        {
                            SimulateMouseMovementAndClick(openContextMenu[0]);
                            BilgiEkle("'Bir Checkbox İşaretlendi");
                        }

                        var startButton = slot.FindElements(By.CssSelector(".textButtonV2.buttonFramed.entry.deactivate.withTextAndIcon.rectangle.withText.green"));
                        if (startButton.Count > 0)
                        {
                            SimulateMouseMovementAndClick(startButton[0]);
                            BilgiEkle("Start butonuna tıklandı.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BilgiEkle("ZararKontrol sırasında hata: " + ex.Message);
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
