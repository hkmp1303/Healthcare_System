namespace HospitalApp;

class JournalEntry
{
    //id på journalen?
    public string PatientID;
    public string DoctorID;
    public string Text;
    //tid när den skrevs?
    public ReadPermisson readPermisson; //read permissions?

    public JournalEntry(string patientid, string doctorid, string text)
    {
        PatientID = patientid;
        DoctorID = doctorid;
        Text = text;

        readPermisson = ReadPermisson.None; //börja som None. set när man lägger in en entry
    }

    public enum ReadPermisson
    {
        None,
        PatientRead, //public? alla kan läsa?
        StaffRead, // dom som jobbar på sjukhuset?
        DoctorRead, // Bara läkare kan läsa
    }
}

/*
Note:
använd ReadPermisson med User role för att skriva logik????
*/