using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Data;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public class CustomersViewModel : ViewModelBase
    {
        public ObservableCollection<CustomerItemViewModel> Customers { get; } = new();

        private CustomerItemViewModel? _selectedCustomer;

        private NavigationSide _navigationColumn;
        public NavigationSide NavigationColumn
        {
            get => _navigationColumn;
            private set
            {
                _navigationColumn = value;
                RaisePropertyChanged();
            }
        }

        public CustomerItemViewModel? SelectedCustomer
        {
            get => _selectedCustomer;

            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged();
            }
        }

        private readonly ICustomerDataProvider _customerDataProvider;


        public CustomersViewModel(ICustomerDataProvider customerDataProvider)
        {
            _customerDataProvider = customerDataProvider;
        }


        public async Task LoadAsync()
        {
            if (Customers.Any())
            {
                return;
            }

            var customers = await _customerDataProvider.GetAllAsync();

            if (customers is not null)
            {
                foreach (var customer in customers)
                {
                    Customers.Add(new CustomerItemViewModel(customer));
                }
            }
        }


        internal void Add()
        {
            var customer = new Customer { FirstName = "New" };
            var viewModel = new CustomerItemViewModel(customer);

            Customers.Add(viewModel);
            SelectedCustomer = viewModel;
        }




        internal void MoveNavigation()
        {
            NavigationColumn = NavigationColumn == NavigationSide.LEFT ? NavigationSide.RIGHT : NavigationSide.LEFT;
        }

        public enum NavigationSide 
        { 
            LEFT,
            RIGHT
        }
    }
}
