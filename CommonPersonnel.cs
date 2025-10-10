namespace HospitalApp;

abstract class CommonPersonnel : IUser
{
    public string Username;
    private string _password;
    private Role _role;
    //public string Region;
    //public string Location;

    public CommonPersonnel(string username, string password, Role role/*, Location location*/)
    {
        Username = username;
        _password = password;
        _role = role;
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
}

