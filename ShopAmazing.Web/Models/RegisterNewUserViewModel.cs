using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
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



        [MaxLength(100, ErrorMessage = "The field {0 }only can contain {1} characters")]
        public string Address { get; set; }




        [MaxLength(20, ErrorMessage = "The field {0 }only can contain {1} characters")]
        public string PhoneNumber { get; set; }




        [Display(Name ="City")]
        [Range(1, int.MaxValue, ErrorMessage ="You must select a city")]
        public int CityId { get; set; }





        public IEnumerable<SelectListItem> Cities { get; set; }





        [Display(Name = "Country")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a country")]
        public int CountryId { get; set; }





        public IEnumerable<SelectListItem> Countries { get; set; }




        [Required]
        public string Password { get; set; }



        [Required]
        [Compare("Password")]//compara com o que esta na password e da erro caso nao sejam iguais
        public string Confirm { get; set; }


    }
}
