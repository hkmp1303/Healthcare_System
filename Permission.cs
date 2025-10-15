using System.Globalization;

namespace HospitalApp;
// As an admin, able to give permission to handle registrations, add locations,
// create personnel accounts, view list of permissions


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

    //constructor
    public Permission()
    {

    }
}

// As admin, be able to assign or give permissions
[Flags] // enables verbate representation when .ToString() is used
enum AdminToAdminPermission
{
    False, // can not assign persmissions
    RegisterPatients, // nonfalse, can or true
    AddLocations, // nonfalse
    CreatePersonnelAccounts = 4, // nonfalse
    ViewPermissionsList = 8, // nonfalse
}

[Flags] // enables verbatim representation when .ToString() is used
enum AdminToPersonnelPermission
{
    False, // can not, lacks permission
    ViewJournalEntries, // nonfalse, can or true
    MarkPermissionLevel, // nonfalse: mark level of read permission
    HandelAppointments = 4, // nonfalse: register, modify, approve
    ViewLocationSchedule = 8, // nonfalse
}

/*
TODO
static method to set permissions, paramerter (Admin)
Dictionary of Permissions
Check permissions method (user.Permissions.CanAssignAdminRegions.Find)
**return user from Utility to new method which checks permissions // filters by role, region, location, ect
**return true or false depending on users access
**may require multiple methods for each specific use case

flexible list permissions - method in permissions, commonpersonnel wrapper method
                                                        (calls permission method and varies parameters)
*/
// static method to set permissions, paramerter (Admin)
public static void GivePermission(Admin admin)
{

}
