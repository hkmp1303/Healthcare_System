namespace HospitalApp;

class JournalSystem
{

    public void AddJournalEntry(List<JournalEntry> journals, List<IUser> users, IUser? activeUser)
    {
        Console.WriteLine("Adding new journal entry:"); //loopa fram till rätt patient
        Console.WriteLine(" ");
        Console.WriteLine("Write the name of the patient you wish to make an entry for");

        Patient? paitentuser = null;

        string? PatientInput = Console.ReadLine();

        foreach (var user in users)
        {
            if (user is Patient p && p.Email == PatientInput)
            {
                paitentuser = p;
                break;
            }
        }

        System.Console.WriteLine($"You have selected {paitentuser!.Email}");
        System.Console.WriteLine("Write documentaion:");
        string? TextInput = Console.ReadLine();

        journals.Add(new JournalEntry(paitentuser, activeUser!, TextInput!));  //fråga om vi kan lägga till username i IUser?
    }
    

    public void SeeJournal()
    {
        
    }
}