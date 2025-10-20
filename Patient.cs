namespace HospitalApp;

class Patient : UnregUser
{
    public Patient(string email, string password) : base(email, password, Role.Patient)
    {
    }

    // access constructor
    public Patient(UnregUser user) : base(user.Username, user.Password, Role.Patient)
    {
    }

    public override string ToString()
    {
        return Email;
    }
}