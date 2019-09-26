using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.WEB.UI.Models
{
    public class DashboardViewModel
    {
        public int products_count { get; set; }
        public int warehouses_count { get; set; }
        public int users_count { get; set; }
        public double UnpaidInvoiceAmount { get; set; }
        public double PaidInvoiceAmount { get; set; }
        public List<Product> FinishedProducts { get; set; }
        public List<PaymentReceive> UnpaidInvoices { get; set; }
    }

    public class GraphData
    {
        public string y { get; set; }
        public double a { get; set; }
        public double b { get; set; }
    }
}