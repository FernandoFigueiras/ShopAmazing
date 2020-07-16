using Microsoft.EntityFrameworkCore;
using ShopAmazing.Web.Data.Entities;

namespace ShopAmazing.Web.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }//O Db Set para saber qual a entidade que deve ser alcancada
        public DataContext(DbContextOptions<DataContext> options) : base(options)//Isto sao as opcoes do EntityFramework
        {
        }
    }
}
