using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.DTOs.AccountDto
{
    public class AccountLoginDto
    {
        [Required(ErrorMessage = "نام کاربری نمی تواند خالی باشد")]
        public string Username { get; set; }
        [Required(ErrorMessage = "وارد کردن رمزعبور اجباری است")]
        [MinLength(6, ErrorMessage = "رمزعبور نمی‌تواند کمتر 4 کاراکتر باشد")]
        public string Password { get; set; }
    }
}
