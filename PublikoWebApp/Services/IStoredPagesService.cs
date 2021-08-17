using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
//using PublikoWebApp.Models;
using PublikoSharedLibrary.Models;

namespace PublikoWebApp.Services
{
    public interface IStoredPagesService
    {
        Task<string> GetPagesByAuthorIDAsync(string userID); //HttpResponseMessage
        Task<string> GetPostsByAuthorIDAsync(string userID); //HttpResponseMessage
        List<WebPage> GetAllPagesAsync();

        Task<string> CreatePageAsync(string pageTitle, string pageBody, int? pageOrder, string userID);
        Task<string> CreatePostAsync(string uRLPostTitle, string uRLPostContent, string userID);
        Task<string> GetPageByIDAsync(string id);
        Task<string> EditPageAsync(string pageID, string pageTitle, string pageBody, int pageOrder);
    }
}
