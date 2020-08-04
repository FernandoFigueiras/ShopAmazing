using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using ShopAmazing.Web.Data.Entities;
using ShopAmazing.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;//usamos este porque fizemos um bipass ao UserManager
        //private readonly UserManager<User> _userManager;
        private Random _random;

        public SeedDb(DataContext context, /*UserManager<User> userManager*/ IUserHelper userHelper)//injectar o data context - O User Manager e o objecto do core que e usado para tratar de toda a gestao dos users
        {
            _context = context;
            _userHelper = userHelper;
          /*  _userManager = userManager;*///Toda a gestao de criacao, alteracao e manutencao dos Users e gerida por esta classe UserManager
            _random = new Random();
        }


        public async Task SeedAsync()
        {    //vamos ver se a basa de dados esta criada
            await _context.Database.EnsureCreatedAsync();


            //fazer os roles com metodo criado por nos para fazer o bypass //Quem estipula os Roles somos nos
            await _userHelper.CheckRoleAsync("Admin");//verificar se ja existem os dois roles
            await _userHelper.CheckRoleAsync("Customer");



            if (!_context.Countries.Any())
            {
                var cities = new List<City>();

                cities.Add(new City { Name = "Lisboa" });
                cities.Add(new City { Name = "Porto" });
                cities.Add(new City { Name = "Faro" });
                cities.Add(new City { Name = "Coimbra" });

                _context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Portugal"
                });


                await _context.SaveChangesAsync();
            }


            //antes de inserir prtodutos temos de ver se ja ha user e se nao criamos
            //var user = await _userManager.FindByEmailAsync("fjfigdev@gmail.com");
            var user = await _userHelper.GetUserByEmailAsync("fjfigdev@gmail.com");

            if (user == null)
            {

                user = new User
                {
                    FirstName = "Fernando",
                    LastName = "Figueiras",
                    Email = "fjfigdev@gmail.com",
                    UserName = "fjfigdev@gmail.com",//podemos preencher mais campos
                    Address = "Rua Maravilha 2Dto",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault(),
                };

                //agora mandamos o UserManager Criar o User.

                /* var result = await _userManager.CreateAsync(user, "123456");*///cria um user com estes dados e esta password na tabela da bd
                var result = await _userHelper.AddUserAsync(user, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("The User could not be created in seeder");
                }
            }


            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");


            //cria o tokn e confirma
            var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            await _userHelper.ConfirmEmailAsync(user, token);


            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }


            if (!_context.Products.Any())
            {
                this.AddProduct("Produto Ficticio1", user);
                this.AddProduct("Produto Ficticio2", user);
                this.AddProduct("Produto Ficticio3", user);
                this.AddProduct("Produto Ficticio4", user);
                this.AddProduct("Produto Ficticio5", user);
                this.AddProduct("Produto Ficticio6", user);
                //depois de colocar novos produtos na memoria temos de gravar
                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {

            //Isto coloca o produto em memoria
            _context.Add(new Product
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvailable = true,
                Stock = _random.Next(100),
                User = user,
            });
        }
    }
}
