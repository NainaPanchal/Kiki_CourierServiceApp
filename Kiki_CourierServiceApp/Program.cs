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
            Console.WriteLine("Paste input lines (end after the vehicles line). Example: see sample input in project README.");
            app.Run();
        }
    }
}
