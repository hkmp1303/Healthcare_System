namespace HospitalApp;

abstract class CommonPersonnel : IUser
{
    public string IdNumber { get; set; }
    public string Username;
    public string _password;
    public Role _role;
    //public string Region;
    //public string Location;

    public CommonPersonnel(string username, string password, Role role, string employid /*, Location location*/)
    {
        Username = username;
        _password = password;
        _role = role;
        IdNumber = employid;
        //Region = location.Region;
        //Location = location;
    }

    public bool TryLogin(string username, string password)
    {
        return username == Username && password == _password;
    }

    public bool IsRole(Role role)
    {
        return role == _role;
    }

    
    public override string ToString()
    {
    return Username;
    }
}

