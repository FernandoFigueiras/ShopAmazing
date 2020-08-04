using System;
using System.ComponentModel.DataAnnotations;

namespace ShopAmazing.Web.Models
{
    public class DeliverViewModel
    {


        public int Id { get; set; }



        [Display(Name = "Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime DeliveryDate { get; set; }


    }
}
