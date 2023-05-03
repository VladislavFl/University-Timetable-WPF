using System.Windows;
using UniversityTimetable.Services;
using UniversityTimetable.ViewModels;

namespace UniversityTimetable
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(new TimetableService());
        }
    }
}
