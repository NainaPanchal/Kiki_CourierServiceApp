using System.Collections.Generic;
using Kiki_CourierServiceApp.Models;
using Kiki_CourierServiceApp.Services;
using Xunit;

namespace Kiki_CourierServiceApp.Tests
{
    public class DeliveryEstimatorServiceTests
    {
        private readonly DeliveryEstimatorService _service = new();

        [Fact]
        public void Should_Assign_Delivery_Times_To_All_Valid_Packages()
        {
            var packages = new List<Package>
            {
                new Package { Id = "PKG1", Weight = 50, Distance = 30 },
                new Package { Id = "PKG2", Weight = 75, Distance = 125 },
                new Package { Id = "PKG3", Weight = 175, Distance = 100 }
            };

            var vehicles = new List<Vehicle>
            {
                new Vehicle { Id = 1, MaxSpeed = 70, MaxCarriableWeight = 200 },
                new Vehicle { Id = 2, MaxSpeed = 70, MaxCarriableWeight = 200 }
            };

            _service.EstimateDeliveryTimes(packages, vehicles);

            Assert.All(packages, p => Assert.True(p.EstimatedDeliveryTime > 0));
        }

        [Fact]
        public void Should_Respect_MaxWeight_Limit()
        {
            var packages = new List<Package>
            {
                new Package { Id = "PKG_OVER", Weight = 500, Distance = 50 }
            };

            var vehicles = new List<Vehicle>
            {
                new Vehicle { Id = 1, MaxSpeed = 70, MaxCarriableWeight = 200 }
            };

            _service.EstimateDeliveryTimes(packages, vehicles);

            Assert.Equal(-1, packages[0].EstimatedDeliveryTime);
        }

        [Fact]
        public void Should_Handle_Zero_Speed_Vehicle()
        {
            var packages = new List<Package>
            {
                new Package { Id = "PKG1", Weight = 10, Distance = 10 }
            };

            var vehicles = new List<Vehicle>
            {
                new Vehicle { Id = 1, MaxSpeed = 0, MaxCarriableWeight = 100 }
            };

            _service.EstimateDeliveryTimes(packages, vehicles);

            Assert.Equal(-1, packages[0].EstimatedDeliveryTime);
        }
    }
}
