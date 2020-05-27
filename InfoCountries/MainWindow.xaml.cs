namespace InfoCountries
{
    using InfoCountries.Data;
    using InfoCountries.Models;
    using InfoCountries.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Country> Countries;
        private List<Rate> Rates;
        private List<Comment> CommentsCountry;

        private Progress<ProgressReportService> progress;
        private OpenApp openApp;

        public MainWindow()
        {
            
            InitializeComponent();
            progress = new Progress<ProgressReportService>();
            openApp = new OpenApp(this, progress);
            openApp.Show();
            this.Hide();
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

            

            Countries = await UIData.GetCountriesListAsync(progress);
            gb_info_start.Visibility = Visibility.Visible;


            Rates = await UIData.GetRatesAsync();
  
            SaveData();
            total_countries_info.DataContext = Countries;
        }

        private void SaveData()
        {
            DataFlow dataFlow = new DataFlow();
            Task.Run(() => dataFlow.SaveData(Countries, Rates));
        }


        private void ListBoxCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //cb_countryRate.ItemsSource = null;
            //cb_rates.ItemsSource = null;
            //if (ListBoxCountries.SelectedItem != null)
            //{
            //    Country country = (Country)ListBoxCountries.SelectedItem;
            //    this.ContentControl.DataContext = country;
            //    TextPais.Visibility = Visibility.Visible;
            //    TextCapital.Visibility = Visibility.Visible;
            //    TextRegion.Visibility = Visibility.Visible;
            //    TextSubRegion.Visibility = Visibility.Visible;
            //    TextPopulation.Visibility = Visibility.Visible;
            //    TextGini.Visibility = Visibility.Visible;
            //    border.Visibility = Visibility.Visible;
            //    List<Rate> CountryRate = new List<Rate>();
            //    CountryRate = Task.Run(() => UIData.ShowCountryRates(country, Rates)).Result;
            //    List<Rate> allRates = new List<Rate>();
            //    allRates = Task.Run(() => UIData.ShowAllCoutryRates(country, CountryRate, Rates)).Result;
            //    if (CountryRate.Count == 0)
            //    {
            //        Rate empty = new Rate();
            //        empty.Name = "Informação indísponível";
            //        CountryRate.Add(empty);
            //        cb_countryRate.ItemsSource = CountryRate;
            //        allRates.Clear();
            //    }
            //    else
            //    {
            //        cb_countryRate.ItemsSource = CountryRate;
            //    }

            //    if (allRates.Count == 0)
            //    {
            //        Rate empty = new Rate();
            //        empty.Name = "Informação indísponível";
            //        allRates.Add(empty);
            //        cb_rates.ItemsSource = allRates;
            //    }
            //    else
            //    {

            //        cb_rates.ItemsSource = allRates;
            //    }
            //    CommentsCountry = Task.Run(() => UIData.GetCommentsData(country)).Result;


            //    textComment.Text = "";
            //    foreach (Comment comment in CommentsCountry)
            //    {
            //        textComment.Text += comment.Comments + Environment.NewLine;

            //    }
            }



        private void btn_image_from_all_Click(object sender, RoutedEventArgs e)
        {
            Button btn_image = sender as Button;
            var country = (Country)btn_image.DataContext;
            ShowCountryInformation(country);
            gb_info_start.Visibility = Visibility.Hidden;
            tb_seach.IsEnabled = false;
        }

        private void ShowCountryInformation(Country country)
        {
            List<Country> ShowCountry = new List<Country>();
            gb_country_details.Visibility = Visibility.Visible;
            ShowCountry.Add(country);
            country_details.DataContext = ShowCountry;
        }


        private void btn_backCDetail_Click(object sender, RoutedEventArgs e)
        {
            gb_country_details.Visibility = Visibility.Hidden;
            gb_info_start.Visibility = Visibility.Visible;
            tb_seach.IsEnabled = true;
        }

        private void btn_infoBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_rateBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_postBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tb_seach_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_seach.Text))
            {
                List<Country> SearchCountries = new List<Country>();
                foreach (Char item in tb_seach.Text)
                {
                    SearchCountries = Countries.Where(n => n.Name.ToLower().StartsWith(tb_seach.Text.ToLower())).ToList();
                }

                total_countries_info.ItemsSource = SearchCountries;
            }
            else
            {
                total_countries_info.ItemsSource = Countries;
            }
        }
    }



    //    private void btn_CalcRate_Click(object sender, RoutedEventArgs e)
    //    {

    //        if (cb_countryRate != null && cb_rates != null && !string.IsNullOrWhiteSpace(tb_rate.Text))
    //        {
    //            Rate origin = (Rate)cb_countryRate.SelectedItem;
    //            Rate destination = (Rate)cb_rates.SelectedItem;
    //            string input = tb_rate.Text;
    //            if (Regex.IsMatch(input, @"^-?[0-9][0-9,\.]+$"))
    //            {
    //                lbl_rate.Content = decimal.Round(Convert.ToDecimal(UIData.CalculateRate(origin, destination, Convert.ToDecimal(input))), 2);
    //                lbl_origin.Content = tb_rate.Text + " " + origin.Name;
    //                lbl_destination.Content = destination.Name;
    //            }
    //        }
    //        tb_rate.Text = string.Empty;
    //    }

    //    private void btn_change_coutryRate_Click(object sender, RoutedEventArgs e)
    //    {
    //        var originRate = cb_countryRate.ItemsSource;
    //        var destinationRate = cb_rates.ItemsSource;
    //        var change = originRate;
    //        originRate = destinationRate;
    //        destinationRate = change;
    //        cb_countryRate.ItemsSource = originRate;
    //        cb_rates.ItemsSource = destinationRate;
    //    }

    //    private void post_Click(object sender, RoutedEventArgs e)
    //    {
    //        Country country = (Country)ListBoxCountries.SelectedItem;
    //        DataFlow dataFlow = new DataFlow();

    //        if (!string.IsNullOrWhiteSpace(posttext.Text) && country != null)
    //        {
    //            Comment comment = new Comment
    //            {
    //                Alphacode = country.Alpha2Code,
    //                Comments = posttext.Text,
    //            };
    //            Task.Run(() => dataFlow.PostCommentsData(country, comment));
    //        }
    //        return;
    //    }
    //}
}
