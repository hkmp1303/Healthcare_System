namespace HospitalApp;    // Explains which project the code belongs to

class Patient : UnregUser // This is the class "patient" and everything within the class {} defines the patient
{
    // The constructor which calls of the base of "unreg" and changes "unreg" into -> "patient"-
    // -when creating a "patient" in the program
    public Patient(string email, string password) : base(email, password, Role.Patient)
    {
    }

    // Access constructor
    public Patient(UnregUser user) : base(user.Username, user.Password, Role.Patient)
    {
    }

    // Prints the patient's e-mail when you want to the patient to-
    // -be seen in say a schedule, appointment requests etc. etc.
    public override string ToString()
    {
        return Email;
    }
}