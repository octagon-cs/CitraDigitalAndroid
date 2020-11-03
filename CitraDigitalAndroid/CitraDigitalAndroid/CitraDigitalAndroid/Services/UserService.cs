using CitraDigitalAndroid.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CitraDigitalAndroid.Services
{
    public interface IUserService
    {
        Task<bool> Login(AuthenticateRequest model);
    }
    public class UserService : IUserService
    {
        public async Task<bool> Login(AuthenticateRequest model)
        {
            try
            {
                using (var client = new RestService())
                {
                    var response = await client.PostAsync("api/users/authenticate", client.GenerateHttpContent(model));
                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<AuthenticateResponse>(resultString);
                        await Account.SetUser(result);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }
    }
}
