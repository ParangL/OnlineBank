using BankAppMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppMvc.ViewModels
{
    public class PayAndWithdrawalViewModel
    {
        public Accounts Account { get; set; }
        public Transactions Transaction { get; set; }

        public PayAndWithdrawalViewModel()
        {
            Account = new Accounts();
            Transaction = new Transactions();
        }
    }
}
