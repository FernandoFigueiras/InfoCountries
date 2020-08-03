using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MechanicShop.Models
{
    public class Brand
    {
        [Key]
        public int BrandID { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage ="Deve inserir uma {0}")]
        public string BrandName { get; set; }

        public virtual List<Vehicle> Vehicles { get; set; }
    }
}