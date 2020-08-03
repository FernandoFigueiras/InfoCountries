using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketMVC.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Display(Name = "Descricao")]
        [StringLength(30, ErrorMessage = "A {0} devera ter entre {2} e {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "Deve inserir uma {0}")]
        public string Description { get; set; }


        [DataType(DataType.Currency)]//Definimos o tipo de dados moeda
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]//formatamos para duas casas decimais, mas como o apply format esta a falso nao huarda assim na tabela
        [Required(ErrorMessage = "Deve inserir um {0}")]
        [Display(Name = "Preco")]
        public decimal Price { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ultima Compra")]
        public DateTime LastBuy { get; set; }

        [DataType(DataType.Currency)]// pomos como currency por causa das casas decimais
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public float Stock { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name ="Anotacoes")]
        public string Remarks { get; set; }

        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } //Um produto tem varios detalhes

    }
}