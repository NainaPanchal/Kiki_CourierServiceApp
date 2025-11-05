namespace Kiki_CourierServiceApp.Models
{
    public class Package
    {
        public string Id { get; set; } = string.Empty;
        public double Weight { get; set; }
        public double Distance { get; set; }
        public string OfferCode { get; set; } = string.Empty;
        public double Discount { get; set; }
        public double TotalCost { get; set; }
        public double EstimatedDeliveryTime { get; set; } = -1;
    }
}
