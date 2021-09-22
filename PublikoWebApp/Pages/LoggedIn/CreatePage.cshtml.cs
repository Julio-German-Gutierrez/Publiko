using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublikoSharedLibrary.Models;
using PublikoWebApp.Data;
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

        public CreatePageModel(IStoredPagesService storedPagesService, UserManager<PublikoUser> userManager)
        {
            _storedPagesService = storedPagesService;
            _userManager = userManager;
        }

        public IStoredPagesService _storedPagesService { get; }
        public UserManager<PublikoUser> _userManager { get; }

        public void OnGet(int pages)
        {
            AmountPages = pages;
        }

        public async Task<IActionResult> OnPostAsync([Bind] WebPage newPage)
        {
            //if (ModelState.IsValid)
            if (newPage != null)
            {
                newPage.UserID = _userManager.GetUserId(User);
                var userObject = await _userManager.GetUserAsync(User);

                //WebPage newPage = new WebPage
                //{
                //    PageTitle = this.PageTitle,
                //    PageBody = this.PageBody,
                //    PageOrder = this.PageOrder,
                //    UserID = userID,
                //};

                string result = await _storedPagesService.CreatePageAsync(newPage, userObject);
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
