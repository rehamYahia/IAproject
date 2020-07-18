using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace database.Models
{
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {

    }
    public class ProductMetaData
    {
        [Display(Name="ID")]
        [Required]
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required]
        [FileExtensions(Extensions ="jpg,jpeg,png")]
        [DataType(DataType.ImageUrl)]
        public string name { get; set; }
        [Display(Name = "Image")]
        [Required]
        public string image { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string description { get; set; }
        [DisplayFormat(DataFormatString ="{0:C}",ApplyFormatInEditMode =false)]
        [Required]
        public Nullable<double> price { get; set; }
        [Display(Name="Category")]
        [Required]
        public Nullable<int> category_id { get; set; }
    }
}