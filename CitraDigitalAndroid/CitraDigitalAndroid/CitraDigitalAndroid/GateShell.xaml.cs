using System;
using System.Collections.Generic;
using CitraDigitalAndroid.ViewModels;
using CitraDigitalAndroid.Views;
using Xamarin.Forms;

namespace CitraDigitalAndroid
{
    public partial class GateShell : Xamarin.Forms.Shell
    {
        public GateShell()
        {
            InitializeComponent();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            MenuItem menu = (MenuItem)sender;

            switch (menu.Text.ToLower())
            {
                case "logout":
                    await Shell.Current.GoToAsync("//LoginPage");
                    break;

                case "home":
                    await Shell.Current.GoToAsync("home");
                    break;

                default:
                    break;
            }
        }
    }
}
