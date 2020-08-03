using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MarketMVC.Data
{
    public class MarketMVCContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MarketMVCContext() : base("name=MarketMVCContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) 
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//isto e para desactivar a regra casacate deleting rule
            //Desactiva a convencao/regra
        }

        public System.Data.Entity.DbSet<MarketMVC.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<MarketMVC.Models.DocumentType> DocumentTypes { get; set; }

        public System.Data.Entity.DbSet<MarketMVC.Models.Emplyee> Emplyees { get; set; }

        public System.Data.Entity.DbSet<MarketMVC.Models.Supplier> Suppliers { get; set; }

        public System.Data.Entity.DbSet<MarketMVC.Models.Customer> Customers { get; set; }
    }
}
