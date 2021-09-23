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
using System.IO;
using PublikoWebApp.Data;

namespace PublikoWebApp.Pages.LoggedIn
{
    [Authorize]
    public class MyStartModel : PageModel
    {
        public WebPage WebPage { get; set; }
        public List<WebPage> Pages { get; set; }
        public List<WebPost> Posts { get; set; }

        //[Bind]
        public string ToDeleteID { get; set; }


        //para contador fallido
        public long ViewsNumber { get; set; }
        class AppCookiesModel
        {
            public long NumberOfViews { get; set; }
        }

        //Constructor
        public MyStartModel(UserManager<PublikoUser> userManager,
                            IStoredPagesService storedPagesService)
        {
            _userManager = userManager;
            _pagesService = storedPagesService;
        }

        public UserManager<PublikoUser> _userManager { get; }
        public IStoredPagesService _pagesService { get; }



        public async Task OnGetAsync(string userName = null)
        {

            var userCalling = await _userManager.GetUserAsync(User);

            string allPages = await _pagesService
                .GetPagesByAuthorIDAsync(userCalling);
            string allPosts = await _pagesService
                .GetPostsByAuthorIDAsync(userCalling);

            if (!allPages.ToLower().Equals("fail") && !allPosts.ToLower().Equals("fail"))
            {
                Pages = JsonSerializer.Deserialize<List<WebPage>>(allPages, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                Posts = JsonSerializer.Deserialize<List<WebPost>>(allPosts, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
    }
}
