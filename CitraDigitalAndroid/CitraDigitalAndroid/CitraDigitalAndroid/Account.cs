using CitraDigitalAndroid.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CitraDigitalAndroid
{
    public class Account
    {

        public static bool UserIsLogin {
            get {
                var user = GetUser().Result;
                return user == null ? false : true;
                                
            }
        }


        public static async Task<AuthenticateResponse> GetUser()
        {
            var userString = await SecureStorage.GetAsync("User");
            if (string.IsNullOrEmpty(userString))
                return null;
            else
                return JsonConvert.DeserializeObject<AuthenticateResponse>(userString);
        }

        public static async Task SetUser(AuthenticateResponse response)
        {
            try
            {
                var userString = JsonConvert.SerializeObject(response);
                await SecureStorage.SetAsync("User", userString);
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public static async Task LogOut()
        {
            try
            {
                await SecureStorage.SetAsync("User", string.Empty);
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }


        public static string Token
        {
            get
            {
                var user = GetUser().Result;
                return user == null ? string.Empty : user.Token;
            }
        }

        public static async Task<bool> UserInRole(UserType type)
        {
            var user = await GetUser();
            if (user != null)
            {
                var role = user.Roles.Where(x => x.ToLower() == type.ToString().ToLower()).FirstOrDefault();
                return string.IsNullOrEmpty(role) ? false : true;
            }
            return false;
        }



    }
}
