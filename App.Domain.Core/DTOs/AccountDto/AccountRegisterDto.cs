using App.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.Domain.Core.DTOs.AccountDto
{
    public class AccountRegisterDto
    {
        [Required(ErrorMessage = "لطفا نام و نام خانوادگی خود را وارد کنید")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "لطفا ایمیل خود را وارد کنید")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "لطفا نام کاربری خود را وارد کنید ")]
        public string Username { get; set; }
        [Required(ErrorMessage = "لطفا شماره تلفن خود را وارد کنید ")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "لطفا رمز عبور را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "لطفا رمز عبور را تکرار نمایید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور یکی نیست")]
        public string ConfirmedPassword { get; set; }
        public string? Role { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }
    }
}
