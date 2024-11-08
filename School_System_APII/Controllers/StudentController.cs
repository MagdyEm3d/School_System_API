using Microsoft.AspNetCore.Mvc;
using School_System_APII.DTO;
using School_System_APII.Reposatory;
using School_System_APII.Repositories;

namespace School_System_APII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentReposatory _studentRepository;

        public StudentController(IStudentReposatory studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            var students = _studentRepository.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("{name}/GetOneStudent")]
        public IActionResult GetStudent(string name)
        {
            var student = _studentRepository.GetStudentByName(name);
            if (!student.Any())
                return NotFound("Student Not Found");

            return Ok(student);
        }

        [HttpPost("Registeration")]
        public IActionResult Registeration([FromForm] StudentRegisterationdto studentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isRegistered = _studentRepository.RegisterStudent(studentDto);
            if (!isRegistered)
                return BadRequest("This Username is already taken");

            return Ok($"{studentDto.FullName} Student is registered successfully");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromForm] StudentLogindto studentDto)
        {
            bool isLogged = _studentRepository.LoginStudent(studentDto);
            if (!isLogged)
                return BadRequest("Invalid Username or Password");

            return Ok($"{studentDto.UserName} Student Login Successfully");
        }

        [HttpPut("{name}/ChooseSubjectAndInstractor")]
        public IActionResult ChooseSubject(string name, string subjectName, string instructorName)
        {
            bool isChosen = _studentRepository.ChooseSubjectAndInstructor(name, subjectName, instructorName);
            if (!isChosen)
                return BadRequest("Invalid Student, Subject, or Instructor");

            return Ok($"{name} has chosen the subject and instructor successfully");
        }

        [HttpPut("{name}/UpdateStudent")]
        public IActionResult UpdateStudent(string name, StudentUpdatedto studentDto)
        {
            bool isUpdated = _studentRepository.UpdateStudent(name, studentDto);
            if (!isUpdated)
                return NotFound("Student Not Found");

            return Ok($"{name} Student Updated Successfully");
        }

        [HttpDelete("{name}")]
        public IActionResult DeleteStudent(string name)
        {
            bool isDeleted = _studentRepository.DeleteStudent(name);
            if (!isDeleted)
                return NotFound("Student Not Found");

            return Ok($"{name} Student Deleted Successfully");
        }
    }
}
