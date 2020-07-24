using ShopAmazing.Web.Data.Entities;
using System.Linq;

namespace ShopAmazing.Web.Data
{
    public interface IProductRepository : IGenericRepository<Product>
    {

        //para devolver os produtos juntamente com o USER
        IQueryable GetAllWithUsers();//devolve uma query
    }
}
