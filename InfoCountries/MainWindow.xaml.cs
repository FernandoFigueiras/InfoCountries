namespace InfoCountries
{
    using InfoCountries.Data;
    using InfoCountries.Models;
    using InfoCountries.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Policy;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Country> Countries;
        private List<Rate> Rates;
        private List<Comment> CommentsCountry; 

        private Progress<ProgressReportService> progress;
        public MainWindow()
        {

            InitializeComponent();
            progress = new Progress<ProgressReportService>();
            load();
        }

        private void load()
        {
            Task.FromResult(UISettings());

        }
        /// <summary>
        /// Runs all the necessary methods to get info To main window
        /// </summary>
        /// <returns></returns>
        public async Task UISettings()
        {
            border.Visibility = Visibility.Hidden;
            Loading.Visibility = Visibility.Visible;
            ProgressReportBar.Visibility = Visibility.Visible;
            progress.ProgressChanged += ReportProgress;
            Countries = await UIData.GetCountriesList(progress);
            ListBoxCountries.ItemsSource = Countries;
            Rates = await UIData.GetRatesAsync();
            ProgressReportBar.Visibility = Visibility.Collapsed;
            Loading.Visibility = Visibility.Collapsed;
            SaveData();

        }

        private void SaveData()
        {
            DataFlow dataFlow = new DataFlow();
            Task.Run(() => dataFlow.SaveData(Countries, Rates));
        }



        private void ReportProgress(object sender, ProgressReportService e)
        {
            ProgressReportBar.Value = e.PercComplete;
        }


        private void ListBoxCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_countryRate.ItemsSource = null;
            cb_rates.ItemsSource = null;
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
                border.Visibility = Visibility.Visible;
                List<Rate> CountryRate = new List<Rate>();
                CountryRate = Task.Run(() => UIData.ShowCountryRates(country, Rates)).Result;
                List<Rate> allRates = new List<Rate>();
                allRates = Task.Run(() => UIData.ShowAllCoutryRates(country, CountryRate, Rates)).Result;
                if (CountryRate.Count == 0)
                {
                    Rate empty = new Rate();
                    empty.Name = "Informação indísponível";
                    CountryRate.Add(empty);
                    cb_countryRate.ItemsSource = CountryRate;
                    allRates.Clear();
                }
                else
                {
                    cb_countryRate.ItemsSource = CountryRate;
                }

                if (allRates.Count == 0)
                {
                    Rate empty = new Rate();
                    empty.Name = "Informação indísponível";
                    allRates.Add(empty);
                    cb_rates.ItemsSource = allRates;
                }
                else
                {

                    cb_rates.ItemsSource = allRates;
                }
                CommentsCountry = Task.Run(() => UIData.GetCommentsData(country)).Result;


                textComment.Text = "";
                foreach (Comment comment in CommentsCountry)
                {
                    textComment.Text += comment.Comments + Environment.NewLine;
                    
                }
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

        private void btn_CalcRate_Click(object sender, RoutedEventArgs e)
        {

            if (cb_countryRate != null && cb_rates != null && !string.IsNullOrWhiteSpace(tb_rate.Text))
            {
                Rate origin = (Rate)cb_countryRate.SelectedItem;
                Rate destination = (Rate)cb_rates.SelectedItem;
                string input = tb_rate.Text;
                if (Regex.IsMatch(input, @"^-?[0-9][0-9,\.]+$"))
                {
                    lbl_rate.Content = decimal.Round(Convert.ToDecimal(UIData.CalculateRate(origin, destination, Convert.ToDecimal(input))), 2);
                    lbl_origin.Content = tb_rate.Text + " " + origin.Name;
                    lbl_destination.Content = destination.Name;
                }
            }
            tb_rate.Text = string.Empty;
        }

        private void btn_change_coutryRate_Click(object sender, RoutedEventArgs e)
        {
            var originRate = cb_countryRate.ItemsSource;
            var destinationRate = cb_rates.ItemsSource;
            var change = originRate;
            originRate = destinationRate;
            destinationRate = change;
            cb_countryRate.ItemsSource = originRate;
            cb_rates.ItemsSource = destinationRate;
        }

        private void post_Click(object sender, RoutedEventArgs e)
        {
            Country country = (Country)ListBoxCountries.SelectedItem;
            DataFlow dataFlow = new DataFlow();

            if (!string.IsNullOrWhiteSpace(posttext.Text) && country != null)
            {
                Comment comment = new Comment
                {
                    Alphacode = country.Alpha2Code,
                    Comments = posttext.Text,
                };
                Task.Run(() => dataFlow.PostCommentsData(country, comment));
            }
            return;
        }
    }
}
