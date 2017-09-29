using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApplication.Models
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}