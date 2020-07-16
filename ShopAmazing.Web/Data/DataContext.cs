﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopAmazing.Web.Data.Entities;

namespace ShopAmazing.Web.Data
{
    public class DataContext : /*DbContext*/IdentityDbContext<User>//injectamos o user porque estamos a extender a class IdentityUser, caso contrario nao era necessario
    {
        public DbSet<Product> Products { get; set; }//O Db Set para saber qual a entidade que deve ser alcancada


        public DbSet<Country> Countries { get; set; }//Um db set de cada entidade 


        public DataContext(DbContextOptions<DataContext> options) : base(options)//Isto sao as opcoes do EntityFramework
        {
        }
    }
}
