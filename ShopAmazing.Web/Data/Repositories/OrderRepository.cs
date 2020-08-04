using Microsoft.EntityFrameworkCore;
using ShopAmazing.Web.Data.Entities;
using ShopAmazing.Web.Helpers;
using ShopAmazing.Web.Models;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public OrderRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }




        public async Task AddItemToOrderAsync(AddItemViewModel model, string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);

            if (user == null)
            {
                return;
            }

            var product = await _context.Products.FindAsync(model.ProductId);


            if (product == null)
            {
                return;
            }


            var orderDetailTemp = await _context.OrderDetailsTemp
                .Where(odt => odt.User == user && odt.Product == product)
                .FirstOrDefaultAsync();//primeiro produto de o user que la esta


            if (orderDetailTemp == null)
            {
                orderDetailTemp = new OrderDetailTemp
                {
                    Price = product.Price,
                    Product = product,
                    Quantity = model.Quantity,
                    User = user
                };


                _context.OrderDetailsTemp.Add(orderDetailTemp);
            }
            else
            {
                orderDetailTemp.Quantity += model.Quantity;//se ja existir produto aumenta apenas a quantidade
                _context.OrderDetailsTemp.Update(orderDetailTemp);
               
            }
            await _context.SaveChangesAsync();
        }





        public async Task<bool> ConfirmOrderAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);


            if (user == null)
            {
                return false;
            }


            var orderTemps = await _context.OrderDetailsTemp
                .Include(o => o.Product)
                .Where(o => o.User == user)
                .ToListAsync();



            if (orderTemps == null || orderTemps.Count == 0)
            {
                return false;
            }


            var details = orderTemps.Select(o => new OrderDetail
            {
                Price = o.Price,
                Product = o.Product,
                Quantity = o.Quantity
            }).ToList(); //Isto sao os items do order




            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                User = user,
                Items = details,
            };


            _context.Orders.Add(order);
            _context.OrderDetailsTemp.RemoveRange(orderTemps);//remove range porque sao mais do que um
            await _context.SaveChangesAsync();

            return true;
        }






        public async Task DeleteDetailTempAsync(int id)
        {
            var orderDetailTemp = await _context.OrderDetailsTemp.FindAsync(id);


            if (orderDetailTemp == null)
            {
                return;
            }


            _context.OrderDetailsTemp.Remove(orderDetailTemp);//Isto so remove em memoria depois temos de executar o processo em bd 
            await _context.SaveChangesAsync();
        }





        public async Task<IQueryable<OrderDetailTemp>> GetDetailTempsAsync(string username)
        {
            var user = await _userHelper.GetUserByEmailAsync(username);

            if (user == null)
            {
                return null;
            }

            return _context.OrderDetailsTemp
                .Include(o => o.Product)
                .Where(o => o.User == user)
                .OrderBy(o => o.Product.Name);

        }







        public async Task<IQueryable<Order>> GetOrdersAsync(string username)
        {
            var user = await _userHelper.GetUserByEmailAsync(username);

            if (user==null)
            {
                return null;
            }


            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return _context.Orders//aqui mostra de todos os users
                    .Include(o => o.User)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .OrderByDescending(o => o.OrderDate);//Isto e como se fossem inner joins
            }

            return _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.User == user)//aqui mostra so os do user
                .OrderByDescending(o => o.OrderDate);
        }





        public async Task ModifyOrderDetailTempQuantityAsync(int id, double quantity)
        {
            var orderDetailTemp = await _context.OrderDetailsTemp.FindAsync(id);


            if (orderDetailTemp == null)
            {
                return;
            }


            orderDetailTemp.Quantity += quantity;

            if (orderDetailTemp.Quantity > 0)//Ja existe produtos
            {
                _context.OrderDetailsTemp.Update(orderDetailTemp);
                await _context.SaveChangesAsync();
            }
        }
    }
}
