using System;
using System.IO;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;
using ComboBox = System.Windows.Forms.ComboBox;
using Telegram.Bot;

using static Travian_Prof.Askeregitimi;
using static Travian_Prof.AnaGiris;

namespace Travian_Prof
{
    public partial class Form1 : Form
    {
        private IWebDriver? driver; // WebDriver
        private bool isLoggedIn = false; // Giriş durumu kontrolü
        private System.Windows.Forms.Timer timer; // Timer nesnesi
        private AnaGiris anaGiris; // AnaGiris sınıfı nesnesi
        private System.Windows.Forms.Timer geriSayimTimer;
        private int geriSayimSure;
        private LisansYonetimi lisansYonetimi = new LisansYonetimi();
        private DateTime? bugunTarihi = null; // Google'dan çekilen güncel tarih
        SaldiriVar saldiriVar = new SaldiriVar();

        private TelegramBotClient botClient;

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            anaGiris = new AnaGiris(); // AnaGiris nesnesi oluştur
            this.Load += new EventHandler(Form1_Load);
            string halk = irklbl.Text;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Formun büyütülmesini engeller.
            this.MaximizeBox = false;

            // Formun küçültülmesini engeller (isteğe bağlı).
            this.MinimizeBox = false;
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folderPath = Path.Combine(desktopPath, "Travian");
            string filePathlis = Path.Combine(folderPath, "travianApi.txt");

