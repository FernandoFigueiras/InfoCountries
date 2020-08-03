using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class Brand : IEntity
    {
        public int Id { get; set; }



        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }



        [Display(Name = "Update Date")]
        public DateTime? UpdateDate { get; set; }



        [Display(Name = "DeleteDate")]
        public DateTime? DeleteDate { get; set; }



        [Display(Name = "Is Active ?")]
        public bool IsActive { get; set; }



        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
    }
}
