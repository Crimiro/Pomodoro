using System;
using System.Collections.ObjectModel
namespace Pomodoro.ViewModels
{
    public class HistoryPageViewModel : NotificationObject
    {
        private ObservableCollection<DateTime> pomodoros;

        public ObservableCollection<DateTime> Pomodoros
        {
            get { return pomodoros; }
            set
            {
                pomodoros = value;
                OnPropertyChanged();
            }
        }

        public HistoryPageViewModel()
        {
        }
    }
}
