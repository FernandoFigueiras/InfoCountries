using InfoCountries.Models;
using InfoCountries.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using Svg;
using System.Drawing.Imaging;

namespace InfoCountries.Data
{
    public static class SetApiData
    {

        public static List<Country> Countries;

        public static async Task LoadCountries()
        {
            NetworkService networkService = new NetworkService();
            var connection = networkService.CheckConnection();

            if (!connection.IsSuccess)
            {
                MessageBox.Show(connection.Message);
                return;
            }
            else
            {
                await LoadAPICountries();
                SaveFlagToDirectory();
            }
            
        }

        private static async Task LoadAPICountries()
        {
            ApiService apiService = new ApiService();
            var response = await apiService.GetCountries("http://restcountries.eu", "/rest/v2/all");
            Countries = (List<Country>)response.Result;
            MessageBox.Show("teste");
        }

        public static void SaveFlagToDirectory()
        {
            string path = "FlagsImage";
            Directory.CreateDirectory(path);

            foreach (var country in Countries)
            {
                using (WebClient client = new WebClient())
                {
                    var flagImage = client.DownloadString(country.Flag);
                    string pathFile = $@"{path}\{country.Name}.svg";
                    File.WriteAllText(pathFile, flagImage);
                }
            }
        }

        public static async Task<List<Country>> GetCountriesList()
        {
            return Countries;
        }
    }
}
