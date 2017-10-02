using System.ComponentModel.DataAnnotations;

namespace Models.Auth
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "pw")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "confirm pw")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}