using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.Models;

namespace UniversityTimetable.Services.Intefaces
{
    interface ITimetableService
    {
        List<Timetable> GetTimetable();
        void InsertOrUpdateItem(Timetable item);
    }
}
