using System;
using System.Collections.Generic;

namespace BankAppMvc.Models
{
    public partial class Dispositions
    {
        public Dispositions()
        {
            Cards = new HashSet<Cards>();
        }

        public int DispositionId { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }

        public Accounts Account { get; set; }
        public Customers Customer { get; set; }
        public ICollection<Cards> Cards { get; set; }
    }
}
