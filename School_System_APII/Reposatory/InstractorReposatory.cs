using Microsoft.EntityFrameworkCore;
using School_System_APII.Connection;
using School_System_APII.DTO;
using School_System_APII.Models;

namespace School_System_APII.Reposatory
{
    public class InstractorReposatory:IInstractorReposatory
    {
        private readonly ApplicationDbContext _context;
        

        public InstractorReposatory(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public bool ChooseSubject(string name, string subjectname)
        {
            try
            {
                var instractor = _context.instractors.FirstOrDefault(x => x.InstractorName == name);
                if (instractor == null) return false;

                var subject = _context.Subjects.FirstOrDefault(x => x.SubjectName.Contains(subjectname));
                if (subject == null) return false;


                instractor.SubjectId = subject.SubjectId;
                _context.instractors.Update(instractor);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool Delete(string name)
        {
            try
            {
                var instractor = _context.instractors.FirstOrDefault(x => x.InstractorName == name);
                if (instractor == null) return false;

                _context.instractors.Remove(instractor);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public IEnumerable<object> GetAllInstractors()
        {
            try
            {
                return _context.instractors.Include(x => x.Students).ThenInclude(x => x.Subject).Select(x => new
                {
                    x.InstractorId,
                    x.InstractorName,
                    x.Salary,
                    StudentName = x.Students.Select(x => x.FullName).ToList(),
                    StudentEmail = x.Students.Select(x => x.Email).ToList(),
                    SubjectNmae = x.Subject.SubjectName,
                    SubjectPassword = x.Subject.Password


                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public IEnumerable<object> GetInstractor(string name)
        {
            try
            {
                return _context.instractors
                .Include(x => x.Subject)
                .ThenInclude(x => x.Students)
                .Where(x => x.InstractorName.Contains(name))
                .Select(x => new
                {
                    InstractorId = x.InstractorId,
                    InstructorName = x.InstractorName,
                    instractorEmail = x.Email,
                    Salary = x.Salary,
                    Password = x.Password,
                    Subject = x.Subject.SubjectName,
                    Students = x.Subject.Students.Select(s => s.FullName).ToList()
                })
                .ToList();
            }catch(Exception ex)
            {
                 throw new Exception(ex.Message);
            }

        }

        public bool IsLogin(InstractorLogindtocs instractorLogindtocs)
        {
            try
            {
                var instractor = _context.instractors.FirstOrDefault(x => x.InstractorName == instractorLogindtocs.InstractorName && x.Password == instractorLogindtocs.Password);
                return instractor != null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public bool IsRegisteration(InstractorRegisterationdto instractorRegisterationdto)
        {
            try
            {
                var existinginstractor = _context.instractors.FirstOrDefault(x => x.InstractorName == instractorRegisterationdto.InstractorName);
                if (existinginstractor != null) return false;

                var instractor = new Instractor
                {

                    InstractorName = instractorRegisterationdto.InstractorName,
                    Email = instractorRegisterationdto.Email,
                    Password = instractorRegisterationdto.Password
                };
                _context.instractors.Add(instractor);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



        public bool Update(string name, InstractorUpdatedto instractorUpdatedto)
        {
            try
            {
                var instractor = _context.instractors.FirstOrDefault(x => x.InstractorName == name);
                if (instractor == null) return false;

                instractor.InstractorName = instractorUpdatedto.InstractorName;
                instractor.Salary = instractorUpdatedto.Salary;
                instractor.Email = instractorUpdatedto.Email;
                instractor.Password = instractorUpdatedto.Password;
                _context.instractors.Update(instractor);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
