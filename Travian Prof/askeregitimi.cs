using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Travian_Prof
{
    internal class Askeregitimi
    {


        //  Irk ve asker bilgilerini yöneten sınıf
        public class IrkAsker
        {
            // Irk ve asker listesi
            private readonly Dictionary<string, Dictionary<string, string>> irkAskerListesi = new Dictionary<string, Dictionary<string, string>>()
            {
                { "Cermenler", new Dictionary<string, string>
                    {
                        { "Tokmak", "//*[@id='nonFavouriteTroops']/div[1]/div/div[2]/div[4]/input" },
                        { "Mızraklı", "//*[@id='nonFavouriteTroops']/div[2]/div/div[2]/div[4]/input" },
                        { "Balta Sallayan", "//*[@id='nonFavouriteTroops']/div[3]/div/div[2]/div[4]/input" },
                        { "Casus", "//*[@id='nonFavouriteTroops']/div[4]/div/div[2]/div[4]/input" }
                    }
                },
                      { "Teutons", new Dictionary<string, string>
                    {
                        { "Clubswinger", "//*[@id='nonFavouriteTroops']/div[1]/div/div[2]/div[4]/input" },
                        { "Spearman", "//*[@id='nonFavouriteTroops']/div[2]/div/div[2]/div[4]/input" },
                        { "Axeman", "//*[@id='nonFavouriteTroops']/div[3]/div/div[2]/div[4]/input" },
                        { "Scout", "//*[@id='nonFavouriteTroops']/div[4]/div/div[2]/div[4]/input" }
                    }
                },
                { "Romalılar", new Dictionary<string, string>
                    {
                        { "Lejyoner", "//*[@id='nonFavouriteTroops']/div[1]/div/div[2]/div[4]/input" },
                        { "Proteryan", "//*[@id='nonFavouriteTroops']/div[2]/div/div[2]/div[4]/input" },
                        { "Emperyan", "//*[@id='nonFavouriteTroops']/div[3]/div/div[2]/div[4]/input" }
                    }
                },
                      { "Romans", new Dictionary<string, string>
                    {
                        { "Legionnaire", "//*[@id='nonFavouriteTroops']/div[1]/div/div[2]/div[4]/input" },
                        { "Praetorian", "//*[@id='nonFavouriteTroops']/div[2]/div/div[2]/div[4]/input" },
                        { "Imperian", "//*[@id='nonFavouriteTroops']/div[3]/div/div[2]/div[4]/input" }
                    }
                },
                { "Galyalılar", new Dictionary<string, string>
                    {
                        { "Phalanx", "//*[@id='nonFavouriteTroops']/div[1]/div/div[2]/div[4]/input" },
                        { "Kılıçlı", "//*[@id='nonFavouriteTroops']/div[2]/div/div[2]/div[4]/input" }
                    }
                },
                      { "Gauls", new Dictionary<string, string>
                    {
                        { "Phalanx", "//*[@id='nonFavouriteTroops']/div[1]/div/div[2]/div[4]/input" },
                        { "Swordsman", "//*[@id='nonFavouriteTroops']/div[2]/div/div[2]/div[4]/input" }
                    }
                },

                    { "Hunlar", new Dictionary<string, string>
                    {
                        { "Baltalı", "//*[@id='nonFavouriteTroops']/div[1]/div/div[2]/div[4]/input" },
                        { "Okçu", "//*[@id='nonFavouriteTroops']/div[2]/div/div[2]/div[4]/input" }
                    }
                },

                { "Huns", new Dictionary<string, string>
                    {
                        { "Mercenary", "//*[@id='nonFavouriteTroops']/div[1]/div/div[2]/div[4]/input" },
                        { "Bowman", "//*[@id='nonFavouriteTroops']/div[2]/div/div[2]/div[4]/input" }
                    }
                },
                       { "Egyptians", new Dictionary<string, string>
                    {
                        { "Slave Militia", "//*[@id='nonFavouriteTroops']/div[1]/div/div[2]/div[4]/input" },
                        { "Ash Warden", "//*[@id='nonFavouriteTroops']/div[2]/div/div[2]/div[4]/input" },
                        { "Khopesh Warrior", "//*[@id='nonFavouriteTroops']/div[3]/div/div[2]/div[4]/input" }
                    }
                },
                { "Mısırlılar", new Dictionary<string, string>
                    {
                        { "Köle", "//*[@id='nonFavouriteTroops']/div[1]/div/div[2]/div[4]/input" },
                        { "Kül Bekçisi", "//*[@id='nonFavouriteTroops']/div[2]/div/div[2]/div[4]/input" },
                        { "Kopeş Savaşçısı", "//*[@id='nonFavouriteTroops']/div[3]/div/div[2]/div[4]/input" }
                    }
                }

            };

            // Irk ve atlı asker listesi
            private readonly Dictionary<string, Dictionary<string, string>> atliAskerListesi = new Dictionary<string, Dictionary<string, string>>()
                   {
                       { "Galyalılar", new Dictionary<string, string>
                            {
                                { "Kaşif", "//*[@id='nonFavouriteTroops']/div[3]/div/div[2]/div[4]/input" },
                                { "Toytat", "//*[@id='nonFavouriteTroops']/div[4]/div/div[2]/div[4]/input" },
                                { "Druid", "//*[@id='nonFavouriteTroops']/div[5]/div/div[2]/div[4]/input" },
                                { "Heduan", "//*[@id='nonFavouriteTroops']/div[6]/div/div[2]/div[4]/input" }

                            }
                        },
                        { "Cermenler", new Dictionary<string, string>
                            {
                                { "Paladin", "//*[@id='nonFavouriteTroops']/div[5]/div/div[2]/div[4]/input" },
                                { "Toyton", "//*[@id='nonFavouriteTroops']/div[6]/div/div[2]/div[4]/input" }
                            }

                        },

                         { "Teutons", new Dictionary<string, string>
                            {
                                { "Paladin", "//*[@id='nonFavouriteTroops']/div[5]/div/div[2]/div[4]/input" },
                                { "Teutonic Knight", "//*[@id='nonFavouriteTroops']/div[6]/div/div[2]/div[4]/input" }
                            }

                        },      
                
                            { "Romans", new Dictionary<string, string>
                            {
                                { "Legati", "//*[@id='nonFavouriteTroops']/div[5]/div/div[2]/div[4]/input" },
                                { "İmperatoris", "//*[@id='nonFavouriteTroops']/div[6]/div/div[2]/div[4]/input" },
                                { "Caesaris", "//*[@id='nonFavouriteTroops']/div[7]/div/div[2]/div[4]/input" }
                            }
                            },


                           { "Romalılar", new Dictionary<string, string>
                            {
                                { "Legati", "//*[@id='nonFavouriteTroops']/div[5]/div/div[2]/div[4]/input" },
                                { "İmperatoris", "//*[@id='nonFavouriteTroops']/div[6]/div/div[2]/div[4]/input" },
                                { "Caesaris", "//*[@id='nonFavouriteTroops']/div[7]/div/div[2]/div[4]/input" }
                            }
                        },

                                   { "Gauls", new Dictionary<string, string>
                            {
                                { "Pathfinder", "//*[@id='nonFavouriteTroops']/div[3]/div/div[2]/div[4]/input" },
                                { "Theutates Thunder", "//*[@id='nonFavouriteTroops']/div[4]/div/div[2]/div[4]/input" },
                                { "Druid", "//*[@id='nonFavouriteTroops']/div[5]/div/div[2]/div[4]/input" },
                                { "Haeduan", "//*[@id='nonFavouriteTroops']/div[6]/div/div[2]/div[4]/input" }
                            }
                        },


                        { "Hunlar", new Dictionary<string, string>
                            {
                                { "Casus", "//*[@id='nonFavouriteTroops']/div[4]/div/div[2]/div[4]/input" },
                                { "Bozkır Atlısı", "//*[@id='nonFavouriteTroops']/div[5]/div/div[2]/div[4]/input" },
                                { "Akasir Atlısı", "//*[@id='nonFavouriteTroops']/div[6]/div/div[2]/div[4]/input" }
                            }
                        },
                               { "Huns", new Dictionary<string, string>
                            {
                                { "Spotter", "//*[@id='nonFavouriteTroops']/div[4]/div/div[2]/div[4]/input" },
                                { "Steppe Rider", "//*[@id='nonFavouriteTroops']/div[5]/div/div[2]/div[4]/input" },
                                { "Marksman", "//*[@id='nonFavouriteTroops']/div[6]/div/div[2]/div[4]/input" }
                            }
                        },
                        { "Mısırlılar", new Dictionary<string, string>
                            {
                                { "Casus", "//*[@id='nonFavouriteTroops']/div[5]/div/div[2]/div[4]/input" },
                                { "Anhur", "//*[@id='nonFavouriteTroops']/div[6]/div/div[2]/div[4]/input" },
                                { "Reşef", "//*[@id='nonFavouriteTroops']/div[7]/div/div[2]/div[4]/input" }

                            }
                        },
                         { "Egyptians", new Dictionary<string, string>
                            {
                                { "Sopdu", "//*[@id='nonFavouriteTroops']/div[5]/div/div[2]/div[4]/input" },
                                { "Anhur", "//*[@id='nonFavouriteTroops']/div[6]/div/div[2]/div[4]/input" },
                                { "Resheph", "//*[@id='nonFavouriteTroops']/div[7]/div/div[2]/div[4]/input" }

                            }
                        },

                    };

            public void IrkaGoreAtliEkle(string labelValue, ComboBox atlicbx)
            {
                atlicbx.Items.Clear();

                if (labelValue == "Henüz Irk Yok")
                    return;

                if (atliAskerListesi.TryGetValue(labelValue, out var atliListesi))
                {
                    foreach (var atli in atliListesi)
                    {
                        atlicbx.Items.Add(new ComboBoxItem(atli.Key, atli.Value));
                    }
                }
            }


            // Irka göre askerleri ComboBox'a ekle
            public void IrkaGoreAskerEkle(string labelValue, ComboBox yayacbx)
            {
                yayacbx.Items.Clear();

                if (labelValue == "Henüz Irk belirlenmedi")
                    return;

                if (irkAskerListesi.TryGetValue(labelValue, out var askerListesi))
                {
                    foreach (var asker in askerListesi)
                    {
                        yayacbx.Items.Add(new ComboBoxItem(asker.Key, asker.Value));
                    }
                }
            }


            // Seçilen askeri işlemek için XPath kullan
            public void AskerSecVeXPathIsle(string labelValue, string selectedAsker)
            {
                if (irkAskerListesi.TryGetValue(labelValue, out var askerListesi) &&
                    askerListesi.TryGetValue(selectedAsker, out var xpath))
                {
                    PerformActionWithXPath(xpath);
                }
                else
                {
                    Console.WriteLine($"Seçilen asker için XPath bulunamadı. Irk: {labelValue}, Asker: {selectedAsker}");
                }
            }

            // XPath ile işlem yapılacak metot
            private void PerformActionWithXPath(string xpath)
            {
                // Burada, XPath ile ilgili işlemi gerçekleştirebilirsiniz.
                Console.WriteLine($"XPath ile işlem yapılıyor: {xpath}");
            }



            public async Task AskerEgitimiYap(IWebDriver driver, WebDriverWait wait, string askerkoyXpath, string atliaskerXpath, string yayaaskerXpath, TextBox atliadettxt, TextBox yayaadettxt, ListBox BilgiEkle)
            {
                try
                {
                    WebDriverWait localWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    Random random = new Random();

                    // 1) Köy ekranına dön
                    var dorf2Element = localWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(@href, 'dorf2.php')]")));
                    new MouseSimulator(driver).SimulateMouseMovementAndClick(dorf2Element); // Fare hareketi ve tıklama
                    await Task.Delay(random.Next(2000, 5000));
                    BilgiEkle.Items.Add("Köy ekranına dönüldü.");

                    // 2) Köy seçimi
                    if (string.IsNullOrWhiteSpace(askerkoyXpath))
                    {
                        BilgiEkle.Items.Add("Geçerli bir köy XPath değeri girilmedi.");
                        return;
                    }

                    var selectedElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(askerkoyXpath)));
                    new MouseSimulator(driver).SimulateMouseMovementAndClick(selectedElement); // Fare hareketi ve tıklama
                    await Task.Delay(random.Next(1000, 3000));
                    BilgiEkle.Items.Add("Köy seçildi.");

                    // 3) Atlı asker eğitimi
                    if (!string.IsNullOrWhiteSpace(atliaskerXpath))
                    {
                        // Eğer atliaskerXpath boş veya null değilse bu blok çalışır.
                        var element = driver.FindElement(By.XPath("//*[contains(@href, '&gid=20')]"));
                        new MouseSimulator(driver).SimulateMouseMovementAndClick(element); // Fare hareketi ve tıklama
                        await Task.Delay(random.Next(2000, 5000));
                        BilgiEkle.Items.Add("Atlı asker eğitimi sayfasına gidildi.");

                        var atliInputElement = localWait.Until(ExpectedConditions.ElementIsVisible(By.XPath(atliaskerXpath)));
                        new MouseSimulator(driver).SimulateMouseMovementAndClick(atliInputElement); // Fare hareketi ve tıklama

                        // Atlı asker sayısını al ve rastgele bir değer ekle veya çıkar
                        if (int.TryParse(atliadettxt.Text, out int atliSayisi))
                        {
                            // Rastgele -1 veya +2 ekle
                            int randomChange = random.Next(-1, 3); // -1, 0 veya +2
                            atliSayisi += randomChange;

                            // Yeni değeri input alanına yaz
                            atliInputElement.Clear();
                            atliInputElement.SendKeys(atliSayisi.ToString());
                            BilgiEkle.Items.Add($"Atlı asker sayısı: {atliSayisi} olarak güncellendi.");
                        }

                        await Task.Delay(random.Next(2000, 5000));

                        var s1Element = localWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='s1']")));
                        new MouseSimulator(driver).SimulateMouseMovementAndClick(s1Element); // Fare hareketi ve tıklama
                        await Task.Delay(random.Next(2000, 5000));
                        BilgiEkle.Items.Add("Atlı asker eğitimi başlatıldı.");
                    }

                    await Task.Delay(random.Next(2000, 5000));

                    // 4) Köy ekranına tekrar dön
                    dorf2Element = localWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(@href, 'dorf2.php')]")));
                    new MouseSimulator(driver).SimulateMouseMovementAndClick(dorf2Element); // Fare hareketi ve tıklama
                    await Task.Delay(random.Next(2000, 5000));
                    BilgiEkle.Items.Add("Köy ekranına tekrar dönüldü.");

                    // 5) Yaya asker eğitimi
                    if (Convert.ToInt32(yayaadettxt.Text) > 0)
                    {
                        var yayaaskeri = localWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(@href, '&gid=19')]")));
                        new MouseSimulator(driver).SimulateMouseMovementAndClick(yayaaskeri); // Fare hareketi ve tıklama
                        await Task.Delay(random.Next(2000, 5000));
                        BilgiEkle.Items.Add("Yaya asker eğitimi sayfasına gidildi.");

                        var yayaInputElement = localWait.Until(ExpectedConditions.ElementIsVisible(By.XPath(yayaaskerXpath)));
                        new MouseSimulator(driver).SimulateMouseMovementAndClick(yayaInputElement); // Fare hareketi ve tıklama

                        // Yaya asker sayısını al ve rastgele bir değer ekle veya çıkar
                        if (int.TryParse(yayaadettxt.Text, out int yayaSayisi))
                        {
                            // Rastgele -1 veya +2 ekle
                            int randomChange = random.Next(-1, 3); // -1, 0 veya +2
                            yayaSayisi += randomChange;

                            // Yeni değeri input alanına yaz
                            yayaInputElement.Clear();
                            yayaInputElement.SendKeys(yayaSayisi.ToString());
                            BilgiEkle.Items.Add($"Yaya asker sayısı: {yayaSayisi} olarak güncellendi.");
                        }

                        await Task.Delay(random.Next(2000, 5000));

                        var s1Element2 = localWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='s1']")));
                        new MouseSimulator(driver).SimulateMouseMovementAndClick(s1Element2); // Fare hareketi ve tıklama
                        await Task.Delay(random.Next(3000, 6000));
                        BilgiEkle.Items.Add("Yaya asker eğitimi başlatıldı.");
                    }
                    else
                    {
                        BilgiEkle.Items.Add("Yaya eğitimi yapılmadı çünkü asker sayısı girilmedi veya XPath boş.");
                    }

                    BilgiEkle.Items.Add("Asker eğitimi tamamlandı.");
                }
                catch (Exception ex)
                {
                    BilgiEkle.Items.Add($"Hata meydana geldi: {ex.Message}");
                }
            }



        }
    }
}
