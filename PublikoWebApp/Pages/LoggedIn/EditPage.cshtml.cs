using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublikoWebApp.Services;
using PublikoSharedLibrary.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using PublikoWebApp.Data;

namespace PublikoWebApp.Pages.LoggedIn
{
    public class EditPageModel : PageModel
    {
        [BindProperty]
        public WebPage EditPage { get; set; }

        public IStoredPagesService _storedPagesService { get; set; }
        public UserManager<PublikoUser> _userManager { get; set; }
        public EditPageModel(IStoredPagesService storedPagesService, UserManager<PublikoUser> userManager)
        {
            _storedPagesService = storedPagesService;
            _userManager = userManager;
        }

        
        public async Task OnGetAsync(string pageId) //I got the ID from the template on the HtmlPage
        {
            var userObject = await _userManager.GetUserAsync(User);
            EditPage = await _storedPagesService.GetPageByIDAsync(pageId, userObject);
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (ModelState.IsValid)
            {
                //var userObject = await _userManager.GetUserAsync(User);
                string result = await _storedPagesService.EditPageAsync(EditPage);

                if (result.ToLower().Equals("ok"))
                    return RedirectToPage("MyStart");
            }
            return Page();
        }
    }
}
