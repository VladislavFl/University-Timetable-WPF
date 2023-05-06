using System;

namespace UniversityTimetable.Models
{
    public class Timetable
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DayOfWeek { get; set; }
        public int NumLesson { get; set; }
        public string? GroupName { get; set; }
        public string? Subject { get; set; }
        public string? Teacher { get; set; }
        public int? Classroom { get; set; }
    }
}
