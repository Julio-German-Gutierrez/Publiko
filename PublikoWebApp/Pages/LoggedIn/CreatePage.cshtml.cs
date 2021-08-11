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
        public WebPage NewPage { get; set; }
        public CreatePageModel(IStoredPagesService storedPagesService, UserManager<IdentityUser> userManager)
        {
            _storedPagesService = storedPagesService;
            _userManager = userManager;
        }

        public IStoredPagesService _storedPagesService { get; }
        public UserManager<IdentityUser> _userManager { get; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Task<string> result = _storedPagesService.CreatePageAsync(NewPage);

                if (await result == "ok")
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
