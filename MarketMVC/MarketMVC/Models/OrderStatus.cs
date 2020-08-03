using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketMVC.Models
{
    public enum OrderStatus
    {
        Created,
        InProgress,
        Shipped,
        Delivered
    }
}