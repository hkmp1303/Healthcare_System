namespace HospitalApp;

    interface IUser
    {
        string IdNumber { get; set; }
        public bool IsRole(Role role);
        public bool TryLogin(string username, string password);
    }

    enum Role
    {
        Patient,
        Admin,
        Personnel,
    }
    

