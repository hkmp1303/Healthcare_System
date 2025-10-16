using HospitalApp;
Duno saving = new();
List<IUser> users = saving.LoadUsers();
List<Patient> patients = new();
IUser? activeUser = null;


Schedule schedule = new();


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
        System.Console.WriteLine("Welcome patient, "); // Should say ("Welcome patient, " + Patient-Name)
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
        System.Console.WriteLine("Welcome personnel, "); // Should say ("Welcome personnel, " + personnel-Name)
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
        System.Console.WriteLine("[10]. Accept user registration as a patient");
        System.Console.WriteLine("[11]. Deny user registration as a patient");
        System.Console.WriteLine("[L]. Log Out");
        System.Console.WriteLine("[Q]. Quit");

        switch (Console.ReadLine())
        {
            case "1":
                // Allow admins to handle permissions system -> in fine granularity 
                break;
            case "2":
                // Allow an admin to handle registrations
                break;
            case "3":
                // Assign an admin to a region
                break;
            case "4":
                // Add a location (or two if you feel it :D)
                break;
            case "5":
                // Allow an admin to add a location
                break;
            case "6":
                // Create an account for a personnel -> so just convert a user to personnel I suppose
                break;
            case "7":
                // Allow admins to create an account for personnel
                break;
            case "8":
                // View role/account-based permission -> A list of who has permissions for what
                break;
            case "9":
                // Add admins to view the role/account-based permissions list
                break;
            case "10":
                // Accept a user registration as a patient
                break;
            case "11":
                // Deny a user registration as a patient
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