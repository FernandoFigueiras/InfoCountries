using System.Windows;

namespace InfoCountries
{
    /// <summary>
    /// Interaction logic for RatingBar.xaml
    /// </summary>
    public partial class RatingBar : Window
    {
        public RatingBar()
        {
            InitializeComponent();
        }

        private void rating_bar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            var rate = e.NewValue;
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.MainWindow.Close();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
