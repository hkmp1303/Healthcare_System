using Microsoft.VisualBasic;

namespace HospitalApp;

class Utilities
{
    // Convert user list to Dictionary of all users
    public static Dictionary<string, IUser> ConvertUserList(List<IUser> users)
    {
        Dictionary<string, IUser> userDict = new();
        foreach (var user in users)
        {
            if (user.GetType().IsSubclassOf(typeof(CommonPersonnel)))
                userDict.Add(((CommonPersonnel)user).Username, user);
            else if (user is Patient || user is UnregUser)
                userDict.Add(((UnregUser)user).Email, user);
        }
        return userDict;
    }

    // returns dictionary collection to file
    public static void ExportToFile(string fileName, Type type, Dictionary<string, IUser> userDict)
    {
        List<string> lines = new();
        foreach (var user in userDict)
        {
            if (type == typeof(Permission) && (user.Value.GetRole() & (Role.Admin | Role.Personnel)) > 0)
            {
                // throw new Exception("ohoh");
                CommonPersonnel personnel = (CommonPersonnel)user.Value;
                string line = personnel.Permissions.Serialize(personnel);
                lines.Add(line);
            }
            else if (type == typeof(JournalEntry))
            {
            }
        }
        // File.Delete(fileName);
        File.WriteAllLines(fileName, lines);
    }

    // returns dictionary collection from file
    public static Dictionary<string, object> ImportFromFile(string fileName, Type type, List<IUser> users)
    {
        string[] lines = File.ReadAllLines(fileName);
        Dictionary<string, object> dictOutput = new();
        // if (type == typeof(Permission)) dictOutput = new Dictionary<string, Permission>();
        // = new Dictionary<string, object>();
        foreach (var dataLine in lines)
        {
            string[] data = dataLine.Split(':');
            if (type == typeof(Permission))
            {
                Console.WriteLine(data[0]);
                Permission perm = Permission.Deserialize(data);
                foreach (var user in users)
                    if ((user.GetRole() & (Role.Personnel | Role.Admin)) > 0) if(((CommonPersonnel)user).Username == data[0])
                        ((CommonPersonnel)user).Permissions = perm;
                dictOutput.Add(data[0], perm);
            }
            else if (type == typeof(JournalEntry))
            {
            }
        }
        return dictOutput;
    }

    // convert generic dictionary to Permission
    public static Dictionary<string, Permission> ConvertObjToPermission(Dictionary<string, object> ObjDict)
    {
        Dictionary<string, Permission> dictOutput = new();
        foreach (var obj in ObjDict)
        {
            dictOutput.Add(obj.Key, (Permission)obj.Value);
        }
        return dictOutput;
    }

    // convert permission dictionary to generic dictionairy
    public static Dictionary<string, object> ConvertPermissionToObj(Dictionary<string, Permission> permDict)
    {
        Dictionary<string, object> objDict = new();
        foreach (var perm in permDict)
        {
            objDict.Add(perm.Key, (object)perm.Value);
        }
        return objDict;
    }

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
                // filter by role, filter can be null, can filter multiple roles
                if (filterRole == null || (user.Value.GetRole() & filterRole) > 0)
                {
                    availableUsers.Add(user.Value); // Add user to pick list
                    System.Console.WriteLine($"[{++i}]. {user.Key}"); // print filtered user to console
                }
            }
            System.Console.Write("Selected User: ");
            int selection = 0;
            if (!int.TryParse(Console.ReadLine(), out selection)) continue;
            if (availableUsers.Count < selection || selection <= 0) continue; // check if selected users is valid
            return availableUsers[selection - 1]; // selected user
        }
    }

    // filter dictionary of users based on role
    public static Dictionary<string, IUser> FilterUsersByRole(Dictionary<string, IUser> users, Role filterRole)
    {
        List<IUser> availableUsers = new(users.Count); // list to pick from
        foreach (var user in users) // loops through users
        {
            // filter by role
            if (user.Value.IsRole((Role)filterRole))
            {
                availableUsers.Add(user.Value); // Add user to pick list
            }
        }
        return ConvertUserList(availableUsers);
    }

    // filter out patients from users
    public static Dictionary<string, CommonPersonnel> FilterCommonPersonnel(Dictionary<string, IUser> users)
    {
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

    // print menu
    public static void PrintLines(IEnumerable<string> lines)
    {
        foreach (string line in lines)
        {
            //System.Console.WriteLine($"[{character}]"+line);
            System.Console.WriteLine(line);
        }
    }

    // username check, any user type
    public static bool CheckUsername(List<IUser> users, string newUsername)
    {
        var userDict = ConvertUserList(users);
        IUser u;
        return userDict.TryGetValue(newUsername, out u!); // throwing away u
    }

    // generate random password
    public static string GeneratePassword(int passLength)
    {
        string newPassword = "";
        Random rnd = new Random();
        string validChar = "abcdefghijklmnopqrstuvwxyz0123456789";
        for (int i = 0; i < passLength; i++)
        {
            newPassword += validChar[rnd.Next(0, validChar.Length - 1)];
        }
        return newPassword;
    }
}