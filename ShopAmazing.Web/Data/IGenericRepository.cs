using System.Linq;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Data
{
    public interface IGenericRepository<T> where T : class //serve para qualquer classe, por isso e de tipo T em que T e classe
    {
        //Aqui vai ser implementado o generico do CRUD, as assinaturas dos metodos

        IQueryable<T> GetAll();//IQueryablee o mais generico das listas retorna um conjunto de registos do tipo T


        Task<T> GetByIdAsync(int id);//recebe um id e vai buscar um registo do tipo T


        Task CreateAsync(T entity);//recebe uma entidade T que neste caso e classe e vai passar para a BD


        Task UpdateAsync(T entity);


        Task DeleteAsync(T Entity);


        Task<bool> ExistsAsync(int id);//ve se uma entidade qq do tipo T existe pelo ID

        //aqui nao leva o save, porque este e o generico, o save vai ser implementado na class
    }
}
