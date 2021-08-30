using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using PublikoSharedLibrary.Models;
using Microsoft.AspNetCore.Identity;
using PublikoWebApp.Data;

namespace PublikoWebApp.Services
{
    public class StoredPagesService : IStoredPagesService
    {
        public StoredPagesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        HttpClient _httpClient { get; }

        string baseURL = @"https://localhost:5001";  //@"https://localhost:5001";



        public async Task<string> GetPagesByAuthorIDAsync(string userID, PublikoUser userObject=null) //HttpResponseMessage
        {
            string searchByAuthorID = $"/api/author/{userID}/pages";
            string fullURL = baseURL + searchByAuthorID;

            var request = new HttpRequestMessage(HttpMethod.Get, fullURL);
            request.Headers.Add("Authorization", "Bearer " + TokenManager.GenerateJwtToken(userObject));
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else throw new Exception("Problema");
            //else throw new Exception(response.ReasonPhrase);
        }

        public async Task<string> GetPostsByAuthorIDAsync(string userID, PublikoUser userObject = null)
        {
            string searchByAuthorID = $"/api/author/{userID}/posts";
            string fullURL = baseURL + searchByAuthorID;

            var request = new HttpRequestMessage(HttpMethod.Get, fullURL);
            request.Headers.Add("Authorization", "Bearer " + TokenManager.GenerateJwtToken(userObject));
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else throw new Exception("Error: StoredPagesServices->GetPostsByAuthorIDAsync()->if(response.IsSuccessStatusCode)");
        }


        public List<WebPage> GetAllPagesAsync()
        {
            List<WebPage> listado = new List<WebPage>();
            
            return listado;
            //throw new NotImplementedException();
        }



        public async Task<string> CreatePageAsync(string pageTitle, string pageBody, int? pageOrder, string userID, PublikoUser userObject)
        {
            if (pageTitle != null && pageBody != null && pageOrder != null && userID != null)
            {
                string fullURL = baseURL + $"/api/pages/create/page/title/{pageTitle}/body/{pageBody}/order/{pageOrder}/user/{userID}";

                var request = new HttpRequestMessage(HttpMethod.Post, fullURL);
                request.Headers.Add("Authorization", "Bearer " + TokenManager.GenerateJwtToken(userObject));
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return "Error: StoredPagesService->CreatePageAsync()->if(!response.IsSuccessStatusCode)";
                }
            }

            return "ok";
        }

        public async Task<string> CreatePostAsync(string uRLPostTitle, string uRLPostContent, string userID, PublikoUser userObject)
        {
            if (uRLPostTitle != null && uRLPostContent != null && userID != null)
            {
                string fullURL = baseURL + $"/api/post/create/title/{uRLPostTitle}/content/{uRLPostContent}/user/{userID}";

                var request = new HttpRequestMessage(HttpMethod.Post, fullURL);
                request.Headers.Add("Authorization", "Bearer " + TokenManager.GenerateJwtToken(userObject));
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return "Error: StoredPagesService->CreatePostAsync()->if(!response.IsSuccessStatusCode)";
                }
            }

            return "ok";
        }

        public async Task<string> GetPageByIDAsync(string id, PublikoUser userObject)
        {
            string fullURL = baseURL + $"/api/page/{id}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fullURL);
            request.Headers.Add("Authorization", "Bearer " + TokenManager.GenerateJwtToken(userObject));
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return "Error: StoredPagesService->GetPageByIDAsync()->if (response.IsSuccessStatusCode)";
        }

        public async Task<string> EditPageAsync(string pageID, string pageTitle, string pageBody, int pageOrder, PublikoUser userObject)
        {
            string URLPageTitle = System.Web.HttpUtility.UrlEncodeUnicode(pageTitle);
            string URLPageBody = System.Web.HttpUtility.UrlEncodeUnicode(pageBody);

            string fullURL = baseURL + $"/api/edit/{pageID}/title/{URLPageTitle}/body/{URLPageBody}/order/{pageOrder}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, fullURL);
            request.Headers.Add("Authorization", "Bearer " + TokenManager.GenerateJwtToken(userObject));
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return "Page Updated";
            }
            else
            {
                return "Error: StoredPagesService->EditPageAsync()->if(response.IsSuccessStatusCode)";
            }

        }

        public async Task<string> GetPostByIDAsync(string id, PublikoUser userObject)
        {
            string fullURL = baseURL + $"/api/post/{id}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fullURL);
            request.Headers.Add("Authorization", "Bearer " + TokenManager.GenerateJwtToken(userObject));
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return "Error: StoredPagesService->GetPostByIDAsync()->if(response.IsSuccessStatusCode)";
        }

        public async Task<string> EditPostAsync(string postID, string postTitle, string postContent, PublikoUser userObject)
        {
            string URLPostTitle = System.Web.HttpUtility.UrlEncodeUnicode(postTitle);
            string URLPostContent = System.Web.HttpUtility.UrlEncodeUnicode(postContent);

            string fullURL = baseURL + $"/api/postedit/{postID}/title/{URLPostTitle}/body/{URLPostContent}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, fullURL);
            request.Headers.Add("Authorization", "Bearer " + TokenManager.GenerateJwtToken(userObject));
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return "Post Updated";
            }
            else
            {
                return "Error: StoredPagesService->EditPostAsync()->if(response.IsSuccessStatusCode)";
            }
        }
    }
}
