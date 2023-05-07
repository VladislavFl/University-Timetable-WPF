using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using UniversityTimetable.Data;
using UniversityTimetable.Models;
using UniversityTimetable.Services.Intefaces;

namespace UniversityTimetable.Services
{
    class TimetableService : ITimetableService
    {
        public List<Timetable>? GetTimetable(string groupName, string date, int dayOfWeek)
        {
            try
            {
                // добавление данных
                using (AppDbContext db = new AppDbContext())
                {
                    // гарантируем, что база данных создана
                    db.Database.EnsureCreated();
                    // загружаем данные из БД
                    var query = from timetable in db.Timetables
                                where timetable.GroupName == groupName && timetable.Date == date && timetable.DayOfWeek == dayOfWeek
                                select new Timetable
                                {
                                    Id = timetable.Id,
                                    Date = timetable.Date,
                                    DayOfWeek = timetable.DayOfWeek,
                                    NumLesson = timetable.NumLesson,
                                    GroupName = timetable.GroupName,
                                    Subject = timetable.Subject,
                                    Teacher = timetable.Teacher,
                                    Classroom = timetable.Classroom
                                };

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void InsertOrUpdateItem(Timetable item)
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    // проверяем, существует ли такой элемент в БД
                    var lesson = db.Timetables.Where(c => c == item).FirstOrDefault();

                    // если не существует, то создаём новую запись в БД
                    if (lesson == null)
                    {
                        db.Timetables.Add(item);
                    }
                    // иначе обновляем найденный элемент
                    else
                    {
                        lesson.Subject = item.Subject;
                        lesson.Teacher = item.Teacher;
                        lesson.Classroom = item.Classroom;
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
