﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using UniversityTimetable.Infrastructure.Commands;
using UniversityTimetable.Models;
using UniversityTimetable.Services.Intefaces;
using UniversityTimetable.ViewModels.Base;

namespace UniversityTimetable.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly ITimetableService _timetableService;
        private Button _btn;
        private int _dayOfWeek;

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
            if (_btn != null)
            {
                _btn.Background = null;
                _btn.Foreground = new SolidColorBrush(Color.FromArgb(255, 33, 150, 243));
            }
            
            if (p is Button btn)
            {
                _btn = btn;
                _btn.Background = Brushes.RoyalBlue;
                _btn.Foreground = Brushes.White;

                int result;
                if (int.TryParse(_btn.Uid, out result))
                    _dayOfWeek = result;
            }
            LoadingData();
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

        #region DataGrid с изменяемыми данными RowEditEnding (сохранение)
        public ICommand TimetableDataGridRowEditEndingCommand { get; }

        private void OnTimetableDataGridRowEditEndingCommandExecuted(object p)
        {
            if (p is DataGridRowEditEndingEventArgs item)
            {
                var editedItem = item.Row.DataContext as Timetable;
                editedItem.Date = SelectedMonday.ToString();
                editedItem.DayOfWeek = _dayOfWeek; // номер дня (от 1 до 6) означающий день недели
                editedItem.NumLesson = item.Row.GetIndex() + 1; // номер пары = порядок строки
                editedItem.GroupName = CurrentGroup;
                _timetableService.InsertOrUpdateItem(editedItem);
            }
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

        #region ContentRendered - событие загрузки всего контента главного окна
        public ICommand MainWindowLoadedCommand { get; }

        private void OnMainWindowLoadedCommandExecuted(object p)
        {
            if (p is Button btn)
                _btn = btn; // сохраняем объект кнопки "понедельник"

            _dayOfWeek = 1;
            LoadingData(); // загружаем данные
        }
        #endregion

        #region GroupCombobox SelectionChanged
        public ICommand GroupComboboxSelectionChangedCommand { get; }

        private void OnGroupComboboxSelectionChangedCommandExecuted(object p)
        {
            LoadingData();
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
            LoadingData();
        }
        #endregion

        #endregion

        #region Метод загрузки данных в таблицу
        private void LoadingData()
        {
            try
            {
                if (TimetableItems != null) TimetableItems.Clear();

                ObservableCollection<Timetable> timetableItems = new ObservableCollection<Timetable>();
                // заполняем 7 пустых строк
                for (int i = 0; i < 7; i++)
                {
                    timetableItems.Add(new Timetable());
                }
                TimetableItems = timetableItems;

                var timetableItems2 = new ObservableCollection<Timetable>(_timetableService.GetTimetable(CurrentGroup, SelectedMonday.ToString(), _dayOfWeek)); // получаем данные из БД
                if (timetableItems2 == null) return;

                var res = timetableItems2.Select(a => new { NumLesson = a.NumLesson }); // делаем выборку по номеру пары
                // чтобы восстановить корректно порядок пар в расписании
                int counter = 0;
                foreach (var i in res.Where(a => a.NumLesson != 0))
                {
                    TimetableItems[i.NumLesson - 1] = timetableItems2[counter];
                    counter++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        public MainWindowViewModel(ITimetableService timetableService)
        {
            _timetableService = timetableService;

            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            WeekDayClickCommand = new LambdaCommand(OnWeekDayClickCommandExecuted);
            LessonsTimeDataGridLoadedCommand = new LambdaCommand(OnLessonsTimeDataGridLoadedCommandExecuted);
            TimetableDataGridLoadedCommand = new LambdaCommand(OnTimetableDataGridLoadedCommandExecuted);
            TimetableDataGridRowEditEndingCommand = new LambdaCommand(OnTimetableDataGridRowEditEndingCommandExecuted);
            MainWindowLoadedCommand = new LambdaCommand(OnMainWindowLoadedCommandExecuted);

            GroupComboboxLoadedCommand = new LambdaCommand(OnGroupComboboxLoadedCommandExecuted);
            GroupComboboxSelectionChangedCommand = new LambdaCommand(OnGroupComboboxSelectionChangedCommandExecuted);
            CalendarSelectedDateChangedCommand = new LambdaCommand(OnCalendarSelectedDateChangedCommandExecuted);
            #endregion
        }
    }
}
