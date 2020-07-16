using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopAmazing.Web.Data;
using ShopAmazing.Web.Data.Entities;
using ShopAmazing.Web.Helpers;

namespace ShopAmazing.Web.Controllers
{
    public class ProductsController : Controller
    {
        //private readonly DataContext _context;
        //private readonly IRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IUserHelper _userHelper;

        public ProductsController(/*DataContext context*/ /*IRepository repository*/ IProductRepository productRepository, IUserHelper userHelper)
        {
            //_context = context;
            //_repository = repository;
            _productRepository = productRepository;
            _userHelper = userHelper;
        }

        // GET: Products
        public IActionResult Index()
        {
            //return View(await _context.Products.ToListAsync());
            return View(_productRepository.GetAll());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            //var product = _repository.GetProduct(id.Value);//id.value porque o int esta como not null (?) por isso pode ou nao vir com valor

            var product = await _productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {

                //por o user no product
                //TODO: Change for logged user
                product.User = await _userHelper.GetUserByEmailAsync("fjfigdev@gmail.com");


                //_repository.AddProduct(product);
                //await _repository.SaveAllAsync();
                await _productRepository.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product = _repository.GetProduct(id.Value);
            var product = await _productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //TODO: Change for logged user
                    product.User = await _userHelper.GetUserByEmailAsync("fjfigdev@gmail.com");
                    //temos de por aqui porque caso corra mal no get como na view nao aparece campo nenhum para editar chamamos novamente o User
                    //se falhar a validacao do GET temos de chamar de novo a validacao


                    //_repository.UpdateProduct(product);
                    //await _repository.SaveAllAsync();
                    await _productRepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _productRepository.ExistsAsync(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product = _repository.GetProduct(id.Value);
            var product = await _productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
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
    }
}
