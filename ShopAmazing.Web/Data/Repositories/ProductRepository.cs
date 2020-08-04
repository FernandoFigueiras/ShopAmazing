using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopAmazing.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ShopAmazing.Web.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context) : base(context) // o igeneric repository vai saber os interfaces que o implementa, assim as classes que implementam vao ser instanciadas
        {//Aqui faz-se a heranca de todos os metodos que estao na class generica com o respectivo interface para ser injectado no startup
            _context = context;
        }




        public IQueryable GetAllWithUsers()
        {
            return _context.Products.Include(p => p.User);//produto onde inclui os respectivos users
        }





        public IEnumerable<SelectListItem> GetComboProducts()
        {
            var list = _context.Products
                .Select(p => new SelectListItem
                {
                    Text = p.Name,              //Como isto e uma combobox tem de ter o texto e o value
                    Value = p.Id.ToString(),
                }).ToList();



            list.Insert(0, new SelectListItem
            {
                Text = "(Select a Product...)",
                Value = "0",
            });//atencao que tem de levar o range no view model


            return list;
        }
    }
}
