using System.Net.Security;

namespace HospitalApp;

class JournalEntry
{
    public IUser Patient;    //testa detta annars får vi gå in och ändra i IUser till Username{get; set;} eller lägga till ett ID som vi kan ha som referens material!!!!
    public IUser Personnel;
    public string Text;
    public DateTime dateTime;
    public ReadPermisson readPermisson; //read permissions?

    public JournalEntry(Patient patient, IUser personnel, string text, ReadPermisson readPerm)
    {
        Patient = patient;
        Personnel = personnel;
        Text = text;
        dateTime = DateTime.Now;

        readPermisson = readPerm; //börja som None. set när man lägger in en entry
    }

    public enum ReadPermisson
    {
        None,
        PatientRead, //public? alla kan läsa?
        PersonnelRead, // dom som jobbar på sjukhuset?
        AdminRead, // Bara läkare kan läsa
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


    // public static void AddJournalEntry(List<JournalEntry> journals, List<IUser> users, IUser? activeUser)
    // {
    //     Console.WriteLine("Adding new journal entry:"); //loopa fram till rätt patient
    //     Console.WriteLine(" ");
    //     Console.WriteLine("Write the name of the patient you wish to make an entry for");

    //     Patient? paitentuser = null;


    //     string? PatientInput = Console.ReadLine();
    //     foreach (var user in users)
    //     {
    //         if (user is Patient p && p.Email == PatientInput)
    //         {
    //             paitentuser = p;
    //             break;
    //         }
    //     }

    //     System.Console.WriteLine($"You have selected {paitentuser!.Email}");
    //     System.Console.WriteLine("Write documentaion:");
    //     string? TextInput = Console.ReadLine();

    //     journals.Add(new JournalEntry(paitentuser, activeUser!, TextInput!));  //fråga om vi kan lägga till username i IUser?

    // }
}

/*
Note:
använd ReadPermisson med User role för att skriva logik????
*/