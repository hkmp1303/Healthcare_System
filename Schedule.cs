namespace HospitalApp;

class Schedule
{
    public const int Days = 5;
    public const int Slots = 7;
    public string[,] WeekSchedule = new string[Days, Slots];

    public string Lunch = "Lunch";
    public string OpenSlot = "No Appointment";


    public Schedule()
    {
        FilledNullOnSchedule();
    }

    public void FilledNullOnSchedule()
    {
        //string Lunch = "Lunch";
        //string OpenSlot = "No Appointment";
        WeekSchedule[0,5] = Lunch;
        WeekSchedule[1,5] = Lunch;
        WeekSchedule[2,5] = Lunch;
        WeekSchedule[3,5] = Lunch;
        WeekSchedule[4,5] = Lunch;

        for(int i = 0; i < WeekSchedule.GetLength(0); i++)
        {
            for(int j = 0; j < WeekSchedule.GetLength(1); j++)
            {
                if(WeekSchedule[i,j] == null)
                {
                    WeekSchedule[i,j] = OpenSlot;
                }
            }
        }
    }

    public void PrintSchedule()
    {
        Console.WriteLine($"Day:  |-Monday-|-Tuesday-|-Wednesday-|-Thursday-|-Friday-|");
        Console.WriteLine($"8.00: |-{WeekSchedule[0, 0]}-|-{WeekSchedule[1, 0]}-|-{WeekSchedule[2, 0]}-|-{WeekSchedule[3, 0]}-|-{WeekSchedule[4, 0]}-|");
        Console.WriteLine($"9.00: |-{WeekSchedule[0, 1]}-|-{WeekSchedule[1, 1]}-|-{WeekSchedule[2, 1]}-|-{WeekSchedule[3, 1]}-|-{WeekSchedule[4, 1]}-|");
        Console.WriteLine($"10.00:|-{WeekSchedule[0, 2]}-|-{WeekSchedule[1, 2]}-|-{WeekSchedule[2, 2]}-|-{WeekSchedule[3, 2]}-|-{WeekSchedule[4, 2]}-|");
        Console.WriteLine($"11.00:|-{WeekSchedule[0, 3]}-|-{WeekSchedule[1, 3]}-|-{WeekSchedule[2, 3]}-|-{WeekSchedule[3, 3]}-|-{WeekSchedule[4, 3]}-|");
        Console.WriteLine($"12.00:|-{WeekSchedule[0, 4]}-|-{WeekSchedule[1, 4]}-|-{WeekSchedule[2, 4]}-|-{WeekSchedule[3, 4]}-|-{WeekSchedule[4, 4]}-|");
        Console.WriteLine($"13.00:|-{WeekSchedule[0, 5]}-|-{WeekSchedule[1, 5]}-|-{WeekSchedule[2, 5]}-|-{WeekSchedule[3, 5]}-|-{WeekSchedule[4, 5]}-|");
        Console.WriteLine($"14.00:|-{WeekSchedule[0, 6]}-|-{WeekSchedule[1, 6]}-|-{WeekSchedule[2, 6]}-|-{WeekSchedule[3, 6]}-|-{WeekSchedule[4, 6]}-|");

    }

    public void BookAppointment(IUser? activeUser)
    {
        Console.WriteLine("Appointment Booking:");

        System.Console.WriteLine("Which day would you like to book an Appointment?");
        System.Console.WriteLine("[mon,tues,wed,thur,fri]");
        switch (Console.ReadLine())
        {
            case "mon":

                int Monint = 0;
                System.Console.WriteLine("Open time slots on Monday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                {
                    if (WeekSchedule[Monint, i] == OpenSlot)
                    {
                        System.Console.WriteLine($"{i}: Open time: {i + 8}:00");
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInput = Console.ReadLine();
                if(int.TryParse(selectedTimeInput, out int selectedTime))
                {
                    if(selectedTime >= 0 && selectedTime <= 6 && WeekSchedule[Monint,selectedTime] == OpenSlot)
                    {
                        System.Console.WriteLine("You Appointment has now been requested");
                        WeekSchedule[Monint, selectedTime] = "activeUser";  //activeUser //fixa koden här!!!!
                    }
                }

                break;
            case "tues":
                break;
            case "wed":
                break;
            case "thur":
                break;
            case "fri":
                break;
            default:
                System.Console.WriteLine("Somthing went wrong. please try again.");
                break;
        }
    }

}

