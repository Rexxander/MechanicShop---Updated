using Microsoft.EntityFrameworkCore;

namespace MechanicShopPOO
{
    public static class AppointmentHelper
    {
        public static void CreateAppointment(MechanicShopContext db)
        {
            int customerId = InputHelper.ReadNumber("Type the ID of the customer: ");

            if (!db.Customers.Any(c => c.Id == customerId))
            {
                Console.WriteLine("Customer not found!");
                return;
            }

            int vehicleId = InputHelper.ReadNumber("Type the ID of the vehicle: ");
            var vehicle = db.Vehicles.FirstOrDefault(v => v.Id == vehicleId);

            if (vehicle == null)
            {
                Console.WriteLine("Vehicle not found!");
                return;
            }

            if (vehicle.CustomerId != customerId)
            {
                Console.WriteLine("That vehicle does not belong to this customer.");
                return;
            }

            string date = InputHelper.ReadText("Date (dd/mm/yyyy): ");
            string time = InputHelper.ReadText("Time (hh:mm): ");

            if (db.Appointments.Any(a => a.Date == date && a.Time == time))
            {
                Console.WriteLine("There is already an appointment on that date and time.");
                return;
            }

            string service = InputHelper.ReadText("Service (e.g. oil change, brake check): ");

            Appointment appointment = new Appointment(customerId, vehicleId, date, time, service);
            db.Appointments.Add(appointment);
            db.SaveChanges();

            Console.WriteLine($"Appointment created successfully with ID {appointment.Id}.");
        }

        public static void SearchAppointments(MechanicShopContext db)
        {
            Console.WriteLine("Search by: 1. Customer name, 2. License plate, 3. Date");
            int criteria = InputHelper.ReadNumber("Choose an option: ");

            if (criteria < 1 || criteria > 3)
            {
                Console.WriteLine("Invalid search option.");
                return;
            }

            string searchTerm = InputHelper.ReadText("Type the text to search: ");
            List<Appointment> results;

            if (criteria == 1)
            {
                results = db.Appointments.Include(a => a.Customer).Include(a => a.Vehicle)
                    .Where(a => a.Customer.Name.Contains(searchTerm)).ToList();
            }
            else if (criteria == 2)
            {
                results = db.Appointments.Include(a => a.Customer).Include(a => a.Vehicle)
                    .Where(a => a.Vehicle.Plate.Contains(searchTerm)).ToList();
            }
            else
            {
                results = db.Appointments.Include(a => a.Customer).Include(a => a.Vehicle)
                    .Where(a => a.Date.Contains(searchTerm)).ToList();
            }

            if (results.Count == 0)
            {
                Console.WriteLine("No appointments found.");
                return;
            }

            PrintAppointments(results);
        }

        public static void ChangeAppointmentSchedule(MechanicShopContext db)
        {
            int id = InputHelper.ReadNumber("Type the ID of the appointment to change: ");
            var appointment = db.Appointments.FirstOrDefault(a => a.Id == id);

            if (appointment == null)
            {
                Console.WriteLine("Appointment not found!");
                return;
            }

            Console.WriteLine($"Current schedule: {appointment.Schedule}");
            string newDate = InputHelper.ReadText("New date (dd/mm/yyyy): ");
            string newTime = InputHelper.ReadText("New time (hh:mm): ");

            if (db.Appointments.Any(a => a.Date == newDate && a.Time == newTime && a.Id != id))
            {
                Console.WriteLine("There is already another appointment on that date and time.");
                return;
            }

            appointment.ChangeSchedule(newDate, newTime);
            appointment.Service = InputHelper.ReadText($"Service (current: {appointment.Service}): ");
            db.SaveChanges();

            Console.WriteLine("Appointment updated successfully.");
        }

        public static void CancelAppointment(MechanicShopContext db)
        {
            int id = InputHelper.ReadNumber("Type the ID of the appointment to cancel: ");
            var appointment = db.Appointments.FirstOrDefault(a => a.Id == id);

            if (appointment == null)
            {
                Console.WriteLine("Appointment not found!");
                return;
            }

            if (InputHelper.AskYesNo($"Cancel the appointment of {appointment.Schedule}? (yes/no): "))
            {
                db.Appointments.Remove(appointment);
                db.SaveChanges();
                Console.WriteLine("Appointment cancelled successfully.");
            }
            else
            {
                Console.WriteLine("The appointment was not cancelled.");
            }
        }

        public static void ListAppointments(MechanicShopContext db)
        {
            List<Appointment> appointments = db.Appointments.Include(a => a.Customer).Include(a => a.Vehicle).ToList();

            if (appointments.Count == 0)
            {
                Console.WriteLine("No appointments registered.");
                return;
            }

            PrintAppointments(appointments);
        }

        private static void PrintAppointments(List<Appointment> appointments)
        {
            Console.WriteLine($"\n{"ID",-5}{"Customer",-20}{"Plate",-12}{"Date",-13}{"Time",-8}{"Service",-25}");

            foreach (Appointment appointment in appointments)
            {
                Console.WriteLine($"{appointment.Id,-5}{appointment.Customer.Name,-20}{appointment.Vehicle.Plate,-12}{appointment.Date,-13}{appointment.Time,-8}{appointment.Service,-25}");
            }
        }
    }
}
