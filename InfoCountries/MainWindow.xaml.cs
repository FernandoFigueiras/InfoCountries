namespace InfoCountries
{
    using InfoCountries.Data;
    using InfoCountries.Services;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Configuration;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Atributtes
        private List<Country> Countries;

        public DataBaseServices DataBase { get; set; }
        #endregion
        Progress<ProgressReportService> progress = new Progress<ProgressReportService>();

        private bool load = false;

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
            DataBase = new DataBaseServices();
            Loading.Visibility = Visibility.Visible;
            ProgressReportBar.Visibility = Visibility.Visible;
            progress.ProgressChanged += ReportProgress;
            Countries = await UIData.GetCountriesList(await GetApiData.LoadAPiAsync(), progress);
            ListBoxCountries.ItemsSource = Countries;
            ProgressReportBar.Visibility = Visibility.Collapsed;
            Loading.Visibility = Visibility.Collapsed;
            await DataManagement(Countries);
        }

        public async Task DataManagement(List<Country> countries)
        {

            if (Countries != null)
            {
                await Task.Run(() => DataBase.SaveDataBase(Countries));
            }
            else
            {
                Countries = await UIData.GetCountriesList(DataBase.GetData(progress), progress);
            }
            if (Countries.Count==0)
            {
                MessageService.ShowMessage("Atenção", "A sua primeira utilização desta aplicação requer uma ligação à internet válida. Por favor tente mais tarde");
            }




        }

        private void ReportProgress(object sender, ProgressReportService e)
        {
            ProgressReportBar.Value = e.PercComplete;
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

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxSearch.Text))
            {
                List<Country> SearchCountries = new List<Country>();
                foreach (Char item in TextBoxSearch.Text)
                {
                    SearchCountries = Countries.Where(n => n.Name.ToLower().StartsWith(TextBoxSearch.Text.ToLower())).ToList();
                }

                ListBoxCountries.ItemsSource = SearchCountries;
            }
            else
            {
                ListBoxCountries.ItemsSource = Countries;
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxSearch.Text))
            {
                TextBoxSearch.Text = string.Empty;
            }
        }
    }
}
