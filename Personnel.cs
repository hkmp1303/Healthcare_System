namespace HospitalApp;

class Personnel : IUser
{
  public string IdNumber { get; set; }
  public string Username;
  public string Password;

  public Personnel(string username, string password, string employid)

  {
    Username = username;
    Password = password;
    IdNumber = employid;
  }

  public bool TryLogin(string username, string password)
  {
    return username == Username && password == Password;
  }

  public bool IsRole(Role role)
  {
    return Role.Personnel == role;
  }

  public Role GetRole()
  {
    return Role.Personnel;
  }

    public override string ToString()
    {
    return Username;
    }

}

