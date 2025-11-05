namespace Kiki_CourierServiceApp.Models
{
    public class Offer
    {
        public string Code { get; set; } = string.Empty;
        public double DiscountPercent { get; set; }
        public double MinWeight { get; set; }
        public double MaxWeight { get; set; }
        public double MinDistance { get; set; }
        public double MaxDistance { get; set; }

        public bool IsApplicable(Package pkg)
        {
            return pkg.Weight >= MinWeight && pkg.Weight <= MaxWeight &&
                   pkg.Distance >= MinDistance && pkg.Distance <= MaxDistance;
        }

        //Offer start and end dates can be added for further validation...
    }
}
