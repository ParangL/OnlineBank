﻿using BankAppMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppMvc.ViewModels
{
    public class LongListViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalNumberOfItems { get; set; }
        public bool CanShowMore { get; set; }

        public IEnumerable<Transactions> Transactions { get; set; }
    }
}
