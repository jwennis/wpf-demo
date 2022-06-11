﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Data;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public class CustomersViewModel
    {
        public ObservableCollection<Customer> Customers { get; } = new();
        public Customer? SelectedCustomer { get; set; }

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
                    Customers.Add(customer);
                }
            }


        }
    }
}
