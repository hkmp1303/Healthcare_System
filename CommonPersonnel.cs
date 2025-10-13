namespace HospitalApp;

abstract class CommonPersonnel : IUser
{
    
    public string Username;
    public string Password;
    public Role Role;
    //public string Region;
    //public string Location;

    public CommonPersonnel(string username, string password, Role role /*, Location location*/)
    {
        Username = username;
        _password = password;
        _role = role;
        //Region = location.Region;
        //Location = location;
    }

    public bool TryLogin(string username, string password)
    {
        return username == Username && password == Password;
    }

    public bool IsRole(Role role)
    {
        return role == Role;
    }

    
    public override string ToString()
    {
    return Username;
    }
}

