using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace database.Models
{
    [MetadataType(typeof(CartMetaData))]
    public partial class Cart
    {
    }
    public class CartMetaData
    {
        [Required]
        [Display(Name ="ID")]
        public int product_id { get; set; }
        [Display(Name = "Added At")]
        [Required]
        
        public Nullable<System.DateTime> added_at { get; set; }
    }

}