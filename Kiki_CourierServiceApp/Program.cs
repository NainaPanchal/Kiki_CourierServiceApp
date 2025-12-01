using System;
using Microsoft.Extensions.DependencyInjection;
using Kiki_CourierServiceApp.Services;

namespace Kiki_CourierServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IOffersService, InMemoryOfferProvider>()
                .AddSingleton<DeliveryCostService>()
                .AddSingleton<DeliverySchedulerService>()
                .AddSingleton<CourierServiceApp>()
                .BuildServiceProvider();

            var app = serviceProvider.GetRequiredService<CourierServiceApp>();
            ShowMenu(app);
        }

        static void ShowMenu(CourierServiceApp app)
        {
            string[] options =
            {
                "Calculate Delivery Cost Only",
                "Estimate Delivery Time Only",
                "Exit"
            };

            int selectedIndex = 0;
            ConsoleKey key;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Kiki Courier Service ===");
                Console.WriteLine("Use ↑ ↓ arrow keys. Press Enter to select.\n");

                // Draw menu
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"> {options[i]}");
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.WriteLine($"  {options[i]}");
                    }
                }

                Console.ResetColor();
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if (selectedIndex < 0)
                        selectedIndex = options.Length - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if (selectedIndex >= options.Length)
                        selectedIndex = 0;
                }
                else if (key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    HandleSelection(selectedIndex, app);
                    return;
                }
            }
        }

        static void HandleSelection(int index, CourierServiceApp app)
        {
           
            switch (index)
            {
                case 0:
                    Console.WriteLine("To Calculate Delivery Cost ::\nPaste input lines from sample input in README.md and press Enter.\n");
                    app.run(0);
                    break;

                case 1:
                    Console.WriteLine("To Estimate Delivery Time ::\nPaste input lines from sample input in README.md and press Enter.\n");
                    app.run(1);
                    break;

                case 2:
                    Console.WriteLine("Exiting application.");
                    return;
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
            ShowMenu(app);
        }
    }
}
