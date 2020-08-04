using ShopAmazing.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Data.Repositories
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

            products.Add(new Product { Id = 1, Name = "Mockum", Price = 10 });
            products.Add(new Product { Id = 2, Name = "Mockdois", Price = 20 });
            products.Add(new Product { Id = 3, Name = "Mocktres", Price = 30 });
            products.Add(new Product { Id = 4, Name = "Mockquatro", Price = 40 });
            products.Add(new Product { Id = 5, Name = "Mockcinco", Price = 50 });

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

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
