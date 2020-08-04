using System.ComponentModel.DataAnnotations;

namespace ShopAmazing.Web.Data.Entities
{
    public class OrderDetailTemp : IEntity
    {

        public int Id { get; set; }




        [Required]
        public User User { get; set; }//User que faz a encomenda temporaria




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
