using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MarketMVC.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [StringLength(30, ErrorMessage = "A {0} devera ter entre {2} e {1}", MinimumLength = 3)]
        [Required(ErrorMessage ="Tem de inserir um {0}")]
        [Display(Name ="Primeiro Nome")]
        public string CustomerFirstName { get; set; }

        [StringLength(30, ErrorMessage = "A {0} devera ter entre {2} e {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "Tem de inserir um {0}")]
        [Display(Name = "Apelido")]
        public string CustomerLastName { get; set; }

        //isto e so para o nome completo nao vai aparecer na base de dados
        [Display(Name ="Nome Completo")]
        [NotMapped] //Para nao aparecer na base de dados
        public string CustomerFullName { get { return $"{CustomerFirstName} {CustomerLastName}"; } }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Tem de inserir um {0}")]
        [StringLength(30, ErrorMessage = "O {0} devera ter entre {2} e {1}", MinimumLength = 9)]
        [Display(Name = "Telefone")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Tem de inserir um {0}")]
        [Display(Name = "Morada")]
        [StringLength(100, ErrorMessage = "O {0} devera ter entre {2} e {1}", MinimumLength = 3)]
        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Tem de inserir um {0}")]
        [Display(Name = "Numero de documento")]
        [StringLength(20, ErrorMessage = "O {0} devera ter entre {2} e {1}", MinimumLength = 5)]
        public string Document { get; set; }


        [Required(ErrorMessage ="O {0} tem de ser preenchido")]
        [Range(1,double.MaxValue, ErrorMessage ="Tem de selecionar um {0}")]
        [Display(Name ="Tipo de Documento")]
        public int DocumentTypeID { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual ICollection<Order> Orders { get; set; }//O cliente faz uma encomenda
    }
}