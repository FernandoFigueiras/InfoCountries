namespace InfoCountries
{
    using InfoCountries.Data;
    using Models;
    using Services;
    using Svg;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Atributtes
        private NetworkService networkService;

        private ApiService apiService;

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
            ComboBoxDisplayCountries.ItemsSource = Countries;
        }

    }
}
