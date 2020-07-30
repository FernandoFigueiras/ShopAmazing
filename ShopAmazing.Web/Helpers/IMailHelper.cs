namespace ShopAmazing.Web.Helpers
{
    public interface IMailHelper
    {
        //isto leva um nuguet instalado no projecto da web que e o mailkit (atencao para windows, mac e linux)

        void SendMail(string to, string subject, string body);
    }
}
