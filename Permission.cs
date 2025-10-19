using System.ComponentModel.Design;

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
        //public List<Region> CanAssignAdminRegions; // multiple regions //TODO Regions
    #endregion
    #region Personnel - the following permissions pertain to personnel
        public bool CanViewLocationSchedule;
        public bool CanHandleAppointments; // register, modify, approve appointments
        public bool CanHandleJournalEntries; // view entries, assign read permissions
        public AdminToPersonnelPermission CanAssignPersonnelPermissions;
    #endregion

    // Dictionary of Permissions
    public static Dictionary<string, Permission> Permissions = new();


    // empty constructor
    public Permission()
    {
    }

    // Admin constructor
    public Permission(bool canRegisterPatients, bool canAddLocations, bool canCreatePersonnel, bool canViewPermissionsList, AdminToAdminPermission adminToAdminPermission)
    {
        CanRegisterPatients = canRegisterPatients;
        CanAddLocations = canAddLocations;
        CanCreatePersonnel = canCreatePersonnel;
        CanViewPermissionsList = canViewPermissionsList;
        CanAssignAdminPermissions = adminToAdminPermission;
    }

    // Personnel constructor
    public Permission(bool canViewLocationSchedule, bool canHandleAppointments, bool canHandleJournalEntries, AdminToPersonnelPermission canAssignPersonnelPermissions)
    {
        CanViewLocationSchedule = canViewLocationSchedule;
        CanHandleAppointments = canHandleAppointments;
        CanHandleJournalEntries = canHandleJournalEntries;
        CanAssignPersonnelPermissions = canAssignPersonnelPermissions;
    }

    // static accessor method to set Admin permissions
    public static void EnablesAdminPermission(Admin admin, AdminToAdminPermission perm)
    {
        // bitwise OR new permission into current permissions
        admin.Permissions.CanAssignAdminPermissions |= perm;
    }

    // static method to set Personnel permissions
    public static void EnablesPersonnelPermission(Personnel personnel, AdminToPersonnelPermission perm)
    {
        personnel.Permissions.CanAssignPersonnelPermissions |= perm;
    }

    // checking permissions
    // permissions are public for now, place holder for locations and regions
    public static void CheckPermissions(CommonPersonnel commonPersonnel, string permissionName)
    {
        switch (permissionName)
        {

        }
    }

    // this method should run after load file
    // sets Admin and Personnel permissions
    public static void AttachPermissionsToUsers(Dictionary<string, IUser> Users)
    {
        foreach (var user in Utilities.FilterCommonPersonnel(Users))
        {
            Permission? perm = null;
            if (Permissions.TryGetValue(user.Key, out perm))
            {
                switch (user.Value.Role)
                {
                    case Role.Admin:
                        user.Value.Permissions = perm;
                        break;
                    case Role.Personnel:
                        user.Value.Permissions = perm;
                        break;
                }

            }
        }
    }

    // Show permissions list, single user
    public static void ViewPermissionsOf(CommonPersonnel user)
    {
        Console.Clear();
        System.Console.WriteLine($"The user{user.Username} has the following permissions: ");
        if (user.Permissions.CanAddLocations)
            System.Console.WriteLine("Can add locations");
        if (user.Permissions.CanCreatePersonnel)
            System.Console.WriteLine("Can create Personnel accounts");
        if (user.Permissions.CanViewPermissionsList)
            System.Console.WriteLine("Can view the permissions list");
        if ((user.Permissions.CanAssignAdminPermissions & AdminToAdminPermission.AddLocations) > 0)
            System.Console.WriteLine("Can assign Admin permission: Add locations ");
        if ((user.Permissions.CanAssignAdminPermissions & AdminToAdminPermission.RegisterPatients) > 0)
            System.Console.WriteLine("Can assign Admin permission: Handle registration");
        if ((user.Permissions.CanAssignAdminPermissions & AdminToAdminPermission.CreatePersonnelAccounts) > 0)
            System.Console.WriteLine("Can assign Admin permission: Create Personnel acounts");
        if ((user.Permissions.CanAssignAdminPermissions & AdminToAdminPermission.ViewPermissionsList) > 0)
            // personnel
        if (user.Permissions.CanViewLocationSchedule)
            System.Console.WriteLine("Can view schedule by location");
        if (user.Permissions.CanHandleAppointments)
            System.Console.WriteLine("Can Handle Appointments");
        if (user.Permissions.CanHandleJournalEntries)
            System.Console.WriteLine("Can Assign level of Read Permission");
        if ((user.Permissions.CanAssignPersonnelPermissions & AdminToPersonnelPermission.HandleAppointments) > 0)
            System.Console.WriteLine("Can assign Personnel permission: Handle Appointments");
        if ((user.Permissions.CanAssignPersonnelPermissions & AdminToPersonnelPermission.ViewJournalEntries) > 0)
            System.Console.WriteLine("Can assign Personnel permission: View Journal Entries");
        if ((user.Permissions.CanAssignPersonnelPermissions & AdminToPersonnelPermission.MarkPermissionLevel) > 0)
            System.Console.WriteLine("Can assign Personnel permission: Assign level of Read Permission");
        if ((user.Permissions.CanAssignPersonnelPermissions & AdminToPersonnelPermission.ViewLocationSchedule) > 0)
            System.Console.WriteLine("Can assign Personnel permission: View schedule by location");
        System.Console.WriteLine("Press enter to return to the previous menu");
        System.Console.ReadLine();
    }
}

// As admin, be able to assign or give Admins permissions
[Flags] // enables verbate representation when .ToString() is used
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

