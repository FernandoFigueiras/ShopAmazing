﻿using Microsoft.AspNetCore.Identity;
using ShopAmazing.Web.Data.Entities;
using ShopAmazing.Web.Models;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserHelper(
            UserManager<User> userManager,
            SignInManager<User> signInManager)//Nao precisamos de injectar a datacontext porque ja vai buscar a IdentityDbContext que e especifica para os users
        {
            _userManager = userManager; //classe do core para gerir utilizador
            _signInManager = signInManager;//classe do core para gerir os logins e logout
        }



        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);//bypass que usa o usermanager mas a partir daqui. Coisas a acrescentar ou alterar a class pode ser feita atravez daqui
        }



        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
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
    }
}
