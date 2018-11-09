using System;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pomodoro.ViewModels
{
    public class PomodoroPageViewModel : NotificationObject
    {
        private Timer timer;

        private TimeSpan elapsed;

        public TimeSpan Elapsed
        {
            get { return elapsed; }
            set
            {
                elapsed = value;
                OnPropertyChanged();
            }
        }

        private bool isRunning;

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                OnPropertyChanged();
            }
        }

        public ICommand StartOrPauseCommand { get; set; }

        public PomodoroPageViewModel()
        {
            InitializeTimer();
            StartOrPauseCommand = new Command(StartOrPauseCommandExecute);
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Elapsed = Elapsed.Add(TimeSpan.FromSeconds(1));
        }

        private void StartTimer()
        {
            timer.Start();
            IsRunning = true;
        }

        private void StopTimer()
        {
            timer.Stop();
            IsRunning = false;
        }

        private void StartOrPauseCommandExecute()
        {
            if(IsRunning)
            {
                StopTimer();
            }
            else
            {
                StartTimer();
            }
        }
    }
}
