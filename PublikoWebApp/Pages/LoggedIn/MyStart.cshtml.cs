using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublikoWebApp.Services;
using PublikoSharedLibrary.Models;

using System.Diagnostics;

namespace PublikoWebApp.Pages.LoggedIn
{
    [Authorize]
    public class MyStartModel : PageModel
    {
        public WebPage WebPage { get; set; }
        public List<WebPage> Pages { get; set; }
        public List<WebPost> Posts { get; set; }
        public MyStartModel(UserManager<IdentityUser> userManager, IStoredPagesService storedPagesService)
        {
            _userManager = userManager;
            _pagesService = storedPagesService;
        }

        public UserManager<IdentityUser> _userManager { get; }
        public IStoredPagesService _pagesService { get; }



        public async Task OnGetAsync()
        {
            string message = await _pagesService
                .GetPagesByAuthorIDAsync(_userManager.GetUserId(User));
            string allPosts = await _pagesService
                .GetPostsByAuthorIDAsync(_userManager.GetUserId(User));

            Pages = JsonSerializer.Deserialize< List<WebPage> >(message, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Posts = JsonSerializer.Deserialize< List<WebPost> >(allPosts, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
