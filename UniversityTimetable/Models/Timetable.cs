namespace UniversityTimetable.Models
{
    public class Timetable
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int WeekDay { get; set; }
        public int LessonId { get; set; }
    }
}
