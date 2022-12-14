using ClientConvertisseurV2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;

namespace ClientConvertisseurV2.Services
{
    class WSService
    {
        private HttpClient httpClient = new();

        public WSService()
        {
            httpClient.BaseAddress = new Uri("https://localhost:44391/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ObservableCollection<Devise>>GetDevisesAsync()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("https://localhost:44391/api/devises");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<ObservableCollection<Devise>>();
                }
            } catch (Exception e){ }
            return new ObservableCollection<Devise>();
        }

        public async Task<Devise>GetDeviseAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("https://localhost:44391/api/devises/" + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Devise>();
                }
            }
            catch (Exception e) { }
            return new Devise();
        }

    }
}
