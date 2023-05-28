using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required, Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required, Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

    }
}
