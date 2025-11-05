using System.Collections.Generic;
using System.Linq;
using Kiki_CourierServiceApp.Models;

namespace Kiki_CourierServiceApp.Services
{
    public class CostEstimationService
    {
        private readonly List<Offer> _offers = new();

        public CostEstimationService()
        {
            _offers.Add(new Offer { Code = "OFR001", DiscountPercent = 10, MinWeight = 70, MaxWeight = 200, MinDistance = 0, MaxDistance = 200 });
            _offers.Add(new Offer { Code = "OFR002", DiscountPercent = 7, MinWeight = 100, MaxWeight = 250, MinDistance = 50, MaxDistance = 150 });
            _offers.Add(new Offer { Code = "OFR003", DiscountPercent = 5, MinWeight = 10, MaxWeight = 150, MinDistance = 50, MaxDistance = 250 });
        }

        public void ComputeCostWithOffers(List<Package> packages, double baseDeliveryCost)
        {
            if (packages == null || packages.Count == 0) return;

            foreach (var pkg in packages)
            {
                double cost = baseDeliveryCost + (pkg.Weight * 10) + (pkg.Distance * 5);
                pkg.Discount = GetDiscount(pkg, cost);
                pkg.TotalCost = cost - pkg.Discount;
            }
        }

        private double GetDiscount(Package pkg, double deliveryCost)
        {
            if (pkg == null) return 0.0;
            var offer = _offers.FirstOrDefault(o => o.Code == pkg.OfferCode);
            if (offer != null && offer.IsApplicable(pkg))
                return deliveryCost * (offer.DiscountPercent / 100.0);
            return 0.0;
        }
    }
}
