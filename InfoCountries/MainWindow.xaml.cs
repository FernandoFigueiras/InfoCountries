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
            
            await GetApiData.LoadCountriesAsync();
            Countries = await Task.Run(()=> UIData.GetCountriesList()) ;
            this.dataTemplate.ItemsSource = Countries;
        }
    }
}
