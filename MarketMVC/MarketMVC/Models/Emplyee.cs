using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MarketMVC.Models
{
    //[Table("Altera a tabela do Sql com o nome dado")]
    public class Emplyee
    {
        [Key]
        public int EmplyeeID { get; set; }

        [StringLength(30, ErrorMessage = "A {0} devera ter entre {2} e {1}", MinimumLength = 3)]
        [Display(Name = "Primeiro Nome")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Tem que inserir o {0}")]
        [StringLength(30, ErrorMessage = "A {0} devera ter entre {2} e {1}", MinimumLength = 3)]
        [Display(Name = "Apelido")]
        public string LastName { get; set; }


        [Display(Name = "Salario")]
        [Required(ErrorMessage = "Tem que inserir o {0}")]
        public decimal Salary { get; set; }


        [Display(Name = "Percentagem Bonus")]
        [Range(0,20, ErrorMessage ="O valor da {0} devera ser entre o campo {1} e {2}")]
        [Required(ErrorMessage = "Tem que inserir um valor para a {0}")]
        public float BonusPercent { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Data Nascimento")]
        [Required(ErrorMessage = "Tem que inserir um valor para a {0}")]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data Inicio")]
        [Required(ErrorMessage = "Tem que inserir um valor para a {0}")]
        public DateTime StartDate { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Column("Altera o nome da coluna")]
        public string URL { get; set; }

        //[ForeignKey("nome da chave estrangeira")]
        [Required(ErrorMessage = "O {0} tem de ser preenchido")]
        [Range(1, double.MaxValue, ErrorMessage = "Tem de selecionar um {0}")]
        [Display(Name = "Tipo de Documento")]
        public int DocumentTypeID { get; set; }

        [NotMapped] //Data annotation schema nao inclui na base de dados
        public int Age
        {
            get
            {
                var myAge = DateTime.Now.Year - DateOfBirth.Year;
                if (DateOfBirth>DateTime.Now.AddYears(-myAge))
                {
                    myAge--;
                }
                return myAge;
            }
        }

        //relacao entre esta tabela e a dos documents
        public virtual DocumentType DocumentType { get; set; }//esta e a propriedade que liga os tipos de documentos da outra tabela do outro modelo

    }
}