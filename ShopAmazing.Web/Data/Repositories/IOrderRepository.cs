using ShopAmazing.Web.Data.Entities;
using ShopAmazing.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Data.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {

        Task<IQueryable<Order>> GetOrdersAsync(string username);//vai servir para ir buscar as encomendas pelo username vai dar todas as encomendas de um user



        Task<IQueryable<OrderDetailTemp>> GetDetailTempsAsync(string username);



        Task AddItemToOrderAsync(AddItemViewModel model, string userName);



        Task ModifyOrderDetailTempQuantityAsync(int id, double quantity);
    }
}
