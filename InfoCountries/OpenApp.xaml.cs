using InfoCountries.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            if (e.DataLoaded.Count==250)
            {
                this.Close();
                mainWindow.Show();
            }
        }

        private void LoadingWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
