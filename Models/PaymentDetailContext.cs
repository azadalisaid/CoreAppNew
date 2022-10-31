using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class PaymentDetailContext : DbContext
    {
        public PaymentDetailContext(DbContextOptions<PaymentDetailContext> options) : base(options)
        {
        }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Menus> Menuss { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<QuantityType> QuantityTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
    }
}
