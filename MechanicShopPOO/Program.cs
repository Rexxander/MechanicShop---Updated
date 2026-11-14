using MechanicShopPOO;

MechanicShopContext db = new MechanicShopContext();

try
{
    db.Database.EnsureCreated();
}
catch (Exception ex)
{
    Console.WriteLine("Could not connect to the MySQL database.");
    Console.WriteLine("Check that MySQL is running and that the connection string in MechanicShopContext.cs is correct.");
    Console.WriteLine($"Details: {ex.Message}");
    return;
}

bool running = true;

Console.WriteLine("Mechanic Shop Appointment Control");

while (running)
{
    ShowMainMenu();

    try
    {
        int mainOption = Convert.ToInt32(Console.ReadLine());

        switch (mainOption)
        {
            case 1:
                CustomersMenu();
                break;
            case 2:
                VehiclesMenu();
                break;
            case 3:
                AppointmentsMenu();
                break;
            case 4:
                running = false;
                break;
            default:
                Console.WriteLine("Invalid option. Please choose a number from 1 to 4.");
                break;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Error: you must type a valid number.");
    }
    catch (OverflowException)
    {
        Console.WriteLine("Error: the number is too large.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected error: {ex.Message}");
    }

    if (running)
    {
        running = InputHelper.AskYesNo("\nDo you want to perform another operation? (yes/no): ");
    }
}

Console.WriteLine("\nClosing the app. Goodbye!");

void ShowMainMenu()
{
    Console.WriteLine("\nMAIN MENU");
    Console.WriteLine("1. Customers");
    Console.WriteLine("2. Vehicles");
    Console.WriteLine("3. Appointments");
    Console.WriteLine("4. Exit");
    Console.Write("Choose an option: ");
}

void CustomersMenu()
{
    Console.WriteLine("\nCUSTOMERS");
    Console.WriteLine("1. Add Customer\n2. Search Customer\n3. Edit Customer\n4. Delete Customer\n5. List Customers\n6. Back");
    int option = InputHelper.ReadNumber("Choose an option: ");

    switch (option)
    {
        case 1:
            CustomerHelper.AddCustomer(db);
            break;
        case 2:
            CustomerHelper.SearchCustomers(db);
            break;
        case 3:
            CustomerHelper.EditCustomer(db);
            break;
        case 4:
            CustomerHelper.DeleteCustomer(db);
            break;
        case 5:
            CustomerHelper.ListCustomers(db);
            break;
        case 6:
            break;
        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

void VehiclesMenu()
{
    Console.WriteLine("\nVEHICLES");
    Console.WriteLine("1. Add Vehicle\n2. Search Vehicle\n3. Edit Vehicle\n4. Delete Vehicle\n5. List Vehicles\n6. Back");
    int option = InputHelper.ReadNumber("Choose an option: ");

    switch (option)
    {
        case 1:
            VehicleHelper.AddVehicle(db);
            break;
        case 2:
            VehicleHelper.SearchVehicles(db);
            break;
        case 3:
            VehicleHelper.EditVehicle(db);
            break;
        case 4:
            VehicleHelper.DeleteVehicle(db);
            break;
        case 5:
            VehicleHelper.ListVehicles(db);
            break;
        case 6:
            break;
        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

void AppointmentsMenu()
{
    Console.WriteLine("\nAPPOINTMENTS");
    Console.WriteLine("1. Create Appointment\n2. Search Appointment\n3. Change Appointment Schedule\n4. Cancel Appointment\n5. List Appointments\n6. Back");
    int option = InputHelper.ReadNumber("Choose an option: ");

    switch (option)
    {
        case 1:
            AppointmentHelper.CreateAppointment(db);
            break;
        case 2:
            AppointmentHelper.SearchAppointments(db);
            break;
        case 3:
            AppointmentHelper.ChangeAppointmentSchedule(db);
            break;
        case 4:
            AppointmentHelper.CancelAppointment(db);
            break;
        case 5:
            AppointmentHelper.ListAppointments(db);
            break;
        case 6:
            break;
        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}
