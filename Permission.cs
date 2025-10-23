
namespace HospitalApp;
// As an admin, give permission to handle registrations, add locations,
// create personnel accounts and view list of permissions

class Permission
{
    #region Admin - the following permissions pertain to Admin users
        public bool CanRegisterPatients; // accept or deny
        public bool CanAddLocations;
        public bool CanCreatePersonnel; // accounts
        public bool CanViewPermissionsList;
        public AdminToAdminPermission CanAssignAdminPermissions; // assign or give
        public AdminToPersonnelPermission CanAssignPersonnelPermissions;
        public List<int> CanAssignPersonnelToRegions; // multiple regions //TODO Regions, List<Regions>
    #endregion
    #region Personnel - the following permissions pertain to personnel
        public bool CanViewLocationSchedule;
        public bool CanHandleAppointments; // register, modify, approve appointments
        public bool CanHandleJournalEntries; // view entries, assign read permissions
    #endregion

    // pre-serialize data for saving, single line
    public string Serialize(CommonPersonnel user)
    {
        string perm = "";
        switch (user.GetRole())
        {
            case Role.Personnel:
                perm = $"{(CanViewLocationSchedule ? "CanViewLocationSchedule" : "")}"
                    + $":{(CanHandleAppointments ? "CanHandleAppointments" : "")}"
                    + $":{(CanHandleJournalEntries? "CanHandleJournalEntries" : "")}";
                break;
            case Role.Admin:
                perm = $"{(CanRegisterPatients ? "CanRegisterPatients" : "")}"
                    + $":{(CanAddLocations ? "CanAddLocations" : "")}"
                    + $":{(CanCreatePersonnel ? "CanCreatePersonnel" : "")}"
                    + $":{(CanViewPermissionsList ? "CanViewPermissionsList" : "")}"
                    + $":{CanAssignAdminPermissions}:{CanAssignPersonnelPermissions}";
                break;
        }
        return $"{user.Username}:{user.GetRole()}:{perm}";
    }

    // create object from savefile, single line
    public static Permission Deserialize(string[] data)
    {
        string username = data[0], role = data[1];
        Permission perm = new(); // new object, unused
        switch (role)
        {
            case "Admin":
                // creating new permission for Admin per savefile


                perm = new Permission(data[2] == "CanRegisterPatients", data[3] == "CanAddLocations", data[4] == "CanCreatePersonnel", data[5] == "CanViewPermissionsList",
                (AdminToAdminPermission)Enum.Parse(typeof(AdminToAdminPermission), data[6]),
                (AdminToPersonnelPermission)Enum.Parse(typeof(AdminToPersonnelPermission), data[7]));
                break;
            case "Personnel":
                perm = new Permission(data[2] == "CanViewLocationSchedule", data[3] == "CanHandleAppointments", data[4] == "CanHandleJournalEntries");
                break;
        }
        return perm; // calling constructor
    }

    // Dictionary of Permissions
    public static Dictionary<string, Permission> Permissions = new();

    // empty constructor
    public Permission()
    {
        CanAssignPersonnelToRegions = new(); // empty list
    }

    // Admin constructor, setting initial property values
    public Permission(bool canRegisterPatients, bool canAddLocations,
        bool canCreatePersonnel, bool canViewPermissionsList,
        // enum flag bitset
        AdminToAdminPermission canAssignAdminPermissions,
        AdminToPersonnelPermission canAssignPersonnelPermissions
    ) {
        // boolean
        CanRegisterPatients = canRegisterPatients;
        CanAddLocations = canAddLocations;
        CanCreatePersonnel = canCreatePersonnel;
        CanViewPermissionsList = canViewPermissionsList;
        CanAssignAdminPermissions = canAssignAdminPermissions;
        CanAssignPersonnelPermissions = canAssignPersonnelPermissions;
        CanAssignPersonnelToRegions = new(); // empty list TODO update loading method once saveable
    }

    // Personnel constructor
    public Permission(bool canViewLocationSchedule, bool canHandleAppointments, bool canHandleJournalEntries)
    {
        CanViewLocationSchedule = canViewLocationSchedule;
        CanHandleAppointments = canHandleAppointments;
        CanHandleJournalEntries = canHandleJournalEntries;
        CanAssignPersonnelToRegions = new(); // empty list
    }

    // static accessor method to set Admin permissions
    public void EnablesAdminPermission(AdminToAdminPermission perm)
    {
        // bitwise OR new permission into current permissions
        CanAssignAdminPermissions |= perm;
    }

    // static method to set Personnel permissions
    public void EnablesPersonnelPermission(AdminToPersonnelPermission perm)
    {
        CanAssignPersonnelPermissions |= perm;
    }

