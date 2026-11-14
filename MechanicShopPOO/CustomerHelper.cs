namespace MechanicShopPOO
{
    public static class CustomerHelper
    {
        public static void AddCustomer(MechanicShopContext db)
        {
            string name = InputHelper.ReadText("Customer name: ");
            string phone = InputHelper.ReadText("Phone number: ");

            Customer customer = new Customer(name, phone);
            db.Customers.Add(customer);
            db.SaveChanges();

            Console.WriteLine($"Customer added successfully with ID {customer.Id}.");
        }

        public static void SearchCustomers(MechanicShopContext db)
        {
            Console.WriteLine("Search by: 1. Name, 2. Phone");
            int criteria = InputHelper.ReadNumber("Choose an option: ");

            if (criteria != 1 && criteria != 2)
            {
                Console.WriteLine("Invalid search option.");
                return;
            }

            string searchTerm = InputHelper.ReadText("Type the text to search: ");

            List<Customer> results = criteria == 1
                ? db.Customers.Where(c => c.Name.Contains(searchTerm)).ToList()
                : db.Customers.Where(c => c.Phone.Contains(searchTerm)).ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No customers found.");
                return;
            }

            PrintCustomers(results);
        }

        public static void EditCustomer(MechanicShopContext db)
        {
            int id = InputHelper.ReadNumber("Type the ID of the customer to edit: ");
            var customer = db.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                Console.WriteLine("Customer not found!");
                return;
            }

            customer.Name = InputHelper.ReadText($"New name for {customer.Name}: ");
            customer.Phone = InputHelper.ReadText($"New phone (current: {customer.Phone}): ");
            db.SaveChanges();

            Console.WriteLine("Customer updated successfully.");
        }

        public static void DeleteCustomer(MechanicShopContext db)
        {
            int id = InputHelper.ReadNumber("Type the ID of the customer to delete: ");
            var customer = db.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                Console.WriteLine("Customer not found!");
                return;
            }

            if (db.Vehicles.Any(v => v.CustomerId == id))
            {
                Console.WriteLine("This customer has registered vehicles. Delete the vehicles first.");
                return;
            }

            if (InputHelper.AskYesNo($"Are you sure you want to delete {customer.Name}? (yes/no): "))
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
                Console.WriteLine("Customer deleted successfully.");
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }
        }

        public static void ListCustomers(MechanicShopContext db)
        {
            List<Customer> customers = db.Customers.ToList();

            if (customers.Count == 0)
            {
                Console.WriteLine("No customers registered.");
                return;
            }

            PrintCustomers(customers);
        }

        private static void PrintCustomers(List<Customer> customers)
        {
            Console.WriteLine($"\n{"ID",-5}{"Name",-25}{"Phone",-15}");

            foreach (Customer customer in customers)
            {
                Console.WriteLine($"{customer.Id,-5}{customer.Name,-25}{customer.Phone,-15}");
            }
        }
    }
}
