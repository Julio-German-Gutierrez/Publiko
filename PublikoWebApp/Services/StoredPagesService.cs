using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PublikoWebApp.Services
{
    public class StoredPagesService : IStoredPagesService
    {
        public StoredPagesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpClient _httpClient { get; }

        public async Task<string> GetPage(string nameOfPage, string userName)
        {
            string baseURL = @"https://localhost:44388/";
            string resource = @"Pages";
            string attribute = @"?pageName=";


            string fullURL = baseURL + resource + attribute + nameOfPage;

            var request = new HttpRequestMessage(HttpMethod.Get, fullURL);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                //NasaModel nasa = await response.Content.ReadFromJsonAsync<NasaModel>();
                return await response.Content.ReadAsStringAsync();
            }
            else throw new Exception(response.ReasonPhrase);

            //return $"The Page: {nameOfPage} is empty for the user: {userName}" ;
        }
    }
}
