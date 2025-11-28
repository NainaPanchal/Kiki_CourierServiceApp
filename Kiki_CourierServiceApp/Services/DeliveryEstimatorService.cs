using System.Collections.Generic;
using System.Linq;
using Kiki_CourierServiceApp.Models;

namespace Kiki_CourierServiceApp.Services
{
    public class DeliveryEstimatorService
    {
        public void EstimateDeliveryTimes(List<Package> packages, List<Vehicle> vehicles)
        {
            if (packages == null || packages.Count == 0 || vehicles == null || vehicles.Count == 0 || vehicles.Any(v => v.MaxSpeed <= 0))
                return;

            // Sort packages so that heavier/farther ones are picked first
            var remaining = packages
                .OrderByDescending(p => p.Distance)
                .ThenByDescending(p => p.Weight)
                .ToList();

            while (remaining.Any())
            {
                // pick earliest available vehicle
                var vehicle = vehicles.OrderBy(v => v.AvailableAt).First();
                double currentWeight = 0;
                var shipment = new List<Package>();

                // greedily fill this trip
                foreach (var pkg in remaining.ToList())
                {
                    if (currentWeight + pkg.Weight <= vehicle.MaxCarriableWeight)
                    {
                        shipment.Add(pkg);
                        currentWeight += pkg.Weight;
                    }
                }

                if (!shipment.Any())
                    break;

                double maxDistance = shipment.Max(p => p.Distance);

                // Assign delivery time per package
                foreach (var pkg in shipment)
                {
                    pkg.EstimatedDeliveryTime = Math.Round((vehicle.AvailableAt + (pkg.Distance / vehicle.MaxSpeed)) , 2,MidpointRounding.ToZero);
                }

                // Update vehicle availability after returning from farthest
                vehicle.AvailableAt += Math.Round(2 * (maxDistance / vehicle.MaxSpeed), 2, MidpointRounding.ToZero);
                // Remove delivered packages
                foreach (var pkg in shipment)
                    remaining.Remove(pkg);
            }
        }
    }
}
