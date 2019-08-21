using System;
using System.Collections.Generic;

namespace BankAppMvc.Models
{
    public partial class Accounts
    {
        public Accounts()
        {
            Dispositions = new HashSet<Dispositions>();
            Loans = new HashSet<Loans>();
            PermenentOrder = new HashSet<PermenentOrder>();
            Transactions = new HashSet<Transactions>();
        }

        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }

        public ICollection<Dispositions> Dispositions { get; set; }
        public ICollection<Loans> Loans { get; set; }
        public ICollection<PermenentOrder> PermenentOrder { get; set; }
        public ICollection<Transactions> Transactions { get; set; }
    }
}
