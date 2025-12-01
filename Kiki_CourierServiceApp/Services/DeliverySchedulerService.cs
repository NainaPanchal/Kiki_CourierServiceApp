using System.Collections.Generic;
using System.Linq;
using Kiki_CourierServiceApp.Models;

namespace Kiki_CourierServiceApp.Services
{
    public class DeliverySchedulerService
    {
        public void EstimateDeliveryTimes(List<Package> packages, List<Vehicle> vehicles)
        {
            if (packages == null || packages.Count == 0 || vehicles == null || vehicles.Count == 0 || vehicles.Any(v => v.MaxSpeed <= 0))
                return;

            // Sort packages so that heavier/farther ones are picked first
            var remaining_pkg = packages .OrderByDescending(p => p.Distance) .ThenByDescending(p => p.Weight).ToList();
               

            while (remaining_pkg.Any())
            {
                // pick earliest available vehicle
                var vehicle = vehicles.OrderBy(v => v.AvailableAt).First();
                double currentWeight = 0;
                var shipment = new List<Package>();

                // greedily fill this trip
                foreach (var pkg in remaining_pkg.OrderByDescending(p => p.Distance).ThenByDescending(p => p.Weight))
                {
                    if (currentWeight + pkg.Weight <= vehicle.MaxCarriableWeight)
                    {
                        shipment.Add(pkg);
                        currentWeight += pkg.Weight;
                    }
                }

                if (!shipment.Any()) break; // no package can be assigned to this vehicle

                // Assign delivery time per package and Remove delivered packages
                foreach (var pkg in shipment)
                {
                    pkg.EstimatedDeliveryTime = Math.Round((vehicle.AvailableAt + (pkg.Distance / vehicle.MaxSpeed)) , 2,MidpointRounding.ToZero);
                    remaining_pkg.Remove(pkg);
                }

                // Update vehicle availability after returning from farthest
                double maxDistance = shipment.Max(p => p.Distance);
                vehicle.AvailableAt += Math.Round(2 * (maxDistance / vehicle.MaxSpeed), 2, MidpointRounding.ToZero);
               
            }
        }
   
   public void EstimateDeliveryTimes_2(List<Package> packages, List<Vehicle> vehicles)
        {

            int TotalPackages = packages.Count;
            int TotalVehicles = vehicles.Count;
            double MaxSpeed = vehicles[0].MaxSpeed;
            double MaxCarriableWeight = vehicles[0].MaxCarriableWeight;

            int pkg_Remaining = TotalPackages;
            int vehicle_available = TotalVehicles;
            double currentTime = 0.0;
            
           foreach(var pkg in packages)
            {
                pkg.EstimatedDeliveryTime = 0.0;
            }
            
            
            
            
            Dictionary<Vehicle, double> vehicleAvailability = vehicles.ToDictionary(v => v, v => 0.0);
            
            if (packages == null || packages.Count == 0 || vehicles == null || vehicles.Count == 0) return;

            
           
            foreach (var pkg in packages.OrderBy(p => p.Distance))
            {
                var availableVehicle = vehicleAvailability.OrderBy(v => v.Value).First().Key;
                currentTime = vehicleAvailability[availableVehicle];

                double travelTime = (pkg.Distance / availableVehicle.MaxSpeed) * 2; // round trip
                double deliveryTime = currentTime + (pkg.Distance / availableVehicle.MaxSpeed);

                pkg.EstimatedDeliveryTime = deliveryTime;

                vehicleAvailability[availableVehicle] = currentTime + travelTime;
            }
        }
    }

     
}
