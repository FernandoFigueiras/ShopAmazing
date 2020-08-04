using System.ComponentModel.DataAnnotations;

namespace ShopAmazing.Web.Data.Entities
{
    public class OrderDetail : IEntity
    {


        public int Id { get; set; }


        //nao preciso do user porque isto e um detail, depois vai estar no user

        [Required]
        public Product Product { get; set; }//O produto que vou encomendar




        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }




        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity { get; set; }




        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Value
        {
            get
            {
                return this.Price * (decimal)Quantity;
            }
        }
    }
}
