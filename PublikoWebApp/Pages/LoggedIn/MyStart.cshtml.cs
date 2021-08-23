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
        public MyStartModel(UserManager<IdentityUser> userManager, IStoredPagesService storedPagesService)
        {
            _userManager = userManager;
            _pagesService = storedPagesService;
        }

        public UserManager<IdentityUser> _userManager { get; }
        public IStoredPagesService _pagesService { get; }



        public async Task OnGetAsync(string userName = null)
        {
            //Contador se ejecuta 2 veces por alguna razon que de momento desconozco
            string myCookies = System.IO.File.ReadAllText("appdata/appcookies.json");
            var opt = new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            AppCookiesModel appData = System.Text.Json.JsonSerializer.Deserialize<AppCookiesModel>(myCookies, opt);
            ViewsNumber = ++appData.NumberOfViews;
            string appDataJSON = System.Text.Json.JsonSerializer.Serialize(appData);
            System.IO.File.WriteAllText("appdata/appcookies.json", appDataJSON);

            var userCalling = await _userManager.GetUserAsync(User);

            string message = await _pagesService
                .GetPagesByAuthorIDAsync(_userManager.GetUserId(User), userCalling);
            string allPosts = await _pagesService
                .GetPostsByAuthorIDAsync(_userManager.GetUserId(User), userCalling);

            Pages = JsonSerializer.Deserialize<List<WebPage>>(message, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Posts = JsonSerializer.Deserialize<List<WebPost>>(allPosts, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public void OnPost(string id)
        {

        }
    }
}
