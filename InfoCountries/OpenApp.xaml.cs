using InfoCountries.Services;
using System;
using System.Windows;
using System.Windows.Input;

namespace InfoCountries
{

    /// <summary>
    /// Interaction logic for OpenApp.xaml
    /// </summary>
    public partial class OpenApp : Window
    {
        private Progress<ProgressReportService> progress;
        public MainWindow mainWindow { get; set; }

        public OpenApp(MainWindow _mainWindow, Progress<ProgressReportService> _progress)
        {
            mainWindow = _mainWindow;
            progress = _progress;
            InitializeComponent();
            Loaded();

        }

        private new void Loaded()
        {
            progress.ProgressChanged += Progress_ProgressChanged;

        }

        private void Progress_ProgressChanged(object sender, ProgressReportService e)
        {

            if (e.PercComplete == 100)
            {
                this.Close();
                mainWindow.Show();
            }
        }

        private void LoadingWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                return;
            }
            else
            {
                this.DragMove();
            }
        }
    }
}
