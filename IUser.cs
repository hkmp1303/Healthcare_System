namespace HospitalApp;

public interface IUser
{
    public bool IsRole(Role role);

    public bool TryLogin(string username, string password);
}

public enum Role
{
    Patient,
    Admin,
    Personnel,
    UnregUser,
    PatientRegRequested,

}
