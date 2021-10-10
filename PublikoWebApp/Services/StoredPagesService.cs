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
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Mime;
using Microsoft.Extensions.Logging;

namespace PublikoWebApp.Services
{
    //Interface in the same file for the sake of simplicity.
    public interface IStoredPagesService
    {
        Task<string> GetPagesByAuthorIDAsync(PublikoUser userObject); //HttpResponseMessage
        Task<string> GetPostsByAuthorIDAsync(PublikoUser userObject); //HttpResponseMessage
        Task<string> CreatePageAsync(WebPage newPage);
        Task<string> CreatePostAsync(WebPost newPost);
        Task<WebPage> GetPageByIDAsync(string id, PublikoUser userObject);
        Task<string> EditPageAsync(WebPage webPage);
        Task<WebPost> GetPostByIDAsync(string postId, PublikoUser userObject);
        Task<string> EditPostAsync(WebPost webPost, PublikoUser userObject);
        
    }


    public class StoredPagesService : IStoredPagesService
    {
        IHttpClientFactory _httpClientFactory { get; }
        public UserManager<PublikoUser> _userManager { get; }
        public RoleManager<IdentityRole> _roleManager { get; }
        public ILogger<StoredPagesService> _logger { get; }
        public ITokenManager _tokenManager { get; }

        public StoredPagesService(IHttpClientFactory httpClientFactory,
                                  UserManager<PublikoUser> userManager,
                                  RoleManager<IdentityRole> roleManager,
                                  ILogger<StoredPagesService> logger,
                                  ITokenManager tokenManager)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _tokenManager = tokenManager;
        }



        // OK! >>>>>>>>>>>>>>>>>>>>>>>>>>>
        /// <summary>
        /// Get User Pages by ID 
        /// </summary>
        /// <param name="userObject">PublikoUser object. It inherits from IdentityUser.</param>
        /// <returns></returns>
        public async Task<string> GetPagesByAuthorIDAsync(PublikoUser userObject=null) //HttpResponseMessage
        {
            var _httpClient = _httpClientFactory.CreateClient("PublikoAPI");

            string resource = $"api/pages";

            var request = new HttpRequestMessage(HttpMethod.Get, resource);

            var token = await _tokenManager.GenerateJwtToken(userObject);

            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                return "fail";
        }



        // OK!
        public async Task<string> GetPostsByAuthorIDAsync(PublikoUser userObject = null)
        {
            var _httpClient = _httpClientFactory.CreateClient("PublikoAPI");

            string resource = $"api/posts";

            var request = new HttpRequestMessage(HttpMethod.Get, resource);
            request.Headers.Add("Authorization", "Bearer " + await _tokenManager.GenerateJwtToken(userObject));
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                return "fail";
        }


        
        // OK!
        public async Task<string> CreatePageAsync(WebPage newPage)
        {
            var userObject = await _userManager.FindByIdAsync(newPage.UserID);

            var _httpClient = _httpClientFactory.CreateClient("PublikoAPI");
            string resource = $"api/pages";

            var newPageInc = ConvertToWebPageIncomming(newPage);

            var request = new HttpRequestMessage(HttpMethod.Post, resource);
            request.Headers.Add("Authorization", "Bearer " + await _tokenManager.GenerateJwtToken(userObject));
            string newPageJson = JsonSerializer.Serialize(newPageInc);
            request.Content = new StringContent(newPageJson, Encoding.UTF8, MediaTypeNames.Application.Json);

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return "ok";
            else
                return "Error: StoredPagesService->CreatePageAsync()->if(!response.IsSuccessStatusCode)";
        }



        // OK!
        public async Task<string> CreatePostAsync(WebPost newPost)
        {
            var userObject = await _userManager.FindByIdAsync(newPost.UserID);

            var _httpClient = _httpClientFactory.CreateClient("PublikoAPI");
            string resource = $"api/posts";

            var newPostInc = ConvertToWebPostIncomming(newPost);

            var request = new HttpRequestMessage(HttpMethod.Post, resource);
            request.Headers.Add("Authorization", "Bearer " + await _tokenManager.GenerateJwtToken(userObject));
            string newPostJson = JsonSerializer.Serialize(newPostInc);
            request.Content = new StringContent(newPostJson, Encoding.UTF8, MediaTypeNames.Application.Json);

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return "ok";
            else
                return "Error: StoredPagesService->CreatePostAsync()->if(!response.IsSuccessStatusCode)";
        }


