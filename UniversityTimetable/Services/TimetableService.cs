using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UniversityTimetable.Data;
using UniversityTimetable.Models;
using UniversityTimetable.Services.Intefaces;
using UniversityTimetable.ViewModels.Base;

namespace UniversityTimetable.Services
{
    class TimetableService : ITimetableService
    {
        public List<Timetable> GetTimetable()
        {
            // добавление данных
            using (AppDbContext db = new AppDbContext())
            {
                // гарантируем, что база данных создана
                db.Database.EnsureCreated();
                // загружаем данные из БД
                var query = from timetable in db.Timetables
                            select new Timetable
                            {
                                GroupName = timetable.GroupName,
                                Subject = timetable.Subject,
                                Teacher = timetable.Teacher,
                                Classroom = timetable.Classroom
                            };

                return query.ToList();
            }
        }
    }
}
