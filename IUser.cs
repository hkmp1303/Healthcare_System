namespace HospitalApp;

    interface IUser
    {


        public bool IsRole(Role role);
        public bool TryLogin(string username, string password);
    }

    enum Role
    {
        Patient,
        Admin,
        Personnel,
    }
    

