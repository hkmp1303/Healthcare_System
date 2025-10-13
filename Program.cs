using HospitalApp;

List<IUser> users = new();
List<JournalEntry> journals = new();
JournalSystem js = new();

users.Add(new Patient("kalle", "k")); //test Patient
users.Add(new Personnel("doc", "d")); //test personal


IUser? activeUser = null;

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

                foreach(IUser user in users)
                {
                    if(user.TryLogin(usernameInput!, passwordInput!))
                    {
                        activeUser = user;
                        break;
                    }
                }

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
                js.AddJournalEntry(journals, users, activeUser);
                break;
            case "2":
                js.SeeJournal(journals);
                break;
        }

    }    
    
}