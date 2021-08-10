using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublikoWebApp.Services;
using PublikoSharedLibrary.Models;

using System.Diagnostics;

namespace PublikoWebApp.Pages.LoggedIn
{
    public class MyStartModel : PageModel
    {
        public WebPage WebPage { get; set; }
        public List<WebPage> Pages { get; set; }
        public MyStartModel(UserManager<IdentityUser> _userManager, IStoredPagesService storedPagesService)
        {
            UserManager = _userManager;
            _pagesService = storedPagesService;

            Trace.WriteLine("Start C# Model");
        }

        public UserManager<IdentityUser> UserManager { get; }
        public IStoredPagesService _pagesService { get; }

        public async Task OnGetAsync()
        {
            //await GetPageAsync();

            Task<string> message = _pagesService.GetPagesByAuthorIDAsync("user01"); //UserManager.GetUserId(User)

            Pages = JsonSerializer.Deserialize<List<WebPage>>(await message, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        private async Task GetPageAsync()
        {
            Task<string> message = _pagesService.GetPagesByAuthorIDAsync("user01"); //UserManager.GetUserId(User)

            var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Pages = System.Text.Json.JsonSerializer.Deserialize< List<WebPage> >(await message, options);
        }
    }
}
