using System.Collections.Generic;
using Kiki_CourierServiceApp.Models;
using Kiki_CourierServiceApp.Services;
using Xunit;

namespace Kiki_CourierServiceApp.Tests
{
    public class CostEstimationServiceTests
    {
        private readonly CostEstimationService _service = new();

        [Fact]
        public void Should_Apply_Valid_Offer_Discount()
        {
            var packages = new List<Package>
            {
                new Package { Id = "PKG1", Weight = 100, Distance = 100, OfferCode = "OFR002" }
            };

            _service.ComputeCostWithOffers(packages, 100);

            Assert.True(packages[0].Discount > 0);
        }

        [Fact]
        public void Should_Not_Apply_Invalid_Offer()
        {
            var packages = new List<Package>
            {
                new Package { Id = "PKG2", Weight = 5, Distance = 5, OfferCode = "OFR999" }
            };

            _service.ComputeCostWithOffers(packages, 100);

            Assert.Equal(0, packages[0].Discount);
        }

        [Fact]
        public void Should_Handle_Zero_Weight_And_Distance()
        {
            var packages = new List<Package>
            {
                new Package { Id = "PKG4", Weight = 0, Distance = 0, OfferCode = "OFR001" }
            };

            _service.ComputeCostWithOffers(packages, 100);

            Assert.Equal(100, packages[0].TotalCost);
        }

        [Fact]
        public void Should_Handle_Empty_Package_List()
        {
            var packages = new List<Package>();

            _service.ComputeCostWithOffers(packages, 100);

            Assert.Empty(packages);
        }
    }
}
