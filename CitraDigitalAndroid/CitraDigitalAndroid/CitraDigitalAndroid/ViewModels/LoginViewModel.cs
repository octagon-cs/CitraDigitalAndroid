using CitraDigitalAndroid.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CitraDigitalAndroid.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command ShowUrlCommand { get; }
        private bool showUrl;

        public bool ShowUrl
        {
            get { return showUrl; }
            set { SetProperty(ref showUrl , value); }
        }



        public LoginViewModel()
        {
           LoginCommand = new Command(OnLoginClicked);
            ShowUrlCommand = new Command(() => {
                ShowUrl = !ShowUrl;
            });
        }


        private string url;

        public string URL
        {
            get {
                if (string.IsNullOrEmpty(url))
                    url = Helper.Url;
                return url; }
            set { SetProperty(ref url , value);
                Helper.Url = value;
            }
            
        }



        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName , value); }
        }


        private string password;

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password , value); }
        }

        private async void OnLoginClicked(object obj)
        {
            try
            {
                var isLogin = await UserService.Login(new Models.AuthenticateRequest { UserName = UserName, Password = Password });
                if (isLogin)
                {
                    if (await Account.UserInRole(UserType.Gate))
                    {
                        Application.Current.MainPage = new GateShell();
                    }
                    else
                    {
                        Application.Current.MainPage = new AppShell();
                    }
                        await Shell.Current.GoToAsync("//Home");
                }
                else
                throw new SystemException("Tidak Berhasil Login !");
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = ex.Message,
                    Cancel = "OK"
                }, "message");
                IsBusy = false;

            }
        }
    }
}
