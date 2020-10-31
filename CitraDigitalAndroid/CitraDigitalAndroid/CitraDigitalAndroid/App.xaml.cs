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

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {

            try
            {
                var result = JsonConvert.DeserializeObject<User>(await SecureStorage.GetAsync("User"));
                if (result == null)
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
