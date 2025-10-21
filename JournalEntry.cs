namespace HospitalApp;

public class JournalEntry
{
    public IUser Patient;    //testa detta annars får vi gå in och ändra i IUser till Username{get; set;} eller lägga till ett ID som vi kan ha som referens material!!!!
    public IUser Personnel;
    public string Text;
    public DateTime dateTime;
    public ReadPermisson readPermisson; //read permissions?

    public JournalEntry(IUser patient, IUser personnel, string text, ReadPermisson readPerm)
    {
        Patient = patient;
        Personnel = personnel;
        Text = text;
        dateTime = DateTime.Now;

        readPermisson = readPerm;
    }

    [Flags]
    public enum ReadPermisson
    {
        None,
        PatientRead, //public? alla kan läsa?
        PersonnelRead, // dom som jobbar på sjukhuset?
        AdminRead = 5, // Bara läkare kan läsa
    }

    public bool CanRead(IUser user)
    {
        if (user.IsRole(Role.Admin) && (readPermisson & ReadPermisson.AdminRead) != 0)
            return true;
        if (user.IsRole(Role.Personnel) && (readPermisson & ReadPermisson.PersonnelRead) != 0)
            return true;
        if (user.IsRole(Role.Patient) && user == Patient && (readPermisson & ReadPermisson.PatientRead) != 0)
            return true;
        return false;
    }



}

/*
Note:
använd ReadPermisson med User role för att skriva logik????
*/