using System.Collections;
using System.ComponentModel;
using System.Net;

namespace HospitalApp;

public enum PaitentWaitingStatus //enum for patient waiting 
    {
        Pending,
        Approved,
        Denied,
    } //fick flytta ut enum här för jag lyckades inte få saving att hitta det annars.. hade fråga på handlening men är tis natt och vi kommer inte ha någon hanleding innan dess.... men ja.... 

class Schedule //class for schedule
{
    

    public const int Days = 5; //int for days
    public const int Slots = 7; //int for all available slot 

    public (string patientslot, string doctorslot)[,] WeekSchedule = new (string patientslot, string doctorslot)[Days, Slots]; //tuple array. used to keep 2 string in one place. 
    
    public List<(String PatientName, string PatDesc, PaitentWaitingStatus status)> AppointmentRequests = new(); //list to keep track of paient in what is waiting on appointment
    public (string PatientName, string PatDesc, PaitentWaitingStatus Status) patiensInSystem = (PatientName: "", PatDesc: "", PaitentWaitingStatus.Pending); //tuple string to keep patient and description in one place

    // public enum PaitentWaitingStatus //enum for patient waiting 
    // {
    //     Pending,
    //     Approved,
    //     Denied,
    // }

    public string Lunch = "Lunch"; //string
    public string OpenSlot = "Open Slot"; //string

    DayOfWeek WeekDay = DateTime.Today.DayOfWeek; //variable to be able to use real-time time

    public Schedule()
    {
        FilledNullOnSchedule(); // to fill the schedule with OpenSlot and Lunch
    }

    public void FilledNullOnSchedule()
    {
        WeekSchedule[0, 4] = (Lunch, " ");
        WeekSchedule[1, 4] = (Lunch, " ");
        WeekSchedule[2, 4] = (Lunch, " ");
        WeekSchedule[3, 4] = (Lunch, " ");
        WeekSchedule[4, 4] = (Lunch, " ");

        for (int i = 0; i < WeekSchedule.GetLength(0); i++)
        {
            for (int j = 0; j < WeekSchedule.GetLength(1); j++)
            {
                if (WeekSchedule[i, j] == (null, null) || WeekSchedule[i, j] == ("",""))
                {
                    WeekSchedule[i, j] = (OpenSlot, " ");
                }
            }
        }
    }

