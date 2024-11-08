using School_System_APII.DTO;
using School_System_APII.Models;
using System.Collections.Generic;

namespace School_System_APII.Repositories
{
    public interface ISubjectRepository
    {
        IEnumerable<object> GetAllSubjects();
        IEnumerable<object> GetSubjectByName(string name);
        bool AddSubject(Subjectdto subjectDto);
        bool UpdateSubject(string name, Subjectdto subjectDto);
        bool DeleteSubject(string name);
    }
}
