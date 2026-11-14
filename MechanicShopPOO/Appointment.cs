namespace MechanicShopPOO
{
    public class Appointment
    {
        public Appointment()
        {
        }

        public Appointment(int customerId, int vehicleId, string date, string time, string service)
        {
            CustomerId = customerId;
            VehicleId = vehicleId;
            Date = date;
            Time = time;
            Service = service;
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string Service { get; set; } = string.Empty;

        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }

        public string Schedule => $"{Date} at {Time}";

        public void ChangeSchedule(string newDate, string newTime)
        {
            Date = newDate;
            Time = newTime;
        }
    }
}
