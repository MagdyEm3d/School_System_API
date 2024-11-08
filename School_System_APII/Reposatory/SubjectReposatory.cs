using School_System_APII.Connection;
using School_System_APII.DTO;
using School_System_APII.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace School_System_APII.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<object> GetAllSubjects()
        {
            return _context.Subjects
                .Include(x => x.Students)
                .ThenInclude(x => x.Instractor)
                .Select(x => new
                {
                    x.SubjectId,
                    x.SubjectName,
                    x.Password,
                    StudentName = x.Students.Select(s => s.FullName).ToList(),
                    StudentEmail = x.Students.Select(s => s.Email).ToList(),
                    InstractorName = x.Instractor.InstractorName
                }).ToList();
        }

        public IEnumerable<object> GetSubjectByName(string name)
        {
            return _context.Subjects
                .Include(x => x.Students)
                .ThenInclude(x => x.Instractor)
                .Where(x => x.SubjectName.Contains(name))
                .Select(x => new
                {
                    x.SubjectId,
                    x.SubjectName,
                    x.Password,
                    StudentsName = x.Students.Select(s => s.FullName).ToList(),
                    InstractorName = x.Instractor.InstractorName
                }).ToList();
        }

        public bool AddSubject(Subjectdto subjectDto)
        {
            var existingSubject = _context.Subjects.FirstOrDefault(x => x.SubjectName == subjectDto.SubjectName);
            if (existingSubject != null) return false;

            var newSubject = new Subject
            {
                SubjectName = subjectDto.SubjectName,
                Password = subjectDto.Password
            };
            _context.Subjects.Add(newSubject);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateSubject(string name, Subjectdto subjectDto)
        {
            var subject = _context.Subjects.FirstOrDefault(x => x.SubjectName.Contains(name));
            if (subject == null) return false;

            subject.SubjectName = subjectDto.SubjectName;
            subject.Password = subjectDto.Password;
            _context.Subjects.Update(subject);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteSubject(string name)
        {
            var subject = _context.Subjects.FirstOrDefault(x => x.SubjectName.Contains(name));
            if (subject == null) return false;

            _context.Subjects.Remove(subject);
            _context.SaveChanges();
            return true;
        }
    }
}
