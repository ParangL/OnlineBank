using BankAppMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppMvc.ViewModels
{
    public class TransactionHistoryViewModel
    {
        public Accounts Account { get; set; }
        public List<Transactions> TransactionList { get; set; }
        public LongListViewModel ListViewModel { get; set; }


        public TransactionHistoryViewModel()
        {
            Account = new Accounts();
        }
    }
}
