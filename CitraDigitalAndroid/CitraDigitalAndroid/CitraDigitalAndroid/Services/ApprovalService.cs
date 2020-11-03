﻿using CitraDigitalAndroid.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CitraDigitalAndroid.Services
{
    public interface IApprovalService
    {
        Task<Persetujuan> Approve(int id, List<HasilPemeriksaan> hasil);
        Task<List<PengajuanItem>> GetPersetujuan();
        Task<List<HasilPemeriksaan>> GetPenilaian(int itemPengajuanId);
    }
    public class ApprovalService : IApprovalService
    {
        private string controller = "api/approval";
        public Task<Persetujuan> Approve(int id, List<HasilPemeriksaan> hasil)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HasilPemeriksaan>> GetPenilaian(int itemPengajuanId)
        {
            try
            {
                using (var client = new RestService())
                {
                    var response = await client.GetAsync($"{controller}/GetPenilaian/{itemPengajuanId}");
                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<HasilPemeriksaan>>(resultString);
                        return result;
                    }
                    throw new SystemException(await client.Error(response));
                }
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<List<PengajuanItem>> GetPersetujuan()
        {
            try
            {
                using (var client = new RestService())
                {
                    var response = await client.GetAsync($"{controller}/GetPersetujuan");
                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<PengajuanItem>>(resultString);
                        return result;
                    }
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
