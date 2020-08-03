using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketMVC.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerID { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public virtual Customer Customer { get; set; }//A encomenda tem de ter o cliente que fez a encomenda

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }//Uma encomenda pode ter varios produtos, liga-se a detalhes que tem o produto
    }
}