using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublikoWebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using PublikoWebApp.Data;
using PublikoSharedLibrary.Models;

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

        public CreatePostModel(IStoredPagesService storedPagesService, UserManager<PublikoUser> userManager)
        {
            _storedPagesService = storedPagesService;
            _userManager = userManager;
        }

        public IStoredPagesService _storedPagesService { get; }
        public UserManager<PublikoUser> _userManager { get; }


        //Trabajar sobre el metodo post
        public async Task<IActionResult> OnPostAsync([Bind] WebPost newPost)
        {
            newPost.UserID = _userManager.GetUserId(User);

            if (newPost != null)
            {
                string result = await _storedPagesService.CreatePostAsync(newPost);

                if (result == "ok") return RedirectToPage("MyStart");
            }
            return Page();
        }
    }
}
