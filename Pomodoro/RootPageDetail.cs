using System;

using Xamarin.Forms;

namespace Pomodoro
{
    public class RootPageDetail : ContentPage
    {
        public RootPageDetail()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

