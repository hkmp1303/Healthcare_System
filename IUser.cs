namespace HospitalApp;

public interface IUser
{
    public bool IsRole(Role role);

    public bool TryLogin(string username, string password);
    public Role GetRole();
}

public enum Role
{
    Patient = 1,
    Admin = 2,
    Personnel = 4,
    UnregUser = 8,
    PatientRegRequested = 16,

}
