﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAmazing.Web.Data.Repositories;
using ShopAmazing.Web.Helpers;
using ShopAmazing.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Controllers
{
    //se colocarmos o authorize aqui ele so entra se tiver aqui disponivel com os roles
    //[Authorize]
    public class ProductsController : Controller
    {
        //private readonly DataContext _context;
        //private readonly IRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public ProductsController(/*DataContext context*/ /*IRepository repository*/
            IProductRepository productRepository,
            IUserHelper userHelper,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)
        {
            //_context = context;
            //_repository = repository;
            _productRepository = productRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: Products
        public IActionResult Index()
        {
            //return View(await _context.Products.ToListAsync());
            return View(_productRepository.GetAll().OrderBy(p => p.Name));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");//Isto vai chamar a view do not found que esta na classe NotFoundViewResult nos helpers
            }


            //var product = _repository.GetProduct(id.Value);//id.value porque o int esta como not null (?) por isso pode ou nao vir com valor

            var product = await _productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }


        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*Product product*/ ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                //caminho para a pasta imagens dos produtos vindas do upload
                var path = string.Empty;

                if (model.ImageFile != null/* && model.ImageFile.Length>0*/)//se existe upload ou nao, pode ou nao por imagem
                {

                    //Aqui vai buscar o helper
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Products");

                    //O mesmo no edit


                    //Isto passou para o Helper

                    //var guid = Guid.NewGuid().ToString();
                    //var file = $"{guid}.jpg";


                    ////a pessoa carrrega a imagem mas o caminho vai para dentro do atributo da bd no image url do products, mas o ficheiro vai para dentro da pasta images / products
                    //path = Path.Combine(
                    //    Directory.GetCurrentDirectory(),//directoria do servidor
                    //    "wwwroot\\images\\Products",//o caminho
                    //    /*model.ImageFile.FileName*/ file);//nome do ficheiro

                    //using (var stream = new FileStream(path, FileMode.Create))//cria o ficheiro
                    //{
                    //    await model.ImageFile.CopyToAsync(stream);
                    //}

                    //path = $"~/images/Products/{/*model.ImageFile.FileName*/ file}";//isto vai servir para guardar na base de dados o caminho da imagem ja guardada na pasta de imagens
                    ////Temos de alterar o mesmo nome do ficheiro usamos para isso o GUID
                }

                //agora temos de passar de um model para um product para guardar na base de dados, temos de converter

                //var product = this.ToProduct(model, path);//isto e um metodo nosso que construimos
                var product = _converterHelper.ToProduct(model, path, true);


                //por o user no product
                //TODO: Change for logged user
                //product.User = await _userHelper.GetUserByEmailAsync("fjfigdev@gmail.com");
                product.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);//Aqui ja vai buscar o user que esta logado


                //_repository.AddProduct(product);
                //await _repository.SaveAllAsync();
                await _productRepository.CreateAsync(product);
                return RedirectToAction(nameof(Index));//tambem queremos que o index tenha as imagens (View)
            }
            return View(model);
        }

        //Isto passou para os helpers
        //private /*object*/ Product ToProduct(ProductViewModel model, string path)
        //{
        //    //retorna um Product depois de ser feita a conversao para ser guardado na base de dadis
        //    return new Product
        //    {
        //        Id = model.Id,
        //        ImageUrl = path,
        //        IsAvailable = model.IsAvailable,
        //        LastPurchase = model.LastPurchase,
        //        LastSale = model.LastSale,
        //        Name = model.Name,
        //        Price = model.Price,
        //        Stock = model.Stock,
        //        User = model.User,
        //    };
        //}

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)//queremos que a imagem ja esteja la quando se vai a view
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            //var product = _repository.GetProduct(id.Value);
            var product = await _productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            //var model = this.ToProductViewModel(product);//Queremos que a view ja tenha a imagem
            var model = _converterHelper.ToProductViewModel(product);
            return View(model);
        }

        //Isto passou para os helpers
        //private ProductViewModel ToProductViewModel(Product product)
        //{
        //    return new ProductViewModel
        //    {
        //        Id = product.Id,
        //        ImageUrl = product.ImageUrl,
        //        IsAvailable = product.IsAvailable,
        //        LastPurchase = product.LastPurchase,
        //        LastSale = product.LastSale,
        //        Name = product.Name,
        //        Price = product.Price,
        //        Stock = product.Stock,
        //        User = product.User,
        //    };
        //}

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(/*Product product*/ ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //aqui ja ha o caminho
                    var path = model.ImageUrl;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)//se existe upload ou nao, pode ou nao por imagem
                    {


                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "Products");


                        //var guid = Guid.NewGuid().ToString();
                        //var file = $"{guid}.jpg";


                        ////a pessoa carrrega a imagem mas o caminho vai para dentro do atributo da bd no image url do products, mas o ficheiro vai para dentro da pasta images / products
                        //path = Path.Combine(
                        //    Directory.GetCurrentDirectory(),//directoria do servidor
                        //    "wwwroot\\images\\Products",//o caminho
                        //    /*model.ImageFile.FileName*/ file);//nome do ficheiro

                        //using (var stream = new FileStream(path, FileMode.Create))//cria o ficheiro
                        //{
                        //    await model.ImageFile.CopyToAsync(stream); //atencao que depois temos de apagar o ficheiro da pasta das imagens
                        //}

                        //path = $"~/images/Products/{/*model.ImageFile.FileName*/ file}";//isto vai servir para guardar na base de dados o caminho da imagem ja guardada na pasta de imagens
                    }

                    //var product = this.ToProduct(model, path);
                    var product = _converterHelper.ToProduct(model, path, false);

                    //TODO: Change for logged user
                    //product.User = await _userHelper.GetUserByEmailAsync("fjfigdev@gmail.com");
                    product.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);//Aqui esta o user que esta logado
                    //temos de por aqui porque caso corra mal no get como na view nao aparece campo nenhum para editar chamamos novamente o User
                    //se falhar a validacao do GET temos de chamar de novo a validacao


                    //_repository.UpdateProduct(product);
                    //await _repository.SaveAllAsync();
                    await _productRepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _productRepository.ExistsAsync(model.Id))
                    {
                        return new NotFoundViewResult("ProductNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Admin, Customer")]Mais do que um role
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            //var product = _repository.GetProduct(id.Value);
            var product = await _productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var product = _repository.GetProduct(id);
            //_repository.RemoveProduct(product);
            //await _repository.SaveAllAsync();

            var product = await _productRepository.GetByIdAsync(id);
            await _productRepository.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }



        public IActionResult ProductNotFound()
        {
            return View();
        }
    }
}
