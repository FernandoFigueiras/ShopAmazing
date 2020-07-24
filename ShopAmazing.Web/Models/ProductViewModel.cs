using Microsoft.AspNetCore.Http;
using ShopAmazing.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace ShopAmazing.Web.Models
{
    public class ProductViewModel : Product //Porque vamos utilizar esta view para chamar as imagens a gravar mas que nao vao ser gravadas na base de dados e uma view especial para este modelo de dados
    {   //Um objecto proprio so para a view - E o que esta entre o modelo e a view
        //vai buscar os ficheiros
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; } //vamos alterar a view do create dos produtos

    }
}
