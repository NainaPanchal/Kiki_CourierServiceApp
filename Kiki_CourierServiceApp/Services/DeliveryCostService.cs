using System.Collections.Generic;
using System.Linq;
using Kiki_CourierServiceApp.Models;

namespace Kiki_CourierServiceApp.Services
{
    public class DeliveryCostService
    { 
        private readonly List<Offer> _offers;

        public DeliveryCostService(IOffersService offersService)
        {
            _offers = offersService.GetOffers();
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
