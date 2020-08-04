using System;
using System.ComponentModel.DataAnnotations;

namespace ShopAmazing.Web.Data.Entities
{
    public class Product : IEntity //Esta classe implementa o IEntity para ser usado pelo repositorio Generico
    {
        public int Id { get; set; }


        [MaxLength(50, ErrorMessage ="The field {0} must contain only {1} characters long")]
        [Required]
        public string Name { get; set; }



        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode =false)]//Isto e para aparecer o simbolo da currency
        public decimal Price { get; set; }//por causa das casas decimais temos de fazer o modelo no data context




        [Display(Name = "Image")]
        public string ImageUrl { get; set; }


        [Display(Name = "Last Purchase")]
        public DateTime? LastPurchase { get; set; }


        [Display(Name = "Last Sale")]
        public DateTime? LastSale { get; set; }


        [Display(Name = "Is Available?")]
        public bool IsAvailable { get; set; }


        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }


        public User User { get; set; }//aqui estou a guardar o User, relacao de muitos para um
        //So temos de guardar onde se pretende que apareca a informacao que a relacao e estabelecida automaticamente.

        //para devolver o link completo da imagem
        public string ImageFullPath 
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImageUrl))
                {
                    return null;
                }
                //link inteiro
                return $"https://shopamazing.azurewebsites.net{this.ImageUrl.Substring(1)}";//o substring 1 tira o til, constroi uma string nova e tira o caracter especificado, neste caso o 1
            }
        }
    }
}
