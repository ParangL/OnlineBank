using BankAppMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppMvc.ViewModels
{
    public class CustomerViewModel
    {
        public Customers Customer { get; set; }

        public string Message { get; set; }



        public CustomerViewModel()

        {

            Customer = new Customers();

        }
    }
}
