namespace InfoCountries.Data
{
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Drawing.Text;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Classe used to get data to be desplayed in UI
    /// </summary>
    public static class UIData
    {
        
        /// <summary>
        /// Sets the list of countries to be displayed in the UI
        /// </summary>
        /// <returns>List of countries</returns>
        public static async Task<List<Country>> GetCountriesListAsync(IProgress<ProgressReportService> progress)
        {
            DataFlow data = new DataFlow();
            List<Country> Countries = new List<Country>();
            Countries = await data.ReturnCountriesData();
            

            string PathImage = Path.Combine($@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures)}\Images\FlagImages");
            DirectoryInfo dir = new DirectoryInfo(PathImage);
            var files = dir.GetFiles();
            ProgressReportService report = new ProgressReportService();
            List<Country> temp = new List<Country>();
            if (Countries != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        Parallel.ForEach<Country>(Countries, (country) =>
                        {
                            foreach (var file in files)
                            {
                                if (file.Name.Contains(country.Name))
                                {
                                    country.Image = new BitmapImage(new Uri(file.FullName));
                                    country.Image.Freeze();
                                    report.DataLoaded = Countries;
                                }
                                temp.Add(country);
                            }


                            report.PercComplete = (temp.Count * 250) / Countries.Count;
                            progress.Report(report);

                        });
                    });
                }
                catch 
                {

                   
                }
              
            }
            return Countries;
        }

        /// <summary>
        /// This method returns Rates from API to be presented in UI
        /// </summary>
        /// <returns> List of Rates</returns>
        public static async Task<List<Rate>> GetRatesAsync()
        {
            DataFlow RatesData = new DataFlow();
            List<Rate> Rates = await RatesData.GetRatesAPIDataAsync();
            return Rates;
        }

        /// <summary>
        /// This method searches for specific Rate of a selected country
        /// </summary>
        /// <param name="country"></param>
        /// <returns>List of string currency name</returns>
        public static List<CalcRate> ShowCountryRates(Country country, List<Rate> rates)
        {
            List<CalcRate> ratesListCountry = new List<CalcRate>();

            if (rates != null && country!=null)
            {

                foreach (var currency in country.Currencies)
                {
                   var list= rates.FirstOrDefault(r => r.Code == currency.Code);

                    if (list!=null)
                    {
                        ratesListCountry.Add(new CalcRate
                        {
                            RateId = list.RateId,
                            Name = list.Name,
                            Code = list.Code,
                            Symbol = currency.Symbol,
                            TaxRate = list.TaxRate
                        });
                    }
                }
            }
            return ratesListCountry;
        }

        /// <summary>
        /// This method presents all the rates available for a specific currency
        /// </summary>
        /// <param name="country"></param>
        /// <param name="OriginRate"></param>
        /// <returns> list of string currency name</returns>
        public static List<CalcRate> ShowAllCoutryRates(Country country, List<Rate> rates)
        {
            List<CalcRate> AllCountriesRates = new List<CalcRate>();


            if (rates != null && country != null)
            {
                foreach (var currency in country.Currencies)
                {
                    var list = rates.Where(r => r.Code != currency.Code);
                    foreach (var rate in list)
                    {
                        
                            AllCountriesRates.Add(new CalcRate
                            {
                                RateId = rate.RateId,
                                Name = rate.Name,
                                Code = rate.Code,
                                Symbol = currency.Symbol,
                                TaxRate = rate.TaxRate
                            });
                    }
                }
            }
            return AllCountriesRates;
        }

        /// <summary>
        /// methos used to calculate a specific rate
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal CalculateRate(CalcRate origin, CalcRate destination, decimal value  )
        {
            decimal calcRate = 0;
            if (origin != null && destination !=null)
            {
                calcRate = value / (decimal)origin.TaxRate * (decimal)destination.TaxRate;
            }
            return calcRate;
        }

        /// <summary>
        /// gets a list of Comments to be presented in the UI
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public static async Task<List<Comment>> GetCommentsData(Country country)
        {
            DataFlow CommentsData = new DataFlow();
            List<Comment> Comments = await CommentsData.GetCommentsAPIAsync(country);
            return Comments;
        }

     
    }
}
