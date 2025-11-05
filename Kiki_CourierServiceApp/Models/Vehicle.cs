namespace Kiki_CourierServiceApp.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public double MaxSpeed { get; set; }
        public double MaxCarriableWeight { get; set; }
        public double AvailableAt { get; set; } = 0.0;

        //Vehicle details can be extended as needed e.g number, registration etc.
    }
}
