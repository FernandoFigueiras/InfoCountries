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
using System.Drawing;
using System.Windows.Media;
using System.Threading;

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
                await Task.Run(() => SaveFlagToDirectory());
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
            string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
            string ImagePath = Path.Combine(rootPath, @"Images\");

            if (!Directory.Exists(ImagePath))
            {
                Directory.CreateDirectory(ImagePath);
            }
            DirectoryInfo directory = new DirectoryInfo(ImagePath); 
            if (directory.GetFiles().Length == 0)
            {
                foreach (var country in Countries)
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
                    catch (Exception e)
                    {

                        MessageBox.Show(e.Message);
                    }

                }
                GetImageFromDirectory();
            }
        }

        public static void GetImageFromDirectory()
        {
            string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
            string ImagePath = Path.Combine(rootPath, @"Images\");
            string path = Path.Combine(ImagePath, @"FlagImages\");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var files = Directory.GetFiles(ImagePath);
            if (files.Length>0)
            {
                foreach (var file in files)
                {
                    FileInfo datFile = new FileInfo(file);
                    try
                    {
                        var svgDocument = SvgDocument.Open(datFile.FullName);
                        using (var bitmap = svgDocument.Draw(100, 100))
                        {
                            bitmap.Save($"{path}{Path.GetFileNameWithoutExtension(file)}.{ImageFormat.Jpeg}");

                        }
                    }
                    catch (Exception e)
                    {

                        MessageBox.Show(e.Message);
                    }
                }

            }
        }

        public static List<Country> GetCountriesList()
        {
            return Countries;
        }

    }
}
       
