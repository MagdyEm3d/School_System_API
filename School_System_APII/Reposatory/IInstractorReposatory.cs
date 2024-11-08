using School_System_APII.DTO;

namespace School_System_APII.Reposatory
{
    public interface IInstractorReposatory
    {
        IEnumerable<object> GetAllInstractors();
        IEnumerable<object> GetInstractor(string name);
        bool IsRegisteration(InstractorRegisterationdto instractorRegisterationdto);
        bool IsLogin(InstractorLogindtocs instractorLogindtocs);

        bool Update(string name,InstractorUpdatedto studentRegisteration);
        bool ChooseSubject(string name, string subjectname);
        bool Delete(string name);
    }
}
