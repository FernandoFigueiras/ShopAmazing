using Microsoft.AspNetCore.Identity;
using ShopAmazing.Web.Data.Entities;
using ShopAmazing.Web.Models;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserHelper(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)//Nao precisamos de injectar a datacontext porque ja vai buscar a IdentityDbContext que e especifica para os users
        {
            _userManager = userManager; //classe do core para gerir utilizador
            _signInManager = signInManager;//classe do core para gerir os logins e logout
            _roleManager = roleManager;//atencao ir ao seed
        }




        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);//bypass que usa o usermanager mas a partir daqui. Coisas a acrescentar ou alterar a class pode ser feita atravez daqui
        }




        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }




        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }



        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole//cria um role se nao existir
                {
                    Name = roleName,
                });
            }
        }




        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }




        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }




        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(//aqui faz-se novamente o bypass usando a class do core para tratar dos login
                model.UserName,
                model.Password,
                model.RememberMe,
                false);//este parametro define quantas tentativas ele bloqueia sem conseguir entrar
        }




        //isto leva o controlador account

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }





        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }




        public async Task<SignInResult> ValidatePasswordAsync(User user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(
                user,
                password,
                false);//o ultimo parametro e para nao bloquear caso o user se engane na password.depois passa para o account
        }
    }
}
