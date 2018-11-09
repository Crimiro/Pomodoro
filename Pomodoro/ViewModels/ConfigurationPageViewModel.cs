using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pomodoro.ViewModels
{
    public class ConfigurationPageViewModel : NotificationObject
    {
        private ObservableCollection<int> pomodoroDurations;

        public ObservableCollection<int> PomodoroDurations
        {
            get { return pomodoroDurations; }
            set 
            {
                pomodoroDurations = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<int> breakDurations;

        public ObservableCollection<int> BreakDurations
        {
            get { return breakDurations; }
            set
            {
                breakDurations = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<int> selectedPomodoroDuration;

        public ObservableCollection<int> SelectedPomodoroDuration
        {
            get { return selectedPomodoroDuration; }
            set
            {
                selectedPomodoroDuration = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<int> selectedBreakDuration;

        public ObservableCollection<int> SelectedBreakDuration
        {
            get { return selectedBreakDuration; }
            set
            {
                selectedBreakDuration = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; set; }

        public ConfigurationPageViewModel()
        {
            SaveCommand = new Command(SaveCommandExecute);
        }

        private async void SaveCommandExecute()
        {
            Application.Current.Properties["PomodoroDuration"] = SelectedPomodoroDuration;
            Application.Current.Properties["BreakDuration"] = SelectedBreakDuration;
            await Application.Current.SavePropertiesAsync();
        }
    }
}
