namespace HospitalApp;

class Journal
{
    public string PatientID;
    public string AutherID;
    public string Text;

    public Journal(string patientid, string autherid, string text)
    {
        PatientID = patientid;
        AutherID = autherid;
        Text = text;
    }
}