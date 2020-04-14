namespace InfoCountries
{
    using InfoCountries.Data;
    using Models;
    using Services;
    using Svg;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;
    using Image = System.Windows.Controls.Image;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Atributtes
        private List<Country> Countries;

        #endregion


        public MainWindow()
        {
            InitializeComponent();
            UISettings();
        }

        public async void UISettings()
        {
            
            await SetApiData.LoadCountries();
            Countries = await Task.Run(()=> SetApiData.GetCountriesList()) ;
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
                    }
                }
            }
            this.dataTemplate.ItemsSource = Countries;
        }
    }
}
