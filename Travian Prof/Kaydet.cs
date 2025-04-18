using System;
using System.IO;
using System.Windows.Forms;

namespace Travian_Prof
{
    internal class Kaydet
    {
        public void VerileriKaydet(
            string url,
            string username,
            string password,
            string chatid,
            ComboBox npckoycbx,
            ComboBox yayacbx,
            ComboBox atlicbx,
            ComboBox askerkoycmbx)
        {
            try
            {
                string npcKoy = npckoycbx.SelectedItem?.ToString() ?? "Seçilmedi";
                string yayaAsker = yayacbx.SelectedItem?.ToString() ?? "Seçilmedi";
                string atliAsker = atlicbx.SelectedItem?.ToString() ?? "Seçilmedi";
                string askerKoy = askerkoycmbx.SelectedItem?.ToString() ?? "Seçilmedi";

                string npcKoyXpath = GetComboBoxValue(npckoycbx);
                string askerKoyXpath = GetComboBoxValue(askerkoycmbx);

                string folderPath = GetOrCreateFolder("Travian");
                string filePath = Path.Combine(folderPath, "Genel.txt");

                string content = $"URL:* {url}\nUsername:* {username}\nPassword:* {password}\nNPC Köy:* {npcKoy}\nNPC Köy Xpath Yolu:* {npcKoyXpath}\nAsker Köy:* {askerKoy}\nAsker Köy Xpath Yolu:* {askerKoyXpath}\nYaya Asker:* {yayaAsker}\nAtlı Asker:* {atliAsker}\nChatid:*{chatid}\n";
                WriteToFile(filePath, content);

                MessageBox.Show($"Veriler başarıyla kaydedildi! Dosya yolu: {filePath}", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetComboBoxValue(ComboBox comboBox)
        {
            if (comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                return selectedItem.Value;
            }
            MessageBox.Show($"'{comboBox.Name}' ComboBox'ında geçerli bir seçim yapılmadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return "Seçilmedi";
        }

        private string GetOrCreateFolder(string folderName)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath;
        }

        private void WriteToFile(string filePath, string content)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false))
                {
                    sw.Write(content);
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show("Dosya yazma işlemi sırasında bir hata oluştu: " + ioEx.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class ComboBoxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public ComboBoxItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
