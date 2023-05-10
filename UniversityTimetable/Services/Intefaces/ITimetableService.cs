using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityTimetable.Models;

namespace UniversityTimetable.Services.Intefaces
{
    interface ITimetableService
    {
        Task<List<Timetable>?> GetTimetable(string groupName, string date, int dayOfWeek);
        Task InsertOrUpdateItem(Timetable item);
        Task<int> CheckingBusyClassrooms(string date, int dayOfWeek, int id, int? classroom, int numLesson);
        List<int?>? GetBusyClassrooms(string date, int dayOfWeek);
    }
}
