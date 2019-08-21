using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAppMvc.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Dispositions = new HashSet<Dispositions>();
        }

        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Write Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Write Givenname")]    
        public string Givenname { get; set; }

        [Required(ErrorMessage = "Write Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Write Streetaddress")]
        public string Streetaddress { get; set; }

        [Required(ErrorMessage = "Write City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Write Zipcode")]
        public string Zipcode { get; set; }

        [Required(ErrorMessage = "Write Country")]
        public string Country { get; set; }
        
        public string CountryCode { get; set; }       
        public DateTime? Birthday { get; set; }        
        public string NationalId { get; set; }       
        public string Telephonecountrycode { get; set; }
        public string Telephonenumber { get; set; }
        public string Emailaddress { get; set; }

        public ICollection<Dispositions> Dispositions { get; set; }
    }
}
