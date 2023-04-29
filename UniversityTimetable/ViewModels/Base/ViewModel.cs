using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UniversityTimetable.ViewModels.Base
{
    // базовый класс модели представления
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? PropertyName = null) // ссылка на поле св-ва, само значение, имя св-ва (определяется  автоматически компилятором)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
}
