using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MechanicShop.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleID { get; set; }

        [Display(Name = "Matricula")]
        [Required(ErrorMessage ="Deve inserir uma {0}")]
        public string LicencePlate { get; set; }

        [Display(Name = "Cor")]
        [Required(ErrorMessage = "Deve inserir uma {0}")]
        public string Color { get; set; }

        [Display(Name = "Cilindrada")]
        [Required(ErrorMessage = "Deve inserir uma {0}")]
        public int EngineCapacity { get; set; }

        [Display(Name = "Combustivel")]
        [Required(ErrorMessage = "Deve indicar o {0}")]
        public string Fuel { get; set; }

        public int BrandID { get; set; }

        public virtual Brand Brand { get; set; }
    }
}