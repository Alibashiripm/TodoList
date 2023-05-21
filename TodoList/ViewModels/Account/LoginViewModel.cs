using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required, Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required, Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }

    }
}
