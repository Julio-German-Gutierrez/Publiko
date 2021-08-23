using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
//using PublikoWebApp.Models;
using PublikoSharedLibrary.Models;

namespace PublikoWebApp.Services
{
    public interface IStoredPagesService
    {
        Task<string> GetPagesByAuthorIDAsync(string userID, IdentityUser userObject); //HttpResponseMessage
        Task<string> GetPostsByAuthorIDAsync(string userID, IdentityUser userObject); //HttpResponseMessage
        List<WebPage> GetAllPagesAsync();

        Task<string> CreatePageAsync(string pageTitle, string pageBody, int? pageOrder, string userID, IdentityUser userObject);
        Task<string> CreatePostAsync(string uRLPostTitle, string uRLPostContent, string userID, IdentityUser userObject);
        Task<string> GetPageByIDAsync(string id, IdentityUser userObject);
        Task<string> EditPageAsync(string pageID, string pageTitle, string pageBody, int pageOrder, IdentityUser userObject);
        Task<string> GetPostByIDAsync(string id, IdentityUser userObject);
        Task<string> EditPostAsync(string postID, string postTitle, string postContent, IdentityUser userObject);
    }
}