    public void MoveAppointment() //method to be able to move appointments
    {
        Console.Clear();
        System.Console.WriteLine("Select the day of the patient you'd wish to re-schedule");
        System.Console.WriteLine(" ");
        System.Console.WriteLine("[0] Monday");
        System.Console.WriteLine("[1] Tuesday");
        System.Console.WriteLine("[2] Wednesday");
        System.Console.WriteLine("[3] Thursday");
        System.Console.WriteLine("[4] Friday"); // choices

        System.Console.WriteLine("Monday:");
        for (int i = 0; i < WeekSchedule.GetLength(0); i++) //loop out the week schedule
        {
            for (int j = 0; j < WeekSchedule.GetLength(1); j++)
            {
                if (i == 0) // if it's the first colum in the array
                {
                    if (WeekSchedule[i, j].patientslot != OpenSlot && WeekSchedule[i, j].patientslot != Lunch) // if it's not an openslot or lunch
                    {
                        System.Console.WriteLine($"Time: {j + 8}:00 {WeekSchedule[i, j].patientslot} {WeekSchedule[i, j].doctorslot} "); //print patient
                    }
                }

            }
        }

        System.Console.WriteLine("Tuesday:"); //same as the loop before
        for (int i = 0; i < WeekSchedule.GetLength(0); i++)
        {
            for (int j = 0; j < WeekSchedule.GetLength(1); j++)
            {
                if (i == 1)
                {
                    if (WeekSchedule[i, j].patientslot != OpenSlot && WeekSchedule[i, j].patientslot != Lunch)
                    {
                        System.Console.WriteLine($"Time: {j + 8}:00 {WeekSchedule[i, j].patientslot} {WeekSchedule[i, j].doctorslot} ");
                    }
                }

            }
        }

        System.Console.WriteLine("Wednesday:"); //same as the loop before
        for (int i = 0; i < WeekSchedule.GetLength(0); i++)
        {
            for (int j = 0; j < WeekSchedule.GetLength(1); j++)
            {
                if (i == 2)
                {
                    if (WeekSchedule[i, j].patientslot != OpenSlot && WeekSchedule[i, j].patientslot != Lunch)
                    {
                        System.Console.WriteLine($"Time: {j + 8}:00 {WeekSchedule[i, j].patientslot} {WeekSchedule[i, j].doctorslot} ");
                    }
                }

            }
        }

        System.Console.WriteLine("Thursday:"); //same as the loop before
        for (int i = 0; i < WeekSchedule.GetLength(0); i++)
        {
            for (int j = 0; j < WeekSchedule.GetLength(1); j++)
            {
                if (i == 3)
                {
                    if (WeekSchedule[i, j].patientslot != OpenSlot && WeekSchedule[i, j].patientslot != Lunch)
                    {
                        System.Console.WriteLine($"Time: {j + 8}:00 {WeekSchedule[i, j].patientslot} {WeekSchedule[i, j].doctorslot} ");
                    }
                }

            }
        }

        System.Console.WriteLine("Friday:"); //same as the loop before
        for (int i = 0; i < WeekSchedule.GetLength(0); i++)
        {
            for (int j = 0; j < WeekSchedule.GetLength(1); j++)
            {
                if (i == 4)
                {
                    if (WeekSchedule[i, j].patientslot != OpenSlot && WeekSchedule[i, j].patientslot != Lunch)
                    {
                        System.Console.WriteLine($"Time: {j + 8}:00 {WeekSchedule[i, j].patientslot} {WeekSchedule[i, j].doctorslot} ");
                    }
                }

            }
        }


        string? selectInputDay = Console.ReadLine();
        if (selectInputDay == null || selectInputDay == "")
        {
            System.Console.WriteLine("Something went wrong.");
        }
        int.TryParse(selectInputDay, out int selectInputDayint);
        System.Console.WriteLine("Select the time:");
        System.Console.WriteLine("Write the hour");
        string? selectedInputTime = Console.ReadLine();
        if (selectedInputTime == null || selectedInputTime == "")
        {
            System.Console.WriteLine("Something went wrong.");
        }
        int.TryParse(selectedInputTime, out int selectedInputTimeint);


        Console.Clear();
        System.Console.WriteLine("Available appointment slots:");
        System.Console.WriteLine("[0] Monday");
        System.Console.WriteLine("[1] Tuesday");
        System.Console.WriteLine("[2] Wednesday");
        System.Console.WriteLine("[3] Thursday");
        System.Console.WriteLine("[4] Friday");
        System.Console.WriteLine(" ");
        System.Console.WriteLine("Monday [0]");
        for (int i = 0; i < WeekSchedule.GetLength(0); i++)
        {
            for (int j = 0; j < WeekSchedule.GetLength(1); j++)
            {
                if (i == 0)
                {

                    if (WeekSchedule[i, j] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"Time: {j + 8}:00 {WeekSchedule[i, j].patientslot}");
                    }
                }
            }
        }

