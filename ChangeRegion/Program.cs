using Microsoft.Win32;
using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Set Windows Region to English (United States) ===");

        try
        {
            SetUserLocale();
            SetSystemLocale();
            SetGeoId();
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: " + ex.Message);
        }

        Console.WriteLine("\nDONE! Please restart Windows to apply changes.");
        Console.ReadLine();
    }

    // ----- 1. USER LOCALE (format, region, dates, numbers...) -----
    static void SetUserLocale()
    {
        Console.WriteLine("Updating user locale...");

        using (var key = Registry.CurrentUser.OpenSubKey(
            @"Control Panel\International", true))
        {
            key.SetValue("LocaleName", "en-US");
            key.SetValue("Locale", "00000409");         // US code
            key.SetValue("sCountry", "United States");
            key.SetValue("sLanguage", "ENU");
            key.SetValue("iCountry", "1");
            key.SetValue("iCalendarType", "1");
        }

        Console.WriteLine("User locale set to en-US");
    }

    // ----- 2. SYSTEM LOCALE (Unicode for non-Unicode apps) -----
    static void SetSystemLocale()
    {
        Console.WriteLine("Updating system locale (non-Unicode apps)...");

        using (var key = Registry.LocalMachine.OpenSubKey(
            @"SYSTEM\CurrentControlSet\Control\Nls\Language", true))
        {
            key.SetValue("Default", "0409");  // en-US
        }

        Console.WriteLine("System locale set to en-US");
    }

    // ----- 3. GEO ID (region in Settings) -----
    static void SetGeoId()
    {
        Console.WriteLine("Updating GEO ID...");

        // 244 = US
        using (var key = Registry.CurrentUser.OpenSubKey(
            @"Control Panel\International\Geo", true))
        {
            key.SetValue("Nation", "244");
            key.SetValue("Name", "US");
        }

        Console.WriteLine("Geo ID set to United States");
    }
}
