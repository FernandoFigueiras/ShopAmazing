using Microsoft.AspNetCore.Mvc.Rendering;
using ShopAmazing.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ShopAmazing.Web.Data.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {

        //para devolver os produtos juntamente com o USER
        IQueryable GetAllWithUsers();//devolve uma query




        IEnumerable<SelectListItem> GetComboProducts();//Nao e necessario por os produtos porque esta implicito que sao produtos que vao popular a lista

    }
}
