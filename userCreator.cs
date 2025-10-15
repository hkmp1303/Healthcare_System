using System;
namespace HospitalApp;

public static class UserCreator
{
    public static IUser CreateUF()
    {
        while (true)
        {
            Console.WriteLine("Choose role:");
            Console.WriteLine("[1] Patient");
            Console.WriteLine("[2] Personnel");
            Console.WriteLine("[3] Admin");
            Console.Write("Choice: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Email: ");
                    string email = Console.ReadLine() ?? "";
                    Console.Write("Password: ");
                    string ppass = Console.ReadLine() ?? "";
                    return new Patient(email, ppass);

                case "2":
                    Console.Write("Username: ");
                    string perUser = Console.ReadLine() ?? "";
                    Console.Write("Password: ");
                    string perPass = Console.ReadLine() ?? "";
                    return new Personnel(perUser, perPass);

                case "3":
                    Console.Write("Username: ");
                    string admUser = Console.ReadLine() ?? "";
                    Console.Write("Password: ");
                    string admPass = Console.ReadLine() ?? "";
                    return new Admin(admUser, admPass);

                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }
}