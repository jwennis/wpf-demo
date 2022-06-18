﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly CustomersViewModel _customersViewModel;
        private readonly ProductsViewModel _productsViewModel;
        private ViewModelBase? _selectedViewModel;

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
            _customersViewModel = customersViewModel;
            _productsViewModel = productsViewModel;
            SelectedViewModel = _customersViewModel;
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
