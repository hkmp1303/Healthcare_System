namespace HospitalApp;

public static class UnregToPatient
{
    public static void PromoteUnregToPatient(List<IUser> users, Duno saving)
    {
        foreach (UnregUser user in users.OfType<UnregUser>())
        {
            Console.WriteLine($"{user.Email}");
        }

        Console.Write("Skriv in email för användaren som ska bli patient: ");
        string email = Console.ReadLine() ?? "";

        // Leta upp en UnregUser med rätt email
        UnregUser? unregUser = users.OfType<UnregUser>()
             .FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (unregUser == null)
        {
            Console.WriteLine("Ingen oregistrerad användare med den e-posten hittades.");
            return;
        }

        // Skapa patient med samma email och password
        Patient newPatient = new Patient(unregUser.Email, unregUser.Password);

        // Byt ut i listan
        int index = users.IndexOf(unregUser);
        if (index >= 0)
            users[index] = newPatient;
        else
        {
            users.Remove(unregUser);
            users.Add(newPatient);
        }

        // Spara hela listan med Duno-klass
        saving.SaveUser(users);

        Console.WriteLine($"Användaren med email {email} är nu patient!");
        Console.ReadKey();
    }
}

