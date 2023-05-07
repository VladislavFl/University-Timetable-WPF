using System;
using System.ComponentModel;

namespace UniversityTimetable.Models
{
    public class Timetable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string? _subject;
        private string? _teacher;
        private int? _classroom;

        public int Id { get; set; }
        public string? Date { get; set; }
        public int DayOfWeek { get; set; }
        public int NumLesson { get; set; }
        public string? GroupName { get; set; }
        public string? Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(nameof(Subject)));
            }
        }
        public string? Teacher
        {
            get { return _teacher; }
            set
            {
                _teacher = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(nameof(Teacher)));
            }
        }
        public int? Classroom
        {
            get { return _classroom; }
            set
            {
                _classroom = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(nameof(Classroom)));
            }
        }
    }
}
