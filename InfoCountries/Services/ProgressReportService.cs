using InfoCountries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoCountries.Services
{
    public class ProgressReportService
    {
        public int PercComplete { get; set; } = 0;

        public List<Country> ApiLoaded { get; set; } = new List<Country>();

    }
}
