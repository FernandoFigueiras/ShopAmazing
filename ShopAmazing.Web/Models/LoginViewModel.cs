using System.ComponentModel.DataAnnotations;

namespace ShopAmazing.Web.Models
{
    public class LoginViewModel//depois temos de ir ao userHelper
    {
        //o que vai ser passado para a View
        [Required]
        [EmailAddress]
        public string UserName { get; set; }//vai ser o email


        [Required]
        [MinLength(6)]//depois temos de alterar isto na producao
        public string Password { get; set; }



        public bool RememberMe { get; set; }
    }
}
