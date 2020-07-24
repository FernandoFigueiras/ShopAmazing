using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Helpers
{
    public class ImageHelper : IImageHelper
    {
        public async Task<string> UploadImageAsync(IFormFile imageFile, string folder)
        {
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";


            //a pessoa carrrega a imagem mas o caminho vai para dentro do atributo da bd no image url do products, mas o ficheiro vai para dentro da pasta images / products
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),//directoria do servidor
                $"wwwroot\\images\\{folder}",//o caminho
                /*model.ImageFile.FileName*/ file);//nome do ficheiro

            using (FileStream stream = new FileStream(path, FileMode.Create))//cria o ficheiro
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"~/images/{folder}/{/*model.ImageFile.FileName*/file}";//isto vai servir para guardar na base de dados o caminho da imagem ja guardada na pasta de imagens
                                                                            //Temos de alterar o mesmo nome do ficheiro usamos para isso o GUID
        }
    }
}
