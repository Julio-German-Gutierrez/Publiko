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
using PublikoWebApp.Data;

namespace PublikoWebApp.Pages.LoggedIn
{
    public class EditPostModel : PageModel
    {
        [BindProperty]
        public WebPost ToEditPost { get; set; }
        public EditPostModel(IStoredPagesService storedPagesService, UserManager<PublikoUser> userManager)
        {
            _storedPagesService = storedPagesService;
            _userManager = userManager;
        }
        public IStoredPagesService _storedPagesService { get; set; }
        public UserManager<PublikoUser> _userManager { get; set; }

        public async Task OnGet(string postId)
        {
            var userObject = await _userManager.GetUserAsync(User);
            ToEditPost = await _storedPagesService.GetPostByIDAsync(postId, userObject);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var userObject = await _userManager.GetUserAsync(User);
                string result = await _storedPagesService.EditPostAsync(ToEditPost, userObject);

                if (result.ToLower().Equals("ok"))
                    return RedirectToPage("MyStart");
            }
            return Page();
        }
    }
}
