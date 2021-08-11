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
        public MyStartModel(UserManager<IdentityUser> userManager, IStoredPagesService storedPagesService)
        {
            _userManager = userManager;
            _pagesService = storedPagesService;
        }

        public UserManager<IdentityUser> _userManager { get; }
        public IStoredPagesService _pagesService { get; }



        public async Task OnGetAsync()
        {
            Task<string> message = _pagesService
                .GetPagesByAuthorIDAsync(_userManager.GetUserId(User));

            Pages = JsonSerializer.Deserialize< List<WebPage> >(await message, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
