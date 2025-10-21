// JournalStorage.cs
namespace HospitalApp;


public class JournalStorage
{
    public static string MemoryDir = "./file/";
    public static string MemoryJournal = "journal.txt";
    public static string JournalSave = Path.Combine(MemoryDir, MemoryJournal);

    public void CheckFile()
    {
        if (!Directory.Exists(MemoryDir))
            Directory.CreateDirectory(MemoryDir);

        if (!File.Exists(JournalSave))
            File.Create(JournalSave).Close();
    }

    // Format (semicolon-separated):
    // PatientEmail ; PersonnelUsername ; DateTime("O") ; ReadPermisson(int) ; Text
    public void SaveJournals(List<JournalEntry> journals)
    {
        CheckFile();
        List<string> lines = new List<string>(journals.Count);

        foreach (JournalEntry j in journals)
        {
            string patientEmail = (j.Patient is Patient p) ? p.Email : "";


            string personnelUsername =
                j.Personnel is Personnel per ? per.Username : "";

            string dt = j.dateTime.ToString();
            string perm = ((int)j.readPermisson).ToString();


            string line = string.Join(';', patientEmail, personnelUsername, dt, perm, j.Text ?? "");
            lines.Add(line);
        }

        File.WriteAllLines(JournalSave, lines);
    }

    public List<JournalEntry> LoadJournals(List<IUser> users)
    {
        CheckFile();
        List<JournalEntry> result = new List<JournalEntry>();

        foreach (string line in File.ReadAllLines(JournalSave))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;


            string[] parts = line.Split(';');
            if (parts.Length < 5) continue;

            string patientEmail = parts[0];
            string personnelUsername = parts[1];
            string dtStr = parts[2];
            string permStr = parts[3];
            // join the rest back as Text
            string text = string.Join(";", parts.Skip(4));

            // Re-link Patient by email
            Patient? patient = users.OfType<Patient>()
                               .FirstOrDefault(u => string.Equals(u.Email, patientEmail, StringComparison.OrdinalIgnoreCase));
            if (patient is null) continue;

            // Re-link Personnel by username 
            IUser? personnel = users.OfType<Personnel>().FirstOrDefault(u => string.Equals(u.Username, personnelUsername, StringComparison.OrdinalIgnoreCase)) as IUser;

            if (personnel is null) continue;

            if (!DateTime.TryParse(dtStr, null, out DateTime dt))
                dt = DateTime.Now;

            if (!int.TryParse(permStr, out var permVal)) permVal = 0;
            JournalEntry.ReadPermisson perm = (JournalEntry.ReadPermisson)permVal;

            // JournalEntry constructor accept IUser as first arg
            JournalEntry entry = new JournalEntry(patient, personnel, text, perm) { dateTime = dt };
            result.Add(entry);
        }

        return result;
    }
}
