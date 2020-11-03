using System;
using System.Collections.Generic;
using CitraDigitalAndroid.ViewModels;
using CitraDigitalAndroid.Views;
using Xamarin.Forms;

namespace CitraDigitalAndroid
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("home/checkpage", typeof(CheckPage));

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


                case "detailchecktruck":
                    await Shell.Current.GoToAsync("detailchecktruck");
                    break;

                case "detailtruck":
                    await Shell.Current.GoToAsync("detailtruck");
                    break;

                case "approve":
                    await Shell.Current.GoToAsync("approve");
                    break;

                default:
                    break;
            }
        }
    }
}
