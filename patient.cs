namespace HospitalApp;

class Patient : IUser
{
    public string Email;
    public string Password;

    public Patient (string email, string password)
    {
        Email = email;
        Password = password;
    }

    public bool TryLogin(string username, string password)
    {
        return username == Email && password == Password;
    }

    public bool IsRole(Role role)
    {
        return role == Role.Patient;
    }
}