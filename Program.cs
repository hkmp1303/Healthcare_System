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
        System.Console.WriteLine("welcome to start page");
        System.Console.WriteLine("[1]. login");

        switch (Console.ReadLine())
        {
            case "1":
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

        switch (Console.ReadLine())
        {
            case "1":
                schedule.AppointmentRequest(activeUser);
                break;
            case "2":
                schedule.SeeAppointmentRequest();
                break;
            case "3":
                schedule.BookAppointment(users);
                break;
            case "4":
                schedule.PrintSchedule();
                break;
            case "5":
                schedule.PrintSchedulePatient(activeUser);
                break;
            case "9":
                activeUser = null;
                break;
        }

    }

}