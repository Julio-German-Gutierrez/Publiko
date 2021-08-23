using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublikoSharedLibrary.Models;
using PublikoWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace PublikoWebApp.Pages.LoggedIn
{
    public class EditPostModel : PageModel
    {
        [BindProperty]
        public WebPost ToEditPost { get; set; }
        public EditPostModel(IStoredPagesService storedPagesService, UserManager<IdentityUser> userManager)
        {
            _storedPagesService = storedPagesService;
            _userManager = userManager;
        }
        public IStoredPagesService _storedPagesService { get; set; }
        public UserManager<IdentityUser> _userManager { get; set; }

        public async Task OnGet(string id)
        {
            var userObject = await _userManager.GetUserAsync(User);
            string message = await _storedPagesService.GetPostByIDAsync(id, userObject);
            ToEditPost = JsonSerializer.Deserialize<WebPost>(message, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var userObject = await _userManager.GetUserAsync(User);
                string result = await _storedPagesService.EditPostAsync(ToEditPost.PostID, ToEditPost.PostTitle, ToEditPost.PostContent, userObject);

                if (result == "Post Updated")
                {
                    return RedirectToPage("MyStart");
                }
                else
                {
                    return Page();
                }
            }
            return Page();
        }
    }
}
