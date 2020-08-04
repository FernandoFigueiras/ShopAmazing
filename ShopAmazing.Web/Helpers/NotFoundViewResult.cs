using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ShopAmazing.Web.Helpers
{
    public class NotFoundViewResult : ViewResult     //Isto e para o caminho do not found page
    {

        public NotFoundViewResult(string viewName)
        {
            ViewName = viewName; //isto e para fazer o override do nome da view que eu quero que apareca 
            StatusCode = (int)HttpStatusCode.NotFound;//isto e o codigo de erro 404 do http
        }
    }
}
