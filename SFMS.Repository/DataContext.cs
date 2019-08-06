using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using SFMS.Entity;



namespace SFMS.Repository
{
    public class DataContext : DbContext
    {
        public DbSet<PurchaseOrderDetail> UserCarMap { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<SalesOrder> Drivers { get; set; }
        public DbSet<PaymentReceive> Concerns { get; set; }
        public DbSet<SalesOrderDetail> Documents { get; set; }
        public DbSet<WareHouse> UserDriverMap { get; set; }
        public DbSet<Product> Car { get; set; }
        public DbSet<PurchaseOrder> FuelBill { get; set; }
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
