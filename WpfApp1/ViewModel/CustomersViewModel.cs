using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Command;
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
                RaisePropertyChanged(nameof(IsCustomerSelected));
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsCustomerSelected => SelectedCustomer is not null;

        private readonly ICustomerDataProvider _customerDataProvider;

        public DelegateCommand AddCommand { get; }
        public DelegateCommand MoveNavigationCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        public CustomersViewModel(ICustomerDataProvider customerDataProvider)
        {
            _customerDataProvider = customerDataProvider;

            AddCommand = new DelegateCommand(Add);
            MoveNavigationCommand = new DelegateCommand(MoveNavigation);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
        }


        public async override Task LoadAsync()
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


        private void Add(object? parameter)
        {
            var customer = new Customer { FirstName = "New" };
            var viewModel = new CustomerItemViewModel(customer);

            Customers.Add(viewModel);
            SelectedCustomer = viewModel;
        }




        private void MoveNavigation(object? parameter)
        {
            NavigationColumn = NavigationColumn == NavigationSide.LEFT ? NavigationSide.RIGHT : NavigationSide.LEFT;
        }

        public enum NavigationSide 
        { 
            LEFT,
            RIGHT
        }
        private void Delete(object? parameter)
        {
            if (SelectedCustomer is not null) 
            {
                Customers.Remove(SelectedCustomer);
                SelectedCustomer = null;
            }
        }

        private bool CanDelete(object? parameter) => SelectedCustomer is not null;
    }
}
