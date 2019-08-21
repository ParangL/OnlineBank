using BankAppMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppMvc.ViewModels
{
    public class TransactionViewModel
    {
        public Transactions Transaction { get; set; }
        public decimal Amount { get; set; }

        public TransactionViewModel()
        {
            Transaction = new Transactions();
            Amount = 0;
        }
    }
}
