namespace HospitalApp;

class Schedule
{
    public const int Days = 5;
    public const int Slots = 7;

    //public (string patientslot, string doctorslot)[,] WeekSchedule = new string(string patientslot, string doctorslot)[Days, Slots];
    public (string patientslot, string doctorslot)[,] WeekSchedule = new (string patientslot, string doctorslot)[Days, Slots];

    public List<String> AppointmentRequests = new();

    public string Doctor = "doc";
    public string Lunch = "Lunch";
    public string OpenSlot = "No Appointment";


    public Schedule()
    {
        FilledNullOnSchedule();
    }

    public void FilledNullOnSchedule()
    {
        WeekSchedule[0, 5] = (Lunch , " ");
        WeekSchedule[1,5] = (Lunch , " ");
        WeekSchedule[2,5] = (Lunch, " ");
        WeekSchedule[3,5] = (Lunch, " ");
        WeekSchedule[4,5] = (Lunch, " ");

        for(int i = 0; i < WeekSchedule.GetLength(0); i++)
        {
            for(int j = 0; j < WeekSchedule.GetLength(1); j++)
            {
                if(WeekSchedule[i,j] == (null, null))
                {
                    WeekSchedule[i,j] = (OpenSlot, " ");
                }
            }
        }
    }

    public void PrintSchedule() //skrota?
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

    public void BookAppointment(List<IUser> users)
    {
        Console.WriteLine("Appointment Booking:");

        System.Console.WriteLine("Write the name of the patient you wish to book an appointment for:");
        string? patientSelectInput = Console.ReadLine();

        Patient? patientuser = users.OfType<Patient>().FirstOrDefault(p => p.Email == patientSelectInput);

        var doctorList = users.OfType<Personnel>().ToList();
        
        System.Console.WriteLine("Which day would you like to book an appointment?");
        System.Console.WriteLine("[mon,tues,wed,thur,fri]");
        switch (Console.ReadLine())
        {
            case "mon":

                int Monint = 0;
                System.Console.WriteLine("Open time slots on Monday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                {
                    if (WeekSchedule[Monint, i] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"{i}: Open time: {i + 8}:00");
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInputMON = Console.ReadLine();

                System.Console.WriteLine("Available doctors:");
                for (int i = 0; i < doctorList.Count; i++)
                {
                    if (WeekSchedule[Monint, int.Parse(selectedTimeInputMON!)] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"{i}: {doctorList[i].Username}");
                    }

                }
                System.Console.WriteLine("Write the nr of the doctor to assign the patient");
                string? selectedDocInputMon = Console.ReadLine();

                    
                if (int.TryParse(selectedTimeInputMON, out int selectedTimeMON) && int.TryParse(selectedDocInputMon, out int selectedDocMon))
                {
                    if (selectedTimeMON >= 0 && selectedTimeMON <= 6 && WeekSchedule[Monint, selectedTimeMON] == (OpenSlot, " "))
                    {
                        var BookedDocMon = doctorList[selectedDocMon];
                        System.Console.WriteLine("You Appointment has now been requested");
                        WeekSchedule[Monint, selectedTimeMON] = (patientuser!.Email,BookedDocMon.Username);   
                    }
                }

                break;
            case "tues":
                int Tuesint = 1;
                System.Console.WriteLine("Open time slots on Tuesday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                {
                    if (WeekSchedule[Tuesint, i] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"{i}: Open time: {i + 8}:00");
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInputTU = Console.ReadLine();

                System.Console.WriteLine("Available doctors:");
                for (int i = 0; i < doctorList.Count; i++)
                {
                    if (WeekSchedule[Tuesint, int.Parse(selectedTimeInputTU!)] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"{i}: {doctorList[i].Username}");
                    }

                }
                System.Console.WriteLine("Write the nr of the doctor to assign the patient");
                string? selectedDocInputTU = Console.ReadLine();

                    
                if (int.TryParse(selectedTimeInputTU, out int selectedTimeTU) && int.TryParse(selectedDocInputTU, out int selectedDocTU))
                {
                    if (selectedTimeTU >= 0 && selectedTimeTU <= 6 && WeekSchedule[Tuesint, selectedTimeTU] == (OpenSlot, " "))
                    {
                        var BookedDocTU = doctorList[selectedDocTU];
                        System.Console.WriteLine("You Appointment has now been requested");
                        WeekSchedule[Tuesint, selectedTimeTU] = (patientuser!.Email,BookedDocTU.Username);   
                    }
                }
                break;
            case "wed":
                int Wedint = 2;
                System.Console.WriteLine("Open time slots on Monday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                {
                    if (WeekSchedule[Wedint, i] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"{i}: Open time: {i + 8}:00");
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInputWED = Console.ReadLine();

                System.Console.WriteLine("Available doctors:");
                for (int i = 0; i < doctorList.Count; i++)
                {
                    if (WeekSchedule[Wedint, int.Parse(selectedTimeInputWED!)] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"{i}: {doctorList[i].Username}");
                    }

                }
                System.Console.WriteLine("Write the nr of the doctor to assign the patient");
                string? selectedDocInputWED = Console.ReadLine();

                    
                if (int.TryParse(selectedTimeInputWED, out int selectedTimeWED) && int.TryParse(selectedDocInputWED, out int selectedDocWED))
                {
                    if (selectedTimeWED >= 0 && selectedTimeWED <= 6 && WeekSchedule[Wedint, selectedTimeWED] == (OpenSlot, " "))
                    {
                        var BookedDocWED = doctorList[selectedDocWED];
                        System.Console.WriteLine("You Appointment has now been requested");
                        WeekSchedule[Wedint, selectedTimeWED] = (patientuser!.Email,BookedDocWED.Username);   
                    }
                }
                break;
            case "thur":
                int Thurint = 3;
                                System.Console.WriteLine("Open time slots on Monday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                {
                    if (WeekSchedule[Thurint, i] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"{i}: Open time: {i + 8}:00");
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInputTH = Console.ReadLine();

                System.Console.WriteLine("Available doctors:");
                for (int i = 0; i < doctorList.Count; i++)
                {
                    if (WeekSchedule[Thurint, int.Parse(selectedTimeInputTH!)] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"{i}: {doctorList[i].Username}");
                    }

                }
                System.Console.WriteLine("Write the nr of the doctor to assign the patient");
                string? selectedDocInputTH = Console.ReadLine();

                    
                if (int.TryParse(selectedTimeInputTH, out int selectedTimeTH) && int.TryParse(selectedDocInputTH, out int selectedDocTH))
                {
                    if (selectedTimeTH >= 0 && selectedTimeTH <= 6 && WeekSchedule[Thurint, selectedTimeTH] == (OpenSlot, " "))
                    {
                        var BookedDocTH = doctorList[selectedDocTH];
                        System.Console.WriteLine("You Appointment has now been requested");
                        WeekSchedule[Thurint, selectedTimeTH] = (patientuser!.Email,BookedDocTH.Username);   
                    }
                }
                break;
            case "fri":
                int Friint = 4;
                System.Console.WriteLine("Open time slots on Monday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                {
                    if (WeekSchedule[Friint, i] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"{i}: Open time: {i + 8}:00");
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInputFR = Console.ReadLine();

                System.Console.WriteLine("Available doctors:");
                for (int i = 0; i < doctorList.Count; i++)
                {
                    if (WeekSchedule[Friint, int.Parse(selectedTimeInputFR!)] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"{i}: {doctorList[i].Username}");
                    }

                }
                System.Console.WriteLine("Write the nr of the doctor to assign the patient");
                string? selectedDocInputFR = Console.ReadLine();

                    
                if (int.TryParse(selectedTimeInputFR, out int selectedTimeFR) && int.TryParse(selectedDocInputFR, out int selectedDocFR))
                {
                    if (selectedTimeFR >= 0 && selectedTimeFR <= 6 && WeekSchedule[Friint, selectedTimeFR] == (OpenSlot, " "))
                    {
                        var BookedDocFR = doctorList[selectedDocFR];
                        System.Console.WriteLine("You Appointment has now been requested");
                        WeekSchedule[Friint, selectedTimeFR] = (patientuser!.Email,BookedDocFR.Username);   
                    }
                }
                break;
            default:
                System.Console.WriteLine("Somthing went wrong. please try again.");
                break;
        }
    }

