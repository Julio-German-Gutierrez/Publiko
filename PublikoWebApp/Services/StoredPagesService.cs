using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using PublikoSharedLibrary.Models;

namespace PublikoWebApp.Services
{
    public class StoredPagesService : IStoredPagesService
    {
        public StoredPagesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpClient _httpClient { get; }
        
        string baseURL = @"https://localhost:5001";



        public async Task<string> GetPagesByAuthorIDAsync(string userID) //HttpResponseMessage
        {
            string searchByAuthorID = $"/api/author/{userID}/pages";
            string fullURL = baseURL + searchByAuthorID;

            var request = new HttpRequestMessage(HttpMethod.Get, fullURL);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else throw new Exception("Problema");
            //else throw new Exception(response.ReasonPhrase);
        }



        public List<WebPage> GetAllPagesAsync()
        {
            List<WebPage> listado = new List<WebPage>();
            


            return listado;
            //throw new NotImplementedException();
        }



        public async Task<string> CreatePageAsync(WebPage webPage = null)
        {
            if (webPage != null)
            {
                string webPageJson = JsonSerializer.Serialize(webPage);

                string fullURL = baseURL + $"/api/pages/create/page/title/{webPage.PageTitle}/body/{webPage.PageBody}/user/{webPage.UserID}";

                var request = new HttpRequestMessage(HttpMethod.Post, fullURL);
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return "error";
                }
            }

            return "ok";
        }
    }
}
