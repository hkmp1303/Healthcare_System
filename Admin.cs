using System.Globalization;

namespace HospitalApp;

class Admin : CommonPersonnel
{
    public Admin(string username, string password) : base(username, password, Role.Admin)
    {

    }

    /*
    public void AssignPersonelToRegion()
    {
        while (true)
        {
        }
    }
    */

    public void AddLocations()
    {
        Console.Clear();
        while (true)
        {
            System.Console.Write("Name of new Location: ");
            string locationName = Console.ReadLine() ?? "";
            //Location.AddLocation(locationName);
        }
    }
}