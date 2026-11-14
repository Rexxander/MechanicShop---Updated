namespace MechanicShopPOO
{
    public class Vehicle
    {
        public Vehicle()
        {
        }

        public Vehicle(string plate, string brand, string model, int customerId)
        {
            Plate = plate;
            Brand = brand;
            Model = model;
            CustomerId = customerId;
        }

        public int Id { get; set; }
        public string Plate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public string Description => $"{Brand} {Model} ({Plate})";
    }
}
