using System;

public interface IProgrammableSoftware
{
    string Name { get; set; }
    string Manufacturer { get; set; }

    void DisplayInfo();
    bool IsUsable(DateTime currentDate);
}

public class FreeSoftware : IProgrammableSoftware
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }

    public FreeSoftware(string name, string manufacturer)
    {
        Name = name;
        Manufacturer = manufacturer;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Free Software: {Name}, Manufacturer: {Manufacturer}");
    }

    public bool IsUsable(DateTime currentDate)
    {
        // Free software is always usable
        return true;
    }
}

public class Shareware : IProgrammableSoftware
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public DateTime InstallationDate { get; set; }
    public int FreeUsagePeriod { get; set; } // in days

    public Shareware(string name, string manufacturer, DateTime installationDate, int freeUsagePeriod)
    {
        Name = name;
        Manufacturer = manufacturer;
        InstallationDate = installationDate;
        FreeUsagePeriod = freeUsagePeriod;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Shareware: {Name}, Manufacturer: {Manufacturer}, Installation Date: {InstallationDate.ToShortDateString()}, Free Usage Period: {FreeUsagePeriod} days");
    }

    public bool IsUsable(DateTime currentDate)
    {
        return (currentDate - InstallationDate).TotalDays <= FreeUsagePeriod;
    }
}

public class CommercialSoftware : IProgrammableSoftware
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public decimal Price { get; set; }
    public DateTime InstallationDate { get; set; }
    public int UsagePeriod { get; set; } // in days

    public CommercialSoftware(string name, string manufacturer, decimal price, DateTime installationDate, int usagePeriod)
    {
        Name = name;
        Manufacturer = manufacturer;
        Price = price;
        InstallationDate = installationDate;
        UsagePeriod = usagePeriod;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Commercial Software: {Name}, Manufacturer: {Manufacturer}, Price: {Price:C}, Installation Date: {InstallationDate.ToShortDateString()}, Usage Period: {UsagePeriod} days");
    }

    public bool IsUsable(DateTime currentDate)
    {
        return (currentDate - InstallationDate).TotalDays <= UsagePeriod;
    }
}

class Program
{
    static void Main()
    {
        // Створення масиву програмного забезпечення
        IProgrammableSoftware[] softwareArray = new IProgrammableSoftware[]
        {
            new FreeSoftware("Linux", "Open Source Community"),
            new Shareware("WinRAR", "RARLAB", new DateTime(2024, 1, 1), 40),
            new CommercialSoftware("Microsoft Office", "Microsoft", 149.99m, new DateTime(2023, 6, 1), 365)
        };

        // Виведення інформації про все програмне забезпечення
        foreach (var software in softwareArray)
        {
            software.DisplayInfo();
            Console.WriteLine($"Usable: {software.IsUsable(DateTime.Now)}\n");
        }

        // Пошук програмного забезпечення, яке припустимо використовувати на поточну дату
        Console.WriteLine("Search for usable software on the current date:");
        foreach (var software in softwareArray)
        {
            if (software.IsUsable(DateTime.Now))
            {
                software.DisplayInfo();
            }
        }
    }
}
