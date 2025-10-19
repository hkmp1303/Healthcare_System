namespace HospitalApp;

/*
assign personnel to regions
add locations
accept user registration as a patient
deny user registration as a patient
create personnel accounts
view the permissions list
assign permissions for the permission system in fine granularity including:
    permission to handle registrations
    permission to add locations
    permission to create personnel accounts
    permission to view the permissions list
*/

class Admin : CommonPersonnel
{
    // instantiates CommonPersonnel abstract class
    // admin class constructor : call to parent class constructor
    public Admin(string username, string password) : base(username, password, Role.Admin)
    {

    }


    // Assign personnel users to a region
    // Requires static List<Region> in Region Class
    public void AssignPersonelToRegion(Personnel user)
    {
        while (true)
        {
        Console.Clear();
        System.Console.Write("Enter the Region you wish to assign: ");

        //Region region = Region.PickRegionFromList(); // call static method, pick region
        System.Console.WriteLine("${Region} has been assigned to user {user}");
        //region.AssignedUsers.Add(user);  // add user to region user list
        //user.Region = region; // assign region to personnel user
        }
    }


    public static void AddLocations()
    {
        Console.Clear();
        while (true)
        {
            System.Console.Write("Name of new Location: ");
            string locationName = Console.ReadLine() ?? "";
            if (locationName == "") continue; // check for empty input, restart while loop
            //Location.AddLocation(locationName);
        }
    }

    // handle patient registration requests
    public void HandlePatientRegistration(List<IUser> users)
    {
        System.Console.WriteLine("Patient Registration Request(s)");
        // calling Utilites to filter users
        Dictionary<string, IUser> dictUsers = Utilities.FilterUsersByRole(Utilities.ConvertUserList(users), Role.PatientRegRequested);
        if (dictUsers.Count == 0)
        {
            System.Console.WriteLine("There are no users requesting patient registration./nPress [Enter] to return to the Admin menu");
            Console.ReadLine();
            return;
        }
        // pick user from Unregistered users requesting patient registration
        UnregUser selectedUser = (UnregUser)Utilities.PickUserFromList(dictUsers, null);
        while (true)
        {
            // calling PrintLine method, printing registration menu
            Utilities.PrintLines(new[] { $"User requesting patient registration: {selectedUser.Username}",
                "[a] approve patient registration request",
                "[d] deny patient registration request",
                "[enter] to return to the Admin menu"
            });
            switch (Console.ReadLine() ?? "")
            {
                case "a": // approve registration, convert UnregUser to Patient
                    System.Console.WriteLine($"Patient registration for {selectedUser.Username} is approved.");
                    users.Add(new Patient(selectedUser)); // add new Patient user
                    users.Remove(selectedUser); // remove Unregistered user
                    return;
                case "d": // deny registration, convert PatientRegRequested to Patient
                    System.Console.WriteLine($"Patient registration for {selectedUser.Username} is denied.");
                    selectedUser.Role = Role.UnregUser;
                    return;
                default: // Admin menu
                    return;
            }
        }

    }

    public static Personnel CreatePersonnelAccount()
    {
        System.Console.Write("Enter the username of the new personnel account: ");
        string username = Console.ReadLine() ?? "";
        string password = "TODO";  // auto generate from CommonPersonnel
        return new Personnel(username, password);
    }

    // static method show admin menu
    public static void ShowMenu(Admin activeUser)
    {
        // TODO Menu
        while (true)
        {
            List<string> menu = new();
            menu.Add($"Welcome {activeUser.Username}!/nAdmin Menu/n");
            menu.Add("[a] ");
            menu.Add("");
            menu.Add("");
            menu.Add("");
            Utilities.PrintLines(menu);
            string selection = Console.ReadLine() ?? "";
            switch (selection)
            {
                case "a":

                break;
            }
            Console.Clear();

        }
    }

}