using InfoCountries.Models;
using Svg;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InfoCountries.Data
{
    public static class UIData
    {
        /// <summary>
        /// Sets the list of countries to be displayed in the UI
        /// </summary>
        /// <returns></returns>
        public static async Task< List<Country>> GetCountriesList(List<Country> Countries)
        {
            string PathImage = Path.Combine($@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures)}\Images\FlagImages");
            DirectoryInfo dir = new DirectoryInfo(PathImage);
            var files = dir.GetFiles();


            if (Countries != null)
            {
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
            }
            return Countries;
        }

        //public static Country ShowCountryInfo(Country country)
        //{
        //    var _country = country;
        //    string PathImage = Path.Combine($@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures)}\Images\FlagImages");
        //    DirectoryInfo dir = new DirectoryInfo(PathImage);
        //    var files = dir.GetFiles();
        //    foreach (var file in files)
        //    {
        //        if (file.Name.Contains(_country.Name))
        //        {
        //            BitmapImage sourceBitmap = new BitmapImage(new Uri(file.FullName));
        //            var targetBitmap = new TransformedBitmap(sourceBitmap, new ScaleTransform(100, 100));
        //            _country.ImageChange = targetBitmap;
        //            _country.Image.Freeze();
        //            break;
        //        }
        //    }
        //    return _country;
        //}

        //public static List<Country> DeleteFlags(List<Country> countries)
        //{
        //    foreach (var country in countries)
        //    {
        //        country.ImageChange = null;
        //    }
        //    return countries;
        //}
    }
}
