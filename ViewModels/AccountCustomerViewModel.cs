using BankAppMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppMvc.ViewModels
{
    public class AccountCustomerViewModel
    {
        public Customers Customer { get; set; }

        public List<Accounts> Accounts { get; set; }



        public decimal Total { get; set; }



        public AccountCustomerViewModel()

        {

            Accounts = new List<Accounts>();

            Total = 0;

        }
    }
}
