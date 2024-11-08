using School_System_APII.DTO;
using School_System_APII.Models;

namespace School_System_APII.Reposatory
{
    public interface IStudentReposatory
    {
        IEnumerable<object> GetAllStudents();
        IEnumerable<object> GetStudentByName(string name);
        bool RegisterStudent(StudentRegisterationdto studentDto);
        bool LoginStudent(StudentLogindto studentDto);
        bool ChooseSubjectAndInstructor(string studentName, string subjectName, string instructorName);
        bool UpdateStudent(string name, StudentUpdatedto studentDto);
        bool DeleteStudent(string name);
    }
}
