namespace HospitalApp;

class Personnel : IUser
{
  public string Username;
  string _password;

  public Personnel(string username, string password)

  {
    Username = username;
    _password = password;
  }

  public bool TryLogin(string username, string password)
  {
    return username == Username && password == _password;
  }

  public bool IsRole(Role role)
  {
    return Role.Personnel == role;
  }

  public Role GetRole()
  {
    return Role.Personnel;
  }

}

