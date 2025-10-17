namespace HospitalApp;

class UnregUser : IUser
{
    public string Email;
    public string Password;

    public UnregUser(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public bool IsRole(Role role)
    {
        return role == Role.UnregUser;
    }

    public bool TryLogin(string username, string password)
    {
        return username == Email && password == Password;
    }
}


