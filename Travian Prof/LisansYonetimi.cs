using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class LisansYonetimi
{
    // Lisans bitiş tarihi
    public DateTime LisansBitisTarihi { get; private set; }

    // Rakamların kripto değerleri
    private Dictionary<char, string> kriptoSozluk = new Dictionary<char, string>
    {
            {'0', "X2@FV"},
            {'1', "L6#MB"},
            {'2', "Z1$RT"},
            {'3', "Q9^YP"},
            {'4', "H3*WD"},
            {'5', "K8%NC"},
            {'6', "U7&GE"},
            {'7', "R0+LS"},
            {'8', "A5!JK"},
            {'9', "T4=HZ"}
    };

    // Constructor
    public LisansYonetimi()
    {
        // Constructor boş bırakıldı, çünkü lisans bitiş tarihi dosyadan alınacak
    }

    // Lisans bitiş tarihini güncelleyen metot
    public void LisansBitisTarihiniGuncelle(DateTime yeniTarih)
    {
        LisansBitisTarihi = yeniTarih;
    }

    // Tarihi şifreleyen metot
    public string Sifrele(string veri, string anahtar)
    {
        byte[] veriBytes = Encoding.UTF8.GetBytes(veri);
        byte[] anahtarBytes = Encoding.UTF8.GetBytes(anahtar);
        using (Aes aes = Aes.Create())
        {
            using (ICryptoTransform encryptor = aes.CreateEncryptor(anahtarBytes, aes.IV))
            {
                byte[] encryptedBytes = encryptor.TransformFinalBlock(veriBytes, 0, veriBytes.Length);
                byte[] result = new byte[aes.IV.Length + encryptedBytes.Length];
                Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
                Buffer.BlockCopy(encryptedBytes, 0, result, aes.IV.Length, encryptedBytes.Length);
                return Convert.ToBase64String(result);
            }
        }
    }

    // Tarihi çözmek için metot
    public string Coz(string sifreliVeri, string anahtar)
    {
        byte[] sifreliVeriBytes = Convert.FromBase64String(sifreliVeri);
        byte[] anahtarBytes = Encoding.UTF8.GetBytes(anahtar);
        byte[] iv = new byte[16];
        byte[] encryptedBytes = new byte[sifreliVeriBytes.Length - iv.Length];
        Buffer.BlockCopy(sifreliVeriBytes, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(sifreliVeriBytes, iv.Length, encryptedBytes, 0, encryptedBytes.Length);
        using (Aes aes = Aes.Create())
        {
            using (ICryptoTransform decryptor = aes.CreateDecryptor(anahtarBytes, iv))
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }

    // Girilen tarihi decrypt eden metot
    public string Decrypt(string input)
    {
        string result = "";
        int i = 0;

        while (i < input.Length)
        {
            foreach (var pair in kriptoSozluk)
            {
                if (input.Substring(i).StartsWith(pair.Value))
                {
                    result += pair.Key;
                    i += pair.Value.Length;
                    break;
                }
            }
        }

        // Tarihi YYGGAAYY formatından GG/MM/YYYY formatına çevirme
        string decryptedDate = $"{result[2]}{result[3]}/{result[4]}{result[5]}/{result[6]}{result[7]}{result[0]}{result[1]}";

        return decryptedDate;
    }

    // Google'dan tarih bilgisi çeken metot
    public async Task<DateTime?> GoogleTarihGetirAsync()
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                // Google'ın ana sayfasına bir GET isteği gönder
                HttpResponseMessage response = await client.GetAsync("https://www.google.com");
                response.EnsureSuccessStatusCode();

                // Gelen yanıtın tarih başlığını al
                if (response.Headers.Date.HasValue)
                {
                    return response.Headers.Date.Value.UtcDateTime.Date; // UTC tarihi al ve sadece günü döndür
                }
            }
        }
        catch
        {
            // Eğer hata olursa null döndür
        }

        return null;
    }

    // Lisans süresini bir dosya ile güncelleyen metot
    public bool LisansEkle(string sifreliVeri, string anahtar)
    {
        try
        {
            // Tarihi çöz ve DateTime'e dönüştür
            string tarihString = Coz(sifreliVeri, anahtar);
            DateTime tarih = DateTime.ParseExact(tarihString, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            // Lisans bitiş tarihini güncelle
            LisansBitisTarihiniGuncelle(tarih);

            return true; // Başarılı
        }
        catch
        {
            // Eğer hata olursa false döndür
        }

        return false; // Geçersiz dosya veya tarih
    }

    // Lisansın geçerli olup olmadığını kontrol eden metot
    public bool LisansGecerliMi(DateTime bugun)
    {
        return bugun <= LisansBitisTarihi; // Bugün, lisans bitiş tarihinden küçük veya eşitse lisans geçerli
    }
}
