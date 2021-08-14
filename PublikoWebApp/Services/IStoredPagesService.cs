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
        List<WebPage> GetAllPagesAsync();

        Task<string> CreatePageAsync(string pageTitle, string pageBody, int? pageOrder, string userID);
    }
}
