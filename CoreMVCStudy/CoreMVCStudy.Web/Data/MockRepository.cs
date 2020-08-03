using CoreMVCStudy.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreMVCStudy.Web.Data
{
    public class MockRepository : IRepository
    {
        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>();
            products.Add(new Product
            {
                Id = 1,
                Name = "Um",
                Price = 10,
                Stock = 20
            });
            products.Add(new Product
            {
                Id = 2,
                Name = "Dois",
                Price = 20,
                Stock = 20
            });
            products.Add(new Product
            {
                Id = 3,
                Name = "Tres",
                Price = 30,
                Stock = 20
            });
            products.Add(new Product
            {
                Id = 4,
                Name = "Quatro",
                Price = 40,
                Stock = 20
            });
            products.Add(new Product
            {
                Id = 5,
                Name = "Cinco",
                Price = 50,
                Stock = 20
            });
            return products;
        }

        public bool ProductExists(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAllASync()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
