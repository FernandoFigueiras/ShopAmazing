using Microsoft.AspNetCore.Mvc;
using ShopAmazing.Web.Data;

namespace ShopAmazing.Web.Controllers.API//APi controller class empty na criacao
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller//Atencao mudar para controller so.O controller tb herda do controller base mas e mais especifico
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)//Injectamos a interface do product repository
        {
            _productRepository = productRepository;
        }


        [HttpGet]
        public IActionResult GetProducts()//mesmo que nao se ponha a notacao httpget ele funciona porque o nome e get, boa pratica por e vai ter mais metodos com o nome de Get.
        {
            return Ok(_productRepository./*GetAll()*/GetAllWithUsers());//Como isto e um HTTP requeste passa as mensagem de HTTP da api
            //Este OK envolve tudo o que ele recebe do GetAll e serializa tudo em Jason e retorna atravez da API.
        }
    }
}
