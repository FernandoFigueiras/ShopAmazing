using Microsoft.CodeAnalysis.Operations;
using ShopAmazing.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private Random _random;

        public SeedDb(DataContext context)//injectar o data context
        {
            _context = context;
            _random = new Random();
        }


        public async Task SeedAsync()
        {    //vamos ver se a basa de dados esta criada
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Products.Any())
            {
                this.AddProduct("Produto Ficticio1");
                this.AddProduct("Produto Ficticio2");
                this.AddProduct("Produto Ficticio3");
                this.AddProduct("Produto Ficticio4");
                this.AddProduct("Produto Ficticio5");
                this.AddProduct("Produto Ficticio6");
                //depois de colocar novos produtos na memoria temos de gravar
                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name)
        {

            //Isto coloca o produto em memoria
            _context.Add(new Product
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvailable = true,
                Stock = _random.Next(100)
            });
        }
    }
}
