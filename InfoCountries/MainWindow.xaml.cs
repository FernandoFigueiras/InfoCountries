namespace InfoCountries
{
    using InfoCountries.Data;
    using InfoCountries.Models;
    using InfoCountries.Services;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Color = System.Drawing.Color;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Country> Countries;
        private List<Rate> Rates;


        private Progress<ProgressReportService> progress;
        private OpenApp openApp;
        private About about;
        RatingBar ratingBar;

        private List<Country> Africa = new List<Country>();
        private List<Country> America = new List<Country>();
        private List<Country> Asia = new List<Country>();
        private List<Country> Europe = new List<Country>();
        private List<Country> Oceania = new List<Country>();
        private List<Country> ShowCountry = new List<Country>();
        private List<Comment> CommentsCountry = new List<Comment>();

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
            total_countries_info.DataContext = Countries;

            gb_info_start.Visibility = Visibility.Visible;


            Rates = await UIData.GetRatesAsync();
            CommentsCountry = Task.Run(() => UIData.GetCommentsData()).Result;


            if (Countries.Count > 0 && Rates.Count > 0)
            {
                SaveData();
            }

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
            if (country != null)
            {
                ShowCountryInformation(country);
                gb_info_start.Visibility = Visibility.Collapsed;
                tb_seach.IsEnabled = false;
                SetRatesWindow(country);
            }
        }

        private void ShowCountryInformation(Country country)
        {
            ShowCountry = new List<Country>();
            gb_country_details.Visibility = Visibility.Visible;
            ShowCountry.Add(country);
            country_details.ItemsSource = ShowCountry;

            var list = ShowCountry.Where(c => c.Gini == null).ToList();
        }

        private void ShowMoreInfo()
        {
            Border_info.ItemsSource = null;
            MapImage.DataContext = null;
            var country = ShowCountry.FirstOrDefault();
            CountryinfoRight.DataContext = country;
            tb_nativeName.DataContext = country;
            tb_Capital.DataContext = country;
            tb_Area.DataContext = country;
            var boarders = country.Borders;
            List<Country> boardersCountry = new List<Country>();

            if (boarders.Count == 0)
            {
                boardersCountry.Add(new Country
                {
                    Name = "No borders available",
                    Image = new BitmapImage(new Uri(@"\Images\no-image-available.jpg", UriKind.Relative))

                });
                Border_info.ItemsSource = boardersCountry;
            }
            else
            {
                foreach (var item in boarders)
                {
                    var b = Countries.FirstOrDefault(c => c.Alpha3Code == item);
                    boardersCountry.Add(b);
                }
            }
            Border_info.ItemsSource = boardersCountry;


            string PathImage = Path.Combine($@"{Environment.CurrentDirectory}\MapsGif");
            DirectoryInfo dir = new DirectoryInfo(PathImage);
            var files = dir.GetFiles();

            string path = $@"{country.Name}.gif";


            try
            {
                FileInfo exists = files.FirstOrDefault(f => f.Name == path);
                if (exists != null)
                {
                    if (exists.Exists)
                    {
                        string fullPath = $@"{PathImage}\{path}";
                        MapImage.DataContext = fullPath;
                    }
                    else
                    {
                        string noimagePath = $@"\Images\no-image-available.jpg";
                        MapImage.DataContext = noimagePath;
                    }
                }
                else
                {
                    string noimagePath = $@"\Images\no-image-available.jpg";
                    MapImage.DataContext = noimagePath;
                }

            }
            catch
            {

                string noimagePath = $@"\Images\no-image-available.jpg";
                MapImage.DataContext = noimagePath;
            }



        }

        private void tb_seach_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_seach.Text))
            {
                close_search.Visibility = Visibility.Visible;
                if (Africa.Count > 0)
                {
                    List<Country> SearchCountries = new List<Country>();
                    SearchCountries = Africa.Where(n => n.Name.ToLower().StartsWith(tb_seach.Text.ToLower())).ToList();
                    total_countries_info.ItemsSource = SearchCountries;
                }
                else if (America.Count > 0)
                {
                    List<Country> SearchCountries = new List<Country>();
                    SearchCountries = America.Where(n => n.Name.ToLower().StartsWith(tb_seach.Text.ToLower())).ToList();
                    total_countries_info.ItemsSource = SearchCountries;
                }
                else if (Asia.Count > 0)
                {
                    List<Country> SearchCountries = new List<Country>();
                    SearchCountries = Asia.Where(n => n.Name.ToLower().StartsWith(tb_seach.Text.ToLower())).ToList();
                    total_countries_info.ItemsSource = SearchCountries;
                }
                else if (Europe.Count > 0)
                {
                    List<Country> SearchCountries = new List<Country>();
                    SearchCountries = Europe.Where(n => n.Name.ToLower().StartsWith(tb_seach.Text.ToLower())).ToList();
                    total_countries_info.ItemsSource = SearchCountries;
                }
                else if (Oceania.Count > 0)
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

        private void btn_infoBack_Click(object sender, RoutedEventArgs e)
        {
            gb_moreInfo.Visibility = Visibility.Collapsed;
            gb_country_details.Visibility = Visibility.Visible;
            tb_seach.IsEnabled = true;
            Border_info.ItemsSource = null;
            ShowCountry.Clear();
        }

        private void btn_backDetail_Click(object sender, RoutedEventArgs e)
        {
            gb_country_details.Visibility = Visibility.Collapsed;
            gb_info_start.Visibility = Visibility.Visible;
            tb_seach.IsEnabled = true;
            country_details.ItemsSource = null;
            ShowCountry.Clear();
        }

        private void btn_backComment_Click(object sender, RoutedEventArgs e)
        {
            gb_Comments.Visibility = Visibility.Collapsed;
            gb_country_details.Visibility = Visibility.Visible;
            comments_details.ItemsSource = null;
        }
        private void btn_backRate_Click(object sender, RoutedEventArgs e)
        {
            gb_rate.Visibility = Visibility.Collapsed;
            gb_country_details.Visibility = Visibility.Visible;
            Rate_details.ItemsSource = null;
            tb_insertRate.Text = string.Empty;
            tb_rateResult.Text = string.Empty;
        }

        //___________________________________________________ SIDE Buttons
        private void btn_rate_Click(object sender, RoutedEventArgs e)
        {
            gb_moreInfo.Visibility = Visibility.Collapsed;
            gb_Comments.Visibility = Visibility.Collapsed;
            if (ShowCountry.Count == 0)
            {
                return;
            }
            Country country = ShowCountry.FirstOrDefault();

            gb_rate.Visibility = Visibility.Visible;
        }
        private void btn_info_Click(object sender, RoutedEventArgs e)
        {
            gb_rate.Visibility = Visibility.Collapsed;
            gb_Comments.Visibility = Visibility.Collapsed;
            if (ShowCountry.Count == 0)
            {
                return;
            }
            Country country = ShowCountry.FirstOrDefault();

            gb_moreInfo.Visibility = Visibility.Visible;
            ShowMoreInfo();
        }
        private void btn_post_Click(object sender, RoutedEventArgs e)
        {

            ShowComment();
        }
        //__________________________________________________________________________


        /// <summary>
        /// returns a from list of comments all comments for the selected country
        /// </summary>
        private void ShowComment()
        {
            if (ShowCountry.Count == 0)
            {
                return;
            }
            gb_moreInfo.Visibility = Visibility.Collapsed;
            gb_rate.Visibility = Visibility.Collapsed;
            comments_details.ItemsSource = null;
            Country country = ShowCountry.FirstOrDefault();
            List<Comment> comment = CommentsCountry.Where(c => c.Alphacode == country.Alpha2Code).ToList();
            tb_comentCountry.DataContext = country;
            comments_details.ItemsSource = comment;
            gb_Comments.Visibility = Visibility.Visible;
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
            allRates = Task.Run(() => UIData.ShowAllCoutryRates(country, Rates)).Result;
            if (CountryRate.Count == 0)
            {
                CountryRate.Add(new CalcRate
                {
                    Name = "Informação indísponível",
                });
                cb_ActiveCountryRate.ItemsSource = CountryRate;
                cb_ActiveCountryRate.DisplayMemberPath = "Name";
                tb_insertRate.IsReadOnly = true;
                btnCalcRate.IsEnabled = false;
                btnChangeRate.IsEnabled = false;
                bt_deleteRateInsert.IsEnabled = false;
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


                tb_rateResult.Text = $"{tb_insertRate.Text} {origin} correspondem a: {Environment.NewLine}          " +
                $"   {Convert.ToString(decimal.Round(Convert.ToDecimal(UIData.CalculateRate(origin, destination, Convert.ToDecimal(input))), 2))} {destination.Name}";
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
            tb_insertRate.Text = string.Empty;
            tb_rateResult.Text = string.Empty;
        }

        private void bt_deleteRateInsert_Click(object sender, RoutedEventArgs e)
        {
            tb_insertRate.Text = string.Empty;
            tb_rateResult.Text = string.Empty;
        }

        private void btn_postComment_Click(object sender, RoutedEventArgs e)
        {
            DataFlow dataFlow = new DataFlow();
            if (!dataFlow.SetConnectionStatus())
            {
                MessageService.ShowMessage("Erro", "Para poder comentar necessita de conexao a internet");
                return;
            }
            if (ShowCountry.Count == 0)
            {
                return;
            }
            var country = ShowCountry.FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(tb_post.Text) && country != null)
            {
                var date = DateTime.Now;

                Comment comment = new Comment
                {
                    Alphacode = country.Alpha2Code,
                    Comments = tb_post.Text,
                    Date = date
                };
                MessageBoxResult result = MessageBox.Show("O seu Post foi realizado com sucesso",
                                           "Confirmation",
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Task.Run(() => dataFlow.PostCommentsData(country, comment));
                    gb_Comments.Visibility = Visibility.Collapsed;
                    gb_Comments.Visibility = Visibility.Visible;
                    CommentsCountry.Add(comment);
                }
                else
                {
                    tb_post.Text = string.Empty;
                    return;
                }
            }
            else
            {
                MessageService.ShowMessage("", "Por favor escreva um comentario");
            }
            ShowComment();
        }

        private void btn_boarder_Click(object sender, RoutedEventArgs e)
        {
            Button btn_image = sender as Button;
            var country = (Country)btn_image.DataContext;
            if (country != null && country.Name != "No borders available")
            {
                ShowCountryInformation(country);
                gb_info_start.Visibility = Visibility.Collapsed;
                tb_seach.IsEnabled = false;
                SetRatesWindow(country);
                gb_moreInfo.Visibility = Visibility.Collapsed;
                gb_country_details.Visibility = Visibility.Visible;
            }
            else
            {
                return;
            }

        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            ratingBar = new RatingBar();
            ratingBar.Show();
            this.Hide();
        }

        private void btn_about_Click(object sender, RoutedEventArgs e)
        {

            about = new About(this);
            about.Show();
        }

    }
}




