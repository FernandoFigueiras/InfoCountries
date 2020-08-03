using AutoRepairShop.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data
{
    public class DataContext :DbContext
    {

        public DbSet<Model> Models { get; set; }



        public DbSet<Brand> Brands { get; set; }

       
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
