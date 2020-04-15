using InfoCountries.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace InfoCountries.Data
{
    public static class UIData
    {
        /// <summary>
        /// Sets the list of countries to be displayed in the UI
        /// </summary>
        /// <returns></returns>
        public static List<Country> GetCountriesList()
        {
            List<Country> Countries;
            Countries = GetApiData.LoadAPICountriesAsync().Result;

            string PathImage = Path.Combine($@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures)}\Images\FlagImages");
            DirectoryInfo dir = new DirectoryInfo(PathImage);
            var files = dir.GetFiles();

            foreach (Country country in Countries)
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
            }

            return Countries;
        }
    }
}
