namespace HospitalApp;

class Personnel : CommonPersonnel
{

    public Personnel(string username, string password) : base(username, password, Role.Personnel)
    {
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

