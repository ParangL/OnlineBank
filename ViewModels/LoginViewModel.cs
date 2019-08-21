using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppMvc.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Write Username!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Write Password!")]
        public string Password { get; set; }

        public string CheckPassword { get; set; }

        public string RoleName { get; set; }
    }
}
