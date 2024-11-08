using Microsoft.AspNetCore.Mvc;
using School_System_APII.DTO;
using School_System_APII.Repositories;

namespace School_System_APII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        [HttpGet("GetAllSubjects")]
        public IActionResult GetAllSubjects()
        {
            var subjects = _subjectRepository.GetAllSubjects();
            return Ok(subjects);
        }

        [HttpGet("{name}/GetSubject")]
        public IActionResult GetSubject(string name)
        {
            var subject = _subjectRepository.GetSubjectByName(name);
            if (!subject.Any())
                return NotFound("Subject Not Found");

            return Ok(subject);
        }

        [HttpPost("AddSubject")]
        public IActionResult AddSubject([FromForm] Subjectdto subjectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isAdded = _subjectRepository.AddSubject(subjectDto);
            if (!isAdded)
                return BadRequest("This Subject Already Exists");

            return Ok($"Subject {subjectDto.SubjectName} Added Successfully");
        }

        [HttpPut("{name}/UpdateSubject")]
        public IActionResult UpdateSubject(string name, Subjectdto subjectDto)
        {
            bool isUpdated = _subjectRepository.UpdateSubject(name, subjectDto);
            if (!isUpdated)
                return NotFound("Subject Not Found");

            return Ok($"Subject {subjectDto.SubjectName} Updated Successfully");
        }

        [HttpDelete("{name}/DeleteSubject")]
        public IActionResult DeleteSubject(string name)
        {
            bool isDeleted = _subjectRepository.DeleteSubject(name);
            if (!isDeleted)
                return NotFound("Subject Not Found");

            return Ok($"Subject {name} Deleted Successfully");
        }
    }
}
