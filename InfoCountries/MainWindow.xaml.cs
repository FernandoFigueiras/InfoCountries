namespace InfoCountries
{
    using InfoCountries.Data;
    using InfoCountries.Models;
    using InfoCountries.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Data.Linq;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.WebSockets;
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

        private List<Country> Africa = new List<Country>();
        private List<Country> America = new List<Country>();
        private List<Country> Asia = new List<Country>();
        private List<Country> Europe = new List<Country>();
        private List<Country> Oceania = new List<Country>();
        private List<Country> ShowCountry = new List<Country>();

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


        private void btn_image_from_all_Click(object sender, RoutedEventArgs e)
        {
            Button btn_image = sender as Button;
            var country = (Country)btn_image.DataContext;
            if (country!= null)
            {
                ShowCountryInformation(country);
                gb_info_start.Visibility = Visibility.Collapsed;
                tb_seach.IsEnabled = false;
            }
        }

        private void ShowCountryInformation(Country country)
        {
            ShowCountry = new List<Country>();
            gb_country_details.Visibility = Visibility.Visible;
            ShowCountry.Add(country);
            country_details.ItemsSource = ShowCountry;
        }

        private void btn_infoBack_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btn_postBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tb_seach_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_seach.Text))
            {
                close_search.Visibility = Visibility.Visible;
                if (Africa.Count>0)
                {
                    List<Country> SearchCountries = new List<Country>();
                    SearchCountries = Africa.Where(n => n.Name.ToLower().StartsWith(tb_seach.Text.ToLower())).ToList();
                    total_countries_info.ItemsSource = SearchCountries;
                }
                else if (America.Count>0)
                {
                    List<Country> SearchCountries = new List<Country>();
                    SearchCountries = America.Where(n => n.Name.ToLower().StartsWith(tb_seach.Text.ToLower())).ToList();
                    total_countries_info.ItemsSource = SearchCountries;
                }
                else if (Asia.Count>0)
                {
                    List<Country> SearchCountries = new List<Country>();
                    SearchCountries = Asia.Where(n => n.Name.ToLower().StartsWith(tb_seach.Text.ToLower())).ToList();
                    total_countries_info.ItemsSource = SearchCountries;
                }
               else if (Europe.Count>0)
                {
                    List<Country> SearchCountries = new List<Country>();
                    SearchCountries = Europe.Where(n => n.Name.ToLower().StartsWith(tb_seach.Text.ToLower())).ToList();
                    total_countries_info.ItemsSource = SearchCountries;
                }
                else if (Oceania.Count>0)
                {
                    List<Country> SearchCountries = new List<Country>();
                    SearchCountries = Oceania.Where(n => n.Name.ToLower().StartsWith(tb_seach.Text.ToLower())).ToList();
                    total_countries_info.ItemsSource = SearchCountries;
                }
                else
                {
                    List<Country> SearchCountries = new List<Country>();
                    SearchCountries = Countries.Where(n => n.Name.ToLower().StartsWith(tb_seach.Text.ToLower())).ToList();
                    total_countries_info.ItemsSource = SearchCountries;
                }
            }
            else
            {
                close_search.Visibility = Visibility.Collapsed;
                if (Africa.Count > 0)
                {
                    total_countries_info.ItemsSource = Africa;
                }
                else if (America.Count > 0)
                {
                    total_countries_info.ItemsSource = America;
                }
                else if (Asia.Count > 0)
                {
                    total_countries_info.ItemsSource = Asia;
                }
                else if (Europe.Count > 0)
                {
                    total_countries_info.ItemsSource = Europe;
                }
                else if (Oceania.Count > 0)
                {
                    total_countries_info.ItemsSource = Oceania;
                }
                else
                {
                    total_countries_info.ItemsSource = Countries;
                }
                
            }
        }

        private void btn_info_Click(object sender, RoutedEventArgs e)
        {

        }

        //___________________________________________________ TOP Buttons Controls
        private void btn_all_Click(object sender, RoutedEventArgs e)
        {
            total_countries_info.ItemsSource = Countries;
            Africa.Clear();
            America.Clear();
            Asia.Clear();
            Europe.Clear();
            Oceania.Clear();
        }
        private void btn_africa_Click(object sender, RoutedEventArgs e)
        {
            string search = "Africa";
            Africa = Countries.Where(x => x.Region == search).ToList();
            total_countries_info.ItemsSource = Africa;
            Asia.Clear();
            America.Clear();
            Europe.Clear();
            Oceania.Clear();
        }
        private void btn_america_Click(object sender, RoutedEventArgs e)
        {
            string _america = "Americas";
            America = Countries.Where(c => c.Region == _america).ToList();
            total_countries_info.ItemsSource = America;
            Africa.Clear();
            Asia.Clear();
            Europe.Clear();
            Oceania.Clear();
        }
        private void btn_asia_Click(object sender, RoutedEventArgs e)
        {
            string _asia = "Asia";
            Asia = Countries.Where(c => c.Region == _asia).ToList();
            total_countries_info.ItemsSource = Asia;
            Africa.Clear();
            America.Clear();
            Europe.Clear();
            Oceania.Clear();
        }
        private void btn_europa_Click(object sender, RoutedEventArgs e)
        {
            string _europ = "Europe";
            Europe = Countries.Where(c => c.Region == _europ).ToList();
            total_countries_info.ItemsSource = Europe;
            Africa.Clear();
            Asia.Clear();
            America.Clear();
            Oceania.Clear();
        }
        private void btn_oceania_Click(object sender, RoutedEventArgs e)
        {
            string _oceania = "Oceania";
            Oceania = Countries.Where(c => c.Region == _oceania).ToList();
            total_countries_info.ItemsSource = Oceania;
            Africa.Clear();
            Asia.Clear();
            America.Clear();
            Europe.Clear();
        }
        //__________________________________________________________________________

        private void close_search_Click(object sender, RoutedEventArgs e)
        {
            tb_seach.Text = string.Empty;
            if (Africa.Count>0)
            {
                total_countries_info.ItemsSource = Africa;
            }
            else if (America.Count>0)
            {
                total_countries_info.ItemsSource = America;
            }
            else if (Asia.Count>0)
            {
                total_countries_info.ItemsSource = Asia;
            }
            else if (Europe.Count>0)
            {
                total_countries_info.ItemsSource = Europe;
            }
            else if (Oceania.Count>0)
            {
                total_countries_info.ItemsSource = Oceania;
            }
            else
            {
                total_countries_info.ItemsSource = Countries;
            }
        }

        private void btn_backDetail_Click(object sender, RoutedEventArgs e)
        {
            gb_country_details.Visibility = Visibility.Collapsed;
            gb_info_start.Visibility = Visibility.Visible;
            tb_seach.IsEnabled = true;
            country_details.ItemsSource = null;
            ShowCountry.Clear();
        }

        private void btn_rate_Click(object sender, RoutedEventArgs e)
        {
            if (ShowCountry.Count==0)
            {
                return;
            }
            Country country = ShowCountry.FirstOrDefault();
            SetRatesWindow(country);

            gb_rate.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// Method used to populate window information about Country Currency and Rates
        /// </summary>
        /// <param name="country"></param>
        private void SetRatesWindow(Country country)
        {
            tb_insertRate.IsReadOnly = false;
            btnCalcRate.IsEnabled = true;
            btnChangeRate.IsEnabled = true;
            Rate_details.ItemsSource = null;
            bt_deleteRateInsert.IsEnabled = true;
            var currency = country.Currencies;

            Rate_details.ItemsSource = null;
            Rate_details.ItemsSource = currency;
            List<CalcRate> CountryRate = new List<CalcRate>();
            
            CountryRate = Task.Run(() => UIData.ShowCountryRates(country, Rates)).Result;
            
            cb_ActiveCountryRate.DisplayMemberPath = "Name";
            List<CalcRate> allRates = new List<CalcRate>();
            allRates = Task.Run(() => UIData.ShowAllCoutryRates(country,  Rates)).Result;
            if (CountryRate.Count == 0)
            {
                CountryRate.Add(new CalcRate {
                    Name = "Informação indísponível",
                });
                cb_ActiveCountryRate.ItemsSource = CountryRate;
                cb_ActiveCountryRate.DisplayMemberPath = "Name";
                tb_insertRate.IsReadOnly = true;
                btnCalcRate.IsEnabled = false;
                btnChangeRate.IsEnabled = false;
                bt_deleteRateInsert.IsEnabled=false;
                allRates.Clear();
            }
            else
            {
                cb_ActiveCountryRate.ItemsSource = CountryRate;
                cb_ActiveCountryRate.DisplayMemberPath = "Name";
                
            }

            if (allRates.Count == 0)
            {
                allRates.Add(new CalcRate
                {
                    Name = "Informação indísponível",
                });
                cb_AllcountriesRate.ItemsSource = allRates;
                cb_AllcountriesRate.DisplayMemberPath = "Name";

            }
            else
            {
                cb_AllcountriesRate.ItemsSource = allRates;
                cb_AllcountriesRate.DisplayMemberPath = "Name";
            }
        }

        private void btn_post_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_backRate_Click(object sender, RoutedEventArgs e)
        {
            gb_rate.Visibility = Visibility.Collapsed;
            gb_country_details.Visibility = Visibility.Visible;
            Rate_details.ItemsSource = null;
            tb_insertRate.Text = string.Empty;
            tb_rateResult.Text = string.Empty;
        }

        private void btnCalcRate_Click(object sender, RoutedEventArgs e)
        {
            CalcRate origin = (CalcRate)cb_ActiveCountryRate.SelectedItem;
            CalcRate destination = (CalcRate)cb_AllcountriesRate.SelectedItem;
            if (origin != null && destination != null)
         
                if (string.IsNullOrWhiteSpace(tb_insertRate.Text))
                {
                    MessageBox.Show("Por favor insira um valor a converter");
                    return;
                }
                
                string input = tb_insertRate.Text;
                if (Regex.IsMatch(input, @"^-?[0-9][0-9,\.]+$"))
                {
                
                
                    tb_rateResult.Text =$"{tb_insertRate.Text} {origin} correspondem a: {Environment.NewLine} {Convert.ToString(decimal.Round(Convert.ToDecimal(UIData.CalculateRate(origin, destination, Convert.ToDecimal(input))), 2))} {destination.Name}";
                }
                else
                {
                    MessageBox.Show("O valor a converter tem de ser numerico");
                    return;
                }
            }

        private void btnChangeRate_Click(object sender, RoutedEventArgs e)
        {
            List<CalcRate> origin = (List<CalcRate>)cb_ActiveCountryRate.ItemsSource;
            List<CalcRate> destination = (List<CalcRate>)cb_AllcountriesRate.ItemsSource;
            var change = origin;
            origin = destination;
            destination = change;
            cb_ActiveCountryRate.ItemsSource = origin;
            cb_AllcountriesRate.ItemsSource = destination;
        }

        private void bt_deleteRateInsert_Click(object sender, RoutedEventArgs e)
        {
            tb_insertRate.Text = string.Empty;
            tb_rateResult.Text = string.Empty;
        }
    }
    }

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

