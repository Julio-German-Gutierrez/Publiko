using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
//using PublikoWebApp.Models;
using PublikoSharedLibrary.Models;
using PublikoWebApp.Data;

namespace PublikoWebApp.Services
{
    public interface IStoredPagesService
    {
        Task<string> GetPagesByAuthorIDAsync(string userID, PublikoUser userObject); //HttpResponseMessage
        Task<string> GetPostsByAuthorIDAsync(string userID, PublikoUser userObject); //HttpResponseMessage
        List<WebPage> GetAllPagesAsync();

        Task<string> CreatePageAsync(string pageTitle, string pageBody, int? pageOrder, string userID, PublikoUser userObject);
        Task<string> CreatePostAsync(string uRLPostTitle, string uRLPostContent, string userID, PublikoUser userObject);
        Task<string> GetPageByIDAsync(string id, PublikoUser userObject);
        Task<string> EditPageAsync(string pageID, string pageTitle, string pageBody, int pageOrder, PublikoUser userObject);
        Task<string> GetPostByIDAsync(string id, PublikoUser userObject);
        Task<string> EditPostAsync(string postID, string postTitle, string postContent, PublikoUser userObject);
    }
}
