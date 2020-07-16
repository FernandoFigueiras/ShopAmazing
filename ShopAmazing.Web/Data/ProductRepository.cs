using ShopAmazing.Web.Data.Entities;

namespace ShopAmazing.Web.Data
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context) // o igeneric repository vai saber os interfaces que o implementa, assim as classes que implementam vao ser instanciadas
        {//Aqui faz-se a heranca de todos os metodos que estao na class generica com o respectivo interface para ser injectado no startup
        }
    }
}
