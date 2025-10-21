namespace HospitalApp;

class JournalSystem //logic for handeling the journal and journal entries
{

    public void AddJournalEntry(List<JournalEntry> journals, List<IUser> users, IUser? activeUser) //add journal entry method. using the journals list, users list, and who is active user
    {
        Console.Clear(); //clear the screen
        Console.WriteLine("Adding new journal entry:"); //text
        Console.WriteLine(" "); //nice space
        System.Console.WriteLine("Choose one of your own patients or seach for one:"); //text
        System.Console.WriteLine("[1] Your patients");
        System.Console.WriteLine("[2] Browers patients"); //giving the two options
        string? AJEInput = Console.ReadLine(); //getting a string to work with

        if(AJEInput == "1") //option 1
        {
            foreach(IUser user in users)
            {
                if (user.IsRole(Role.Patient))
                {
                    System.Console.WriteLine($"Patient: {user.ToString()}");
                }
            }
        Console.WriteLine("Write the name of the patient you wish to make an entry for");

        string? PatientInput = Console.ReadLine(); //string to use to get the right patient.
        if(PatientInput == null) //it it's null
        {
            System.Console.WriteLine("Wrong input");
            Console.ReadLine();
            return;
        }

        Patient? patientuser = users.OfType<Patient>().FirstOrDefault(p => p.Email == PatientInput); // creating a variable to use as the patient by checking the users list. first the linq sort the list by user type (patient), the it looks up the first patient (email) that matches the string input.

        if(patientuser == null) //if it's null
        {
            System.Console.WriteLine("Patient do not exist");
            Console.ReadLine();
            return;
        }

        System.Console.WriteLine($"You have selected {patientuser!.Email}"); //sanity check to see if it's the right patient
        System.Console.WriteLine("Write documentaion:");
        string? TextInput = Console.ReadLine(); //input for the text documentaion
        if(TextInput == null) //if it's null
        {
            System.Console.WriteLine("Something went wrong");
            Console.ReadLine();
            return;
        }
        System.Console.WriteLine("You have succesfully entered the documentaion"); //sanity check 

        System.Console.WriteLine("Select read permission:");
        System.Console.WriteLine("[1] Patient lvl permission"); //sets the read permission to patient och personnel
        System.Console.WriteLine("[2] Personnel lvl permission"); //sets the read permission to only personnel

        switch (Console.ReadLine())
        {
            case "1": //sets the read permission to patient och personnel.... by using the patient variable from earlier, who the active user is, text documentaion string and enum read permission.
                journals.Add(new JournalEntry(patientuser, activeUser!, TextInput!, JournalEntry.ReadPermisson.PatientRead | JournalEntry.ReadPermisson.PersonnelRead));
                break;
            case "2":
                journals.Add(new JournalEntry(patientuser, activeUser!, TextInput!, JournalEntry.ReadPermisson.PersonnelRead));
                break;
            default:
                System.Console.WriteLine("Something went wrong"); //if you do an incorrent choice
                Console.ReadLine();
                break;
        }

        System.Console.WriteLine("text that it is finished"); //sanity check to see that it worked.
        Console.ReadLine();
        
    }    
}
        
        
        
    
    public void SeeJournalEntry(List<JournalEntry> journals, List<IUser> users, IUser? activeUser)
    {
        if (activeUser!.IsRole(Role.Patient))
        {
            Console.Clear();
            System.Console.WriteLine($"Journal entries for {activeUser.ToString()}");
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
            Console.ReadLine();
        }
        else if (activeUser!.IsRole(Role.Personnel))
        {
            Console.Clear();
            System.Console.WriteLine("Welcome to the Journal System:");
            System.Console.WriteLine("[1] Search patient journal");
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
                    Console.ReadLine();

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
                    Console.ReadLine();
                    break;
                default:
                System.Console.WriteLine("Something went wrong.");
                    break;
            }
        }
        else
        {
            System.Console.WriteLine("You do not have the right permission to view these files");
        }        
    }
    

}
