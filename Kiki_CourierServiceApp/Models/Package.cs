namespace Kiki_CourierServiceApp.Models
{
    public class Package
    {
        public string Id { get; set; } = string.Empty;
        public double Weight { get; set; }
        //public string WeightUnit { get; set; } = string.Empty;
        public double Distance { get; set; }
        //public string DistanceUnit { get; set; } = string.Empty;
        public string OfferCode { get; set; } = string.Empty;
        public double Discount { get; set; }
        public double TotalCost { get; set; }
        //public string DiscountUnit { get; set; } = string.Empty;
        public double EstimatedDeliveryTime { get; set; } = -1;
    }
}
