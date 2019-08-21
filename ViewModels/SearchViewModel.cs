using BankAppMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppMvc.ViewModels
{
    public class SearchViewModel
    {
        public List<Customers> CustomerList { get; set; }
        public Customers Customer { get; set; }
        public string SearchValue { get; set; }
        public string NameOrCity { get; set; }
        public int TotalPages { get; set; }
        public bool HasMorePages { get; set; }
        public int page { get; set; } = 1;

        public SearchViewModel()
        {
            CustomerList = new List<Customers>();
            Customer = new Customers();
        }
    }
}
