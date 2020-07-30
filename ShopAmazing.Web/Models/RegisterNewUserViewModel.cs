using System.ComponentModel.DataAnnotations;

namespace ShopAmazing.Web.Models
{
    public class RegisterNewUserViewModel//modelo de registo com os dados que se pretendem passar
    {
        //depois ir ao account controller
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }



        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }



        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }



        [Required]
        public string Password { get; set; }



        [Required]
        [Compare("Password")]//compara com o que esta na password e da erro caso nao sejam iguais
        public string Confirm { get; set; }
    }
}
