using UniversityTimetable.Data;
using UniversityTimetable.Models;
using UniversityTimetable.Services.Intefaces;

namespace UniversityTimetable.Services
{
    class TimetableService : ITimetableService
    {
        public void AddData()
        {
            // добавление данных
            using (AppDbContext db = new AppDbContext())
            {
                // создаем два объекта User
                Timetable time = new Timetable { Id = 1, GroupId = 23, SubjectId = 3, TeacherId = 9, WeekDay = 5, LessonId = 6 };

                // добавляем их в бд
                db.Timetables.AddRange(time);
                db.SaveChanges();
            }
        }
    }
}
