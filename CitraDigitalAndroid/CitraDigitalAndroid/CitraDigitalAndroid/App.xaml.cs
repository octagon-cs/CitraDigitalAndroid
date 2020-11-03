using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CitraDigitalAndroid.Services;
using CitraDigitalAndroid.Views;
using Xamarin.Essentials;
using CitraDigitalAndroid.Models;
using Newtonsoft.Json;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace CitraDigitalAndroid
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            AppCenter.Start("android=ba425392-a4f3-412b-b6d0-2745f88f1500;" ,
                   typeof(Analytics), typeof(Crashes));
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
                else
                {
                    if (await Account.UserInRole(UserType.Gate))
                    {
                        MainPage = new GateShell();
                    }
                }
            }
            catch 
            {
                await Shell.Current.GoToAsync("//LoginPage");
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
