using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Pomodoro.ViewModels
{
    
    public class PomodoroPageViewModel : NotificationObject
    {
        private int pomodoroDutation;
        private int breakDutation;
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

        private int duration;

        public int Duration
        {
            get { return duration; }
            set
            {
                duration = value;
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

        private bool isInBreak;

        public bool IsInBreak
        {
            get { return isInBreak; }
            set
            {
                isInBreak = value;
                OnPropertyChanged();
            }
        }

        public ICommand StartOrPauseCommand { get; set; }

        public PomodoroPageViewModel()
        {
            InitializeTimer();
            LoadConfiguredValues();
            StartOrPauseCommand = new Command(StartOrPauseCommandExecute);
        }

        private void LoadConfiguredValues()
        {
            pomodoroDutation = (int)Application.Current.Properties[Literals.PomodoroDuration];
            breakDutation = (int)Application.Current.Properties[Literals.BreakDuration];
            Duration = pomodoroDutation * 60;
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
        }

        async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Elapsed = Elapsed.Add(TimeSpan.FromSeconds(1));
            if (IsRunning && Elapsed.TotalSeconds == pomodoroDutation * 60)
            {
                IsRunning = false;
                IsInBreak = true;
                Elapsed = TimeSpan.Zero;

                await SavePomodoroAsync();
            }
            if (IsInBreak && Elapsed.TotalSeconds == pomodoroDutation * 60)
            {
                IsRunning = true;
                IsInBreak = false;
                Elapsed = TimeSpan.Zero;
            }
        }

        private async Task SavePomodoroAsync()
        {
            List<DateTime> history;
            if (Application.Current.Properties.ContainsKey(Literals.History))
            {
                var json = Application.Current.Properties[Literals.History].ToString();
                history = JsonConvert.DeserializeObject<List<DateTime>>(json);
            }
            else
            {
                history = new List<DateTime>();
            }
            history.Add(DateTime.Now);

            var serializedObject = JsonConvert.SerializeObject(history);
            Application.Current.Properties[Literals.History] = serializedObject;

            await Application.Current.SavePropertiesAsync();
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
