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

namespace PublikoWebApp.Pages.LoggedIn
{
    public class EditPageModel : PageModel
    {
        [BindProperty]
        public WebPage EditPage { get; set; }

        public IStoredPagesService _storedPagesService { get; set; }
        public UserManager<IdentityUser> _userManager { get; set; }
        public EditPageModel(IStoredPagesService storedPagesService, UserManager<IdentityUser> userManager)
        {
            _storedPagesService = storedPagesService;
            _userManager = userManager;
        }

        
        public async Task OnGetAsync(string id) //I got the ID from the template on the HtmlPage
        {
            string message = await _storedPagesService.GetPageByIDAsync(id);
            EditPage = JsonSerializer.Deserialize<WebPage>(message, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (ModelState.IsValid)
            {
                string result = await _storedPagesService.EditPageAsync(EditPage.PageID, EditPage.PageTitle, EditPage.PageBody, EditPage.PageOrder);

                if (result == "Page Updated")
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
