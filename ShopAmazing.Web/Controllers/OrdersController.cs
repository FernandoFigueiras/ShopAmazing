using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using ShopAmazing.Web.Data.Repositories;
using ShopAmazing.Web.Models;
using System;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Controllers
{

    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrdersController(
            IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }



        public async Task<IActionResult> Index()
        {
            var model = await _orderRepository.GetOrdersAsync(this.User.Identity.Name);
            return View(model);
        }



        public async Task<IActionResult> Create()
        {
            var model = await _orderRepository.GetDetailTempsAsync(this.User.Identity.Name);
            return View(model);
        }



       public IActionResult AddProduct()
        {
            var model = new AddItemViewModel
            {
                Quantity = 1,
                Products = _productRepository.GetComboProducts(),
            };


            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(AddItemViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await _orderRepository.AddItemToOrderAsync(model, this.User.Identity.Name);
                return this.RedirectToAction(nameof(Create));
            }


            return View(model);
        }




        public async Task<IActionResult> DeleteItem(int? id)//Este e o nome que sta no botao create do orders
        {
            if (id == null)
            {
                return NotFound();
            }


            await _orderRepository.DeleteDetailTempAsync(id.Value);


            return this.RedirectToAction(nameof(Create));
        }




        public async Task<IActionResult> Increase(int? id)//Este e o nome que sta no botao create do orders
        {
            if (id == null)
            {
                return NotFound();
            }


            await _orderRepository.ModifyOrderDetailTempQuantityAsync(id.Value, 1);//aumenta de um em um ver orderRepository


            return this.RedirectToAction(nameof(Create));
        }


        public async Task<IActionResult> Decrease(int? id)//Este e o nome que sta no botao create do orders
        {
            if (id == null)
            {
                return NotFound();
            }


            await _orderRepository.ModifyOrderDetailTempQuantityAsync(id.Value, -1);//diminui de um em um ver orderRepository


            return this.RedirectToAction(nameof(Create));
        }




        public async Task<IActionResult> ConfirmOrder()
        {
            var response = await _orderRepository.ConfirmOrderAsync(this.User.Identity.Name);


            if (response)//esta no OrderRepository devolve um bool
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.RedirectToAction(nameof(Create));
        }




        public async Task<IActionResult> Deliver(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }


            var order = await _orderRepository.GetOrdersAsync(id.Value);

            if (order==null)
            {
                return NotFound();
            }


            var model = new DeliverViewModel
            {
                Id = order.Id,
                DeliveryDate = DateTime.Today,
            };


            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Deliver(DeliverViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _orderRepository.DeliverOrder(model);
                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }
    }
}
