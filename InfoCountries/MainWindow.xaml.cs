namespace InfoCountries
{
    using InfoCountries.Data;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

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
            Task.FromResult(UISettings());

            //Task.WaitAll(test);
            // teste(test);
            //if (test.IsCompleted)
            //{
            //    MessageBox.Show("Test");
            //}
            //else
            //{
            //    MessageBox.Show("teste1");
            //}

        }

        public async Task UISettings()
        {
            Countries = await UIData.GetCountriesList(await GetApiData.LoadAPiAsync());
            ListBoxCountries.ItemsSource = Countries;
        }

        private void ListBoxCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxCountries.SelectedItem != null)
            {
                Country country = (Country)ListBoxCountries.SelectedItem;
                this.ContentControl.DataContext = country;
                TextPais.Visibility = Visibility.Visible;
                TextCapital.Visibility = Visibility.Visible;
                TextRegion.Visibility = Visibility.Visible;
                TextSubRegion.Visibility = Visibility.Visible;
                TextPopulation.Visibility = Visibility.Visible;
                TextGini.Visibility = Visibility.Visible;
            }
        }
    }
}
