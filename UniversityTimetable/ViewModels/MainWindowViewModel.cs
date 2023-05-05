using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UniversityTimetable.Infrastructure.Commands;
using UniversityTimetable.Models;
using UniversityTimetable.Services.Intefaces;
using UniversityTimetable.ViewModels.Base;

namespace UniversityTimetable.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly ITimetableService _timetableService;

        #region Свойства

        private string _title = "Расписание";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private ObservableCollection<Timetable> _timetableItems;
        public ObservableCollection<Timetable> TimetableItems
        {
            get => _timetableItems;
            set => Set(ref _timetableItems, value);
        }

        private DateTime _selectedMonday = DateTime.Now.AddDays(-1 * (7 + (DateTime.Now.DayOfWeek - DayOfWeek.Monday)) % 7).Date;
        public DateTime SelectedMonday
        {
            get => _selectedMonday;
            set => Set(ref _selectedMonday, value);
        }

        #endregion

        #region Команды

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        // выполняется, когда команда выполняется
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationCommandExecute(object p) => true;
        #endregion

        #region Клик по дням недели
        public ICommand WeekDayClickCommand { get; }

        private void OnWeekDayClickCommandExecuted(object p)
        {
            
        }
        #endregion

        #region DataGrid Loaded
        public ICommand DataGridLoadedCommand { get; }

        private void OnDataGridLoadedCommandExecuted(object p)
        {
            ObservableCollection<Timetable> timetable = new ObservableCollection<Timetable>()
            {
                new Timetable() { Id = 1, NumLesson = 1, Time = "9:00 - 10:20", GroupName = "22-A-1", Subject = "Математика", Teacher = "Волопенко С.В.", Classroom = 216 },
                new Timetable() { Id = 2, NumLesson = 2, Time = "10:30 - 11:50", GroupName = "22-A-1", Subject = "Русский", Teacher = "Иванов С.В.", Classroom = 316 },
                new Timetable() { Id = 3, NumLesson = 3, Time = "12:20 - 13:40", GroupName = "22-A-1", Subject = "Психология", Teacher = "Колопенко С.В.", Classroom = 245 },
                new Timetable() { Id = 4, NumLesson = 4, Time = "13:50 - 15:10", GroupName = "22-A-1", Subject = "Физкультура", Teacher = "Стислав С.В.", Classroom = 111 },
                new Timetable() { Id = 5, NumLesson = 5, Time = "15:20 - 16:40", GroupName = "22-A-1", Subject = "Литература", Teacher = "Кожевников С.В.", Classroom = 421 },
                new Timetable() { Id = 6, NumLesson = 6, Time = "16:50 - 18:10", GroupName = "22-A-1", Subject = "Англ. яз.", Teacher = "Петров С.В.", Classroom = 541 },
                new Timetable() { Id = 7, NumLesson = 7, Time = "18:20 - 19:40", GroupName = "22-A-1", Subject = "Информатика", Teacher = "Сидоров С.В.", Classroom = 133 }
            };
            TimetableItems = timetable;
        }
        #endregion

        #region Событие смены даты
        public ICommand CalendarSelectedDateChangedCommand { get; }

        private void OnCalendarSelectedDateChangedCommandExecuted(object p)
        {
            SelectedMonday = SelectedMonday.AddDays(-1 * (7 + (SelectedMonday.DayOfWeek - DayOfWeek.Monday)) % 7).Date;
        }
        #endregion

        #endregion

        public MainWindowViewModel(ITimetableService timetableService)
        {
            _timetableService = timetableService;

            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            WeekDayClickCommand = new LambdaCommand(OnWeekDayClickCommandExecuted);
            DataGridLoadedCommand = new LambdaCommand(OnDataGridLoadedCommandExecuted);
            CalendarSelectedDateChangedCommand = new LambdaCommand(OnCalendarSelectedDateChangedCommandExecuted);
            #endregion
        }
    }
}
