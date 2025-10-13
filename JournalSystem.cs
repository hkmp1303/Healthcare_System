using System.Reflection.Metadata;

namespace HospitalApp;

class JournalSystem
{

    public void AddJournalEntry(List<JournalEntry> journals, List<IUser> users, IUser? activeUser)
    {
        Console.WriteLine("Adding new journal entry:"); //loopa fram till rätt patient
        Console.WriteLine(" ");
        Console.WriteLine("Write the name of the patient you wish to make an entry for");

        string? PatientInput = Console.ReadLine();

        Patient? patientuser = users.OfType<Patient>().FirstOrDefault(p => p.Email == PatientInput);

        if(patientuser == null)
        {
            System.Console.WriteLine("Something went wrong");
            return;
        }

        System.Console.WriteLine($"You have selected {patientuser!.Email}");
        System.Console.WriteLine("Write documentaion:");
        string? TextInput = Console.ReadLine();
        if(TextInput == null)
        {
            System.Console.WriteLine("Something went wrong");
            return;
        }
        System.Console.WriteLine("You have succesfully entered the documentaion");

        System.Console.WriteLine("Select read permission:");
        System.Console.WriteLine("[1] Patient lvl permission");
        System.Console.WriteLine("[2] Personnel lvl permission");
        System.Console.WriteLine("[3] something something");

        switch (Console.ReadLine())
        {
            case "1":
                journals.Add(new JournalEntry(patientuser, activeUser!, TextInput!, JournalEntry.ReadPermisson.PatientRead));
                break;
            case "2":
                journals.Add(new JournalEntry(patientuser, activeUser!, TextInput!, JournalEntry.ReadPermisson.PersonnelRead));
                break;
            case "3":
                journals.Add(new JournalEntry(patientuser, activeUser!, TextInput!, JournalEntry.ReadPermisson.None));
                break;
            default:
                System.Console.WriteLine("Something went wrong");
                break;
        }

        System.Console.WriteLine("text that it is finished");
        Console.ReadLine();
        
    }


    public void SeeJournal(List<JournalEntry> journals)
    {
        foreach (JournalEntry je in journals)
        {
            System.Console.WriteLine($"Patient {je.Patient}");
            System.Console.WriteLine($"Entry: {je.Text}");
            System.Console.WriteLine($"Auther: {je.Personnel}");
            System.Console.WriteLine($"Writen: {je.dateTime}");
            System.Console.WriteLine($"Read Permission: {je.readPermisson}");
        }

    }
    

}