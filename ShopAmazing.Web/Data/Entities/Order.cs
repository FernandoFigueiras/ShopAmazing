using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShopAmazing.Web.Data.Entities
{
    public class Order : IEntity
    {


        public int Id { get; set; }



        [Required]
        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime OrderDate { get; set; }




        [Display(Name = "Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime? DeliveryDate { get; set; }





        [Required]
        public User User { get; set; }




        public IEnumerable<OrderDetail> Items { get; set; }




        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity
        {
            get
            {
                return this.Items == null ? 0 : this.Items.Sum(i => i.Quantity);//se nao tiver items a quantidade vem null e se tiver soma os items
            }
        }




        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value
        {
            get
            {
                return this.Items == null ? 0 : this.Items.Sum(i => i.Value);//vai agarrar nos items e soma o valor
            }
        }

    }
}
