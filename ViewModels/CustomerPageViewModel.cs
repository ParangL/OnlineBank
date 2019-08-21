using BankAppMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppMvc.ViewModels
{
    public class CustomerPageViewModel
    {
        public string Gender { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
        public string SearchValue { get; set; }
        public List<Customers> SearchCustomers { get; set; }
        public List<Dispositions> SearchDispositions { get; set; }
        public List<Accounts> SearchAccounts { get; set; }
        public List<int> SearchAccountIds { get; set; }
        public List<Transactions> SearchTransactions { get; set; }
        public Decimal TotalBalance { get; set; }
        



        public CustomerPageViewModel()
        {
            SearchCustomers = new List<Customers>();
            SearchDispositions = new List<Dispositions>();
            SearchAccounts = new List<Accounts>();
            SearchAccountIds = new List<int>();
            SearchTransactions = new List<Transactions>();
        }
    }
}
