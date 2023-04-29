using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.ViewModels.Base;

namespace UniversityTimetable.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _title = "Расписание";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }
}
