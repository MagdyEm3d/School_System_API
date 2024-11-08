using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_System_APII.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }

        [Required(ErrorMessage ="Please Enter SubjectName")]
        [StringLength(maximumLength:20,ErrorMessage ="SubjectName No Long Than 20 Char")]
        public string SubjectName { get; set; }

        [StringLength(maximumLength: 40, MinimumLength = 5, ErrorMessage = "Password must between 5 and 20 char")]
        [PasswordPropertyText]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [Required(ErrorMessage = "Please Enter Your Password")]
        public string Password { get; set; }

        public List<Student> Students { get; set; }
        public int ?InstractorId { get; set; }
        
        public Instractor Instractor { get; set; }

    }
}
