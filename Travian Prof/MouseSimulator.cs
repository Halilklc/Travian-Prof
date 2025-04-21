using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;

public class MouseSimulator
{
    private readonly IWebDriver driver;
    private readonly Random random = new Random();

    public MouseSimulator(IWebDriver driver)
    {
        this.driver = driver ?? throw new ArgumentNullException(nameof(driver), "Driver nesnesi null olamaz.");
    }

    public void SimulateMouseMovementAndClick(IWebElement element)
    {
        if (element == null)
            throw new ArgumentNullException(nameof(element), "Web öğesi null olamaz.");

        try
        {
            var actions = new Actions(driver);

            // Rastgele küçük bir hareket ile öğeye doğru yaklaş
            int offsetX = random.Next(-10, 10);
            int offsetY = random.Next(-10, 10);
            actions.MoveToElement(element, offsetX, offsetY).Perform();
            Thread.Sleep(random.Next(300, 600)); // Küçük gecikmeler

            // Öğeye tam olarak git ve bir tıkla
            offsetX = random.Next(-5, 5);
            offsetY = random.Next(-5, 5);
            actions.MoveToElement(element, offsetX, offsetY).Perform();
            Thread.Sleep(random.Next(200, 400)); // Doğal bekleme

            actions.Click().Perform(); // Tıklama
            Thread.Sleep(random.Next(800, 1500)); // Tıklama sonrası küçük bekleme
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fare simülasyonu sırasında bir hata oluştu: {ex.Message}");
        }
    }

    public void SimulateMouseMovementAndClick(int x, int y)
    {
        try
        {
            var actions = new Actions(driver);
            int steps = random.Next(4, 8); // 4-8 adım arasında hareket
            int stepX = x / steps;
            int stepY = y / steps;

            // Fareyi doğal bir şekilde kaydırmak için hareketleri rastgele yap
            for (int i = 0; i < steps; i++)
            {
                actions.MoveByOffset(stepX + random.Next(-5, 5), stepY + random.Next(-5, 5)).Perform();
                Thread.Sleep(random.Next(100, 200)); // Küçük bekleme
            }

            Thread.Sleep(random.Next(300, 500)); // Hareket tamamlandıktan sonra küçük bekleme
            actions.Click().Perform(); // Tıklama
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fare simülasyonu sırasında bir hata oluştu: {ex.Message}");
        }
    }

    public void SimulateRightClick(IWebElement element)
    {
        if (element == null)
            throw new ArgumentNullException(nameof(element), "Web öğesi null olamaz.");

        try
        {
            var actions = new Actions(driver);
            actions.MoveToElement(element, random.Next(-5, 5), random.Next(-5, 5)).Perform();
            Thread.Sleep(random.Next(200, 500)); // Sağ tıklama için bekleme
            actions.ContextClick().Perform(); // Sağ tıklama
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Sağ tıklama simülasyonu sırasında bir hata oluştu: {ex.Message}");
        }
    }

    public void MoveMouseToPosition(int x, int y)
    {
        try
        {
            var actions = new Actions(driver);
            int steps = random.Next(4, 7); // Hareket adımlarını belirle
            int stepX = x / steps;
            int stepY = y / steps;

            // Doğal hareket için adımları rastgele yap
            for (int i = 0; i < steps; i++)
            {
                actions.MoveByOffset(stepX + random.Next(-3, 3), stepY + random.Next(-3, 3)).Perform();
                Thread.Sleep(random.Next(80, 150)); // Küçük beklemeler
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fareyi belirtilen konuma taşırken bir hata oluştu: {ex.Message}");
        }
    }
}