        System.Console.WriteLine("Tuesday [1]");
        for (int i = 0; i < WeekSchedule.GetLength(0); i++)
        {
            for (int j = 0; j < WeekSchedule.GetLength(1); j++)
            {
                if (i == 1)
                {

                    if (WeekSchedule[i, j] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"Time: {j + 8}:00 {WeekSchedule[i, j].patientslot}");
                    }
                }
            }
        }

        System.Console.WriteLine("Wednesday [2]");
        for (int i = 0; i < WeekSchedule.GetLength(0); i++)
        {
            for (int j = 0; j < WeekSchedule.GetLength(1); j++)
            {
                if (i == 2)
                {

                    if (WeekSchedule[i, j] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"Time: {j + 8}:00 {WeekSchedule[i, j].patientslot}");
                    }
                }
            }
        }

        System.Console.WriteLine("Thursday [3]");
        for (int i = 0; i < WeekSchedule.GetLength(0); i++)
        {
            for (int j = 0; j < WeekSchedule.GetLength(1); j++)
            {
                if (i == 3)
                {

                    if (WeekSchedule[i, j] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"Time: {j + 8}:00 {WeekSchedule[i, j].patientslot}");
                    }
                }
            }
        }

        System.Console.WriteLine("Friday [4]");
        for (int i = 0; i < WeekSchedule.GetLength(0); i++)
        {
            for (int j = 0; j < WeekSchedule.GetLength(1); j++)
            {
                if (i == 4)
                {

                    if (WeekSchedule[i, j] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"Time: {j + 8}:00 {WeekSchedule[i, j].patientslot}");
                    }
                }
            }
        }

        System.Console.WriteLine("Select new day you would like to change to:");
        string? selectnewDay = Console.ReadLine();
        if (selectnewDay == null || selectnewDay == "")
        {
            System.Console.WriteLine("Something went wrong.");
        }
        int.TryParse(selectnewDay, out int selectnewDayint);
        System.Console.WriteLine("Select time");
        System.Console.WriteLine("Write the hour");
        string? selectednewTime = Console.ReadLine();
        if (selectednewTime == null || selectednewTime == "")
        {
            System.Console.WriteLine("Something went wrong.");
        }
        int.TryParse(selectednewTime, out int selectednewTimeint);


        WeekSchedule[selectnewDayint, selectednewTimeint - 8] = WeekSchedule[selectInputDayint, selectedInputTimeint - 8];
        WeekSchedule[selectInputDayint, selectedInputTimeint - 8] = (OpenSlot, " ");

        System.Console.WriteLine($"Time is now changed");
        Console.ReadLine();
    }

    public void PrintSchedule()
    {

        string[] schedDays = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        string[] schedTime = { "8.00", "9.00", "10.00", "11.00", "12.00", "13.00", "14.00" };

        int maxLength = schedDays.Max(day => day.Length);
        for (int i = 0; i < schedDays.Length; i++)
        {
            for (int j = 0; j < schedTime.Length; j++)
            {
                var pat = $"Patient: {WeekSchedule[i, j].patientslot}";
                var doc = $"Doctor:  {WeekSchedule[i, j].doctorslot}";
                maxLength = Math.Max(maxLength, Math.Max(pat.Length, doc.Length));
            }
        }

        int box = maxLength + 2;

        Console.WriteLine("Staff Schedule:");


        Console.Write("Time".PadRight(8));
        foreach (var day in schedDays)
            Console.Write(day.PadRight(box));
        Console.WriteLine();

        for (int i = 0; i < schedTime.Length; i++)
        {

            Console.Write(schedTime[i].PadRight(8));
            for (int j = 0; j < schedDays.Length; j++)
            {

                Console.Write($"Patient: {WeekSchedule[j, i].patientslot}".PadRight(box));

            }

            System.Console.WriteLine();

            Console.Write("".PadRight(8));
            for (int j = 0; j < schedDays.Length; j++)
            {
                Console.Write($"Doctor: {WeekSchedule[j, i].doctorslot}".PadRight(box));
            }

            System.Console.WriteLine();


        }
        Console.ReadLine();
    }

    public void BookAppointment(List<IUser> users)
    {
        Console.Clear();
        Console.WriteLine("Appointment Booking:");
        System.Console.WriteLine(" ");
        for (int k = 0; k < AppointmentRequests.Count; k++)
        {
            if (AppointmentRequests[k].status == PaitentWaitingStatus.Pending || AppointmentRequests[k].status == PaitentWaitingStatus.Denied || AppointmentRequests.Count == 0)
            {
                System.Console.WriteLine("No appointments that need booking");
                System.Console.WriteLine("Press ENTER to continue");
                Console.ReadLine();
                return;
            }
            else if (AppointmentRequests[k].status == PaitentWaitingStatus.Approved)
            {

                Console.WriteLine($"booking number: [{k}] Patient: {AppointmentRequests[k].PatientName}");
            }
        }
        System.Console.WriteLine(" ");
        System.Console.WriteLine("Select the booking request you would like to handle:");
        string? requestSelectedInput = Console.ReadLine();
        if (requestSelectedInput == null || requestSelectedInput == "")
        {
            System.Console.WriteLine("Something went wrong");
        }
        int.TryParse(requestSelectedInput, out int requestedInt);
        System.Console.WriteLine($"You have selected: {AppointmentRequests![requestedInt].PatientName}");

        Patient? patientuser = users.OfType<Patient>().FirstOrDefault(p => p.Email == AppointmentRequests![requestedInt].PatientName);

        var doctorList = users.OfType<Personnel>().ToList();

        System.Console.WriteLine("Which day would you like to book an appointment?");
        System.Console.WriteLine("[1] Monday");
        System.Console.WriteLine("[2] Tuesday");
        System.Console.WriteLine("[3] Wednesday");
        System.Console.WriteLine("[4] Thursday");
        System.Console.WriteLine("[5] Friday");
        switch (Console.ReadLine())
        {
            case "1":

                int Monint = 0;
                System.Console.WriteLine("Open time slots on Monday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                {
                    if (WeekSchedule[Monint, i] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"[{i}] Open time: {i + 8}:00");
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInputMON = Console.ReadLine();
                if (selectedTimeInputMON == null || selectedTimeInputMON == "")
                {
                    System.Console.WriteLine("Something went wrong");
                }
                System.Console.WriteLine("Available doctors:");
                for (int i = 0; i < doctorList.Count; i++)
                {
                    if (WeekSchedule[Monint, int.Parse(selectedTimeInputMON!)] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"[{i}] {doctorList[i].Username}");
                    }

                }
                System.Console.WriteLine("Write the number of the doctor to assign the patient");
                string? selectedDocInputMon = Console.ReadLine();
                if (selectedDocInputMon == null || selectedDocInputMon == "")
                {
                    System.Console.WriteLine("Something went wrong");
                }
                if (int.TryParse(selectedTimeInputMON, out int selectedTimeMON) && int.TryParse(selectedDocInputMon, out int selectedDocMon))
                {
                    if (selectedTimeMON >= 0 && selectedTimeMON <= 6 && WeekSchedule[Monint, selectedTimeMON] == (OpenSlot, " "))
                    {
                        var BookedDocMon = doctorList[selectedDocMon];
                        System.Console.WriteLine("The appointment is now booked");
                        AppointmentRequests.RemoveAt(requestedInt);
                        WeekSchedule[Monint, selectedTimeMON] = (patientuser!.Email, BookedDocMon.Username);
                    }
                }

                break;
            case "2":
                int Tuesint = 1;
                System.Console.WriteLine("Open time slots on Tuesday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                {
                    if (WeekSchedule[Tuesint, i] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"[{i}] Open time: {i + 8}:00");
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInputTU = Console.ReadLine();
                if (selectedTimeInputTU == null || selectedTimeInputTU == "")
                {
                    System.Console.WriteLine("Something went wrong");
                }
                System.Console.WriteLine("Available doctors:");
                for (int i = 0; i < doctorList.Count; i++)
                {
                    if (WeekSchedule[Tuesint, int.Parse(selectedTimeInputTU!)] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"[{i}] {doctorList[i].Username}");
                    }

                }
                System.Console.WriteLine("Write the number of the doctor to assign the patient");
                string? selectedDocInputTU = Console.ReadLine();
                if (selectedDocInputTU == null || selectedDocInputTU == "")
                {
                    System.Console.WriteLine("Something went wrong");
                }

                if (int.TryParse(selectedTimeInputTU, out int selectedTimeTU) && int.TryParse(selectedDocInputTU, out int selectedDocTU))
                {
                    if (selectedTimeTU >= 0 && selectedTimeTU <= 6 && WeekSchedule[Tuesint, selectedTimeTU] == (OpenSlot, " "))
                    {
                        var BookedDocTU = doctorList[selectedDocTU];
                        System.Console.WriteLine("The appointment is now booked");
                        AppointmentRequests.RemoveAt(requestedInt);
                        WeekSchedule[Tuesint, selectedTimeTU] = (patientuser!.Email, BookedDocTU.Username);
                    }
                }
                break;
            case "3":
                int Wedint = 2;
                System.Console.WriteLine("Open time slots on Monday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                {
                    if (WeekSchedule[Wedint, i] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"[{i}] Open time: {i + 8}:00");
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInputWED = Console.ReadLine();
                if (selectedTimeInputWED == null || selectedTimeInputWED == "")
                {
                    System.Console.WriteLine("Something went wrong");
                }
                System.Console.WriteLine("Available doctors:");
                for (int i = 0; i < doctorList.Count; i++)
                {
                    if (WeekSchedule[Wedint, int.Parse(selectedTimeInputWED!)] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"[{i}] {doctorList[i].Username}");
                    }

                }
                System.Console.WriteLine("Write the number of the doctor to assign the patient");
                string? selectedDocInputWED = Console.ReadLine();
                if (selectedDocInputWED == null || selectedDocInputWED == "")
                {
                    System.Console.WriteLine("Something went wrong");
                }

                if (int.TryParse(selectedTimeInputWED, out int selectedTimeWED) && int.TryParse(selectedDocInputWED, out int selectedDocWED))
                {
                    if (selectedTimeWED >= 0 && selectedTimeWED <= 6 && WeekSchedule[Wedint, selectedTimeWED] == (OpenSlot, " "))
                    {
                        var BookedDocWED = doctorList[selectedDocWED];
                        System.Console.WriteLine("The appointment is now booked");
                        AppointmentRequests.RemoveAt(requestedInt);
                        WeekSchedule[Wedint, selectedTimeWED] = (patientuser!.Email, BookedDocWED.Username);
                    }
                }
                break;
            case "4":
                int Thurint = 3;
                System.Console.WriteLine("Open time slots on Monday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                {
                    if (WeekSchedule[Thurint, i] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"[{i}] Open time: {i + 8}:00");
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInputTH = Console.ReadLine();
                if (selectedTimeInputTH == null || selectedTimeInputTH == "")
                {
                    System.Console.WriteLine("Something went wrong");
                }
                System.Console.WriteLine("Available doctors:");
                for (int i = 0; i < doctorList.Count; i++)
                {
                    if (WeekSchedule[Thurint, int.Parse(selectedTimeInputTH!)] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"[{i}] {doctorList[i].Username}");
                    }

                }
                System.Console.WriteLine("Write the number of the doctor to assign the patient");
                string? selectedDocInputTH = Console.ReadLine();
                if (selectedDocInputTH == null || selectedDocInputTH == "")
                {
                    System.Console.WriteLine("Something went wrong");
                }

                if (int.TryParse(selectedTimeInputTH, out int selectedTimeTH) && int.TryParse(selectedDocInputTH, out int selectedDocTH))
                {
                    if (selectedTimeTH >= 0 && selectedTimeTH <= 6 && WeekSchedule[Thurint, selectedTimeTH] == (OpenSlot, " "))
                    {
                        var BookedDocTH = doctorList[selectedDocTH];
                        System.Console.WriteLine("The appointment is now booked");
                        AppointmentRequests.RemoveAt(requestedInt);
                        WeekSchedule[Thurint, selectedTimeTH] = (patientuser!.Email, BookedDocTH.Username);
                    }
                }
                break;
            case "5":
                int Friint = 4;
                System.Console.WriteLine("Open time slots on Monday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                {
                    if (WeekSchedule[Friint, i] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"[{i}] Open time: {i + 8}:00");
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInputFR = Console.ReadLine();
                if (selectedTimeInputFR == null || selectedTimeInputFR == "")
                {
                    System.Console.WriteLine("Something went wrong");
                }
                System.Console.WriteLine("Available doctors:");
                for (int i = 0; i < doctorList.Count; i++)
                {
                    if (WeekSchedule[Friint, int.Parse(selectedTimeInputFR!)] == (OpenSlot, " "))
                    {
                        System.Console.WriteLine($"[{i}] {doctorList[i].Username}");
                    }

                }
                System.Console.WriteLine("Write the number of the doctor to assign the patient");
                string? selectedDocInputFR = Console.ReadLine();
                if (selectedDocInputFR == null || selectedDocInputFR == "")
                {
                    System.Console.WriteLine("Something went wrong");
                }

                if (int.TryParse(selectedTimeInputFR, out int selectedTimeFR) && int.TryParse(selectedDocInputFR, out int selectedDocFR))
                {
                    if (selectedTimeFR >= 0 && selectedTimeFR <= 6 && WeekSchedule[Friint, selectedTimeFR] == (OpenSlot, " "))
                    {
                        var BookedDocFR = doctorList[selectedDocFR];
                        System.Console.WriteLine("The appointment is now booked");
                        AppointmentRequests.RemoveAt(requestedInt);
                        WeekSchedule[Friint, selectedTimeFR] = (patientuser!.Email, BookedDocFR.Username);
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
            System.Console.WriteLine("Your booked appointments");

            for (int i = 0; i < WeekSchedule.GetLength(0); i++)
            {
                for (int j = 0; j < WeekSchedule.GetLength(1); j++)
                {
                    if (WeekSchedule[i, j].patientslot == activeUser.ToString())
                    {
                        switch (i)
                        {
                            case 0:
                                System.Console.WriteLine($"You have an appointment on Monday at {j + 8}:00 with doctor: {WeekSchedule[0, j].doctorslot}");
                                break;
                            case 1:
                                System.Console.WriteLine($"You have an appointment on Tuesday at {j + 8}:00 with doctor: {WeekSchedule[1, j].doctorslot}");
                                break;
                            case 2:
                                System.Console.WriteLine($"You have an appointment on Wednesday at {j + 8}:00 with doctor: {WeekSchedule[2, j].doctorslot}");
                                break;
                            case 3:
                                System.Console.WriteLine($"You have an appointment on Thursday at {j + 8}:00 with doctor: {WeekSchedule[3, j].doctorslot}");
                                break;
                            case 4:
                                System.Console.WriteLine($"You have an appointment on Friday at {j + 8}:00 with doctor: {WeekSchedule[4, j].doctorslot}");
                                break;
                        }
                    }
                }
            }
            Console.ReadLine();
        }
    }

    public void PrintSchedulePersonnal(IUser? activeUser)
    {
        System.Console.WriteLine("Schedule:");
        System.Console.WriteLine("[1] See your schedule for the day.");
        System.Console.WriteLine("[2] See your schedule for the week.");
        switch (Console.ReadLine())
        {
            case "1":
                switch (WeekDay)
                {
                    case DayOfWeek.Monday:
                        for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                        {
                            if (activeUser!.ToString() == WeekSchedule[0, i].doctorslot)
                            {
                                System.Console.WriteLine($"{i + 8}:00. Paitent: {WeekSchedule[0, i].patientslot}");
                            }
                        }
                        Console.ReadLine();
                        break;
                    case DayOfWeek.Tuesday:
                        for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                        {
                            if (activeUser!.ToString() == WeekSchedule[1, i].doctorslot)
                            {
                                System.Console.WriteLine($"{i + 8}:00. Paitent: {WeekSchedule[1, i].patientslot}");
                            }
                        }
                        Console.ReadLine();
                        break;
                    case DayOfWeek.Wednesday:
                        for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                        {
                            if (activeUser!.ToString() == WeekSchedule[2, i].doctorslot)
                            {
                                System.Console.WriteLine($"{i + 8}:00. Paitent: {WeekSchedule[0, i].patientslot}");
                            }
                        }
                        Console.ReadLine();
                        break;
                    case DayOfWeek.Thursday:
                        for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                        {
                            if (activeUser!.ToString() == WeekSchedule[3, i].doctorslot)
                            {
                                System.Console.WriteLine($"{i + 8}:00. Paitent: {WeekSchedule[3, i].patientslot}");
                            }
                        }
                        Console.ReadLine();
                        break;
                    case DayOfWeek.Friday:
                        for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                        {
                            if (activeUser!.ToString() == WeekSchedule[4, i].doctorslot)
                            {
                                System.Console.WriteLine($"{i + 8}:00. Paitent: {WeekSchedule[4, i].patientslot}");
                            }
                        }
                        Console.ReadLine();
                        break;
                    default:
                        System.Console.WriteLine("You have no appointments today");
                        Console.ReadLine();
                        break;
                }
                break;
            case "2":
                for (int i = 0; i < WeekSchedule.GetLength(0); i++)
                {
                    for (int j = 0; j < WeekSchedule.GetLength(1); j++)
                    {
                        if (activeUser!.ToString() == WeekSchedule[i, j].doctorslot)
                        {
                            if (i == 0)
                            {
                                System.Console.WriteLine($"Monday at {i + 8}:00. Paitent: {WeekSchedule[i, j].patientslot}");
                            }
                            else if (i == 1)
                            {
                                System.Console.WriteLine($"Tuesday at {i + 8}:00. Paitent: {WeekSchedule[i, j].patientslot}");
                            }
                            else if (i == 2)
                            {
                                System.Console.WriteLine($"Wednesday at {i + 8}:00. Paitent: {WeekSchedule[i, j].patientslot}");
                            }
                            else if (i == 3)
                            {
                                System.Console.WriteLine($"Thursday at {i + 8}:00. Paitent: {WeekSchedule[i, j].patientslot}");
                            }
                            else if (i == 4)
                            {
                                System.Console.WriteLine($"Friday at {i + 8}:00. Paitent: {WeekSchedule[i, j].patientslot}");
                            }

                        }
                    }
                }
                Console.ReadLine();
                break;
            default:
                System.Console.WriteLine("Something went wrong");
                break;
        }
    }
    public void AppointmentRequest(IUser? activeUser)
    {
        System.Console.WriteLine("Request Appointment:");
        System.Console.WriteLine("Please leave a small description of your condition:");
        string? DescCond = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(DescCond))
        {
            System.Console.WriteLine("Description cannot be blank");
            Console.ReadLine();
            return;
        }

        System.Console.WriteLine($"You wrote: {DescCond}");
        System.Console.WriteLine("press ENTER to send your request");
        Console.ReadLine();

        AppointmentRequests.Add((activeUser!.ToString() ?? "", DescCond, PaitentWaitingStatus.Pending));
    }
    public void SeeAppointmentRequest()
    {
        Console.Clear();
        System.Console.WriteLine("Paitent waiting for booking approval");
        for (int i = 0; i < AppointmentRequests.Count; i++)
        {
            if (AppointmentRequests[i].status == PaitentWaitingStatus.Pending)
            {
                Console.WriteLine($"Booking request number [{i}], Patient name:{AppointmentRequests[i].PatientName}, Note: {AppointmentRequests[i].PatDesc}");
            }
        }
        System.Console.WriteLine("Select the patient you wish to handle");
        string? requestSelectedInput = Console.ReadLine();
        if (requestSelectedInput == null || requestSelectedInput == "")
        {
            System.Console.WriteLine("Something went wrong.");
        }
        int.TryParse(requestSelectedInput, out int requestedInt);
        System.Console.WriteLine($"You have selected: {AppointmentRequests![requestedInt].PatientName}");

        System.Console.WriteLine("Do you want to approve this patient's appointment request?");
        System.Console.WriteLine("[Y/N]");

        switch (Console.ReadLine())
        {
            case "Y":
                var patapproved = AppointmentRequests[requestedInt];
                patapproved.status = PaitentWaitingStatus.Approved;
                AppointmentRequests[requestedInt] = patapproved;

                System.Console.WriteLine("The patient is now approved and is waiting for a time booking");
                Console.ReadLine();
                break;
            case "N":
                var patdenied = AppointmentRequests[requestedInt];
                patdenied.status = PaitentWaitingStatus.Denied;
                AppointmentRequests[requestedInt] = patdenied;
                System.Console.WriteLine("The appointment is now denied");
                Console.ReadLine();
                break;
            default:
                System.Console.WriteLine("Something went wrong.");
                Console.ReadLine();
                break;
        }
    }

    public void Notifications()
    {
        int waitCount = 0;
        int approvedCount = 0;

        foreach (var patientrequest in AppointmentRequests)
        {
            if (patientrequest.status == PaitentWaitingStatus.Pending)
            {
                waitCount += 1;
            }
        }
        foreach (var patient in AppointmentRequests)
        {
            if (patient.status == PaitentWaitingStatus.Approved)
            {
                approvedCount += 1;
            }
        }
        if (waitCount > 0)
        {
            System.Console.WriteLine($"You have {waitCount} patients waiting for appoval of booking. ");
        }
        if (approvedCount > 0)
        {
            System.Console.WriteLine($"You have {approvedCount} patient waiting for appointment booking");
        }
        if (waitCount == 0 && approvedCount == 0)
        {
            System.Console.WriteLine("You have no Notifications");
        }
    }
}