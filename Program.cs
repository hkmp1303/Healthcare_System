using HospitalApp;
Duno saving = new();
List<IUser> users = saving.LoadUsers();
Dictionary<string, IUser> usersPrint = Utilities.ConvertUserList(users);
Permission.Permissions = Utilities.ConvertObjToPermission(Utilities.ImportFromFile(Path.Combine("file/permissions.txt"), typeof(Permission), users)); // load all permissions from file
//Permission.AttachPermissionsToUsers(Utilities.ConvertUserList(users));
List<Patient> patients = new();
IUser? activeUser = null;
Schedule schedule = new();
JournalSystem journalSystem = new();
const int days = 5;
const int slots = 7;
schedule.WeekSchedule = saving.Loadschedule(days, slots);
schedule.AppointmentRequests = saving.LoadRequestsList();
schedule.FilledNullOnSchedule();

List<JournalEntry> journals = saving.LoadJournals(users);


saving.CheckFile();

bool running = true;


while (running)
{

    if (activeUser == null)
    {
        Console.Clear();
        System.Console.WriteLine("Welcome to the Start Menu");
        System.Console.WriteLine(" ");
        System.Console.WriteLine("[1] Register");
        System.Console.WriteLine("[2]. Login");
        System.Console.WriteLine("[Q]. Quit");

        switch (Console.ReadLine())
        {
            case "1":
                Console.Clear();
                UnregUser newUSer = (UnregUser)UserCreator.CreateUnreg();
                if (Utilities.CheckUsername(users, newUSer.Username))
                {
                    System.Console.WriteLine($"{newUSer.Username} is not available. Enter a different selection.\n\nPress [enter] to return to the Start Menu ");
                    Console.ReadLine();
                    break;
                }
                users.Add(newUSer);
                saving.SaveUser(users);
                Console.Write("Your account has been registered.\n\nPress [enter] to return to the Start Menu ");
                Console.ReadLine();
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
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Welcome {((UnregUser)activeUser).Username}! You are a registered user.\nUser Menu");
            System.Console.WriteLine("[v] View my schedule");
            if (((UnregUser)activeUser).Role != Role.PatientRegRequested)
                System.Console.WriteLine("[r] Request patient registration");
            System.Console.WriteLine("\n[exit] to exit the program");
            System.Console.WriteLine("[logout] to logout of the program");
            switch (Console.ReadLine() ?? "")
            {
                case "v":
                    Console.Clear();
                    schedule.PrintSchedulePatient(activeUser);
                    continue;
                case "r":
                    Console.Clear();
                    ((UnregUser)activeUser).Role = Role.PatientRegRequested;
                    System.Console.WriteLine("Your request for patient registration has been sent.\n\nPress [Enter] to continue. ");
                    System.Console.ReadLine();
                    continue;
                case "exit":
                    running = false;
                    break;
                case "logout":
                    activeUser = null;
                    break;
                case "":
                    continue;
            }
            break; // exit program per user input
        }
    }
    else if (activeUser!.IsRole(Role.Patient))
    {
        Console.Clear();
        System.Console.WriteLine($"Welcome {((Patient)activeUser).Username}!\nPatient Menu");
        System.Console.WriteLine(" ");
        System.Console.WriteLine("[1]. View Journal");
        System.Console.WriteLine("[2]. Request an appointment");
        System.Console.WriteLine("[3]. View scheduled appointments");
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
                saving.SaveRequestsList(schedule.AppointmentRequests);
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
        schedule.Notifications();
        System.Console.WriteLine(" ");
        System.Console.WriteLine("[1]. View patients journal");
        System.Console.WriteLine("[2]. Make journal entries");
        System.Console.WriteLine("[3]. Register appointments");
        System.Console.WriteLine("[4]. Modify appointments");
        System.Console.WriteLine("[5]. View appointment requests"); //Approve
        System.Console.WriteLine("[6]. View Staff schedule");
        System.Console.WriteLine("[7]. View own schedule");
        System.Console.WriteLine("[L]. Log Out");
        System.Console.WriteLine("[Q]. Quit");

        switch (Console.ReadLine())
        {
            case "1":
                journalSystem.SeeJournalEntry(journals, users, activeUser);
                break;
            case "2":
                journalSystem.AddJournalEntry(journals, users, activeUser, schedule.WeekSchedule);
                break;
            case "3":
                schedule.BookAppointment(users);
                break;
            case "4":
                schedule.MoveAppointment();
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
                saving.SaveRequestsList(schedule.AppointmentRequests);
                saving.SaveSchedule(schedule.WeekSchedule);
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
saving.SaveJournals(journals);
// saving all permissions when exiting the program
Utilities.ExportToFile("file/permissions.txt", typeof(Permission), Utilities.ConvertUserList(users));
