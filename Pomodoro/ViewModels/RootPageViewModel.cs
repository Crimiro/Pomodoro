using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Pomodoro.ViewModels
{
    public class RootPageViewModel : NotificationObject
    {
        private ObservableCollection<string> menuItems;

        public ObservableCollection<string> MenuItems
        {
            get { return menuItems; }
            set 
            {
                menuItems = value;
                OnPropertyChanged();
            }
        }

        private string selectedMenuItem;

        public string SelectedMenuItem
        {
            get { return selectedMenuItem; }
            set
            {
                selectedMenuItem = value;
                OnPropertyChanged();
            }
        }

        public RootPageViewModel()
        {
            MenuItems = new ObservableCollection<string>();
            MenuItems.Add("Pomodoro");
            MenuItems.Add("Histórico");
            MenuItems.Add("Configuración");

            PropertyChanged += RootPageViewModel_PropertyChanged;    
        }

        void RootPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SelectedMenuItem))
            {
                // No es la mejor implementación estar comparando cadenas
                if (SelectedMenuItem == "Pomodoro")
                {
                    MessagingCenter.Send(this, Literals.GoToPomodoro);
                }
                if (SelectedMenuItem == "Histórico")
                {
                    MessagingCenter.Send(this, Literals.GoToHistory);
                }
                if(SelectedMenuItem=="Configuración")
                {
                    MessagingCenter.Send(this, Literals.GoToConfiguration);
                }

            }
        }

    }
}
