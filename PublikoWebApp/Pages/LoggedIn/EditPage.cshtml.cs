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

        
        public async Task OnGetAsync(string id) //I got the ID from the template on the HtmlPage
        {
            var userObject = await _userManager.GetUserAsync(User);
            string message = await _storedPagesService.GetPageByIDAsync(id, userObject);
            EditPage = JsonSerializer.Deserialize<WebPage>(message, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (ModelState.IsValid)
            {
                var userObject = await _userManager.GetUserAsync(User);
                string result = await _storedPagesService.EditPageAsync(EditPage.PageID, EditPage.PageTitle, EditPage.PageBody, EditPage.PageOrder, userObject);

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
