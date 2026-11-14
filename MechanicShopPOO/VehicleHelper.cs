using Microsoft.EntityFrameworkCore;

namespace MechanicShopPOO
{
    public static class VehicleHelper
    {
        public static void AddVehicle(MechanicShopContext db)
        {
            int customerId = InputHelper.ReadNumber("Type the ID of the owner (customer): ");

            if (!db.Customers.Any(c => c.Id == customerId))
            {
                Console.WriteLine("Customer not found! Register the customer first.");
                return;
            }

            string plate = InputHelper.ReadText("License plate: ");

            if (db.Vehicles.Any(v => v.Plate == plate))
            {
                Console.WriteLine("There is already a vehicle with that license plate.");
                return;
            }

            string brand = InputHelper.ReadText("Brand: ");
            string model = InputHelper.ReadText("Model: ");

            Vehicle vehicle = new Vehicle(plate, brand, model, customerId);
            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            Console.WriteLine($"Vehicle added successfully with ID {vehicle.Id}.");
        }

        public static void SearchVehicles(MechanicShopContext db)
        {
            Console.WriteLine("Search by: 1. License plate, 2. Brand, 3. Owner name");
            int criteria = InputHelper.ReadNumber("Choose an option: ");

            if (criteria < 1 || criteria > 3)
            {
                Console.WriteLine("Invalid search option.");
                return;
            }

            string searchTerm = InputHelper.ReadText("Type the text to search: ");
            List<Vehicle> results;

            if (criteria == 1)
            {
                results = db.Vehicles.Include(v => v.Customer).Where(v => v.Plate.Contains(searchTerm)).ToList();
            }
            else if (criteria == 2)
            {
                results = db.Vehicles.Include(v => v.Customer).Where(v => v.Brand.Contains(searchTerm)).ToList();
            }
            else
            {
                results = db.Vehicles.Include(v => v.Customer).Where(v => v.Customer.Name.Contains(searchTerm)).ToList();
            }

            if (results.Count == 0)
            {
                Console.WriteLine("No vehicles found.");
                return;
            }

            PrintVehicles(results);
        }

        public static void EditVehicle(MechanicShopContext db)
        {
            int id = InputHelper.ReadNumber("Type the ID of the vehicle to edit: ");
            var vehicle = db.Vehicles.FirstOrDefault(v => v.Id == id);

            if (vehicle == null)
            {
                Console.WriteLine("Vehicle not found!");
                return;
            }

            string newPlate = InputHelper.ReadText($"New license plate (current: {vehicle.Plate}): ");

            if (db.Vehicles.Any(v => v.Plate == newPlate && v.Id != id))
            {
                Console.WriteLine("Another vehicle already has that license plate.");
                return;
            }

            vehicle.Plate = newPlate;
            vehicle.Brand = InputHelper.ReadText($"New brand (current: {vehicle.Brand}): ");
            vehicle.Model = InputHelper.ReadText($"New model (current: {vehicle.Model}): ");
            db.SaveChanges();

            Console.WriteLine("Vehicle updated successfully.");
        }

        public static void DeleteVehicle(MechanicShopContext db)
        {
            int id = InputHelper.ReadNumber("Type the ID of the vehicle to delete: ");
            var vehicle = db.Vehicles.FirstOrDefault(v => v.Id == id);

            if (vehicle == null)
            {
                Console.WriteLine("Vehicle not found!");
                return;
            }

            if (db.Appointments.Any(a => a.VehicleId == id))
            {
                Console.WriteLine("This vehicle has appointments. Cancel the appointments first.");
                return;
            }

            if (InputHelper.AskYesNo($"Are you sure you want to delete the vehicle {vehicle.Description}? (yes/no): "))
            {
                db.Vehicles.Remove(vehicle);
                db.SaveChanges();
                Console.WriteLine("Vehicle deleted successfully.");
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }
        }

        public static void ListVehicles(MechanicShopContext db)
        {
            List<Vehicle> vehicles = db.Vehicles.Include(v => v.Customer).ToList();

            if (vehicles.Count == 0)
            {
                Console.WriteLine("No vehicles registered.");
                return;
            }

            PrintVehicles(vehicles);
        }

        private static void PrintVehicles(List<Vehicle> vehicles)
        {
            Console.WriteLine($"\n{"ID",-5}{"Plate",-12}{"Brand",-15}{"Model",-15}{"Owner",-25}");

            foreach (Vehicle vehicle in vehicles)
            {
                Console.WriteLine($"{vehicle.Id,-5}{vehicle.Plate,-12}{vehicle.Brand,-15}{vehicle.Model,-15}{vehicle.Customer.Name,-25}");
            }
        }
    }
}
