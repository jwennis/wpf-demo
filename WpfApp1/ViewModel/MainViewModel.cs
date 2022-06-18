using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Command;

namespace WpfApp1.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public CustomersViewModel CustomersViewModel { get; }
        public ProductsViewModel ProductsViewModel { get; }
        private ViewModelBase? _selectedViewModel;
        public DelegateCommand SelectViewModelCommand { get; }

        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set 
            {
                _selectedViewModel = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel(CustomersViewModel customersViewModel, ProductsViewModel productsViewModel)
        {
            CustomersViewModel = customersViewModel;
            ProductsViewModel = productsViewModel;
            SelectedViewModel = CustomersViewModel;
            SelectViewModelCommand = new DelegateCommand(SelectViewModel);
        }

        private async void SelectViewModel(object? parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            await LoadAsync();
        }

        public async override Task LoadAsync() 
        {
            if (SelectedViewModel is not null) 
            {
                await SelectedViewModel.LoadAsync();
            }        
        }
    }
}
