using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAppMVCStore.Models;

namespace WebAppMVCStore.Context
{
    public class StoreContext: DbContext
    {
        public DbSet<Product> Products { get; set; }//Isto é o responsável por criar a tabela de produtos na bd
    }
}