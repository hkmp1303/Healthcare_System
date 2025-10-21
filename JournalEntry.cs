namespace HospitalApp;

<<<<<<< HEAD
public class JournalEntry
=======
class JournalEntry //class for JournalEntry
>>>>>>> main
{
    public IUser Patient;    //get patient
    public IUser Personnel; //get personal
    public string Text; //string used for journal documentation
    public DateTime dateTime; //real time date and time
    public ReadPermisson readPermisson; //read permissions

<<<<<<< HEAD
    public JournalEntry(IUser patient, IUser personnel, string text, ReadPermisson readPerm)
=======
    public JournalEntry(Patient patient, IUser personnel, string text, ReadPermisson readPerm) //constructor
>>>>>>> main
    {
        Patient = patient;
        Personnel = personnel;
        Text = text;
        dateTime = DateTime.Now;
        readPermisson = readPerm;
    }

    [Flags] //flags so you can set multiple enums
    public enum ReadPermisson //read permissions
    {
        None,   // basic setting
        PatientRead, //patient lvl, everyone can read
        PersonnelRead, // only personnel can read
        AdminRead = 5, // only admin (really no use for it. it would be wierd if admins could read patients journals)
    }

    public bool CanRead(IUser user) //who can use permissions. based on what role a user have
    {
        if (user.IsRole(Role.Admin) && (readPermisson & ReadPermisson.AdminRead) != 0) //set admin role to admin read Permission
            return true;
        if (user.IsRole(Role.Personnel) && (readPermisson & ReadPermisson.PersonnelRead) != 0) //set personnel role to personnel read permissions
            return true;
        if (user.IsRole(Role.Patient) && user == Patient && (readPermisson & ReadPermisson.PatientRead) != 0) //set patient role to patient read permission
            return true;
        return false;
    }
}
