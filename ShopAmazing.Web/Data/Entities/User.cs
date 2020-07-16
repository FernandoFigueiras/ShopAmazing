using Microsoft.AspNetCore.Identity;

namespace ShopAmazing.Web.Data.Entities
{
    public class User : IdentityUser //estamos a gerar campos que o user nao tem na classe IdentityUser, dai usarmos uma class nossa que herda do IdentityUser
    {   //extendemos a classe do Core

        //atencao que como estamos a usar o IdentityUser temos de mudar o Data Context e usar um proprio para a autenticacao
        public string FirstName { get; set; }


        public string LastName { get; set; }
    }
}
