using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketMVC.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage ="Deve inserir um {0} para o fornecedor")]
        [StringLength(30, ErrorMessage =" o campo {0} tem de ter entre {2} e {1} caracteres", MinimumLength =3)]
        public string SupplierName { get; set; }


        [Display(Name ="Nome do contacto")]
        [Required(ErrorMessage = "Deve inserir um {0} para o fornecedor")]
        [StringLength(30, ErrorMessage = " o campo {0} tem de ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string ContactFirstName { get; set; }

        [Display(Name = "Apelido do contacto")]
        [Required(ErrorMessage = "Deve inserir um {0} para o fornecedor")]
        [StringLength(30, ErrorMessage = " o campo {0} tem de ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string ContactLastName { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Deve inserir um {0}")]
        [Display(Name = "Telefone")]
        [StringLength(30, ErrorMessage = " o campo {0} tem de ter entre {2} e {1} caracteres", MinimumLength = 9)]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Deve inserir uma {0}")]
        [Display(Name = "Morada")]
        [StringLength(100, ErrorMessage = " o campo {0} tem de ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }


    }
}