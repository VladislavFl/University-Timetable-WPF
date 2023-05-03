using System.Windows;
using System.Windows.Input;
using UniversityTimetable.Infrastructure.Commands;
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

        #region Datagrid Loaded
        public ICommand ScheduleDatagridLoadedCommand { get; }

        private void OnScheduleDatagridLoadedCommandExecuted(object p)
        {
            MessageBox.Show("ff!");
        }
        #endregion

        #endregion

        public MainWindowViewModel(ITimetableService timetableService)
        {
            _timetableService = timetableService;

            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ScheduleDatagridLoadedCommand = new LambdaCommand(OnScheduleDatagridLoadedCommandExecuted);
            #endregion
        }
    }
}
