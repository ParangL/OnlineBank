using BankAppMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppMvc.ViewModels
{
    public class CustomerMessageViewModel
    {
        public Customers Customer { get; set; }
        public string Message { get; set; }

        public CustomerMessageViewModel()
        {
            Customer = new Customers();
        }
    }
}
