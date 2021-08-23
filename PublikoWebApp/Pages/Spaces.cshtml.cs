using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublikoSharedLibrary.Models;
using PublikoWebApp.Services;

namespace PublikoWebApp.Pages
{
    public class SpacesModel : PageModel
    {
        public List<WebPage> Pages { get; set; }
        public List<WebPost> Posts { get; set; }

        //Constructor
        public SpacesModel(UserManager<IdentityUser> userManager, IStoredPagesService storedPagesService)
        {
            _userManager = userManager;
            _pagesService = storedPagesService;
        }

        public UserManager<IdentityUser> _userManager { get; }
        public IStoredPagesService _pagesService { get; }



        public async Task OnGetAsync(string websiteName = null)
        {
            var userObject = await _userManager.FindByNameAsync(websiteName);

            string message = await _pagesService
                .GetPagesByAuthorIDAsync(userObject.Id, userObject);
            string allPosts = await _pagesService
                .GetPostsByAuthorIDAsync(userObject.Id, userObject);

            Pages = JsonSerializer.Deserialize<List<WebPage>>(message, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Posts = JsonSerializer.Deserialize<List<WebPost>>(allPosts, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
