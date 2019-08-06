using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using SFMS.Entity;



namespace IMS.Repository
{
    public class DataContext : DbContext
    {
        public DbSet<UserCarMap> UserCarMap { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Drivers> Drivers { get; set; }
        public DbSet<Concerns> Concerns { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<UserDriverMap> UserDriverMap { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<FuelBill> FuelBill { get; set; }
        public DbSet<LookUp> LookUp { get; set; }
        public DbSet<EmailTemplate> EmailTemplate { get; set; }
        public DbSet<EmailHistory> EmailHistory { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }

        private DataContext() { }
        public static DataContext context = null;
        public static DataContext getInstance()
        {
            if (context == null)
            {
                context = new DataContext();
                return context;
            }
            return context;
        }
    }
}
