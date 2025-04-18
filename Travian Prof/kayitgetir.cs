using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Travian_Prof
{
    internal class kayitgetir
    {
        public void GenelVerileriYukle(
      Label irklbl,
      TextBox sunucutxt,
      TextBox chatidtxt,
      TextBox nicknametxt,
      TextBox passwordtxt,
      ComboBox npckoycbx,
      ComboBox askerkoycmbx,
      ComboBox yayacbx,
      ComboBox atlicbx)
        {
            try
            {
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Travian");
                string filePath = Path.Combine(folderPath, "Genel.txt");

                if (!File.Exists(filePath))
                {
                    return;
                }

                string[] lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    if (line.StartsWith("Halk:*"))
                    {
                        string value = line.Substring("Halk:*".Length).Trim();
                        irklbl.Text = value.Replace(" ", "");
                    }
                    else if (line.StartsWith("URL:*"))
                    {
                        sunucutxt.Text = line.Substring("URL:*".Length).Trim();
                    }
                    else if (line.StartsWith("Username:*"))
                    {
                        nicknametxt.Text = line.Substring("Username:*".Length).Trim();
                    }
                    else if (line.StartsWith("Password:*"))
                    {
                        passwordtxt.Text = line.Substring("Password:*".Length).Trim();
                        passwordtxt.UseSystemPasswordChar = true;
                    }
                    else if (line.StartsWith("Chatid:*"))
                    {
                        chatidtxt.Text = line.Substring("Chatid:*".Length).Trim();
                    }
                    else if (line.StartsWith("NPC Köy:*"))
                    {
                        string name = line.Substring("NPC Köy:*".Length).Trim();
                        string xpath = lines[i + 1].Substring("NPC Köy Xpath Yolu:*".Length).Trim();
                        ComboBoxItem item = new ComboBoxItem(name, xpath);
                        npckoycbx.Items.Add(item);
                        i++; // Bir sonraki satırı atla
                    }
                    else if (line.StartsWith("Asker Köy:*"))
                    {
                        string name = line.Substring("Asker Köy:*".Length).Trim();
                        string xpath = lines[i + 1].Substring("Asker Köy Xpath Yolu:*".Length).Trim();
                        ComboBoxItem item = new ComboBoxItem(name, xpath);
                        askerkoycmbx.Items.Add(item);
                        i++; // Bir sonraki satırı atla
                    }
                    else if (line.StartsWith("Yaya Asker:*"))
                    {
                        string name = line.Substring("Yaya Asker:*".Length).Trim();
                        // ComboBox'a yaya askerini seç
                        for (int j = 0; j < yayacbx.Items.Count; j++)
                        {
                            ComboBoxItem item = (ComboBoxItem)yayacbx.Items[j];
                            if (item.Text == name)
                            {
                                yayacbx.SelectedItem = item;
                                break;
                            }
                        }
                    }
                    else if (line.StartsWith("Atlı Asker:*"))
                    {
                        string name = line.Substring("Atlı Asker:*".Length).Trim();
                        // ComboBox'a atlı askerini seç
                        for (int j = 0; j < atlicbx.Items.Count; j++)
                        {
                            ComboBoxItem item = (ComboBoxItem)atlicbx.Items[j];
                            if (item.Text == name)
                            {
                                atlicbx.SelectedItem = item;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Genel.txt okunurken hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void VillagelariYukle(
            ComboBox npckoycbx,
            ComboBox buildkoycbx,
            ComboBox askerkoycmbx,
            ListBox bilgilertxt = null)
        {
            try
            {
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Travian");
                string filePath = Path.Combine(folderPath, "villages.txt");

                if (!File.Exists(filePath))
                {
                    MessageBox.Show(
                        "Lütfen ilk girişte giriş yap ve bilgi çek butonuna tıklayın.\nSonrasında verilerinizi kayıt edin.\n\nBu işlem yalnızca bir kez yapılacaktır.",
                        "Bilgi Eksik",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    bilgilertxt?.Items.Add("villages.txt dosyası bulunamadı.");
                    return;
                }

                List<ComboBoxItem> villageItems = VillagesDosyasindanOku(filePath, bilgilertxt);

                // ComboBox'ları temizle
                npckoycbx.Items.Clear();
                buildkoycbx.Items.Clear();
                askerkoycmbx.Items.Clear();

                foreach (var item in villageItems)
                {
                    npckoycbx.Items.Add(item);
                    buildkoycbx.Items.Add(item);
                    askerkoycmbx.Items.Add(item);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Köyler yüklenirken bir hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bilgilertxt?.Items.Add("Köyler yüklenirken hata oluştu: " + ex.Message);
            }
            bilgilertxt?.Items.Add("Köyler başarıyla yüklendi.");


        }

        private List<ComboBoxItem> VillagesDosyasindanOku(string filePath, ListBox bilgilertxt = null)
        {
            List<ComboBoxItem> items = new List<ComboBoxItem>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length - 1; i += 2)
                {
                    string nameLine = lines[i];
                    string xpathLine = lines[i + 1];

                    string visibleText = nameLine.Split('*')[1].Trim();
                    string xpath = xpathLine.Split(new[] { ":*" }, StringSplitOptions.None)[1].Trim();

                    items.Add(new ComboBoxItem(visibleText, xpath));
                }

                return items;
            }
            catch (Exception ex)
            {
                bilgilertxt?.Items.Add("villages.txt okunurken hata: " + ex.Message);
                return items;
            }
        }
    }
}
