using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_System_APII.Connection;
using School_System_APII.DTO;
using School_System_APII.Models;
using School_System_APII.Reposatory;

namespace School_System_APII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstractorController : ControllerBase
    {
        private readonly IInstractorReposatory _repo;
        private readonly IToken _token;

        public InstractorController(IInstractorReposatory repo, IToken token)
        {
            _repo = repo;
            _token = token;
        }
        [HttpGet("GetAllInstractors")]
        public IActionResult GetAllInstractors()
        {
            try
            {
                var instractors = _repo.GetAllInstractors();
                return Ok(instractors);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ann Error Occured In Server{ex.Message}");
            }

        }

        [HttpGet("{name}/GetInstructor")]
        public IActionResult GetInstructor(string name)
        {
            try
            {
                var instructor = _repo.GetInstractor(name);

                if (!instructor.Any())
                    return NotFound("Instructor Not Found");

                return Ok(instructor);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ann Error Occured In Server{ex.Message}");
            }

        }



        [HttpPost("Registeration")]
        public IActionResult Registeration([FromForm]InstractorRegisterationdto instractorRegisterationdto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                bool isregister = _repo.IsRegisteration(instractorRegisterationdto);
                if (!isregister)
                    //return BadRequest("This Instractor is already taken");
                    return Unauthorized();

                var token = _token.GenerateJwtToken(instractorRegisterationdto.InstractorName,instractorRegisterationdto.Email);
                return Ok(new { token });   
                //return Ok($"This Instracot {instractorRegisterationdto.InstractorName} Register Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ann Error Occured In Server{ex.Message}");
            }
        }
        [HttpPost("Login")]
        public IActionResult Login([FromForm]InstractorLogindtocs instractorLogindtocs)
        {
            try
            {
                bool islogin = _repo.IsLogin(instractorLogindtocs);
                if (!islogin)
                    return BadRequest("This Instractor Not Found");
                return Ok("Instractor Login Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ann Error Occured In Server{ex.Message}");
            }



        }
        [HttpPut("{name}/UpdateInstractor")]
        public IActionResult UpdateInstractor([FromRoute]string name,InstractorUpdatedto instractorUpdatedto)
        {
            try
            {
                bool updated = _repo.Update(name, instractorUpdatedto);
                if (!updated)
                    return BadRequest("This Instractor Not Found");
                return Ok("Instractor Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ann Error Occured In Server{ex.Message}");
            }

        }
        [HttpPut("{name}/ChooseSubject")]
        public IActionResult ChooseSubject(string name,string subjectname)
        {
            try
            {
                bool ischosen = _repo.ChooseSubject(name, subjectname);
                if (!ischosen)
                    return BadRequest("Invalid Subject Or Instractor");

                return Ok("Instractor Choose Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ann Error Occured In Server{ex.Message}");
            }



        }
        [HttpDelete("{name}/DeletedInstractor")]
        public IActionResult Delete(string name)
        {
            try
            {
                bool deleted = _repo.Delete(name);
                if (!deleted)
                    return BadRequest("This instractor Not Found");
                return Ok($"{name} Instractor Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ann Error Occured In Server{ex.Message}");
            }


        }
    }
}
