namespace HospitalApp;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;

public class Duno
{
    public List<IUser> userList = new();

    public static string MemoryDir = "./file/";
    public static string MemoryUser = "user.txt";
    public static string MemorySchedule = "schedule.txt";
    public static string MemoryRequestList = "requestlist.txt";
    public static string UserSave = Path.Combine(MemoryDir, MemoryUser);
    public static string ScheduleSave = Path.Combine(MemoryDir, MemorySchedule);
    public static string RequestListSave = Path.Combine(MemoryDir, MemoryRequestList);



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
        if (!File.Exists(ScheduleSave))
        {
            File.Create(ScheduleSave).Close();
        }
        if (!File.Exists(RequestListSave))
        {
            File.Create(RequestListSave).Close();
        }

    }

    public void SaveUser(List<IUser> users)
    {

        CheckFile();    // ser till att filen finns
        List<string> lines = new();
        //loopar alla users
        foreach (IUser u in users)
        {
            if (u is Patient p)
                lines.Add($"Patient;{p.Email};{p.Password}");
            else if (u is Personnel per)
                lines.Add($"Personnel;{per.Username};{per.Password}");
            else if (u is Admin ad)
                lines.Add($"Admin;{ad.Username};{ad.Password}");
            else if (u is UnregUser uu)
                lines.Add($"UnregUser;{uu.Email};{uu.Password}");
        }
        File.WriteAllLines(UserSave, lines);

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
                    loadedUsers.Add(new Admin(username, password));
                    break;
                case "Patient":
                    loadedUsers.Add(new Patient(username, password));
                    break;
                case "Personnel":
                    loadedUsers.Add(new Personnel(username, password));
                    break;
                case "UnregUser":
                    loadedUsers.Add(new UnregUser(username, password));
                    break;
                default:
                    Console.WriteLine($"Okänd användartyp: {type}");
                    break;
            }
        }


        return loadedUsers;
    }

    public void SaveSchedule((string patientslot, string doctorslot)[,] schedule)
    {
        CheckFile();

        List<string> lines = new();

        int days = schedule.GetLength(0);
        int slots = schedule.GetLength(1);

        for (int i = 0; i < days; i++)
        {
            for (int j = 0; j < slots; j++)
            {
                var (patient, doctor) = schedule[i, j];
                lines.Add($"{i};{j};{patient};{doctor}");
            }
        }
        File.WriteAllLines(ScheduleSave, lines);
    }

    public (string patientslot, string doctorslot)[,] Loadschedule(int days, int slots)
    {
        CheckFile();

        var schedule = new (string patientslot, string doctorslot)[days, slots];

        if (!File.Exists(ScheduleSave))
            return schedule;

        string[] lines = File.ReadAllLines(ScheduleSave);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split(';');

            if (parts.Length < 4)
                continue;

            int day = int.Parse(parts[0]);
            int slot = int.Parse(parts[1]);
            string patient = parts[2];
            string doctor = parts[3];

            schedule[day, slot] = (patient, doctor);

        }
        return schedule;
    }

    public void SaveRequestsList(List<(string PatientName, string PatDesc, PaitentWaitingStatus status)> requests)
    {
        CheckFile();

        List<string> lines = new();

        foreach (var request in requests)
        {
            lines.Add($"{request.PatientName};{request.PatDesc};{request.status}");
        }

        File.WriteAllLines(RequestListSave, lines);
    }

    public List<(string PatientName, string PatDesc, PaitentWaitingStatus status)> LoadRequestsList()
    {
        CheckFile();

        var requests = new List<(string PatientName, string PatDesc, PaitentWaitingStatus status)>();

        if (!File.Exists(RequestListSave))
            return requests;

        string[] lines = File.ReadAllLines(RequestListSave);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split(';');
            if (parts.Length < 3)
                continue;

            string name = parts[0];
            string pdesc = parts[1];

            if (Enum.TryParse(parts[2], out PaitentWaitingStatus status))
            {
                requests.Add((name, pdesc, status));
            }
            else
            {
                requests.Add((name, pdesc, PaitentWaitingStatus.Pending));
            }
        }
        return requests;
    }

}
