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
        // foreach (var user in users)
        // {
        //     if (user is Patient p && p.Email == PatientInput)
        //     {
        //         patientuser = p;
        //         break;
        //     }
        // }

        if(patientuser == null)
        {
            System.Console.WriteLine("Something went wrong");
            return;
        }

        System.Console.WriteLine($"You have selected {patientuser!.Email}");
        System.Console.WriteLine("Write documentaion:");
        string? TextInput = Console.ReadLine();

        journals.Add(new JournalEntry(patientuser, activeUser!, TextInput!));  //fråga om vi kan lägga till username i IUser?
    }
    

    public void SeeJournal(List<JournalEntry> journals)
    {
        foreach(JournalEntry je in journals)
        {
            System.Console.WriteLine($"Patient {je.Patient}");
            System.Console.WriteLine($"Entry: {je.Text}");
            System.Console.WriteLine($"Auther: {je.Personnel}");
        }

    }
}