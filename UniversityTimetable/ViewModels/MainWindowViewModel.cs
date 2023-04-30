using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UniversityTimetable.Infrastructure.Commands;
using UniversityTimetable.Models;
using UniversityTimetable.Services;
using UniversityTimetable.Services.Intefaces;
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

        private readonly ITimetableService _timetableService;

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

        #endregion

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ITimetableService timetableService = new TimetableService();
            _timetableService = timetableService;
            _timetableService.AddData();

            #endregion
        }
    }
}
