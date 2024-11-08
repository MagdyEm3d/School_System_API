using Microsoft.EntityFrameworkCore;
using School_System_APII.Connection;
using School_System_APII.DTO;
using School_System_APII.Models;
using System.Collections.Generic;
using System.Linq;

namespace School_System_APII.Reposatory
{
    public class StudentRepository : IStudentReposatory
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<object> GetAllStudents()
        {
            return _context.Students
                .Include(x => x.Subject)
                .ThenInclude(x => x.Instractor)
                .Select(x => new
                {
                    x.StudentId,
                    x.UserName,
                    x.FullName,
                    x.Email,
                    x.Password,
                    SubjectName = x.Subject.SubjectName,
                    InstractorName = x.Instractor.InstractorName
                })
                .ToList();
        }

        public IEnumerable<object> GetStudentByName(string name)
        {
            return _context.Students
                .Include(x => x.Subject)
                .ThenInclude(x => x.Instractor)
                .Where(x => x.FullName.Contains(name))
                .Select(x => new
                {
                    x.UserName,
                    StudentName = x.FullName,
                    StudentEmail = x.Email,
                    StudentPassword = x.Password,
                    InstractorName = x.Instractor.InstractorName,
                    SubjectName = x.Subject.SubjectName
                })
                .ToList();
        }

        public bool RegisterStudent(StudentRegisterationdto studentDto)
        {
            var existingStudent = _context.Students.FirstOrDefault(x => x.UserName == studentDto.UserName);
            if (existingStudent != null) return false;

            var student = new Student
            {
                UserName = studentDto.UserName,
                FullName = studentDto.FullName,
                Email = studentDto.Email,
                Password = studentDto.Password
            };
            _context.Students.Add(student);
            _context.SaveChanges();
            return true;
        }

        public bool LoginStudent(StudentLogindto studentDto)
        {
            var student = _context.Students.FirstOrDefault(x => x.UserName == studentDto.UserName && x.Password == studentDto.Password);
            return student != null;
        }

        public bool ChooseSubjectAndInstructor(string studentName, string subjectName, string instructorName)
        {
            var student = _context.Students.FirstOrDefault(x => x.FullName == studentName);
            if (student == null) return false;

            var subject = _context.Subjects.FirstOrDefault(x => x.SubjectName.Contains(subjectName));
            if (subject == null) return false;

            var instructor = _context.instractors.FirstOrDefault(x => x.InstractorName.Contains(instructorName));
            if (instructor == null) return false;

            student.SubjectId = subject.SubjectId;
            student.InstractorId = instructor.InstractorId;
            _context.Students.Update(student);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateStudent(string name, StudentUpdatedto studentDto)
        {
            var student = _context.Students.FirstOrDefault(x => x.FullName == name);
            if (student == null) return false;

            student.FullName = studentDto.FullName;
            student.UserName = studentDto.UserName;
            student.Email = studentDto.Email;
            student.Password = studentDto.Password;
            _context.Students.Update(student);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteStudent(string name)
        {
            var student = _context.Students.FirstOrDefault(x => x.FullName == name);
            if (student == null) return false;

            _context.Students.Remove(student);
            _context.SaveChanges();
            return true;
        }
    }
}
