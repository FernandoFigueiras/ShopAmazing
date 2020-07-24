using ShopAmazing.Web.Data.Entities;
using ShopAmazing.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Helpers
{
    public interface IConverterHelper
    {


        Product ToProduct(ProductViewModel model, string path, bool isNew);//o bool serve para saber se e um novo registo ou alterado atraves do edit


        ProductViewModel ToProductViewModel(Product model);
    }
}
