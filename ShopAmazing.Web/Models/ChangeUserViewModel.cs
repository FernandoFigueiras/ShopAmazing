using System.ComponentModel.DataAnnotations;

namespace ShopAmazing.Web.Models
{
    public class ChangeUserViewModel
    {
        //depois ir ao user helper
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }



        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        //Podemos por mais, bem como as fotos de perfil e tudo o mais
    }
}