            string halk = irklbl.Text;
            try
            {
                // kayitgetir sınıfından bir nesne oluştur
                kayitgetir getir = new kayitgetir();

                // Genel.txt dosyasındaki verileri yükle
                getir.GenelVerileriYukle(irklbl, sunucutxt, chatidtxt, nicknametxt, passwordtxt, npckoycbx, askerkoycmbx, yayacbx, atlicbx);
                getir.VillagelariYukle(npckoycbx, buildkoycbx, askerkoycmbx, bilgilbx);
                IrkAsker irkAsker = new IrkAsker();
                irkAsker.IrkaGoreAskerEkle(halk, yayacbx);
                irkAsker.IrkaGoreAtliEkle(irklbl.Text, atlicbx);
                // Timer oluştur ve ayarla
                timer = new System.Windows.Forms.Timer();
                timer.Interval = 1000; // 1 saniye aralıklarla
                string anahtar = "anahtarınız"; // Bu anahtarı güvenli bir şekilde saklayın



                bugunTarihi = await lisansYonetimi.GoogleTarihGetirAsync();

                if (bugunTarihi == null)
                {
                    MessageBox.Show("İnternet bağlantısı kısıtlı veya yok. Program çalışmaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }

                // travianApi.txt içindeki şifreli veriyi oku ve çöz
                if (File.Exists(filePathlis))
                {
                    try
                    {
                        string sifreliVeri = File.ReadAllText(filePathlis);
                        string cozulmusTarih = lisansYonetimi.Decrypt(sifreliVeri);

                        // Çözülmüş tarihi label8 içinde göster

                        DateTime lisansBitisTarihi = DateTime.ParseExact(cozulmusTarih, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        lisansYonetimi.LisansBitisTarihiniGuncelle(lisansBitisTarihi);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lisans bilgisi okunurken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                        return;
                    }
                }
                else
                {
                    baslatbtn.Enabled = false;

                    MessageBox.Show("Lisans dosyası bulunamadı. Program çalışmaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lisans durumunu kontrol et ve gerekli işlemleri yap
                LisansDurumunuGuncelle();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Form yüklenirken bir hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LisansDurumunuGuncelle()
        {
            if (!lisansYonetimi.LisansGecerliMi(bugunTarihi.Value))
            {
                baslatbtn.Enabled = false;
                lblLisansDurumu2.Text = $"Lisans süresi doldu! ({lisansYonetimi.LisansBitisTarihi:yyyy-MM-dd})";
            }
            else
            {
                baslatbtn.Enabled = true;
                lblLisansDurumu2.Text = $"Lisans Bitiş Tarihi: {lisansYonetimi.LisansBitisTarihi:yyyy-MM-dd}";
            }
        }





        private void girisbtn_Click(object sender, EventArgs e)
        {
            try
            {
                // TextBox'lardan verileri al
                string urladresi = sunucutxt.Text.Trim();
                string username = nicknametxt.Text.Trim();
                string passwrd = passwordtxt.Text.Trim();

                // Boş alan kontrolü
                if (string.IsNullOrWhiteSpace(urladresi) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(passwrd))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // URL'yi kontrol et ve gerekli düzeltmeleri yap
                if (!urladresi.StartsWith("http://") && !urladresi.StartsWith("https://"))
                {
                    urladresi = "http://" + urladresi;
                }

                // Kullanıcıya bilgi göster
                BilgiEkle("Giriş işlemi başlatılıyor...", bilgilbx);

                // Giriş işlemini AnaGiris sınıfı üzerinden çağır
                driver = anaGiris.GirisYap(urladresi, username, passwrd, bilgilbx, npckoycbx, buildkoycbx, askerkoycmbx, driver);

                // Giriş başarılıysa işlem yap
                if (driver != null)
                {
                    // Giriş durumunu güncelle
                    isLoggedIn = true;
                    BilgiEkle("Giriş başarılı! Köy bilgileri işleniyor.", bilgilbx);

                    // Timer başlat
                    timer.Start();
                    try
                    {
                        driver.Navigate().GoToUrl(urladresi.TrimEnd('/') + "/profile");

                        WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        var element2 = wait2.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@colspan='2']")));

                        string textValue = element2.Text;
                        irklbl.Text = textValue;

                        driver.Navigate().GoToUrl(urladresi.TrimEnd('/') + "/dorf1.php");
                    }
                    catch (NoSuchElementException ex)
                    {
                        MessageBox.Show("Eleman bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    string irkLabelValue = irklbl.Text;
                    irklbl.ForeColor = Color.Black;
                    irklbl.Font = new Font(irklbl.Font, FontStyle.Bold);

                    // Irka göre askerleri ComboBox'a eklemek için IrkAsker sınıfındaki metodunu çağır
                    IrkAsker irkAsker = new IrkAsker();
                    irkAsker.IrkaGoreAskerEkle(irkLabelValue, yayacbx);
                    irkAsker.IrkaGoreAtliEkle(irklbl.Text, atlicbx);

                    driver.Quit();
                    // Kullanıcıya başarı mesajı göster
                    MessageBox.Show("Köy ve asker bilgileri Çekildi!", "Veri Çekme başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Driver null döndüyse uyarı ver
                    BilgiEkle("Giriş başarısız. Lütfen bilgilerinizi kontrol edin.", bilgilbx);
                    MessageBox.Show("Giriş başarısız oldu. Lütfen bilgilerinizi kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya mesaj göster ve logla
                BilgiEkle($"Giriş sırasında bir hata oluştu: {ex.Message}", bilgilbx);
                MessageBox.Show($"Giriş sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private async void baslatbtn_Click(object sender, EventArgs e)
        {
            if (npcchk.Checked || buildchk.Checked || askeregitchk.Checked || saldirivarchk.Checked || askerolmesinchk.Checked || otoyagmachk.Checked || otomacerachk.Checked)
            {
                try
                {
                    // Zamanlayıcı süreyi ayarla ve başlat
                    Random random = new Random(); // Rastgele sayı üretimi için Random sınıfı
                    int ekstraSure = random.Next(1, 3); // 1 ile 4 dakika arasında rastgele süre oluştur

                    geriSayimSure = (int.Parse(tekrartxt.Text) + ekstraSure) * 60; // Dakika cinsinden süreyi saniyeye çevir
                    geriSayimTimer = new System.Windows.Forms.Timer();
                    geriSayimTimer.Interval = 1000; // 1 saniye aralıklarla çalıştır
                    geriSayimTimer.Tick += async (senderTimer, eTimer) =>
                    {
                        if (geriSayimSure > 0)
                        {
                            geriSayimSure--;
                            int minutes = geriSayimSure / 60;
                            int seconds = geriSayimSure % 60;
                            label31.Text = "Programın başlaması için Kalan Süre: " + string.Format("{0:D2}:{1:D2}", minutes, seconds);
                        }
                        else
                        {
                            geriSayimTimer.Stop(); // Timer'ı durdur
                            try
                            {
                                // Giriş bilgilerini oku
                                string url = sunucutxt.Text.Trim();           // Sunucu URL'si
                                string username = nicknametxt.Text.Trim();    // Kullanıcı adı
                                string password = passwordtxt.Text.Trim();    // Şifre

                                // Giris_Sade sınıfından bir nesne oluştur ve giriş yap
                                Giris_Sade girisSade = new Giris_Sade();
                                IWebDriver driver = girisSade.GirisYapSade(url, username, password, bilgilbx, this.driver);

                                if (driver == null)
                                {
                                    MessageBox.Show("Giriş sırasında hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                // Görevleri liste olarak oluştur
                                List<Func<IWebDriver, Task>> görevler = new List<Func<IWebDriver, Task>>();

                                // Görevleri ekleyelim
                                if (saldirivarchk.Checked)
                                {
                                    görevler.Add(async (driver) => await saldiriVar.Baslat(driver, long.Parse(chatidtxt.Text), bilgilbx));
                                }
                                if (otoyagmachk.Checked)
                                {
                                    görevler.Add(async (driver) =>
                                    {
                                        OtoYagma otoYagma = new OtoYagma(driver, bilgilbx);
                                        otoYagma.Execute(url);
                                    });
                                }
                                if (otomacerachk.Checked)
                                {
                                    görevler.Add(async (driver) =>
                                    {
                                        otomacera macera = new otomacera();
                                        macera.Baslat(driver);
                                    });
                                }
                                if (npcchk.Checked)
                                {
                                    görevler.Add(async (driver) =>
                                    {
                                        string npcKoyXpath = "";
                                        if (npckoycbx.SelectedItem is ComboBoxItem selectedItem)
                                        {
                                            npcKoyXpath = selectedItem.Value;
                                        }
                                        OtoNpc otoNpc = new OtoNpc(driver, bilgilbx, npcKoyXpath);
                                        otoNpc.Execute(url);
                                    });
                                }
                                if (askerolmesinchk.Checked)
                                {
                                    görevler.Add(async (driver) =>
                                    {
                                        BilgiEkle("Asker Ölmesin işlemi başlatılıyor...", bilgilbx);
                                        Askerolmesin askerOlmesin = new Askerolmesin(driver, bilgilbx, this, url);
                                        askerOlmesin.TumKoylereTikla();



                                    });
                                }
                                if (askeregitchk.Checked)
                                {
                                    görevler.Add(async (driver) =>
                                    {
                                        try
                                        {
                                            Askeregitimi askeregitimiInstance = new Askeregitimi();
                                            var irkAsker = new Askeregitimi.IrkAsker();
                                            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                                            string askerkoycmbxXpath = "";
                                            string atliaskerxpath = "";
                                            string yayaaskerxpath = "";

                                            if (askerkoycmbx.SelectedItem is ComboBoxItem selectedItem)
                                            {
                                                askerkoycmbxXpath = selectedItem.Value;
                                            }
                                            if (atlicbx.SelectedItem is ComboBoxItem selectedAtli)
                                            {
                                                atliaskerxpath = selectedAtli.Value;
                                            }
                                            if (yayacbx.SelectedItem is ComboBoxItem selectedYaya)
                                            {
                                                yayaaskerxpath = selectedYaya.Value;
                                            }

                                            await irkAsker.AskerEgitimiYap(driver, wait2, askerkoycmbxXpath, atliaskerxpath, yayaaskerxpath, atliadettxt, yayaadettxt, bilgilbx);
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show($"Asker Eğitimi işlemi sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    });
                                }


                                // Listeyi karıştır (random sıraya göre)
                                var görevlerKaristirilmis = görevler.OrderBy(x => random.Next()).ToList();

                                // Karıştırılmış listeyi sırayla çalıştır
                                foreach (var görev in görevlerKaristirilmis)
                                {
                                    await görev(driver);
                                }

                                driver.Quit(); // Tarayıcıyı tamamen kapatır
                                driver.Dispose(); // Tarayıcı kaynaklarını temizler
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                int ekstraSure2 = random.Next(1, 8); // 1 ile 4 dakika arasında rastgele süre oluştur

                                // Timer'ı yeniden başlat
                                geriSayimSure = (int.Parse(tekrartxt.Text) + ekstraSure2) * 60; // Süreyi yeniden başlat
                                geriSayimTimer.Start();
                            }
                        }
                    };

                    geriSayimTimer.Start(); // Timer'ı başlat
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Programın çalışması için Tekrar dakikası girmeniz lazım.", "UYARI!!!");
                }
            }
            else
            {
                MessageBox.Show("Hiç bir koşul aktif edilmedi. Ne çalışmasını bekliyorsun?", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        // BilgiEkle metodunu kullanarak bilgilendirme yapılır
        public void BilgiEkle(string mesaj, ListBox bilgilertxt)
        {
            bilgilertxt.Items.Add(mesaj);
            bilgilertxt.SelectedIndex = bilgilertxt.Items.Count - 1;
        }

        private void kaydetbtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcıdan alınan veriler
                string url = sunucutxt.Text.Trim();
                string username = nicknametxt.Text.Trim();
                string password = passwordtxt.Text.Trim();
                string chatid = chatidtxt.Text.Trim();
                // ComboBox'lardan seçilen değerler
                string npcKoy = npckoycbx.SelectedItem?.ToString() ?? "Seçilmedi"; // NPC Köy
                string askerKoy = askerkoycmbx.SelectedItem?.ToString() ?? "Seçilmedi"; // Asker Köy

                // XPath değerleri ComboBox'dan alınır
                string npcKoyXpath = (npckoycbx.SelectedItem is ComboBoxItem selectedNpcItem) ? selectedNpcItem.Value : "Seçilmedi";
                string askerKoyXpath = (askerkoycmbx.SelectedItem is ComboBoxItem selectedAskerItem) ? selectedAskerItem.Value : "Seçilmedi";

                // ComboBox'dan seçilen asker türleri ve XPath değerleri
                string yayaAsker = yayacbx.SelectedItem?.ToString() ?? "Seçilmedi";
                string atliAsker = atlicbx.SelectedItem?.ToString() ?? "Seçilmedi";
                string yayaAskerXpath = GetComboBoxValue(yayacbx);
                string atliAskerXpath = GetComboBoxValue(atlicbx);

                // Geçersiz XPath kontrolü


                // Masaüstü yolunu belirleme
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string folderPath = Path.Combine(desktopPath, "Travian");

                // Klasör yoksa oluştur
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Dosya yolu belirleme ve veri yazımı
                string filePath = Path.Combine(folderPath, "Genel.txt");
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    writer.WriteLine($"Halk:* {irklbl.Text}");
                    writer.WriteLine($"URL:* {url}");
                    writer.WriteLine($"Username:* {username}");
                    writer.WriteLine($"Password:* {password}");
                    writer.WriteLine($"NPC Köy:* {npcKoy}");
                    writer.WriteLine($"NPC Köy Xpath Yolu:* {npcKoyXpath}");
                    writer.WriteLine($"Asker Köy:* {askerKoy}");
                    writer.WriteLine($"Asker Köy Xpath Yolu:* {askerKoyXpath}");
                    writer.WriteLine($"Yaya Asker:* {yayaAsker}");
                    writer.WriteLine($"Yaya Asker Xpath Yolu:* {yayaAskerXpath}");
                    writer.WriteLine($"Atlı Asker:* {atliAsker}");
                    writer.WriteLine($"Atlı Asker Xpath Yolu:* {atliAskerXpath}");
                    writer.WriteLine($"Chatid:* {chatid}");

                }

                // Başarı mesajı
                MessageBox.Show("Veriler başarıyla kaydedildi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Hata mesajı
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ComboBox'tan XPath değeri almak için yardımcı metod
        private string GetComboBoxValue(ComboBox comboBox)
        {
            if (comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                return selectedItem.Value; // Seçilen öğenin XPath değeri
            }
            return "Seçilmedi"; // Seçim yapılmamışsa varsayılan değer dönecek
        }

        private void formatbtn_Click(object sender, EventArgs e)
        {
            // Kullanıcıya onay mesajı göster
            DialogResult dialogResult = MessageBox.Show("Tüm ayarların silinecek! Yeniden ayar yapılabilmen için tekrardan giriş bilgilerini doldurup 'Giriş Yap ve Bilgi Çek' butonuna tıklaman ve kaydetmen gerekiyor aksi halde programı kullanamazsın... ", "Mevcut Ayarları Silme İşlemi!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            // Eğer kullanıcı 'Evet' derse işlemi gerçekleştirin
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string travianPath = Path.Combine(desktopPath, "Travian");

                    if (Directory.Exists(travianPath))
                    {
                        string[] txtFiles = Directory.GetFiles(travianPath, "*.txt");

                        foreach (string file in txtFiles)
                        {
                            File.Delete(file); // .txt dosyalarını siliyor
                        }

                        MessageBox.Show("Kayıtlı bilgiler silindi..", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show("Travian klasörü bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {

                    BilgiEkle("Silme işleminde hata mevcut", bilgilbx);


                }
            }
        }

        private void npcchk_CheckedChanged(object sender, EventArgs e)
        {

            if (npckoycbx.SelectedIndex == -1)
            {

                panel1.BackColor = Color.Gold;

            }
            if (npckoycbx.SelectedIndex != -1)
            {

                panel1.BackColor = Color.LightGreen;

            }
        }

        private void npckoycbx_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (npcchk.Checked)
            {

                panel1.BackColor = Color.LightGreen;

            }
            else
            {
                panel1.BackColor = Color.Gold;


            }
        }

        private void buildchk_CheckedChanged(object sender, EventArgs e)
        {
            Panel4KontrolleriniKontrolEt();
        }

        private void binalarcbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel4KontrolleriniKontrolEt();
        }

        private void buildkoycbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel4KontrolleriniKontrolEt();
        }



        private void Panel4KontrolleriniKontrolEt()
        {
            if (buildchk.Checked &&
                binalarcbx.SelectedIndex != -1 &&
                buildkoycbx.SelectedIndex != -1)
            {
                // Tüm seçimler yapılmış → yapılacak işlem
                panel4.BackColor = Color.LightGreen; // Örnek: Panel4 rengini değiştir
                                                     // ya da başka bir işlem: buton aktif et vs.
            }
            else
            {
                panel4.BackColor = Color.Gold; // Eksik seçim varsa farklı renk
            }
        }

        private void askeregitchk_CheckedChanged(object sender, EventArgs e)
        {
            Panel5KontrolleriniKontrolEt();
        }

        private void yayacbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel5KontrolleriniKontrolEt();
        }

        private void atlicbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel5KontrolleriniKontrolEt();
        }

        private void askerkoycmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel5KontrolleriniKontrolEt();
        }

        private void yayaadettxt_TextChanged(object sender, EventArgs e)
        {
            Panel5KontrolleriniKontrolEt();
        }

        private void atliadettxt_TextChanged(object sender, EventArgs e)
        {
            Panel5KontrolleriniKontrolEt();
        }



        private void Panel5KontrolleriniKontrolEt()
        {
            bool askerEgitimiVarMi = askeregitchk.Checked;

            bool yayaSecildi = yayacbx.SelectedIndex != -1;
            bool atliSecildi = atlicbx.SelectedIndex != -1;

            bool yayaAdetGirildi = !string.IsNullOrWhiteSpace(yayaadettxt.Text);
            bool atliAdetGirildi = !string.IsNullOrWhiteSpace(atliadettxt.Text);

            bool askerKoySecildi = askerkoycmbx.SelectedIndex != -1;

            bool eksikYaya = yayaSecildi && !yayaAdetGirildi;
            bool eksikAtli = atliSecildi && !atliAdetGirildi;

            // Yeni kontrol: asker eğitimi, köy ve tüm diğer alanlar birlikte değerlendiriliyor
            if (!askerEgitimiVarMi || !askerKoySecildi || eksikYaya || eksikAtli)
            {
                panel5.BackColor = Color.Gold; // Eksik varsa sarı
            }
            else if ((yayaSecildi && yayaAdetGirildi) || (atliSecildi && atliAdetGirildi))
            {
                panel5.BackColor = Color.LightGreen; // Her şey tamam
            }
            else
            {
                panel5.BackColor = Color.Gold; // Hiçbir şey seçili değilse de sarı
            }
        }

        private void askerolmesinchk_CheckedChanged(object sender, EventArgs e)
        {
            if (askerolmesinchk.Checked)
            {
                panel6.BackColor = Color.LightGreen; // Seçili ise yeşil
            }
            else
            {
                panel6.BackColor = SystemColors.ControlLight; // Seçili değilse ControlLight (gri)
            }

        }

        private void otoyagmachk_CheckedChanged(object sender, EventArgs e)
        {
            if (otoyagmachk.Checked)
            {
                panel2.BackColor = Color.LightGreen; // Seçili ise yeşil
            }
            else
            {
                panel2.BackColor = SystemColors.ControlLight; // Seçili değilse ControlLight (gri)
            }

        }

        private void otomacerachk_CheckedChanged(object sender, EventArgs e)
        {
            if (otomacerachk.Checked)
            {
                panel3.BackColor = Color.LightGreen; // Seçili ise yeşil
            }
            else
            {
                panel3.BackColor = SystemColors.ControlLight; // Seçili değilse ControlLight (gri)
            }

        }

        private void lisansbtn_Click(object sender, EventArgs e)
        {
            string girilenSifreliVeri = lisanstxt.Text.Trim(); // txtSifre adında bir TextBox ile şifreli veriyi al

            // Şifreleme anahtarı
            string anahtar = "anahtarınız"; // Bu anahtarı güvenli bir şekilde saklayın

            try
            {
                // Tarihi çöz
                string cozulmusTarih = lisansYonetimi.Decrypt(girilenSifreliVeri);

                // Tarihi DateTime'e dönüştür
                DateTime tarih = DateTime.ParseExact(cozulmusTarih, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                // Lisans bitiş tarihini güncelle
                lisansYonetimi.LisansBitisTarihiniGuncelle(tarih);

                // Masaüstü yolunu al
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string folderPath = Path.Combine(desktopPath, "Travian");
                string filePath = Path.Combine(folderPath, "travianApi.txt");

                // Klasör yoksa oluştur
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Şifreli veriyi dosyaya yaz
                File.WriteAllText(filePath, girilenSifreliVeri);

                // Lisans eklendi, durumu güncelle
                MessageBox.Show(
                    "Lisans Eklendi. Güncelleme yapıldı.",
                    "Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LisansDurumunuGuncelle(); // Lisans durumunu güncelle
            }
            catch (Exception ex)
            {
                // Genel hata
                MessageBox.Show(
                    $"Bir hata oluştu: {ex.Message}",
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            lisanstxt.Clear(); // TextBox'ı temizle
        }

        private void saldirivarchk_CheckedChanged(object sender, EventArgs e)
        {

            if (saldirivarchk.Checked)
            {
                panel7.BackColor = Color.Gold; // Seçili ise yeşil

                if (chatidtxt.Text == "")
                {

                    panel7.BackColor = Color.Gold;
                }
                else
                {
                    panel7.BackColor = Color.LightGreen;


                }

            }
            else
            {
                panel7.BackColor = SystemColors.ControlLight; // Seçili değilse ControlLight (gri)
            }
        }

        private void chatidtxt_TextChanged(object sender, EventArgs e)
        {
            if (saldirivarchk.Checked != true)
            {
                panel7.BackColor = Color.Gold;

            }
            else panel7.BackColor = Color.LightGreen;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            label1.Text = "Server URL:";
            label2.Text = "Email :";
            label3.Text = "Password:";
            kaydetbtn.Text = "Save Settings";
            girisbtn.Text = "Login and Fetch Info";
            label4.Text = "NPC CHECK";
            label5.Text = "AUTO RAID";
            label6.Text = "AUTO ADVENTURE";
            label7.Text = "Upgrade Building";
            label8.Text = "TRAIN TROOPS";
            label9.Text = "NO TROOP DEATH";
            label10.Text = "On/Off:";
            label11.Text = "On/Off:";
            label12.Text = "On/Off:";
            label13.Text = "On/Off:";
            label14.Text = "On/Off:";
            label15.Text = "On/Off:";
            label16.Text = "Infantry:";
            label17.Text = "Cavalry:";
            label18.Text = "Village:";
            label19.Text = "Village:";
            label20.Text = "Village:";
            label21.Text = "Units";
            label22.Text = "Every (min):";
            label23.Text = "License:";
            label24.Text = "Building:";
            label25.Text = "Upgrade +1 level if resources are sufficient";
            label26.Text = "Under Attack!";
            label27.Text = "On/Off:";
            label28.Text = "Chat ID:";
            label29.Text = "RACE:";
            label30.Text = "If there's less than 1 hour of crop left in negative production villages, NPC is triggered.";
            label32.Text = "NOT WORKING YET";
            baslatbtn.Text = "START";


        }
    }
}
