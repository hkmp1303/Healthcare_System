using HospitalApp;
Duno saving = new();
List<IUser> users = saving.LoadUsers();
List<Patient> patients = new();
IUser? activeUser = null;
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
                     //Add a registration case for non-patients/personnel/Admins
                break;
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
        }
    }
    if (activeUser!.IsRole(Role.Patient))
    {
        System.Console.WriteLine("Welcome patient,");
        System.Console.WriteLine("[1]. View Journal");
        System.Console.WriteLine("[2]. Request an appointment");
        System.Console.WriteLine("[3]. View schedule");
        System.Console.WriteLine("[L]. Log Out");
        System.Console.WriteLine("[Q]. Quit");

        switch (Console.ReadLine())
        {
            case "1":
                        // View Journal
                break;
            case "2":
                        // Request an appointment
                break;
            case "3":
                        // View Schedule
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

    if (activeUser!.IsRole(Role.Personnel))
    {
        System.Console.WriteLine("Welcome personnel,");
        System.Console.WriteLine("[1]. View patients journal");
        System.Console.WriteLine("[2]. Make journal entries");
        System.Console.WriteLine("[3]. Register appointments");
        System.Console.WriteLine("[4]. Modify appointments");
        System.Console.WriteLine("[5]. Approve appointment requests");
        System.Console.WriteLine("[6]. View locaction specific appointments");
        System.Console.WriteLine("[L]. Log Out");
        System.Console.WriteLine("[Q]. Quit");

        switch (Console.ReadLine())
        {
            case "1":
                // View patients Journal
                break;
            case "2":
                // Make journal entries
                break;
            case "3":
                // Register appointments
                break;
            case "4":
                // Modify appointments
                break;
            case "5":
                // approve appointment requests
                break;
            case "6":
                // View location specific appointments
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

    if (activeUser!.IsRole(Role.Admin))
    {
        System.Console.WriteLine("Welcome admin,");
        System.Console.WriteLine("[L]. Log Out");
        System.Console.WriteLine("[Q]. Quit");

        switch (Console.ReadLine())
        {
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
            case "2":
                IUser newUser = UserCreator.CreateUF();
                users.Add(newUser);
                saving.SaveUser(users);
                Console.WriteLine("User saved!");

                break;


        }
    }
    else
    {
        System.Console.WriteLine("welcome");
        System.Console.WriteLine("You're logged in");

    }

}