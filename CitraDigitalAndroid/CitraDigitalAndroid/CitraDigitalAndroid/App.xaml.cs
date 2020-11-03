using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CitraDigitalAndroid.Services;
using CitraDigitalAndroid.Views;
using Xamarin.Essentials;
using CitraDigitalAndroid.Models;
using Newtonsoft.Json;

namespace CitraDigitalAndroid
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<MessagingCenterAlert>(this, "message", async (message) =>
            {
                await Current.MainPage.DisplayAlert(message.Title, message.Message, message.Cancel);
            });

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<UserService>();
            DependencyService.Register<ApprovalService>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {

            try
            {
                if (!Account.UserIsLogin)
                {
                    await Shell.Current.GoToAsync("//LoginPage");
                }
            }
            catch 
            {
                
            }
            

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
