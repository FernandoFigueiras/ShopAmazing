using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopAmazing.Web.Data.Entities;
using ShopAmazing.Web.Helpers;
using ShopAmazing.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Controllers
{

    //autenticacao passa tudo por aqui
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;



        public AccountController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }




        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)//Se o user esta autenticado
            {
                return this.RedirectToAction("index", "Home"); //redireciona para a action index do controlador home
            }
            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)//Aqui temos de acionar o servico de autenticacao no startup.
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        //direccao de retorno procura pela key que se quer na url esta key aparece sempre que a tentativa de login e feita fora da home
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());//redireciona para onde ele estava anteriormente no http
                    }

                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to login");
            return this.View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogOutAsync();
            return this.RedirectToAction("index", "Home");
        }


        public IActionResult Register()
        {
            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //chekar se o user ja existe
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                //se o user nao existe
                if (user==null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username,
                    };
                    //adicionar a base de dados
                    var result = await _userHelper.AddUserAsync(user, model.Password);

                    //se nao tiver criado um user com sucesso
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created");
                        return this.View(model);
                    }

                    //caso consiga criar regista e entre logo
                    //so neste caso, mas depois alteramos
                    var loginViewModel = new LoginViewModel
                    {
                        Password = model.Password,
                        RememberMe = false,
                        UserName = model.Username,
                    };

                    //autenticar
                    var result2 = await _userHelper.LoginAsync(loginViewModel);

                    if (result2.Succeeded)
                    {
                        return this.RedirectToAction("Index", "Home");
                    }

                    //se o login nao correu bem

                    this.ModelState.AddModelError(string.Empty, "The user couldn't be login");
                    return this.View(model);
                }
                this.ModelState.AddModelError(string.Empty, "The user allready exists");
                return this.View(model);
            }
            return View(model);
        } 
    }
}
