using BankAppMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppMvc.ViewModels
{
    public class MyBankStaticsInfo
    {
        public int nrOfCustomers { get; set; }

        public int nrOfAccounts { get; set; }

        public decimal totalBalance { get; set; }



        public MyBankStaticsInfo()

        {

            nrOfCustomers = 0;

            nrOfAccounts = 0;

            totalBalance = 0;

        }
    }
}
