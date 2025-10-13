namespace HospitalApp;

using System;
using System.Collections.Generic;
using System.IO;



public class Duno
{
    public List<Patient> userlist = new();

    Role role = new Role();
    public static string MemoryDir = "./file/";
    public static string MemoryUser = "user.txt";
    public static string UserSave = Path.Combine(MemoryDir, MemoryUser);



    public void CheckFile()
    {
        if (!Directory.Exists(MemoryDir))
        {
            Directory.CreateDirectory(MemoryDir);
        }
        if (!File.Exists(UserSave))
        {
            File.Create(UserSave).Close();
        }

    }

    public void SaveUser(List<IUser> users)
    {
        CheckFile();
        List<string> lines = new();
        foreach (IUser u in users)
        {
            if (u is Patient p)
                lines.Add($"Patient;{p.Email};{p.Password}");
            else if (u is Personnel per)
                lines.Add($"Personnel;{per.Username};{per.Password}");
        }
        File.AppendAllLines(UserSave, lines);

    }
    public List<IUser> LoadUsers()
    {
        CheckFile(); // ser till att filen finns

        List<IUser> loadedUsers = new();

        // Läs in alla rader
        string[] lines = File.ReadAllLines(UserSave);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Dela upp på semikolon
            string[] parts = line.Split(';');
            if (parts.Length < 3)
                continue;

            string type = parts[0];
            string username = parts[1];
            string password = parts[2];

            // Skapa rätt typ av användare
            switch (type)
            {
                case "Admin":
                    loadedUsers.Add(new Patient(username, password));
                    break;
                case "Patient":
                    loadedUsers.Add(new Patient(username, password));
                    break;
                case "Personnel":
                    loadedUsers.Add(new Personnel(username, password));
                    break;
                default:
                    Console.WriteLine($"Okänd användartyp: {type}");
                    break;
            }
        }

        return loadedUsers;
    }


}
