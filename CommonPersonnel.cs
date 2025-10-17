namespace HospitalApp;

abstract class CommonPersonnel : IUser
{
    public string Username;
    public string Password;
    public Role Role;
    //public string Region;
    //public string Location;
    public Permission Permissions; // list of type Permission

    // constructor
    public CommonPersonnel(string username, string password, Role role/*, Location location*/)
    {
        Username = username;
        Password = password;
        Role = role;
        //Region = location.Region;
        //Location = location;
        Permissions = new Permission();
        Permission.Permissions.Add(username, Permissions);  // to save permissions in save system
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

