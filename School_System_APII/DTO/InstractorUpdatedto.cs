using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace School_System_APII.DTO
{
    public class InstractorUpdatedto
    {
        [Required(ErrorMessage = "Please Enter InstractorName")]
        public string InstractorName { get; set; }
        [EmailAddress(ErrorMessage = "Please Enter Correct Email Structure")]
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your Salary")]
        public int Salary { get; set; }
        [StringLength(maximumLength: 40, MinimumLength = 5, ErrorMessage = "Password must between 5 and 20 char")]
        [PasswordPropertyText]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [Required(ErrorMessage = "Please Enter Your Password")]
        public string Password { get; set; }
    }
}
