using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using SFMS.Entity;



namespace IMSRepository
{
    public class DataContext : DbContext
    {
   
        public DbSet<SalesOrder> SalesOrder { get; set; }
        public DbSet<PaymentReceive> PaymentReceive { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetail { get; set; }
        public DbSet<WareHouse> WareHouse { get; set; }
        public DbSet<ProductWarehouseMap> ProductWarehouseMap { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        public DbSet<LookUp> LookUp { get; set; }
        public DbSet<EmailTemplate> EmailTemplate { get; set; }
        public DbSet<EmailHistory> EmailHistory { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }
        public DbSet<Users> Users { get; set; }
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
