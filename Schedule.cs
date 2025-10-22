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


        string? selectInputDay = Console.ReadLine(); //string to use as the day you selected
        if (selectInputDay == null || selectInputDay == "")
        {
            System.Console.WriteLine("Something went wrong."); //null check
        }
        int.TryParse(selectInputDay, out int selectInputDayint); //make the string an int 
        System.Console.WriteLine("Select the time:");
        System.Console.WriteLine("Write the hour");
        string? selectedInputTime = Console.ReadLine(); //string for hour
        if (selectedInputTime == null || selectedInputTime == "") // null check
        {
            System.Console.WriteLine("Something went wrong.");
        }
        int.TryParse(selectedInputTime, out int selectedInputTimeint); //make the string into an int 


        Console.Clear();
        System.Console.WriteLine("Available appointment slots:");
        System.Console.WriteLine("[0] Monday");
        System.Console.WriteLine("[1] Tuesday");
        System.Console.WriteLine("[2] Wednesday");
        System.Console.WriteLine("[3] Thursday");
        System.Console.WriteLine("[4] Friday"); //menu
        System.Console.WriteLine(" ");
        System.Console.WriteLine("Monday [0]");
        for (int i = 0; i < WeekSchedule.GetLength(0); i++)
        {
            for (int j = 0; j < WeekSchedule.GetLength(1); j++) //loop to check the array
            {
                if (i == 0) //checking the first number in the 2d array
                {

                    if (WeekSchedule[i, j] == (OpenSlot, " ")) // check if it's open slot
                    {
                        System.Console.WriteLine($"Time: {j + 8}:00 {WeekSchedule[i, j].patientslot}"); //print
                    }
                }
            }
        }

        System.Console.WriteLine("Tuesday [1]"); //same as above
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

        System.Console.WriteLine("Wednesday [2]"); //same as above
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

        System.Console.WriteLine("Thursday [3]"); //same as above
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

        System.Console.WriteLine("Friday [4]"); //same as above
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
        string? selectnewDay = Console.ReadLine(); //string to keep the new day 
        if (selectnewDay == null || selectnewDay == "") //null check
        {
            System.Console.WriteLine("Something went wrong.");
        }
        int.TryParse(selectnewDay, out int selectnewDayint); //make the string to an int 
        System.Console.WriteLine("Select time");
        System.Console.WriteLine("Write the hour");
        string? selectednewTime = Console.ReadLine(); //string for the hour select
        if (selectednewTime == null || selectednewTime == "") // null check
        {
            System.Console.WriteLine("Something went wrong.");
        }
        int.TryParse(selectednewTime, out int selectednewTimeint); //making the string into an int


        WeekSchedule[selectnewDayint, selectednewTimeint - 8] = WeekSchedule[selectInputDayint, selectedInputTimeint - 8]; //copy the old array elements and adds them to the new array position (the -8 is to adjust for the time input to the array position)
        WeekSchedule[selectInputDayint, selectedInputTimeint - 8] = (OpenSlot, " "); //make the old array position an "open" slot

        System.Console.WriteLine($"Time is now changed"); //sanity check to se if it worked
        Console.ReadLine();
    }

    public void PrintSchedule() //print the whole schedule
    {

        string[] schedDays = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" }; //array of the week days
        string[] schedTime = { "8.00", "9.00", "10.00", "11.00", "12.00", "13.00", "14.00" }; //array with all of the times

        int maxLength = schedDays.Max(day => day.Length); //int variable to get a max size based on all the days
        for (int i = 0; i < schedDays.Length; i++) //loop the check all the Length of days (characters)
        {
            for (int j = 0; j < schedTime.Length; j++) //same but with time
            {
                var pat = $"Patient: {WeekSchedule[i, j].patientslot}"; //make a variable that contains the whole "writen" string. this is so that we can check the Length of the whole string to adjust the size of the string to find the longest string. this is so that we can have the longest string as a base ref to print the schedule
                var doc = $"Doctor:  {WeekSchedule[i, j].doctorslot}"; // same as above
                maxLength = Math.Max(maxLength, Math.Max(pat.Length, doc.Length)); //making the max Length variable the same as the longest string available
            }
        }

        int box = maxLength + 2; //adding +2 for size

        Console.WriteLine("Staff Schedule:");


        Console.Write("Time".PadRight(8)); //print all the time with padding
        foreach (var day in schedDays) //loop all the days
        Console.Write(day.PadRight(box)); //print days and addjusted padding //second comment: I realize while making this method that I dont need {} in the loop on effects the next line of code. this saves space.. anyway let continue
        Console.WriteLine();

        for (int i = 0; i < schedTime.Length; i++) //loop to get all the "time slots"
        {

            Console.Write(schedTime[i].PadRight(8)); //print all the time with padding
            for (int j = 0; j < schedDays.Length; j++) //loop to get all the days
            {

                Console.Write($"Patient: {WeekSchedule[j, i].patientslot}".PadRight(box)); //print all the patient names //with padding

            }

            System.Console.WriteLine(); //use to break the line

            Console.Write("".PadRight(8)); // empty space under "Time" because we're a new line
            for (int j = 0; j < schedDays.Length; j++) //loop again
            {
                Console.Write($"Doctor: {WeekSchedule[j, i].doctorslot}".PadRight(box)); //same as with patient
            }

            System.Console.WriteLine();


        }
        Console.ReadLine();
    }

    public void BookAppointment(List<IUser> users) //booking an appointment
    {
        Console.Clear();
        Console.WriteLine("Appointment Booking:");
        System.Console.WriteLine(" ");
        for (int k = 0; k < AppointmentRequests.Count; k++) //loop thought the AppointmentRequests list
        {
            if (AppointmentRequests[k].status == PaitentWaitingStatus.Pending || AppointmentRequests[k].status == PaitentWaitingStatus.Denied || AppointmentRequests.Count == 0) //check to see if the list if empty
            {
                System.Console.WriteLine("No appointments that need booking");
                System.Console.WriteLine("Press ENTER to continue");
                Console.ReadLine();
                return;
            }
            else if (AppointmentRequests[k].status == PaitentWaitingStatus.Approved) //checking for approved appointment that you can schedule
            {

                Console.WriteLine($"booking number: [{k}] Patient: {AppointmentRequests[k].PatientName}"); //print the approved patients 
            }
        }
        System.Console.WriteLine(" ");
        System.Console.WriteLine("Select the booking request you would like to handle:");
        string? requestSelectedInput = Console.ReadLine(); //string that hold the selected position in the list
        if (requestSelectedInput == null || requestSelectedInput == "") //null check
        {
            System.Console.WriteLine("Something went wrong");
        }
        int.TryParse(requestSelectedInput, out int requestedInt); //make the string into an int 
        System.Console.WriteLine($"You have selected: {AppointmentRequests![requestedInt].PatientName}"); //sanity check to see that you got the right patient 

        Patient? patientuser = users.OfType<Patient>().FirstOrDefault(p => p.Email == AppointmentRequests![requestedInt].PatientName); //making a variable that check if the corrent select patient is a patient in the user list by comparing the select patient with the patient.email 

        var doctorList = users.OfType<Personnel>().ToList(); //making a variable that is a list of all the personnels in the users list 

        System.Console.WriteLine("Which day would you like to book an appointment?");
        System.Console.WriteLine("[1] Monday");
        System.Console.WriteLine("[2] Tuesday");
        System.Console.WriteLine("[3] Wednesday");
        System.Console.WriteLine("[4] Thursday");
        System.Console.WriteLine("[5] Friday"); // menu 
        switch (Console.ReadLine()) //switch to easy menu select
        {
            case "1":

                int Monint = 0; //int to keep track of the first element in the array
                System.Console.WriteLine("Open time slots on Monday:");
                for (int i = 0; i < WeekSchedule.GetLength(1); i++) //loop throught the WeekSchedule array
                {
                    if (WeekSchedule[Monint, i] == (OpenSlot, " ")) //check if it finds open time slots
                    {
                        System.Console.WriteLine($"[{i}] Open time: {i + 8}:00"); //print the open slot and time with a array position
                    }
                }
                System.Console.WriteLine("Write the number to select time");
                string? selectedTimeInputMON = Console.ReadLine(); //string input that selects the time 
                if (selectedTimeInputMON == null || selectedTimeInputMON == "") //null check 
                {
                    System.Console.WriteLine("Something went wrong");
                }
                System.Console.WriteLine("Available doctors:");
                for (int i = 0; i < doctorList.Count; i++) //looping thought all personnel with help from the variable from earlier
                {
                    if (WeekSchedule[Monint, int.Parse(selectedTimeInputMON!)] == (OpenSlot, " ")) // check so the doc isnt Schedule in that time slot (int.parse to quickliy make the string to an int)
                    {
                        System.Console.WriteLine($"[{i}] {doctorList[i].Username}"); //print available docs 
                    }

                }
                System.Console.WriteLine("Write the number of the doctor to assign the patient");
                string? selectedDocInputMon = Console.ReadLine(); //selected doc 
                if (selectedDocInputMon == null || selectedDocInputMon == "") //null check 
                {
                    System.Console.WriteLine("Something went wrong");
                }
                if (int.TryParse(selectedTimeInputMON, out int selectedTimeMON) && int.TryParse(selectedDocInputMon, out int selectedDocMon)) //making the string into int's 
                {
                    if (selectedTimeMON >= 0 && selectedTimeMON <= 6 && WeekSchedule[Monint, selectedTimeMON] == (OpenSlot, " ")) //making sure the slot is within range and that it's an open slot 
                    {
                        var BookedDocMon = doctorList[selectedDocMon]; // making the doc in the list an variable we can use 
                        System.Console.WriteLine("The appointment is now booked");
                        AppointmentRequests.RemoveAt(requestedInt); //removing the approved booking request from the AppointmentRequests list 
                        WeekSchedule[Monint, selectedTimeMON] = (patientuser!.Email, BookedDocMon.Username); //adding the patient and the doctor to the WeekSchedule
                    }
                }

                break;
            case "2": //same as above
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
            case "3": //same as above
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
            case "4": //same as above
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
            case "5": //same as above
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
            default: //in case of wrong input 
                System.Console.WriteLine("Somthing went wrong. please try again.");
                break;
        }

    }

    public void PrintSchedulePatient(IUser? activeUser) //patient schedule over booked appointments 
    {
        if (activeUser!.IsRole(Role.Patient)) //checking to see that activeUser has a patient role 
        {
            System.Console.WriteLine("Your booked appointments");

            for (int i = 0; i < WeekSchedule.GetLength(0); i++) //looping thought the WeekSchedule array 
            {
                for (int j = 0; j < WeekSchedule.GetLength(1); j++)
                {
                    if (WeekSchedule[i, j].patientslot == activeUser.ToString()) //check to see in a patient is the same as activeUser
                    {
                        switch (i) //switch to make it earlier to make nice prints based on the first position in the 2d arrary
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
        else if (activeUser is UnregUser)
        {
            System.Console.WriteLine("You have no appointments today\nPress [Enter] to continue");
            Console.ReadLine();
        }
    }

    public void PrintSchedulePersonnal(IUser? activeUser) //personnel Schedule (personal to the activeUser)
    {
        System.Console.WriteLine("Schedule:");
        System.Console.WriteLine("[1] See your schedule for the day.");
        System.Console.WriteLine("[2] See your schedule for the week."); // menu 
        switch (Console.ReadLine())
        {
            case "1":
                switch (WeekDay) //real-time based check for the switch
                {
                    case DayOfWeek.Monday: //checking the real-time day 
                        for (int i = 0; i < WeekSchedule.GetLength(1); i++) //looping thought the arrary
                        {
                            if (activeUser!.ToString() == WeekSchedule[0, i].doctorslot) //ceking to see that the activeUser is the doc in any of the doctorslot elements
                            {
                                System.Console.WriteLine($"{i + 8}:00. Paitent: {WeekSchedule[0, i].patientslot}"); // print 
                            }
                        }
                        Console.ReadLine();
                        break;
                    case DayOfWeek.Tuesday: //same as above
                        for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                        {
                            if (activeUser!.ToString() == WeekSchedule[1, i].doctorslot)
                            {
                                System.Console.WriteLine($"{i + 8}:00. Paitent: {WeekSchedule[1, i].patientslot}");
                            }
                        }
                        Console.ReadLine();
                        break;
                    case DayOfWeek.Wednesday: //same as above
                        for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                        {
                            if (activeUser!.ToString() == WeekSchedule[2, i].doctorslot)
                            {
                                System.Console.WriteLine($"{i + 8}:00. Paitent: {WeekSchedule[0, i].patientslot}");
                            }
                        }
                        Console.ReadLine();
                        break;
                    case DayOfWeek.Thursday: //same as above
                        for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                        {
                            if (activeUser!.ToString() == WeekSchedule[3, i].doctorslot)
                            {
                                System.Console.WriteLine($"{i + 8}:00. Paitent: {WeekSchedule[3, i].patientslot}");
                            }
                        }
                        Console.ReadLine();
                        break;
                    case DayOfWeek.Friday: //same as above
                        for (int i = 0; i < WeekSchedule.GetLength(1); i++)
                        {
                            if (activeUser!.ToString() == WeekSchedule[4, i].doctorslot)
                            {
                                System.Console.WriteLine($"{i + 8}:00. Paitent: {WeekSchedule[4, i].patientslot}");
                            }
                        }
                        Console.ReadLine();
                        break;
                    default: //if the activeUser dosnt have any book appointments 
                        System.Console.WriteLine("You have no appointments today");
                        Console.ReadLine();
                        break;
                }
                break;
            case "2": //prints all the booking the activeUser has 
                for (int i = 0; i < WeekSchedule.GetLength(0); i++)
                {
                    for (int j = 0; j < WeekSchedule.GetLength(1); j++) //looping throught the WeekSchedule array
                    {
                        if (activeUser!.ToString() == WeekSchedule[i, j].doctorslot) //check to see if activeUser is in any of the doctorslots
                        {
                            if (i == 0) //made this an if but could have use a switch. felt like i wanted to show of the both works and that I can program using both.
                            {  //i checks the first position in the array
                                System.Console.WriteLine($"Monday at {i + 8}:00. Paitent: {WeekSchedule[i, j].patientslot}"); // print
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
    public void AppointmentRequest(IUser? activeUser) //appointment request
    {
        System.Console.WriteLine("Request Appointment:");
        System.Console.WriteLine("Please leave a small description of your condition:");
        string? DescCond = Console.ReadLine(); //string so that the patient can descibe thier sickness
        if (string.IsNullOrWhiteSpace(DescCond)) //nullcheck (should have used the more often... was easier to do null checks this was.... )
        {
            System.Console.WriteLine("Description cannot be blank");
            Console.ReadLine();
            return;
        }

        System.Console.WriteLine($"You wrote: {DescCond}"); //sanity check 
        System.Console.WriteLine("press ENTER to send your request");
        Console.ReadLine();

        AppointmentRequests.Add((activeUser!.ToString() ?? "", DescCond, PaitentWaitingStatus.Pending)); //add the patient and the description to the list and set a enum for pending 
    }
    public void SeeAppointmentRequest() //see appointment requests
    {
        Console.Clear();
        System.Console.WriteLine("Paitent waiting for booking approval");
        for (int i = 0; i < AppointmentRequests.Count; i++) //loop thought the list 
        {
            if (AppointmentRequests[i].status == PaitentWaitingStatus.Pending) //if the enum is pending
            {
                Console.WriteLine($"Booking request number [{i}], Patient name:{AppointmentRequests[i].PatientName}, Note: {AppointmentRequests[i].PatDesc}"); // print 
            }
        }
        System.Console.WriteLine("Select the patient you wish to handle");
        string? requestSelectedInput = Console.ReadLine(); //string with the selected request
        if (requestSelectedInput == null || requestSelectedInput == "") //null check
        {
            System.Console.WriteLine("Something went wrong.");
        }
        int.TryParse(requestSelectedInput, out int requestedInt); // make the string an int
        System.Console.WriteLine($"You have selected: {AppointmentRequests![requestedInt].PatientName}"); //sanity check

        System.Console.WriteLine("Do you want to approve this patient's appointment request?");
        System.Console.WriteLine("[Y/N]");

        switch (Console.ReadLine()) //quick menu to set the new enum
        {
            case "Y":
                var patapproved = AppointmentRequests[requestedInt]; //needed to make it into a variable that i could use...  or else i could manipulate it 
                patapproved.status = PaitentWaitingStatus.Approved; //change the enum to approved
                AppointmentRequests[requestedInt] = patapproved;  // give the variable the right enum 

                System.Console.WriteLine("The patient is now approved and is waiting for a time booking");
                Console.ReadLine();
                break;
            case "N":
                var patdenied = AppointmentRequests[requestedInt];
                patdenied.status = PaitentWaitingStatus.Denied;
                AppointmentRequests[requestedInt] = patdenied;
                System.Console.WriteLine("The appointment is now denied"); //same as above but with denied 
                Console.ReadLine();
                break;
            default:
                System.Console.WriteLine("Something went wrong."); //in case of wrong input in menu
                Console.ReadLine();
                break;
        }
    }

    public void Notifications() //Notifications system that personnel can see 
    {
        int waitCount = 0;
        int approvedCount = 0; //two int's that we¨ll use as counters

        foreach (var patientrequest in AppointmentRequests) //looping thought the AppointmentRequests list 
        {
            if (patientrequest.status == PaitentWaitingStatus.Pending) //check's in anyone has the pending enum 
            {
                waitCount += 1; //add 1 to counter 
            }
        }
        foreach (var patient in AppointmentRequests) //same as above but with the approved enum 
        {
            if (patient.status == PaitentWaitingStatus.Approved)
            {
                approvedCount += 1;
            }
        }
        if (waitCount > 0) //check the value of the int 
        {
            System.Console.WriteLine($"You have {waitCount} patients waiting for appoval of booking. "); //print
        }
        if (approvedCount > 0) //same as above
        {
            System.Console.WriteLine($"You have {approvedCount} patient waiting for appointment booking");
        }
        if (waitCount == 0 && approvedCount == 0) //same as above 
        {
            System.Console.WriteLine("You have no Notifications");
        }
    }
}