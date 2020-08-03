using System.ComponentModel.DataAnnotations;

namespace MarketMVC.Models
{
    public class ProductOrder : Product
    {

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]//formatamos para duas casas decimais, mas como o apply format esta a falso nao huarda assim na tabela
        [Required(ErrorMessage = "Deve inserir um {0}")]
        [Display(Name = "Quantidade")]
        public float Quantity { get; set; }


        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]//formatamos para duas casas decimais, mas como o apply format esta a falso nao huarda assim na tabela
        [Display(Name = "Valor a pagar")]
        public decimal Value { get { return Price * (decimal)Quantity; } }

    }
}