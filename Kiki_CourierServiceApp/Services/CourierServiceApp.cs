using System;
using System.Collections.Generic;
using Kiki_CourierServiceApp.Models;

namespace Kiki_CourierServiceApp.Services
{
    public class CourierServiceApp
    {
        private readonly DeliveryCostService _costService;
        private readonly DeliverySchedulerService _deliveryService;

        private double BaseDeliveryCost;
        private readonly List<Package> Packages = new();
        private readonly List<Vehicle> Vehicles = new();

        public CourierServiceApp(DeliveryCostService costService, DeliverySchedulerService deliveryService)
        {
            _costService = costService;
            _deliveryService = deliveryService;
        }

        public void run(int selection_option)
        {
            ParseInput(selection_option);
            switch (selection_option)
            {
                case 0:
                    _costService.ComputeCostWithOffers(Packages, BaseDeliveryCost);
                     PrintDeliveryCost();
                    break;
                case 1:
                     _costService.ComputeCostWithOffers(Packages, BaseDeliveryCost);
                     _deliveryService.EstimateDeliveryTimes(Packages, Vehicles);
                    PrintDeliveryScheduler();
                    break;
                default:
                    Console.WriteLine("Exiting application.");
                    break;
            }  
        }
         

        private void ParseInput(int selection_option)
        {
            var firstLine = Console.ReadLine();

            if (firstLine == null || firstLine.Length < 2)
            {
                Console.WriteLine("Invalid input. Expected format: <base_cost> <no_of_packages>");
                return;
            }
            var firstParts = firstLine.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            BaseDeliveryCost = double.Parse(firstParts[0]);
            int pkgCount = int.Parse(firstParts[1]);

        #region  Input Validations
    
if (BaseDeliveryCost < 0)
{
    Console.WriteLine("Invalid base cost. Must be a positive number.");
    return;
}
if (pkgCount <= 0)
{
    Console.WriteLine("Invalid package count. Must be greater than 0.");
    return;
}

Packages.Clear();

#endregion
            for (int i = 0; i < pkgCount; i++)
            {
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) { Console.WriteLine($"Expected package line {i+1}."); return; }
                var parts = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);


#region package Input Validations   

                if (parts == null || parts.Length < 4)
                {
                    Console.WriteLine("Invalid input for package details.");
                    return;
                }
                double weight = double.Parse(parts[1]);
                double distance = double.Parse(parts[2]);
                if (weight <= 0)
                {
                Console.WriteLine($"Invalid weight for package {parts[0]}. Must be > 0.");
                return;
                }

                if (distance <= 0)
                {
                    Console.WriteLine($"Invalid distance for package {parts[0]}. Must be > 0.");
                    return;
                }
#endregion
               
               Packages.Add(new Package
                {
                    Id = parts[0],
                    Weight = weight,
                    Distance = distance,
                    OfferCode = parts[3]
                });
            }
if (selection_option == 1) // Only parse vehicles for delivery scheduler
{
            var vehicleLine = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(vehicleLine)) { Console.WriteLine("Expected vehicle line."); return; }
            var vparts = vehicleLine?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (vparts?.Length < 3) { Console.WriteLine("Vehicle line must contain number_of_vehicles max_speed max_carriable_weight."); return; }

            int vehicleCount = int.Parse(vparts[0]);
            double maxSpeed = double.Parse(vparts[1]);
            double maxWeight = double.Parse(vparts[2]);
            
        #region vehicle Input Validations  

 if (vehicleCount <= 0)
{
    Console.WriteLine("Invalid number of vehicles. Must be > 0.");
    return;
}

if (maxSpeed <= 0)
{
    Console.WriteLine("Invalid max speed. Must be > 0.");
    return;
}
if (maxWeight <= 0)
{
    Console.WriteLine("Invalid max carriable weight. Must be > 0.");
    return;
}
#endregion
            for (int i = 0; i < vehicleCount; i++)
            {
                Vehicles.Add(new Vehicle { Id = i + 1, MaxSpeed = maxSpeed, MaxCarriableWeight = maxWeight });
            }
        }       
}
        private void PrintDeliveryCost()
        {
            Console.WriteLine("==========================================");
             Console.WriteLine("OutPut: PackageID Discount TotalCost: \n");
            foreach (var pkg in Packages)
                Console.WriteLine($"{pkg.Id} {pkg.Discount:0} {pkg.TotalCost:0}");
    
        }
        private void PrintDeliveryScheduler()
        {
             Console.WriteLine("==========================================");
            Console.WriteLine("OutPut: PackageID Discount TotalCost EstimatedDeliveryTime \n");
            foreach (var pkg in Packages)
            {
                var timeStr = pkg.EstimatedDeliveryTime < 0 ? "NA" : Math.Round(pkg.EstimatedDeliveryTime, 2).ToString("0.00");
                Console.WriteLine($"{pkg.Id} {pkg.Discount:0} {pkg.TotalCost:0} {timeStr}");

            }
        }

        public List<Package> GetPackages() => Packages;
    }
}
