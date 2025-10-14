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
                string? selectedTimeInputMON = Console.ReadLine();
                if (int.TryParse(selectedTimeInputMON, out int selectedTimeMON))
                {
                    if (selectedTimeMON >= 0 && selectedTimeMON <= 6 && WeekSchedule[Monint, selectedTimeMON] == OpenSlot)
                    {
                        System.Console.WriteLine("You Appointment has now been requested");
                        WeekSchedule[Monint, selectedTimeMON] = activeUser!.ToString()!;  //activeUser //fixa koden här!!!!    
                    }
                }
        
                break;
            case "tues":
                 int Tuesint = 1;
                System.Console.WriteLine("Open time slots on Monday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                {
                    if (WeekSchedule[Tuesint, i] == OpenSlot)
                    {
                        System.Console.WriteLine($"{i}: Open time: {i + 8}:00");
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInputTU = Console.ReadLine();
                if(int.TryParse(selectedTimeInputTU, out int selectedTimeTU))
                {
                    if(selectedTimeTU >= 0 && selectedTimeTU <= 6 && WeekSchedule[Tuesint,selectedTimeTU] == OpenSlot)
                    {
                        System.Console.WriteLine("You Appointment has now been requested");
                        WeekSchedule[Tuesint, selectedTimeTU] = activeUser!.ToString()!;  //activeUser //fixa koden här!!!!    
                    }
                }
                break;
            case "wed":
                int Wedint = 2;
                System.Console.WriteLine("Open time slots on Monday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                {
                    if (WeekSchedule[Wedint, i] == OpenSlot)
                    {
                        System.Console.WriteLine($"{i}: Open time: {i + 8}:00");
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInputWED = Console.ReadLine();
                if(int.TryParse(selectedTimeInputWED, out int selectedTimeWED))
                {
                    if(selectedTimeWED >= 0 && selectedTimeWED <= 6 && WeekSchedule[Wedint,selectedTimeWED] == OpenSlot)
                    {
                        System.Console.WriteLine("You Appointment has now been requested");
                        WeekSchedule[Wedint, selectedTimeWED] = activeUser!.ToString()!;  //activeUser //fixa koden här!!!!    
                    }
                }
                break;
            case "thur":
            int Thurint = 3;
                System.Console.WriteLine("Open time slots on Monday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                {
                    if (WeekSchedule[Thurint, i] == OpenSlot)
                    {
                        System.Console.WriteLine($"{i}: Open time: {i + 8}:00");
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInputTH = Console.ReadLine();
                if(int.TryParse(selectedTimeInputTH, out int selectedTimeTH))
                {
                    if(selectedTimeTH >= 0 && selectedTimeTH <= 6 && WeekSchedule[Thurint,selectedTimeTH] == OpenSlot)
                    {
                        System.Console.WriteLine("You Appointment has now been requested");
                        WeekSchedule[Thurint, selectedTimeTH] = activeUser!.ToString()!;  //activeUser //fixa koden här!!!!    
                    }
                }
                break;
            case "fri":
                int Friint = 4;
                System.Console.WriteLine("Open time slots on Monday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                {
                    if (WeekSchedule[Friint, i] == OpenSlot)
                    {
                        System.Console.WriteLine($"{i}: Open time: {i + 8}:00");
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInputFRI = Console.ReadLine();
                if(int.TryParse(selectedTimeInputFRI, out int selectedTimeFRI))
                {
                    if(selectedTimeFRI >= 0 && selectedTimeFRI <= 6 && WeekSchedule[Friint,selectedTimeFRI] == OpenSlot)
                    {
                        System.Console.WriteLine("You Appointment has now been requested");
                        WeekSchedule[Friint, selectedTimeFRI] = activeUser!.ToString()!;  //activeUser //fixa koden här!!!!    
                    }
                }
                break;
            default:
                System.Console.WriteLine("Somthing went wrong. please try again.");
                break;
        }
    }

}

