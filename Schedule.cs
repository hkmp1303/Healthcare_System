namespace HospitalApp;

class Schedule
{
    public const int Days = 5;
    public const int Slots = 7;
    public string[,] WeekSchedule = new string[Days, Slots];

    public Schedule()
    {

    }
    
    public void PrintSchedule()
    {
        Console.WriteLine($"|-Monday-|-Tuseday-|-Wednesday-|-Thursday-|-Friday-|");
        Console.WriteLine($"|-{WeekSchedule[0,0]}-|-{WeekSchedule[1,0]}-|-{WeekSchedule[2,0]}-|-{WeekSchedule[3,0]}-|-{WeekSchedule[4,0]}-|");
        Console.WriteLine($"|-{WeekSchedule[0,1]}-|-{WeekSchedule[1,1]}-|-{WeekSchedule[2,1]}-|-{WeekSchedule[3,1]}-|-{WeekSchedule[4,1]}-|");
        Console.WriteLine($"|-{WeekSchedule[0,2]}-|-{WeekSchedule[1,2]}-|-{WeekSchedule[2,2]}-|-{WeekSchedule[3,2]}-|-{WeekSchedule[4,2]}-|");
        Console.WriteLine($"|-{WeekSchedule[0,3]}-|-{WeekSchedule[1,3]}-|-{WeekSchedule[2,3]}-|-{WeekSchedule[3,3]}-|-{WeekSchedule[4,3]}-|");
        Console.WriteLine($"|-{WeekSchedule[0,4]}-|-{WeekSchedule[1,4]}-|-{WeekSchedule[2,4]}-|-{WeekSchedule[3,4]}-|-{WeekSchedule[4,4]}-|");
        Console.WriteLine($"|-{WeekSchedule[0,5]}-|-{WeekSchedule[1,5]}-|-{WeekSchedule[2,5]}-|-{WeekSchedule[3,5]}-|-{WeekSchedule[4,5]}-|");
        Console.WriteLine($"|-{WeekSchedule[0,6]}-|-{WeekSchedule[1,6]}-|-{WeekSchedule[2,6]}-|-{WeekSchedule[3,6]}-|-{WeekSchedule[4,6]}-|");

    }

}

