using Microsoft.AspNetCore.Identity;
using ShopAmazing.Web.Data.Entities;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;

        public UserHelper(UserManager<User> userManager)//Nao precisamos de injectar a datacontext porque ja vai buscar a IdentityDbContext que e especifica para os users
        {
            _userManager = userManager;
        }



        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);//bypass que usa o usermanager mas a partir daqui. Coisas a acrescentar ou alterar a class pode ser feita atravez daqui
        }



        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
