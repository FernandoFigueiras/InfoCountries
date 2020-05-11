namespace InfoCountries.Data
{
    using Models;
    using Services;
    using Svg;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Threading.Tasks;
    using System.Web.Caching;
    using System.Windows;

    /// <summary>
    /// This class is used to get and store data from the API
    /// </summary>
    public static class GetApiData
    {
        public static List<Country> Countries;

        public static List<Rate> Rates;

        /// <summary>
        /// Gets the list of countries from API
        /// </summary>
        /// <returns>Task</returns>
        public static async Task<List<Country>> LoadCountriesFromAPIAsync()
        {
            ApiService apiService = new ApiService();
            var response = await apiService.GetCountriesAsync("http://restcountries.eu", "/rest/v2/all");
            Countries = (List<Country>)response.Result;
            await SaveImagesFrompApiAsync();
            return Countries;
        }

        /// <summary>
        /// Gets the list of rates from API
        /// </summary>
        /// <returns>list of Rate</returns>
        public static async Task<List<Rate>> LoadRatesFromAPIAsync()
        {
            ApiService apiService = new ApiService();
            var response = await apiService.GetRatesAsync("https://cambiosrafa.azurewebsites.net", "/api/rates");
            Rates = (List<Rate>)response.Result;
            return Rates;
        }


        /// <summary>
        /// Gets flag images from API and stores as SVG in directory
        /// </summary>
        /// <returns>Task</returns>
        public static async Task SaveImagesFrompApiAsync()
        {
            string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
            string ImagePath = Path.Combine(rootPath, @"Images\");
            

            if (!Directory.Exists(ImagePath))
            {
                Directory.CreateDirectory(ImagePath);
            }
            DirectoryInfo directory = new DirectoryInfo(ImagePath);

            if (directory.GetFiles().Length == 0)
            {
                await Task.Run(() =>
                {
                    Parallel.ForEach<Country>(Countries, (country) =>
                    {
                        try
                        {
                            using (WebClient client = new WebClient())
                            {
                                var flagImage = client.DownloadString(country.Flag);
                                string pathFile = $@"{ImagePath}{country.Name}.svg";
                                File.WriteAllText(pathFile, flagImage);
                            }
                        }
                        catch
                        {

                        }
                    });
                });
                await SaveConvertedflagImagesAsync();
            }
        }

        /// <summary>
        /// Gets the Flag Images from the Directory, and stores the converted image into a new directory.
        /// </summary>
        /// <returns>Task</returns>
        public static async Task SaveConvertedflagImagesAsync()
        {
            string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
            string ImagePath = Path.Combine(rootPath, @"Images\");
            string path = Path.Combine(ImagePath, @"FlagImages\");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            List<string> files = Directory.GetFiles(ImagePath).ToList();
            if (files.Count > 0)
            {
                await Task.Run(() =>
                {
                    Parallel.ForEach<string>(files, (file) =>
                    {
                        FileInfo datFile = new FileInfo(file);
                        try
                        {
                            SvgDocument svgDocument = SvgDocument.Open(datFile.FullName);

                            using (Bitmap bitmap = svgDocument.Draw(100, 100))
                            {
                                bitmap.Save($"{path}{Path.GetFileNameWithoutExtension(file)}.{ImageFormat.Jpeg}");
                            }
                        }
                        catch
                        {
                        }
                    });
                });
            }
        }


    }
}

