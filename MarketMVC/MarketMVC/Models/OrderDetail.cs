using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketMVC.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

        [StringLength(30, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 3)]
        [Required(ErrorMessage = "Tem de inserir um {0}")]
        [Display(Name = "Descricao do Produto")]
        public string OrderDetailDescription { get; set; }


        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode =false)]
        [Required(ErrorMessage ="Tem de inserir um {0}")]
        [Display(Name ="Preco")]
        public decimal Price { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Tem de inserir um {0}")]
        [Display(Name = "Quantidade")]
        public float Quantity { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; } // tem o produto associado que vem das order.
    }
}