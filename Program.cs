using HospitalApp;
Duno saving = new();
List<IUser> users = saving.LoadUsers();
List<Patient> patients = new();
IUser? activeUser = null;
List<JournalEntry> journals = new();


Schedule schedule = new();
JournalSystem journalSystem = new();


saving.CheckFile();

bool running = true;


while (running)
{

    if (activeUser == null)
    {
        System.Console.WriteLine("welcome to the start page");
        System.Console.WriteLine("[1] Register");
        System.Console.WriteLine("[2]. Login");
        System.Console.WriteLine("[Q]. Quit");

        switch (Console.ReadLine())
        {
            case "1":
                IUser newUSer = UserCreator.CreateUnreg();
                users.Add(newUSer);
                saving.SaveUser(users);
                Console.WriteLine("sucssses");
                break;


            // break;
            case "2":
                Console.Write("Username:");
                string? usernameInput = Console.ReadLine();
                Console.Write("Password:");
                string? passwordInput = Console.ReadLine();

                foreach (IUser user in users)
                {
                    if (user.TryLogin(usernameInput!, passwordInput!))
                    {
                        activeUser = user;
                        break;
                    }
                }
                break;
            case "Q":
                running = false;
                break;
        }
    }
    else if (activeUser!.IsRole(Role.UnregUser))
    {
        Console.WriteLine("you are unregisterd");
        Console.ReadKey();
        activeUser = null;
    }
    else if (activeUser!.IsRole(Role.Patient))
    {
        System.Console.WriteLine("Welcome patient, "); // Should say ("Welcome patient, " + Patient-Name)
        System.Console.WriteLine("[1]. View Journal");
        System.Console.WriteLine("[2]. Request an appointment");
        System.Console.WriteLine("[3]. View schedule");
        System.Console.WriteLine("[L]. Log Out");
        System.Console.WriteLine("[Q]. Quit");

        switch (Console.ReadLine())
        {
            case "1":
            journalSystem.SeeJournalEntry(journals, users, activeUser);
                break;
            case "2":
                schedule.AppointmentRequest(activeUser);
                break;
            case "3":
                schedule.PrintSchedulePatient(activeUser);
                break;
            case "L":
                if (activeUser != null)
                {
                    activeUser = null;
                    Console.WriteLine("Succesfully logged out");
                }
                break;
            case "Q":
                running = false;
                break;
        }
    }

    else if (activeUser!.IsRole(Role.Personnel))
    {
        System.Console.WriteLine("Welcome personnel, "); // Should say ("Welcome personnel, " + personnel-Name)
        System.Console.WriteLine("[1]. View patients journal");
        System.Console.WriteLine("[2]. Make journal entries");
        System.Console.WriteLine("[3]. Register appointments");
        System.Console.WriteLine("[4]. Modify appointments");
        System.Console.WriteLine("[5]. Approve appointment requests");
        System.Console.WriteLine("[6]. View locaction specific appointments");
        System.Console.WriteLine("[7]. View own shcedule");
        System.Console.WriteLine("[L]. Log Out");
        System.Console.WriteLine("[Q]. Quit");

        switch (Console.ReadLine())
        {
            case "1":
                journalSystem.SeeJournalEntry(journals, users, activeUser);
                break;
            case "2":
                journalSystem.AddJournalEntry(journals, users, activeUser);
                break;
            case "3":
                schedule.BookAppointment(users);
                break;
            case "4":
                // modify appointments - inte gjord än
                break;
            case "5":
                // approve appointment requests
                break;
            case "6":
                // View location specific appointments - inte gjord än
                schedule.PrintSchedule();
                break;
            case "7":
                schedule.PrintSchedulePersonnal(activeUser);
                break;
            case "L":
                if (activeUser != null)
                {
                    activeUser = null;
                    Console.WriteLine("Succesfully logged out");
                }
                break;
            case "Q":
                running = false;
                break;
        }
    }

    else if (activeUser!.IsRole(Role.Admin))
    {
        System.Console.WriteLine("Welcome admin,"); // Should say ("Welcome admin, " + Admin-Name)
        System.Console.WriteLine("[1]. Add admins to handle permissions system");
        System.Console.WriteLine("[2]. Allow an admin to handle registrations");
        System.Console.WriteLine("[3]. Assign an admin to a region");
        System.Console.WriteLine("[4]. Add location(s)");
        System.Console.WriteLine("[5]. Allow an admin to add location(s)");
        System.Console.WriteLine("[6]. Create personnel account");
        System.Console.WriteLine("[7]. Allow admins to create personnel accounts");
        System.Console.WriteLine("[8]. View account-based permissions");
        System.Console.WriteLine("[9]. Allow an admin to view account-based permissions");
        System.Console.WriteLine("[10]. Accept/Deny user registration as a patient");
        System.Console.WriteLine("[L]. Log Out");
        System.Console.WriteLine("[Q]. Quit");

        switch (Console.ReadLine())
        {
            case "1":
                // Allow admins to handle permissions system -> in fine granularity 
                break;
            case "3":
                // Assign an admin to a region
                break;
            case "4":
                Admin.AddLocations();
                break;
            case "5":
                // allow an admin to add location(s)
                break;
            case "6":
                Admin.CreatePersonnelAccount();
                break;
            case "7":
                IUser newUser = UserCreator.CreateUF();
                users.Add(newUser);
                saving.SaveUser(users);
                Console.WriteLine("User saved!");
                break;
            case "8":
                // View role/account-based permission -> A list of who has permissions for what
                break;
            case "9":
                // Add admins to view the role/account-based permissions list -> for loop
                break;
            case "10":
                // Accept/Deny a user registration as a patient
                break;
            case "L":
                if (activeUser != null)
                {
                    activeUser = null;
                    Console.WriteLine("Succesfully logged out");
                }
                break;
            case "Q":
                running = false;
                break;
        }
    }

    else
    {
        System.Console.WriteLine("welcome");
        System.Console.WriteLine("You're logged in");

    }

}