    public void PrintSchedulePatient(IUser? activeUser)
    {
        if (activeUser!.IsRole(Role.Patient))
        {
            System.Console.WriteLine("Here are your booked appointments");

            for (int i = 0; i < WeekSchedule.GetLength(0); i++)
            {
                for (int j = 0; j < WeekSchedule.GetLength(1); j++)
                {
                    if (WeekSchedule[i, j] == (activeUser.ToString(), " "))
                    {
                        switch (i)
                        {
                            case 0:
                                System.Console.WriteLine($"You have an appointments on the Monday at {j + 8}:00");
                                break;
                            case 1:
                                System.Console.WriteLine($"You have an appointments on the Tuesday at {j + 8}:00");
                                break;
                            case 2:
                                System.Console.WriteLine($"You have an appointments on the Wednesday at {j + 8}:00");
                                break;
                            case 3:
                                System.Console.WriteLine($"You have an appointments on the Thursday at {j + 8}:00");
                                break;
                            case 4:
                                System.Console.WriteLine($"You have an appointments on the Friday at {j + 8}:00");
                                break;
                        }

                    }
                }
            }
        }
    }

    public void AppointmentRequest(IUser? activeUser)
    {
        System.Console.WriteLine("Request Appointment:");
        System.Console.WriteLine("Please leave a small description of you condition:");
        string? DescCond = Console.ReadLine();

        System.Console.WriteLine("press ENTER to send your request");
        Console.ReadLine();

        AppointmentRequests.Add($"Paitent: {activeUser!.ToString()} request an appointment. description of condition: {DescCond}");
    }
    
    public void SeeAppointmentRequest()
    {
        foreach(var ap in AppointmentRequests)
        {
            System.Console.WriteLine(ap);
        }
    }

}

