using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoCountries.Models
{
    public class CalcRate
    {
        public int RateId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Symbol { get; set; }

        public decimal TaxRate { get; set; }


        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
