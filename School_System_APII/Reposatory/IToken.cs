namespace School_System_APII.Reposatory
{
    public interface IToken
    {
        string GenerateJwtToken(string username,string email);
    }
}
