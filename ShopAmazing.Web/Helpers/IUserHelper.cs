using Microsoft.AspNetCore.Identity;
using ShopAmazing.Web.Data.Entities;
using ShopAmazing.Web.Models;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Helpers
{
    public interface IUserHelper//Tudo o que que tem a ver com gestao de utilizadores faz-se um bypass para nao usar a classe do core e juntar tudo no mesmo sitio usando a class da framework UserManager
    {
        //metodo que permite criar um user
        //O identityResult retorna a informacao se consegiu criar ou nao (bool)
        Task<IdentityResult> AddUserAsync(User user, string password);//vai servir para criar utilizador e fazer o bypass do createasync do usermanager



        //este metodo pesquisa por um user pelo email, que neste exemplo em especifico e tb o username
        Task<User> GetUserByEmailAsync(string email);



        Task<SignInResult> LoginAsync(LoginViewModel model);//classe que deriva do signinmanager que diz se fez ou nao passando o LogInViewModel



        Task LogOutAsync();



        Task<IdentityResult> UpdateUserAsync(User user);



        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);



        Task<SignInResult> ValidatePasswordAsync(User user, string password);//para ver se a password esta correcta para gerar o token, (ver json) da o resultado



        Task CheckRoleAsync(string roleName);



        Task<bool> IsUserInRoleAsync(User user, string roleName);



        Task AddUserToRoleAsync(User user, string roleName);



        Task<string> GenerateEmailConfirmationTokenAsync(User user);//vai gerar o email de confirmacao que vai mandar ao user com o token (manda email para o user confirmar)



        Task<IdentityResult> ConfirmEmailAsync(User user, string token);//quando o user mandar o email de confirmacao vai ver se o user bate com o token e deixa entar (recebe email de confirmacao do user)



        Task<User> GetUserByIdAsync(string userId);//este vai buscar o user pelo id



        Task<string> GeneratePasswordResetTokenAsync(User user);



        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);
    }
}