    // checking permissions
    // permissions are public for now, place holder for locations and regions
    public static void CheckPermissions(CommonPersonnel commonPersonnel, string permissionName)
    {
        switch (permissionName)
        {
            default:
                break;
        }
    }

    // this method should run after load file
    // sets Admin and Personnel permissions
    /*
    public static void AttachPermissionsToUsers(Dictionary<string, IUser> users)
    {
        System.Console.WriteLine(Permissions.Count);

        foreach (var user in Utilities.FilterCommonPersonnel(users))
        {
            Permission? perm = null;
            if (Permissions.TryGetValue(user.Key, out perm))
            {
                switch (user.Value.Role)
                {
                    case Role.Admin:
                        user.Value.Permissions = perm;
                        System.Console.WriteLine(user.Key);
                        break;
                    case Role.Personnel:
                        user.Value.Permissions = perm;
                        break;
                }
            }
        }
    }
    */

    // Show permissions list, single user
    public static void ViewPermissionsOf(CommonPersonnel user)
    {
        Console.Clear();
        System.Console.WriteLine($"{user.Username} has the following permissions: ");
        if (user.Permissions.CanAddLocations)
            System.Console.WriteLine("Can add locations");
        if (user.Permissions.CanCreatePersonnel)
            System.Console.WriteLine("Can create Personnel accounts");
        if (user.Permissions.CanViewPermissionsList)
            System.Console.WriteLine("Can view the permissions list");
        if (user.Permissions.CanRegisterPatients)
            System.Console.WriteLine("Can register patients");
        // Admin to Admin
        if ((user.Permissions.CanAssignAdminPermissions & AdminToAdminPermission.AddLocations) > 0)
            System.Console.WriteLine("Can assign Admin permission: Add locations ");
        if ((user.Permissions.CanAssignAdminPermissions & AdminToAdminPermission.RegisterPatients) > 0)
            System.Console.WriteLine("Can assign Admin permission: Handle registration");
        if ((user.Permissions.CanAssignAdminPermissions & AdminToAdminPermission.CreatePersonnelAccounts) > 0)
            System.Console.WriteLine("Can assign Admin permission: Create Personnel acounts");
        if ((user.Permissions.CanAssignAdminPermissions & AdminToAdminPermission.ViewPermissionsList) > 0)
            System.Console.WriteLine("Can assign Admin permission: View Permissions List");
        // Admin to Personnel
        if ((user.Permissions.CanAssignPersonnelPermissions & AdminToPersonnelPermission.HandleAppointments) > 0)
            System.Console.WriteLine("Can assign Personnel permission: Handle Appointments");
        if ((user.Permissions.CanAssignPersonnelPermissions & AdminToPersonnelPermission.ViewJournalEntries) > 0)
            System.Console.WriteLine("Can assign Personnel permission: View Journal Entries");
        if ((user.Permissions.CanAssignPersonnelPermissions & AdminToPersonnelPermission.MarkPermissionLevel) > 0)
            System.Console.WriteLine("Can assign Personnel permission: Assign level of Read Permission");
        if ((user.Permissions.CanAssignPersonnelPermissions & AdminToPersonnelPermission.ViewLocationSchedule) > 0)
            System.Console.WriteLine("Can assign Personnel permission: View schedule by location");
        // Personnel
        if (user.Permissions.CanViewLocationSchedule)
            System.Console.WriteLine("Can view schedule by location");
        if (user.Permissions.CanHandleAppointments)
            System.Console.WriteLine("Can Handle Appointments");
        if (user.Permissions.CanHandleJournalEntries)
            System.Console.WriteLine("Can Assign level of Read Permission");
        System.Console.WriteLine("\nPress [enter] to return to the previous menu");
        System.Console.ReadLine();
    }
}

// As admin, be able to assign or give Admins permissions
[Flags] // enables verbatim representation when .ToString() is used
public enum AdminToAdminPermission
{
    False, // can not assign persmissions
    RegisterPatients, // nonfalse, can or true
    AddLocations, // nonfalse
    CreatePersonnelAccounts = 4, // nonfalse
    ViewPermissionsList = 8, // nonfalse
}

[Flags] // enables verbatim representation when .ToString() is used
public enum AdminToPersonnelPermission
{
    False, // can not, lacks permission
    ViewJournalEntries, // nonfalse, can or true
    MarkPermissionLevel, // nonfalse: mark level of read permission of journal entries
    HandleAppointments = 4, // nonfalse: register, modify, approve
    ViewLocationSchedule = 8, // nonfalse
}

