using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using лр27.ViewModels;
using лр27.Models;

namespace лр27
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ConsultationViewModel(new Consultation());
            ViewModel = new MainViewModel(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ConsultationDB.mdf;Integrated Security=True;Connect Timeout=30");
            DataContext = ViewModel;
        }

        private void Button_Book_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ConsultationViewModel viewModel)
            {
                viewModel.Book();
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ConsultationViewModel viewModel)
            {
                viewModel.Cancel();
            }
        }
    }
}

