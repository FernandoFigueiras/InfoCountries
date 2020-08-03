using MechanicShop.Data;
using MechanicShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MechanicShop.Helpers
{
    public class CombosHelpers
    {
        private static MechanicShopContext db = new MechanicShopContext();
        public static List<Brand> GetBrands()
        {

            var brands = db.Brands.ToList();
            brands.Add(new Brand {
            BrandID=0,
            BrandName = "[Selecione uma marca]"
            });

            return brands.OrderBy(b => b.BrandName).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}