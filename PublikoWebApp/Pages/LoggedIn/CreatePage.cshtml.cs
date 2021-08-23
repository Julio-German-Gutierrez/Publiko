using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublikoSharedLibrary.Models;
using PublikoWebApp.Services;

namespace PublikoWebApp.Pages.LoggedIn
{
    public class CreatePageModel : PageModel
    {
        [BindProperty]
        public string PageTitle { get; set; }
        [BindProperty]
        public string PageBody { get; set; }
        [BindProperty]
        public int PageOrder { get; set; }
        public int AmountPages { get; set; }

        public CreatePageModel(IStoredPagesService storedPagesService, UserManager<IdentityUser> userManager)
        {
            _storedPagesService = storedPagesService;
            _userManager = userManager;
        }

        public IStoredPagesService _storedPagesService { get; }
        public UserManager<IdentityUser> _userManager { get; }

        public void OnGet(int pages)
        {
            AmountPages = pages;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                string userID = _userManager.GetUserId(User);
                var userObject = await _userManager.GetUserAsync(User);
                string URLPageTitle = System.Web.HttpUtility.UrlEncodeUnicode(PageTitle);
                string URLPageBody = System.Web.HttpUtility.UrlEncodeUnicode(PageBody);

                string result = await _storedPagesService.CreatePageAsync(URLPageTitle, URLPageBody, PageOrder, userID, userObject);
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
