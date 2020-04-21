namespace InfoCountries
{
    using InfoCountries.Data;
    using InfoCountries.Services;
    using Models;
    using System;
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
        Progress<ProgressReportService> progress = new Progress<ProgressReportService>();

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
            Loading.Visibility = Visibility.Visible;
            ProgressReportBar.Visibility = Visibility.Visible;
            progress.ProgressChanged += ReportProgress;
            Countries = await UIData.GetCountriesList(await GetApiData.LoadAPiAsync(), progress);
            ListBoxCountries.ItemsSource = Countries;
            ProgressReportBar.Visibility = Visibility.Collapsed;
            Loading.Visibility = Visibility.Collapsed;
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
    }
}
