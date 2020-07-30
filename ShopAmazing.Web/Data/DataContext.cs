using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopAmazing.Web.Data.Entities;
using System.Linq;

namespace ShopAmazing.Web.Data
{
    public class DataContext : /*DbContext*/IdentityDbContext<User>//injectamos o user porque estamos a extender a class IdentityUser, caso contrario nao era necessario
    {
        public DbSet<Product> Products { get; set; }//O Db Set para saber qual a entidade que deve ser alcancada


        public DbSet<Country> Countries { get; set; }//Um db set de cada entidade 


        public DataContext(DbContextOptions<DataContext> options) : base(options)//Isto sao as opcoes do EntityFramework
        {
        }


        //Isto altera as definicoes de criacao do modelo da base de dados./introduzir alguma coisa ao modelo sem ser pela entity framework.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");//Isto e para alterar as definicoes do valor do price
            //Isto deixa de dar o worning


            //Cascading Delete Rule
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())//buscar todas as chaves estrangeiras de todas as entidades que tenha
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade); //filtrar as chaves estrangeiras que tenham o comportamento de apagar

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;//para nao apagar as que tenham chaves estrangeiras associadas.
            }


            base.OnModelCreating(modelBuilder);
        }
    }
}
