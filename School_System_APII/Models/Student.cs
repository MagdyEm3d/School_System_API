using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_System_APII.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [MaxLength(20,ErrorMessage ="UserName Maxium 20 Char Only")]
        [Required]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Please Enter FullName")]

        public string FullName {  get; set; }
        [EmailAddress(ErrorMessage ="Please Enter Correct Email Stracture")]
        [Required(ErrorMessage ="Please Enter Email")]
        public string Email { get; set; }
        [StringLength(maximumLength:40,MinimumLength =5,ErrorMessage ="Password must between 5 and 20 char")]
        [PasswordPropertyText]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [Required(ErrorMessage ="Please Enter Your Password")]
        public string Password { get; set; }

        public int ?SubjectId { get; set; }
        
        public Subject Subject { get; set; }

        public int ?InstractorId { get; set; }
        
        public Instractor Instractor { get; set; }

    }
}
