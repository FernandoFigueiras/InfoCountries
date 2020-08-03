using MarketMVC.Data;
using MarketMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketMVC.Helpers
{
    public class CombosHelper : IDisposable
    {
        private static MarketMVCContext db = new MarketMVCContext();

        public static List<DocumentType> GetDocumentTypes()
        {
            var documentTypes = db.DocumentTypes.ToList();
            documentTypes.Add(new DocumentType 
            { 
                DocumentTypeID =0,
                Description = "[Selecione um tipo de documento]"
            });

            return documentTypes.OrderBy(d => d.Description).ToList();
        }

        public static List<Customer> GetCustomersNames()
        {
            var Customers = db.Customers.ToList();
            Customers.Add(new Customer
            {
                CustomerID = 0,
                CustomerFirstName="[Selecione um cliente]"
            });

            return Customers.OrderBy(c => c.CustomerFullName).ToList();
        }

        public static List<Product> GetProducts()
        {
            var Products = db.Products.ToList();

            Products.Add(new Product
            {
                ProductID = 0,
                Description = "[Selecione um Produto]"
            });

            return Products.OrderBy(p => p.Description).ToList();
        }


        public void Dispose()
        {
            db.Dispose();
        }
    }
}