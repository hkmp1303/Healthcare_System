namespace HospitalApp;

/*
DFN assign personnel to regions
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

    /*
    // Assign personnel users to a region
    // Requires static List<Region> in Region Class
    public void AssignPersonelToRegion(Personnel user)
    {
        while (true)
        {
        TODO Menu
            Region region = Region.PickRegionFromList(); // call static method, pick region
            region.AssignedUsers.Add(user);  // add user to region user list
            user.Region = region; // assign region to personnel user
        }
    }
    */

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

        }
    }

}