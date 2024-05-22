using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using лр27.ViewModels;

namespace лр27
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ConsultationDB.mdf;Integrated Security=True;Connect Timeout=30";
            var mainWindow = new MainWindow();
            mainWindow.DataContext = new MainViewModel(connectionString);
            mainWindow.Show();
        }
    }
}
