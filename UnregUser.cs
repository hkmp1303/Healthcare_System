namespace HospitalApp;

class UnregUser : IUser
{
    public string Email;
    public string Password;
    public string Username // allias of email, read-only
    {
        get { return Email; }
    }
    public Role Role;

    public UnregUser(string email, string password)
    {
        Email = email;
        Password = password;
        Role = Role.UnregUser;
    }

    public UnregUser(string email, string password, Role role)
    {
        Email = email;
        Password = password;
        Role = role;
    }

    public bool IsRole(Role role)
    {
        return role == Role;
    }

    public bool TryLogin(string username, string password)
    {
        return username == Email && password == Password;
    }
}


