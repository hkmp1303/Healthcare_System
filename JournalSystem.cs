namespace HospitalApp;

class JournalSystem
{

    public void AddJournalEntry(List<JournalEntry> journals, List<IUser> users, IUser? activeUser)
    {
        Console.WriteLine("Adding new journal entry:");
        Console.WriteLine(" ");
        Console.WriteLine("Write the name of the patient you wish to make an entry for");

        string? PatientInput = Console.ReadLine();

        Patient? patientuser = users.OfType<Patient>().FirstOrDefault(p => p.Email == PatientInput); // skapar först en variabel av typen Patient. kollar listan an användare, men klassen Patient, letar rätt på den första som har den emailen och gemför så att emailen är samma som input.

        if(patientuser == null) //om det är null
        {
            System.Console.WriteLine("Something went wrong");
            return;
        }

        System.Console.WriteLine($"You have selected {patientuser!.Email}"); //sanity check så man har valt rätt
        System.Console.WriteLine("Write documentaion:");
        string? TextInput = Console.ReadLine(); //input som blir text dokumentation
        if(TextInput == null) //om null
        {
            System.Console.WriteLine("Something went wrong");
            return;
        }
        System.Console.WriteLine("You have succesfully entered the documentaion");

        System.Console.WriteLine("Select read permission:");
        System.Console.WriteLine("[1] Patient lvl permission"); //ger patient och personal access
        System.Console.WriteLine("[2] Personnel lvl permission"); //ger bara personal access
        System.Console.WriteLine("[3] something something"); //test med .none role

        switch (Console.ReadLine())
        {
            case "1":
                journals.Add(new JournalEntry(patientuser, activeUser!, TextInput!, JournalEntry.ReadPermisson.PatientRead | JournalEntry.ReadPermisson.PersonnelRead));
                break;
            case "2":
                journals.Add(new JournalEntry(patientuser, activeUser!, TextInput!, JournalEntry.ReadPermisson.PersonnelRead));
                break;
            case "3":
                journals.Add(new JournalEntry(patientuser, activeUser!, TextInput!, JournalEntry.ReadPermisson.None)); //bara en test
                break;
            default:
                System.Console.WriteLine("Something went wrong"); //om något annat skrevs in som inte är ett valt
                break;
        }

        System.Console.WriteLine("text that it is finished"); //sanity check så att det gick ignom
        Console.ReadLine();
        
    }
    
    public void SeeJournalEntry(List<JournalEntry> journals, List<IUser> users, IUser? activeUser)
    {
        if (activeUser!.IsRole(Role.Patient))
        {
            foreach (JournalEntry je in journals)
            {
                if (activeUser == je.Patient)
                {

                    Console.WriteLine($"Patient {je.Patient}");
                    Console.WriteLine($"Entry: {je.Text}");
                    Console.WriteLine($"Auther: {je.Personnel}");
                    Console.WriteLine($"Writen: {je.dateTime}");
                }
            }
        }
        else if (activeUser!.IsRole(Role.Personnel))
        {
            System.Console.WriteLine("Welcome to the Journal System:");
            System.Console.WriteLine("[1] Search patients journal");
            System.Console.WriteLine("[2] Browers all entrys you have made");
            switch (Console.ReadLine())
            {
                case "1":

                    System.Console.WriteLine("Write the Patient you wish to look up journal for");
                    string? LookUpInput = Console.ReadLine();

                    var paitenSearch = users.OfType<Patient>().FirstOrDefault(p => p.Email == LookUpInput);

                    foreach (JournalEntry je in journals)
                    {
                        if (paitenSearch == je.Patient)
                        {

                            Console.WriteLine($"Patient {je.Patient}");
                            Console.WriteLine($"Entry: {je.Text}");
                            Console.WriteLine($"Auther: {je.Personnel}");
                            Console.WriteLine($"Writen: {je.dateTime}");
                        }
                    }

                    break;
                case "2":

                    if (activeUser!.IsRole(Role.Personnel))
                    {
                        foreach (JournalEntry je in journals)
                        {
                            if (activeUser == je.Personnel)
                            {

                                Console.WriteLine($"Patient {je.Patient}");
                                Console.WriteLine($"Entry: {je.Text}");
                                Console.WriteLine($"Auther: {je.Personnel}");
                                Console.WriteLine($"Writen: {je.dateTime}");
                            }
                        }
                    }
                break;
            }
        }
        else
        {
            System.Console.WriteLine("You do not have the right permission to view these files");
        }        
    }
    

}
