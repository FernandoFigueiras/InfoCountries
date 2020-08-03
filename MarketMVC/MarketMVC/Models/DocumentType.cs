using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketMVC.Models
{
    public class DocumentType
    {
        [Key]
        [Display(Name ="Tipo Documento")] //Aquilo que se pretende que apareca na tabela da aplicacao
        public int DocumentTypeID { get; set; }


        [Display(Name ="Documento")]
        public string Description { get; set; }
        //varios empregados
        public virtual ICollection<Emplyee> Emplyees { get; set; } //esta propriedade guarda a lista de enpregados
        //quando e criado um documento traz tambem todos os empregados que tem este tipo de documento

        public virtual ICollection<Customer> Customers { get; set; }
    }
}