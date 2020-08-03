namespace InfoCountries.Data
{
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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
            report.PercComplete = 0;
            if (Countries != null)
            {
                List<Country> temp = new List<Country>();
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
                                    if (country.Gini == null)
                                    {
                                        country.Gini = "Information not available";
                                    }
                                    if (country.Capital == "")
                                    {
                                        country.Capital = "Information not available";
                                    }
                                    if (country.Region == "")
                                    {
                                        country.Region = "Information not available";
                                    }
                                    if (country.Subregion == "")
                                    {
                                        country.Subregion = "Information not available";
                                    }

                                    country.Image = new BitmapImage(new Uri(file.FullName));

                                    country.Image.Freeze();
                                    
                                }
                            }
                            

                        });
                    });
                }
                catch
                {


                }
                
            }
            var route = Countries.Where(c => c.Image == null);


            foreach (var item in route)
            {
                Countries.Remove(item);
                item.Image = new BitmapImage(new Uri(@"\Images\no-image-available.jpg", UriKind.Relative));

                Countries.Add(item);
                break;
            }
            report.PercComplete = 100;
            progress.Report(report);
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

            if (rates != null && country.Currencies != null)
            {

                foreach (var currency in country.Currencies)
                {
                    var list = rates.FirstOrDefault(r => r.Code == currency.Code);

                    if (list != null)
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


            if (rates != null && country.Currencies != null)
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
        public static decimal CalculateRate(CalcRate origin, CalcRate destination, decimal value)
        {
            decimal calcRate = 0;
            if (origin != null && destination != null)
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
        public static async Task<List<Comment>> GetCommentsData()
        {
            DataFlow dataflow = new DataFlow();
            List<Comment> Comments = await dataflow.GetCommentsAPIAsync();
            return Comments;
        }


    }
}
