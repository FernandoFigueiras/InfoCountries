namespace InfoCountries.Services
{
    using InfoCountries.Models;
    using System.Collections.Generic;
    public class ProgressReportService
    {
        public int PercComplete { get; set; } = 0;

        public List<Country> DataLoaded { get; set; } = new List<Country>();

    }
}
