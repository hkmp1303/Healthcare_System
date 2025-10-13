namespace HospitalApp;

class Patient : IUser
{
    public string IdNumber { get; set; }

    public string Email;
    public string Password;

    public Patient (string email, string password, string socialsecuritynumber)
    {
        Email = email;
        Password = password;
        IdNumber = socialsecuritynumber;
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