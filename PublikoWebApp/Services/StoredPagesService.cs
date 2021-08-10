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
        //string resourceFetch = @"Fetch";
        //string resourceCreate = @"Create";
        //string aPageName= @"pageName=";
        //string aPageHead= @"pageHead=";
        //string aPageBody = @"pageBody=";
        //string aUserID = @"userID=";

        public async Task<string> GetPagesByAuthorIDAsync(string userID) //HttpResponseMessage
        {
            string searchByAuthorID = $"/api/author/{userID}/pages";
            string fullURL = baseURL + searchByAuthorID;

            var request = new HttpRequestMessage(HttpMethod.Get, fullURL);
            var response = await _httpClient.SendAsync(request);

            if (response != null)
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

        //public string CreatePageAsync(Page page)
        //{
        //    string pageJson = JsonSerializer.Serialize(page);

        //    string fullURL = baseURL + resourceCreate + "?" + "page=" + pageJson;

        //    var request = new HttpRequestMessage(HttpMethod.Post, fullURL);
        //    var response = _httpClient.SendAsync();

        //    return "";
        //}
    }
}