        // OK!
        public async Task<WebPage> GetPageByIDAsync(string pageId, PublikoUser userObject)
        {
            var _httpClient = _httpClientFactory.CreateClient("PublikoAPI");
            string resource = $"api/pages/{pageId}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, resource);
            request.Headers.Add("Authorization", "Bearer " + await _tokenManager.GenerateJwtToken(userObject));
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var page = JsonSerializer.Deserialize<WebPage>
                    (
                        jsonString, 
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
                return page;
            }

            return null;
        }


        // OK!
        public async Task<string> EditPageAsync(WebPage webPage)
        {
            var userObject = await _userManager.FindByIdAsync(webPage.UserID);

            var _httpClient = _httpClientFactory.CreateClient("PublikoAPI");
            string source = $"api/pages";

            var webPageEdit = ConvertToWebPageEditIncomming(webPage);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, source);
            request.Headers.Add("Authorization", "Bearer " + await _tokenManager.GenerateJwtToken(userObject));
            var webPageJson = JsonSerializer.Serialize(webPageEdit, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            request.Content = new StringContent(webPageJson, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return "ok";
            else
            {
                _logger.LogInformation("Error: StoredPagesService->EditPageAsync()->if(response.IsSuccessStatusCode)");
                return "fail";
            }
        }




        // OK!
        public async Task<WebPost> GetPostByIDAsync(string postId, PublikoUser userObject)
        {
            var _httpClient = _httpClientFactory.CreateClient("PublikoAPI");
            string resource = $"api/posts/{postId}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, resource);
            request.Headers.Add("Authorization", "Bearer " + await _tokenManager.GenerateJwtToken(userObject));
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var post = JsonSerializer.Deserialize<WebPost>
                    (
                        jsonString,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
                return post;
            }
            return null;
        }


        // 
        public async Task<string> EditPostAsync(WebPost webPost, PublikoUser userObject)
        {
            var _httpClient = _httpClientFactory.CreateClient("PublikoAPI");
            string resource = $"api/posts";

            var editPost = ConvertToWebPostEditIncomming(webPost);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, resource);
            request.Headers.Add("Authorization", "Bearer " + await _tokenManager.GenerateJwtToken(userObject));
            var editPostJson = JsonSerializer.Serialize(editPost, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            request.Content = new StringContent(editPostJson, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return "ok";
            else
            {
                _logger.LogInformation("Error: StoredPagesService->EditPostAsync()->if(response.IsSuccessStatusCode)");
                return "fail";
            }
        }

        private WebPostEditIncomming ConvertToWebPostEditIncomming(WebPost toEditPost)
        {
            return new WebPostEditIncomming
            {
                PostId = toEditPost.PostID,
                PostTitle = toEditPost.PostTitle,
                PostContent = toEditPost.PostContent,
                PostUserID = toEditPost.UserID
            };
        }
        private WebPageEditIncomming ConvertToWebPageEditIncomming(WebPage webPage)
        {
            return new WebPageEditIncomming
            {
                PageId = webPage.PageID,
                PageTitle = webPage.PageTitle,
                PageBody = webPage.PageBody,
                PageOrder = webPage.PageOrder,
                PageUserID = webPage.UserID
            };
        }
        private WebPageIncomming ConvertToWebPageIncomming(WebPage newPage)
        {
            return new WebPageIncomming
            {
                PageTitle = newPage.PageTitle,
                PageBody = newPage.PageBody,
                PageOrder = newPage.PageOrder,
                PageUserID = newPage.UserID
            };
        }

        private WebPostIncomming ConvertToWebPostIncomming(WebPost newPost)
        {
            return new WebPostIncomming
            {
                PostTitle = newPost.PostTitle,
                PostContent = newPost.PostContent,
                PostUserID = newPost.UserID
            };
        }
    }
}
