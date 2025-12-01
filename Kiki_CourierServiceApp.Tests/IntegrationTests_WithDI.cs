using System.Collections.Generic;
using Kiki_CourierServiceApp.Models;
using Kiki_CourierServiceApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Kiki_CourierServiceApp.Tests
{
    public class IntegrationTests_WithDI
    {
        private readonly ServiceProvider _serviceProvider;

        public IntegrationTests_WithDI()
        {
            var serviceCollection = new ServiceCollection()
                .AddSingleton<IOffersService, InMemoryOfferProvider>()
                .AddSingleton<DeliveryCostService>()
                .AddSingleton<DeliverySchedulerService>()
                .AddSingleton<CourierServiceApp>()
                .BuildServiceProvider();

            _serviceProvider = serviceCollection;
        }

        [Fact]
        public void Should_Inject_Services_And_Run_Combined_Logic()
        {
            var costService = _serviceProvider.GetRequiredService<DeliveryCostService>();
            var deliveryService = _serviceProvider.GetRequiredService<DeliverySchedulerService>();

            var packages = new List<Package>
            {
                new Package { Id = "PKG1", Weight = 100, Distance = 100, OfferCode = "OFR002" },
                new Package { Id = "PKG2", Weight = 50, Distance = 50, OfferCode = "NA" }
            };

            var vehicles = new List<Vehicle>
            {
                new Vehicle { Id = 1, MaxSpeed = 70, MaxCarriableWeight = 200 }
            };

            costService.ComputeCostWithOffers(packages, 100);
            deliveryService.EstimateDeliveryTimes(packages, vehicles);

            Assert.All(packages, p =>
            {
                Assert.True(p.TotalCost > 0);
            });
        }
    }
}
