﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ShopAmazing.Web.Data.Entities
{
    public class User : IdentityUser //estamos a gerar campos que o user nao tem na classe IdentityUser, dai usarmos uma class nossa que herda do IdentityUser
    {   //extendemos a classe do Core

        //atencao que como estamos a usar o IdentityUser temos de mudar o Data Context e usar um proprio para a autenticacao
        [MaxLength(50, ErrorMessage = "The field {0 }only can contain {1} characters")]
        public string FirstName { get; set; }



        [MaxLength(50, ErrorMessage = "The field {0 }only can contain {1} characters")]
        public string LastName { get; set; }




        [MaxLength(100, ErrorMessage = "The field {0 }only can contain {1} characters")]
        public string Address { get; set; }




        public int CityId { get; set; }





        public City City { get; set; }





        [Display(Name ="Full Name")]
        public string FullName 
        {
            get 
            {
                return $"{ this.FirstName} {this.LastName}";
            } 
        }
    }
}
