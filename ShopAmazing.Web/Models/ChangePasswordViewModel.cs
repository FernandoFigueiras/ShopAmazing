using System.ComponentModel.DataAnnotations;

namespace ShopAmazing.Web.Models
{
    public class ChangePasswordViewModel
    {

        //depois ir ao user helper
        [Required]
        public string OldPassword { get; set; }



        [Required]
        public string NewPassword { get; set; }



        [Required]
        [Compare("NewPassword")]//compara com o que esta na password e da erro caso nao sejam iguais
        public string Confirm { get; set; }
    }
}
