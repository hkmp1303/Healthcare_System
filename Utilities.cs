namespace HospitalApp;

class Utilities
{
    // TODO requires Dictionary of users
    // select a user from unfiltered list, filter by role
    public static IUser PickUserFromList(Dictionary<string, IUser> users, Role? filterRole)
    {
        while (true)
        {
            System.Console.WriteLine("Pick a user from the list:");
            List<IUser> availableUsers = new(users.Count); // list to pick from
            int i = 0;
            foreach (var user in users) // loops through users
            {
                // filter by role, filter can be null
                if (filterRole == null || user.Value.IsRole((Role)filterRole))
                {
                    availableUsers.Add(user.Value); // Add user to pick list
                    System.Console.WriteLine($"[{++i}]. {user.Key}"); // print filtered user to console
                }
            }
            System.Console.Write("Choice: ");
            int selection = 0;
            if (!int.TryParse(Console.ReadLine(), out selection)) continue;
            if (availableUsers.Count < selection) continue; // check if selected users is valid
            return availableUsers[selection - 1]; // selected user
        }

    }
    public static Dictionary<string, CommonPersonnel> FilterCommonPersonnel(Dictionary<string, IUser> users)
    {
        //return (Dictionary<string, CommonPersonnel>)users.Where(
        //    kvp => kvp.Value.GetType().IsSubclassOf(typeof(CommonPersonnel)));
        Dictionary<string, CommonPersonnel> commonPersonnel = new();
        foreach (var user in users)
        {
            // sanity check object derived from abstract class
            if (!user.GetType().IsSubclassOf(typeof(CommonPersonnel))) continue;
            if (user.Value.IsRole(Role.Admin))
                commonPersonnel.Add(user.Key, (Admin)user.Value);
            if (user.Value.IsRole(Role.Personnel))
                commonPersonnel.Add(user.Key, (Personnel)user.Value);
        }
        return commonPersonnel;
    }
}