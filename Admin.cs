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
            System.Console.WriteLine("${Region} has been assigned to user {user}. Press enter to continue.");
            //region.AssignedUsers.Add(user);  // add user to region user list
            //user.Region = region; // assign region to personnel user
            Console.ReadLine();
        }
    }

    // Add locations
    public void AddLocations()
    {
        Console.Clear();
        if (!Permissions.CanAddLocations) // permission check
        {
            System.Console.WriteLine("You don't have permission to add locations.\nPress enter to continue.");
            Console.ReadLine();
            return;
        }
        while (true)
        {
            System.Console.Write("Press [Enter] to return to the Admin Menu\n\nName of new Location: ");
            string locationName = Console.ReadLine() ?? "";
            if (locationName == "")
            {
                break; // check for empty input, restart while loop
            }
            if (locationName.Trim() == "")
            {
                System.Console.WriteLine("Invalid entry. Enter the location name or press [Enter] to return to the Admin menu.");
                continue; // check for empty input, restart while loop
            }
                System.Console.Write($"The location {locationName} has been added. Press [Enter] to return to the Admin menu.");
            Console.ReadLine();
            // Location.AddLocation(locationName);
            return;
        }
    }

    // handle patient registration requests
    private void HandlePatientRegistration(List<IUser> users)
    {
        if (!Permissions.CanRegisterPatients)
        {
            System.Console.WriteLine("You don't have permission to handle patient registration.\nPress [Enter] to continue.");
            Console.ReadLine();
            return;
        }
        Console.Clear();
        System.Console.WriteLine("Patient Registration Request(s)");
        // calling Utilites to filter users
        Dictionary<string, IUser> dictUsers = Utilities.FilterUsersByRole(Utilities.ConvertUserList(users), Role.PatientRegRequested);
        if (dictUsers.Count == 0)
        {
            System.Console.WriteLine("There are no users requesting patient registration.\n\nPress [Enter] to return to the Admin menu");
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
                    System.Console.WriteLine($"Patient registration for {selectedUser.Username} is approved. Press enter to continue.");
                    users.Add(new Patient(selectedUser)); // add new Patient user
                    users.Remove(selectedUser); // remove Unregistered user
                    Console.ReadLine();
                    return;
                case "d": // deny registration, convert PatientRegRequested to UnregUser
                    System.Console.WriteLine($"Patient registration for {selectedUser.Username} is denied. Press enter to continue.");
                    selectedUser.Role = Role.UnregUser;
                    Console.ReadLine();
                    return;
                default: // Admin menu
                    return;
            }
        }
    }

    private Personnel? CreatePersonnelAccount(List<IUser> users)
    {
        Console.Clear();
        System.Console.Write("Enter the username of the new personnel account: ");
        while (true)
        {
            string newUsername = Console.ReadLine() ?? "";
            if (newUsername == "") return null;
            if (newUsername.Trim() == "")
            {
                System.Console.WriteLine("Invalid entry. Try a different username.");
                continue;
            }
            if (Utilities.CheckUsername(users, newUsername))
            {
                System.Console.WriteLine("This username already exists.");
                System.Console.WriteLine("\nPress [Enter] to return to the Personnel menu");
                continue;
            }
            string password = Utilities.GeneratePassword(4);  // auto generate from Utilities
            Console.Clear();
            System.Console.WriteLine($"A personnel account for {newUsername} has been created.\nAccount password: {password}");
            System.Console.Write("\nPress [Enter] to return to the admin menu");
            Console.ReadLine();
            return new Personnel(newUsername, password);
        }
    }

    // Assign permssions, to Admin
    private void AssignPermissionToAdmin(Admin admin)
    {
        while (true)
        {
            Console.Clear();
            System.Console.WriteLine($"Select the permission to assign to: {admin.Username}");
            if (!admin.Permissions.CanAddLocations)
                System.Console.WriteLine("[a] Can add locations");
            if (!admin.Permissions.CanCreatePersonnel)
                System.Console.WriteLine("[c] Can create Personnel accounts");
            if (!admin.Permissions.CanViewPermissionsList)
                System.Console.WriteLine("[v] Can view the permissions list");
            if (!admin.Permissions.CanRegisterPatients)
                System.Console.WriteLine("[r] Can register patients");
            // Admin to Admin
            if ((admin.Permissions.CanAssignAdminPermissions & AdminToAdminPermission.AddLocations) == 0)
                System.Console.WriteLine("[aa] Can assign Admin permission: Add locations ");
            if ((admin.Permissions.CanAssignAdminPermissions & AdminToAdminPermission.RegisterPatients) == 0)
                System.Console.WriteLine("[ar] Can assign Admin permission: Handle registration");
            if ((admin.Permissions.CanAssignAdminPermissions & AdminToAdminPermission.CreatePersonnelAccounts) == 0)
                System.Console.WriteLine("[ac] Can assign Admin permission: Create Personnel acounts");
            if ((admin.Permissions.CanAssignAdminPermissions & AdminToAdminPermission.ViewPermissionsList) == 0)
                System.Console.WriteLine("[av] Can assign Admin permission: View Permissions List");
            // Admin to Personnel
            if ((admin.Permissions.CanAssignPersonnelPermissions & AdminToPersonnelPermission.HandleAppointments) == 0)
                System.Console.WriteLine("[pa] Can assign Personnel permission: Handle Appointments");
            if ((admin.Permissions.CanAssignPersonnelPermissions & AdminToPersonnelPermission.ViewJournalEntries) == 0)
                System.Console.WriteLine("[pj] Can assign Personnel permission: View Journal Entries");
            if ((admin.Permissions.CanAssignPersonnelPermissions & AdminToPersonnelPermission.MarkPermissionLevel) == 0)
                System.Console.WriteLine("[pr] Can assign Personnel permission: Assign level of Read Permission");
            if ((admin.Permissions.CanAssignPersonnelPermissions & AdminToPersonnelPermission.ViewLocationSchedule) == 0)
                System.Console.WriteLine("[pv] Can assign Personnel permission: View schedule by location");
            switch (System.Console.ReadLine())
            {
                case "a":
                    admin.Permissions.CanAddLocations = true;
                    break;
                case "c":
                    admin.Permissions.CanCreatePersonnel = true;
                    break;
                case "v":
                    admin.Permissions.CanViewPermissionsList = true;
                    break;
                case "r":
                    admin.Permissions.CanRegisterPatients = true;
                    break;
                case "aa":
                    admin.Permissions.EnablesAdminPermission(AdminToAdminPermission.AddLocations);
                    break;
                case "ar":
                    admin.Permissions.EnablesAdminPermission(AdminToAdminPermission.RegisterPatients);
                    break;
                case "ac":
                    admin.Permissions.EnablesAdminPermission(AdminToAdminPermission.CreatePersonnelAccounts);
                    break;
                case "av":
                    admin.Permissions.EnablesAdminPermission(AdminToAdminPermission.ViewPermissionsList);
                    break;
                case "pa":
                    admin.Permissions.EnablesPersonnelPermission(AdminToPersonnelPermission.HandleAppointments);
                    break;
                case "pj":
                    admin.Permissions.EnablesPersonnelPermission(AdminToPersonnelPermission.ViewJournalEntries);
                    break;
                case "pr":
                    admin.Permissions.EnablesPersonnelPermission(AdminToPersonnelPermission.MarkPermissionLevel);
                    break;
                case "pv":
                    admin.Permissions.EnablesPersonnelPermission(AdminToPersonnelPermission.ViewLocationSchedule);
                    break;
                case "":  // exits to previous menu
                    return;
                default: // restarts if bad user input
                    continue;
            }
            break;
        }
    }

    // Assign permssions, to Personnel
    private void AssignPermissionToPersonnel(Personnel personnel)
    {
        while (true)
        {
            Console.Clear();
            System.Console.WriteLine($"Select the permission to assign to: {personnel.Username}");
            if (!personnel.Permissions.CanHandleAppointments)
                System.Console.WriteLine("[a] Can accept, deny or modify Appointments");
            if (!personnel.Permissions.CanHandleJournalEntries)
                System.Console.WriteLine("[p] Can Assign level of Read Permission");
            if (!personnel.Permissions.CanViewLocationSchedule)
                System.Console.WriteLine("[v] Can view schedule by location");
            System.Console.WriteLine("Press enter to return to the previous menu");
            switch (System.Console.ReadLine())
            {
                case "v":
                    personnel.Permissions.CanViewLocationSchedule = true;
                    break;
                case "a":
                    personnel.Permissions.CanHandleAppointments = true;
                    break;
                case "p":
                    personnel.Permissions.CanHandleJournalEntries = true;
                    break;
                case "":  // exits to previous menu

                    return;
                default: // restarts if bad user input
                    continue;
            }
            break;
        }
    }

    // static method show admin menu
    public bool? ShowAdminMenu(List<IUser> allUsers)
    {
        Console.Clear();
        while (true)
        {
            List<string> menu = new();
            menu.Add($"Welcome {Username}!\nAdmin Menu\n");
            if (Permissions.CanAddLocations)
                menu.Add("[a] Add locations ");
            if (Permissions.CanRegisterPatients)
                menu.Add("[r] Handle patient(s) registration");
            if (Permissions.CanCreatePersonnel || Permissions.CanAssignPersonnelToRegions.Count > 0)
                menu.Add("[p] View Personnel Menu");
            if (Permissions.CanViewPermissionsList || Permissions.CanAssignAdminPermissions != AdminToAdminPermission.False
                    || Permissions.CanAssignPersonnelPermissions != AdminToPersonnelPermission.False)
                menu.Add("[v] View Permissions Menu");
            menu.Add("\n[exit] to exit the program");
            menu.Add("[logout] to logout of the program");
            Utilities.PrintLines(menu);
            string selection = Console.ReadLine() ?? "";
            Console.Clear();
            menu.Clear();
            switch (selection)
            {
                case "a":
                    AddLocations();
                    break;
                case "r": // accept/deny
                    HandlePatientRegistration(allUsers);
                    break;
                case "p":
                    while (true)
                    {
                        menu.Clear();
                        Console.Clear();
                        menu.Add("Personnel Menu");
                        if (Permissions.CanCreatePersonnel)
                            menu.Add("[c] Create Personnel Account");
                        if (Permissions.CanAssignPersonnelToRegions.Count > 0)
                            menu.Add("[a] Assign Personnel to a Region"); // not shown Regions do not exits
                        menu.Add("\nPress [Enter] to return to the Admin Menu");
                        Utilities.PrintLines(menu);
                        switch (Console.ReadLine() ?? "")
                        {
                            case "c":
                                if (!Permissions.CanCreatePersonnel)
                                {
                                    System.Console.WriteLine("You don't have permission to create personnel accounts.\nPress enter to continue.");
                                    Console.ReadLine();
                                    break;
                                }
                                Personnel? newPersonnel = CreatePersonnelAccount(allUsers);
                                if (newPersonnel == null) continue;
                                allUsers.Add(newPersonnel);
                                break;
                            case "a":
                                Personnel personnel = (Personnel)Utilities.PickUserFromList(Utilities.ConvertUserList(allUsers), Role.Personnel);
                                AssignPersonelToRegion(personnel);
                                break;
                            case "":
                                break;
                            default:
                                continue;
                        }
                        break;
                    }
                    break;
                case "v":
                    bool menuActive = true;
                    while (menuActive)
                    {
                        menu.Clear();
                        menu.Add("Permissions Menu");
                        if (Permissions.CanViewPermissionsList)
                            menu.Add("[v] View Assigned Permissions");
                        if (Permissions.CanAssignAdminPermissions != AdminToAdminPermission.False)
                            menu.Add("[a] Assign Admin Permissions System Access");
                        if (Permissions.CanAssignPersonnelPermissions != AdminToPersonnelPermission.False)
                            menu.Add("[p] Assign Personnel Permissions");
                        menu.Add("[enter] to go back to previous menu");
                        Utilities.PrintLines(menu);
                        switch (Console.ReadLine())
                        {
                            case "v":
                                if (!Permissions.CanViewPermissionsList)
                                {
                                    System.Console.WriteLine("You don't have permission to view the permissions list.\nPress enter to continue.");
                                    Console.ReadLine();
                                    break;
                                }
                                Console.Clear();
                                Permission.ViewPermissionsOf((CommonPersonnel)Utilities.PickUserFromList(Utilities.ConvertUserList(allUsers), Role.Personnel | Role.Admin));
                                break;
                            case "a":
                                if (Permissions.CanAssignAdminPermissions == AdminToAdminPermission.False)
                                {
                                    System.Console.WriteLine("You don't have permission to handle admin permissions.\nPress enter to continue.");
                                    Console.ReadLine();
                                    break;
                                }
                                Admin adminSelected = (Admin)Utilities.PickUserFromList(Utilities.ConvertUserList(allUsers), Role.Admin);
                                AssignPermissionToAdmin(adminSelected);
                                System.Console.WriteLine("The permissions have been updated. ");
                                Permission.ViewPermissionsOf(adminSelected);
                                break;
                            case "p":
                                if (Permissions.CanAssignPersonnelPermissions == AdminToPersonnelPermission.False)
                                {
                                    System.Console.WriteLine("You don't have permission to handle admin permissions.\nPress enter to continue.");
                                    Console.ReadLine();
                                    break;
                                }
                                Personnel personnelSelected = (Personnel)Utilities.PickUserFromList(Utilities.ConvertUserList(allUsers), Role.Personnel);
                                AssignPermissionToPersonnel(personnelSelected);
                                System.Console.WriteLine("The permissions have been updated. ");
                                Permission.ViewPermissionsOf(personnelSelected);
                                break;
                            case "":
                                menuActive = false;
                                break;
                            default:
                                continue;
                        }
                        break; // breaks loop
                    }
                    break; // ends case
                case "logout":
                    return null;
                case "exit":
                    return false;
            }
            Console.Clear();
        }
    }

}