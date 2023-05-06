using System.ComponentModel;

namespace UniversityTimetable.Models
{
    public class LessonsTime : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string? _groupName;

        public int NumLesson { get; set; }
        public string? Time { get; set; }
        public string? GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(nameof(GroupName)));
            }
        }
    }
}
