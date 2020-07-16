using ShopAmazing.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Data
{
    public class Repository : IRepository
    {
        //Este produto so vai trabalhar com produtos
        private readonly DataContext _context;

        public Repository(DataContext context)//injectar a data context
        {
            _context = context;
        }

        //agora vamos criar os metodos de CRUD do controlador para aceder aos produtos a partir desta camada intermedia

        //Metodo que vai buscar os produtos todos
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.OrderBy(p => p.Name);
        }

        //vai buscar um produto por ID
        public Product GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        //adicionar produto
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);//create do crud
        }

        //edit do produto
        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        //remover o produto
        public void RemoveProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        //gravar tudo, grava todas as mudancas que estejam em pool
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;//Maior que zero porque se houver mais do que uma alteracao para fazer ele fica true, porque ele grava um conjunto de mudancas que pode ser mais do que uma
        }

        //detectar se um produto existe
        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);//o any ja e um bool, se encontrar o id retorna true
        }
    }
}
