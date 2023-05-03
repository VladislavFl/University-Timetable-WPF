using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UniversityTimetable.Infrastructure.Commands;
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

        #endregion

        public MainWindowViewModel(ITimetableService timetableService)
        {
            _timetableService = timetableService;

            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            WeekDayClickCommand = new LambdaCommand(OnWeekDayClickCommandExecuted);
            #endregion
        }
    }
}
