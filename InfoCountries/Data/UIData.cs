namespace InfoCountries.Data
{
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.IO;
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
        public static async Task<List<Country>> GetCountriesList(IProgress<ProgressReportService> progress)
        {
            DataFlow data = new DataFlow();
            List<Country> Countries = new List<Country>();
            Countries = await data.ReturnData();

            string PathImage = Path.Combine($@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures)}\Images\FlagImages");
            DirectoryInfo dir = new DirectoryInfo(PathImage);
            var files = dir.GetFiles();
            ProgressReportService report = new ProgressReportService();

            if (Countries != null)
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
                                break;
                            }
                        }
                        report.DataLoaded = Countries;
                        report.PercComplete = (Countries.Count * 100) / Countries.Count;
                        progress.Report(report);

                    });
                });
            }
            return Countries;
        }

        /// <summary>
        /// This method returns Rates from API to be presented in UI
        /// </summary>
        /// <returns> List of Rates</returns>
        public static async Task<List<Rate>> ShowRates()
        {
            DataFlow RatesData = new DataFlow();
            List<Rate> Rates = await RatesData.GetRatesAPIDataAsync();
            return Rates;
        }



        /// <summary>
        /// This method searches for specific Rate of a coutry selected
        /// </summary>
        /// <param name="country"></param>
        /// <returns>List of string currency name</returns>
        public static async Task<List<Rate>> ShowCountryRates(Country country)
        {
            List<Rate> Rates = new List<Rate>();
            Rates = await ShowRates();
            List<Rate> ratesListCountry = new List<Rate>();

            if (Rates != null)
            {
                foreach (var currency in country.Currencies)
                {
                    foreach (var Rate in Rates)
                    {
                        if (currency.Code == Rate.Code)
                        {
                            ratesListCountry.Add(Rate);//This is the name of the currencies that each country has
                        }
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
        public static async Task<List<Rate>> ShowAllCoutryRates(Country country, List<Rate> OriginRate)
        {
            List<Rate> Rates = new List<Rate>();
            Rates = await ShowRates();
            List<Rate> AllCountriesRates = new List<Rate>();
            if (OriginRate.Count != 0)//If no information is available for a specific currency
            {

                if (Rates != null)
                {
                    foreach (var currency in country.Currencies)
                    {
                        foreach (var Rate in Rates)
                        {
                            if (currency.Code != Rate.Code)
                            {
                                AllCountriesRates.Add(Rate);//This is all currencies except the one of the country chosen
                            }
                        }
                    }
                }
            }
            return AllCountriesRates;
        }


        public static decimal CalculateRate(Rate origin, Rate destination, decimal value  )
        {
            decimal calcRate = 0;
            if (origin != null && destination !=null)
            {
                calcRate = value / (decimal)origin.TaxRate * (decimal)destination.TaxRate;
            }
            return calcRate;
        }


    }
}
