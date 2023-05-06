using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
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

        private ObservableCollection<LessonsTime> _lessonsTimeItems;
        public ObservableCollection<LessonsTime> LessonsTimeItems
        {
            get => _lessonsTimeItems;
            set => Set(ref _lessonsTimeItems, value);
        }

        private ObservableCollection<Timetable> _timetableItems;
        public ObservableCollection<Timetable> TimetableItems
        {
            get => _timetableItems;
            set => Set(ref _timetableItems, value);
        }

        private List<string> _groupItems;
        public List<string> GroupItems
        {
            get => _groupItems;
            set => Set(ref _groupItems, value);
        }

        private DateTime _selectedMonday = DateTime.Now.AddDays(-1 * (7 + (DateTime.Now.DayOfWeek - DayOfWeek.Monday)) % 7).Date;
        public DateTime SelectedMonday
        {
            get => _selectedMonday;
            set => Set(ref _selectedMonday, value);
        }

        private string _currentGroup;
        public string CurrentGroup
        {
            get => _currentGroup;
            set => Set(ref _currentGroup, value);
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

        #region DataGrid с неизменяемыми данными Loaded
        public ICommand LessonsTimeDataGridLoadedCommand { get; }

        private void OnLessonsTimeDataGridLoadedCommandExecuted(object p)
        {
            // заполняем статическими данными датагрид
            ObservableCollection<LessonsTime> lessonsTime = new ObservableCollection<LessonsTime>()
            {
                new LessonsTime() { NumLesson = 1, Time = "9:00 - 10:20", GroupName = CurrentGroup },
                new LessonsTime() { NumLesson = 2, Time = "10:30 - 11:50", GroupName = CurrentGroup },
                new LessonsTime() { NumLesson = 3, Time = "12:20 - 13:40", GroupName = CurrentGroup },
                new LessonsTime() { NumLesson = 4, Time = "13:50 - 15:10", GroupName = CurrentGroup },
                new LessonsTime() { NumLesson = 5, Time = "15:20 - 16:40", GroupName = CurrentGroup },
                new LessonsTime() { NumLesson = 6, Time = "16:50 - 18:10", GroupName = CurrentGroup },
                new LessonsTime() { NumLesson = 7, Time = "18:20 - 19:40", GroupName = CurrentGroup }
            };
            LessonsTimeItems = lessonsTime;
        }
        #endregion

        #region DataGrid с изменяемыми данными Loaded
        public ICommand TimetableDataGridLoadedCommand { get; }

        private void OnTimetableDataGridLoadedCommandExecuted(object p)
        {
            
        }
        #endregion

        #region GroupCombobox Loaded
        public ICommand GroupComboboxLoadedCommand { get; }

        private void OnGroupComboboxLoadedCommandExecuted(object p)
        {
            // заполняем статическими данными выпадающий список с группами
            GroupItems = new List<string> { "22-А-1", "22-С-1", "22-Д-1", "22-П-1", "22-ПД-1/1", "22-ПД-1/2", 
                                            "22-Э-1", "22-ИСП-1", "22-СА-1", "22-Р-1", "22-ГД-2", "21-ГС-3",
                                            "22-ПД-2/1", "22-ПД-2/2", "22-ПД-2/3", "21-ПД-3/1", "21-ПД-3/2",
                                            "21-ПД-3/3", "22-П-2", "21-П-3", "20-П-4", "22-3М-2", "21-3М-3",
                                            "22-3М-3", "20-3М-4", "22-К-2", "21-К-3", "22-БД-2", "21-БД-3",
                                            "22-ЦД-2", "22-Р-2", "21-Р-3", "20-Р-4", "22-ИД-2", "21-ИД-3",
                                            "20-ИД-4", "22-СА-2", "21-СА-3", "20-СА-4", "22-ИСП-2/1",
                                            "22-ИСП-2/2", "21-ИСП-3", "20-ИСП-4", "22-Д-2", "21-Д-3", "22-Д-3",
                                            "20-Д-4", "22-А-2", "21-А-3", "20-А-4"};
        }
        #endregion

        #region GroupCombobox SelectionChanged
        public ICommand GroupComboboxSelectionChangedCommand { get; }

        private void OnGroupComboboxSelectionChangedCommandExecuted(object p)
        {
            for (int i = 0; i < LessonsTimeItems.Count; i++)
            {
                LessonsTimeItems[i].GroupName = CurrentGroup;
            }
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
            ObservableCollection<Timetable> timetableItems = new ObservableCollection<Timetable>();
            for (int i = 0; i < 7; i++)
            {
                timetableItems.Add(new Timetable());
            }
            TimetableItems = timetableItems;

            _timetableService = timetableService;
            var timetableItems2 = new ObservableCollection<Timetable>(_timetableService.GetTimetable());
            var res = timetableItems2.Select(a => new { NumLesson =  a.NumLesson });
            foreach (var i in res.Where(a => a.NumLesson != 0))
            {
                TimetableItems[i.NumLesson - 1] = timetableItems2[i.NumLesson - 1];
            }

            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            WeekDayClickCommand = new LambdaCommand(OnWeekDayClickCommandExecuted);
            LessonsTimeDataGridLoadedCommand = new LambdaCommand(OnLessonsTimeDataGridLoadedCommandExecuted);
            TimetableDataGridLoadedCommand = new LambdaCommand(OnTimetableDataGridLoadedCommandExecuted);
            GroupComboboxLoadedCommand = new LambdaCommand(OnGroupComboboxLoadedCommandExecuted);
            GroupComboboxSelectionChangedCommand = new LambdaCommand(OnGroupComboboxSelectionChangedCommandExecuted);
            CalendarSelectedDateChangedCommand = new LambdaCommand(OnCalendarSelectedDateChangedCommandExecuted);
            #endregion
        }
    }
}
