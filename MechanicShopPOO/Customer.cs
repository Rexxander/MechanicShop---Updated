namespace MechanicShopPOO
{
    public class Customer
    {
        public Customer()
        {
        }

        public Customer(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
