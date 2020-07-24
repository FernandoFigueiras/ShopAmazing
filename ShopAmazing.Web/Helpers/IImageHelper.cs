using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ShopAmazing.Web.Helpers
{
    public interface IImageHelper
    {

        Task<string> UploadImageAsync(IFormFile imageFile, string folder);//recebe o ficheiro e a folder onde vai guardar o ficheiro
        //o iformfile e como o html que colocamos nas views do create e do edit
    }
}
