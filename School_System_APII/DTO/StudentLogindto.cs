using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace School_System_APII.DTO
{
    public class StudentLogindto
    {
        [MaxLength(20, ErrorMessage = "UserName Maxium 20 Char Only")]
        [Required]
        public string UserName { get; set; }
        [StringLength(maximumLength: 40, MinimumLength = 5, ErrorMessage = "Password must between 5 and 20 char")]
        [PasswordPropertyText]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [Required(ErrorMessage = "Please Enter Your Password")]
        public string Password { get; set; }

    }
}
