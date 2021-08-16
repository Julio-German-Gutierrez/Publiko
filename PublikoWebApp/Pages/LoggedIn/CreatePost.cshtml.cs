using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublikoWebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

/*
public string PostID { get; set; }
public DateTime PostDateCreated { get; set; }
public DateTime PostDateModified { get; set; }
public string PostTitle { get; set; }
public string PostContent { get; set; }
public string UserID { get; set; }
*/


namespace PublikoWebApp.Pages.LoggedIn
{
    [Authorize]
    public class CreatePostModel : PageModel
    {
        [BindProperty]
        public string PostTitle { get; set; }
        [BindProperty]
        public string PostContent { get; set; }

        public CreatePostModel(IStoredPagesService storedPagesService, UserManager<IdentityUser> userManager)
        {
            _storedPagesService = storedPagesService;
            _userManager = userManager;
        }

        public IStoredPagesService _storedPagesService { get; }
        public UserManager<IdentityUser> _userManager { get; }

        public void OnGet()
        {
        }

        //Trabajar sobre el metodo post
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                string userID = _userManager.GetUserId(User);
                string URLPostTitle = System.Web.HttpUtility.UrlEncodeUnicode(PostTitle);
                string URLPostContent = System.Web.HttpUtility.UrlEncodeUnicode(PostContent);

                string result = await _storedPagesService.CreatePostAsync(URLPostTitle, URLPostContent, userID);
                if (result == "ok")
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
