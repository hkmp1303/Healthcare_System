using HospitalApp;
Duno saving = new();
List<IUser> users = saving.LoadUsers();
//saving.LoadPermissions(); // Permission.Permissions -contains all permissions for saving
Permission.AttachPermissionsToUsers(Utilities.ConvertUserList(users));
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
        Console.Clear();
        System.Console.WriteLine("welcome to the start page");
        System.Console.WriteLine(" ");
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
        Console.Clear();
        Console.WriteLine("you are unregisterd");
        Console.ReadKey();
        activeUser = null;
    }
    else if (activeUser!.IsRole(Role.Patient))
    {
        Console.Clear();
        System.Console.WriteLine("Welcome patient, "); // Should say ("Welcome patient, " + Patient-Name)
        System.Console.WriteLine(" ");
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
        Console.Clear();
        System.Console.WriteLine($"Welcome {((Personnel)activeUser).Username}\nPersonnel Menu\n");
        System.Console.WriteLine("[1]. View patients journal");
        System.Console.WriteLine("[2]. Make journal entries");
        System.Console.WriteLine("[3]. Register appointments");
        System.Console.WriteLine("[4]. Modify appointments");
        System.Console.WriteLine("[5]. View appointment requests"); //Approve
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
                schedule.SeeAppointmentRequest();
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
        bool? run = ((Admin)activeUser).ShowAdminMenu(users);
        if (run == null) // null meaning logout
        {
            activeUser = null;
        }
        else
        {
            if (run == false)
            {
                Console.Clear();
                System.Console.WriteLine("Goodbye");
            }
            running = (bool)run; // Exit program from ShowAdminMenu user input
        }
    }
}

// save all users when exiting the program
saving.SaveUser(users);