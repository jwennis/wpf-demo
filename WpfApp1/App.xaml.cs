using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Data;
using WpfApp1.ViewModel;

namespace WpfApp1
{
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow(new MainViewModel(new CustomersViewModel(new CustomerDataProvider()), new ProductsViewModel()));
            mainWindow.Show();
        }

    }
}
