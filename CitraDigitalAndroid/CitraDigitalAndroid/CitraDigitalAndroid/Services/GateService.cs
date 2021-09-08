using CitraDigitalAndroid.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CitraDigitalAndroid.Services
{
    public interface IGateService
    {
        Task<List<Truck>> Trucks();
        Task<PengajuanItem> TruckLastChencUp(int id);
        Task<Truck> TruckHistories(int id);
        Task<TruckIncomming> Approve(int id, List<HasilPemeriksaan> list);
        Task<TruckIncomming> Reject(int id, List<HasilPemeriksaan> list);
    }
    public class GateService : IGateService
    {
        private string controller = "api/gate";
        public async Task<List<Truck>> Trucks()
        {
            try
            {
                using (var client = new RestService())
                {
                    var response = await client.GetAsync($"{controller}/trucks");
                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<Truck>>(resultString);
                        return result;
                    }else
                        throw new SystemException(await client.Error(response));
                }
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<PengajuanItem> TruckLastChencUp(int idtruck)
        {
            try
            {
                using (var client = new RestService())
                {
                    var response = await client.GetAsync($"{controller}/trucklastcheckup/{idtruck}");
                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<PengajuanItem>(resultString);
                        return result;
                    }  else
                    throw new SystemException(await client.Error(response));
                }
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }


        public async Task<Truck> TruckHistories(int idtruck)
        {
            try
            {
                using (var client = new RestService())
                {
                    var response = await client.GetAsync($"{controller}/TruckHistories/{idtruck}");
                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Truck>(resultString);
                        return result;
                    }
                    else
                        throw new SystemException(await client.Error(response));
                }
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }


        public async Task<TruckIncomming> Approve(int id, List<HasilPemeriksaan> hasil)
        {
            try
            {
                using (var client = new RestService())
                {
                    var response = await client.PostAsync($"{controller}/approve/{id}", client.GenerateHttpContent(hasil));
                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TruckIncomming>(resultString);
                        return result;
                    }
                    else
                        throw new SystemException(await client.Error(response));
                }
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<TruckIncomming> Reject(int id, List<HasilPemeriksaan> hasil)
        {
            try
            {
                using (var client = new RestService())
                {
                    var response = await client.PostAsync($"{controller}/reject/{id}", client.GenerateHttpContent(hasil));
                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TruckIncomming>(resultString);
                        return result;
                    }
                    else
                        throw new SystemException(await client.Error(response));
                }
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }
    }
}
