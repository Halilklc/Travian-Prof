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

            // Rastgele offsetli uzak konuma git
            actions.MoveToElement(element, random.Next(-100, -50), random.Next(-50, 50)).Perform();
            Thread.Sleep(random.Next(300, 600));

            // Sonra yaklaşıp tekrar üzerine git
            actions.MoveToElement(element, random.Next(-10, 10), random.Next(-10, 10)).Perform();
            Thread.Sleep(random.Next(200, 400));

            // Tıkla
            actions.Click().Perform();
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

            int steps = random.Next(4, 8);
            int stepX = x / steps;
            int stepY = y / steps;

            for (int i = 0; i < steps; i++)
            {
                actions.MoveByOffset(stepX, stepY).Perform();
                Thread.Sleep(random.Next(100, 200));
            }

            Thread.Sleep(random.Next(300, 500));
            actions.Click().Perform();
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
            Thread.Sleep(random.Next(200, 500));
            actions.ContextClick().Perform();
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
            int steps = random.Next(4, 7);
            int stepX = x / steps;
            int stepY = y / steps;

            for (int i = 0; i < steps; i++)
            {
                actions.MoveByOffset(stepX, stepY).Perform();
                Thread.Sleep(random.Next(80, 150));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fareyi belirtilen konuma taşırken bir hata oluştu: {ex.Message}");
        }
    }
}
