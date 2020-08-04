using Microsoft.EntityFrameworkCore;
using ShopAmazing.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity //uma classe generica de T com os metodos do nterface em que todos os T que sao classes implementam a IEntity, dai ele saber quais as classes que implementam
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)//injecta o data context
        {
            _context = context;
        }



        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveAllAsync();//Metodo que criamos so aqui para gravar as entidades na BD
        }

        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;//devolve se faz pelo menos uma gravacao an bd
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>()
                .Remove(entity);
            await SaveAllAsync();
        }


        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<T>()
                .AllAsync(e => e.Id == id);//retorna o bool caso exista ou nao.
        }


        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();//este Set<> e como o db set do contrutor da datacontext e o AsNoTracking devolve  o conjunto de registos mas nao os grava em memoria porque nao sao para ser guardados na base de dados.
            //O Set<T> e porque nao estamos a dizer qual a entidade que esta a passar, sao todas as T que implementam a IEntity
        }


        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(e => e.Id == id);
        }


        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>()
               .Update(entity);
            await SaveAllAsync();
        }
    }
}
