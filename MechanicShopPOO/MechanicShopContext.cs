using Microsoft.EntityFrameworkCore;

namespace MechanicShopPOO
{
    public class MechanicShopContext : DbContext
    {
        private const string CONNECTION_STRING = "server=localhost;port=3306;database=mechanic_shop;user=root;password=Letrazasa559$";

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(CONNECTION_STRING);
        }
    }
}
