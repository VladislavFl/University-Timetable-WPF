using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UniversityTimetable.Data;
using UniversityTimetable.Models;
using UniversityTimetable.Services.Intefaces;

namespace UniversityTimetable.Services
{
    class TimetableService : ITimetableService
    {
        public async Task<List<Timetable>?> GetTimetable(string groupName, string date, int dayOfWeek)
        {
            try
            {
                // добавление данных
                using (AppDbContext db = new AppDbContext())
                {
                    // гарантируем, что база данных создана
                    await db.Database.EnsureCreatedAsync();
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

                    return await query.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public async Task InsertOrUpdateItem(Timetable item)
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    // проверяем, существует ли такой элемент в БД
                    var lesson = await db.Timetables.Where(c => c == item).FirstOrDefaultAsync();

                    // если не существует, то создаём новую запись в БД
                    if (lesson == null)
                    {
                        await db.Timetables.AddAsync(item);
                    }
                    // иначе обновляем найденный элемент
                    else
                    {
                        lesson.Subject = item.Subject;
                        lesson.Teacher = item.Teacher;
                        lesson.Classroom = item.Classroom;
                    }

                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task<int> CheckingBusyClassrooms(string date, int dayOfWeek, int id, int? classroom, int numLesson)
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    return await db.Timetables.Where(c => c.Id != id && c.Classroom != null && c.Classroom == classroom && c.Date == date && c.DayOfWeek == dayOfWeek && c.NumLesson == numLesson).CountAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public List<int?>? GetBusyClassrooms(string date, int dayOfWeek)
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var classrooms = db.Timetables.Where(c => c.Classroom != null && c.Date == date && c.DayOfWeek == dayOfWeek);

                    List<int?> result = new List<int?>();
                    foreach (var item in classrooms)
                    {
                        result.Add(item.Classroom);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
