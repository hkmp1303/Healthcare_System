using System.Net.Security;

namespace HospitalApp;

class JournalEntry
{
    //id på journalen?
    public IUser Patient;
    public IUser Personnel;
    public string Text;
    //tid när den skrevs?
    public ReadPermisson readPermisson; //read permissions?

    public JournalEntry(IUser patient, IUser personnel, string text)
    {
        Patient = patient;
        Personnel = personnel;
        Text = text;

        readPermisson = ReadPermisson.None; //börja som None. set när man lägger in en entry
    }

    public enum ReadPermisson
    {
        None,
        PatientRead, //public? alla kan läsa?
        StaffRead, // dom som jobbar på sjukhuset?
        DoctorRead, // Bara läkare kan läsa
    }

    
    public void AddJournalEntry(List<JournalEntry> journals, List<IUser> users, IUser? activeUser)
    {
        Console.WriteLine("Adding new journal entry:"); //loopa fram till rätt patient
        Console.WriteLine(" ");
        Console.WriteLine("Write the name of the patient you wish to make an entry for");

        Patient? paitentuser = null;


        string? PatientInput = Console.ReadLine();
        foreach (Patient user in users)
        {
            if (user.Email == PatientInput)
            {
                paitentuser = user;
                break;
            }
        }

        System.Console.WriteLine($"You have selected {paitentuser!.Email}");
        System.Console.WriteLine("Write documentaion:");
        string? TextInput = Console.ReadLine();

        journals.Add(new JournalEntry(paitentuser, activeUser!, TextInput!));  //fråga om vi kan lägga till username i IUser?
        
    }
}

/*
Note:
använd ReadPermisson med User role för att skriva logik????
*/