using BankAppMvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppMvc.ViewModels
{
    public class RegisterNewCustomerViewModel
    {
        public Customers Customer { get; set; }
        public int CustomerId { get; set; }
        public string Gender { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public RegisterNewCustomerViewModel()
        {
            Customer = new Customers();
        }
    }
}
