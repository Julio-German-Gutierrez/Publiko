using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublikoWebApp.Data;

namespace PublikoWebApp.Pages.Admin
{
    [Authorize("AdministratorAccess")]
    public class IndexAdminModel : PageModel
    {
        public List<List<string>> listOfUsers = new List<List<string>>();
        [BindProperty]
        public SelectedUser selUser { get; set; } = new SelectedUser();
        
        public IndexAdminModel(UserManager<PublikoUser> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public RoleManager<IdentityRole> _roleManager { get; set; }
        public UserManager<PublikoUser> _userManager { get; }

        public class SelectedUser
        {
            public string Userid { get; set; }
            public string UserName { get; set; }
            public string UserEmail { get; set; }
            public string UserPhone { get; set; }
            public string UserAccess { get; set; }
            public bool UserLock { get; set; }
            public bool UserPassReset { get; set; }
            public string UserWebSite { get; set; }
        }


        public async Task OnGet()
        {
            await SetPageAsync();
        }

        public async Task SetPageAsync()
        {
            var lUsers = _userManager.Users
                .OrderBy(u => u.UserName)
                .Select(u => new[] { u.UserName, u.Email, u.PhoneNumber, u.Id, (u.LockoutEnd != null) ? "true" : "false", u.PasswordHash, u.WebSiteName } )
                .ToList();

            var lRoles = await _userManager.GetUsersInRoleAsync("User");

            foreach (var item in lUsers)
            {
                List<string> u = new List<string>();
                u.Add(item[0]);//name
                u.Add(item[1]);//email
                u.Add(item[2]);//phone
                u.Add(item[3]);//id
                u.Add(item[4]);//lockdate

                //item 5
                foreach (var e in lRoles)
                {
                    if (item[3] == e.Id)
                    {
                        u.Add("User");//access (item 5)
                    }
                    else
                    {
                        u.Add("Admin");//access (item 5)
                    }
                }

                var userSelected = _userManager.Users.FirstOrDefault(i => i.Id == selUser.Userid);
                bool passTemporal = await _userManager.CheckPasswordAsync(userSelected, "temporal");
                if (passTemporal)
                {
                    u.Add("true");//passwordReset?
                }
                else
                {
                    u.Add("false");
                }

                u.Add(item[6]);//websitename


                listOfUsers.Add(u);
            }
        }

        //userLock returns "on"/null
        public async Task<IActionResult> OnPost(string userName, string userEmail, string userPhone, string userID, string userLock, string userAccess, string userPassReset, string userWebSite)
        {
            selUser.UserName = userName;
            selUser.UserEmail = userEmail;
            selUser.UserPhone = userPhone;
            selUser.Userid = userID;
            if (userLock != null)
            {
                selUser.UserLock = true;
            }
            else
            {
                selUser.UserLock = false;
            }
            selUser.UserAccess = userAccess;
            if (userPassReset != null)
            {
                selUser.UserPassReset = true;
            }
            else
            {
                selUser.UserPassReset = false;
            }
            selUser.UserWebSite = userWebSite;


            var userSelected = _userManager.Users.FirstOrDefault(i => i.Id == selUser.Userid);
            
            //Lock Account
            if (selUser.UserLock)
            {
                //await _userManager.SetLockoutEndDateAsync(userSelected, DateTime.Now.AddDays(1));
                userSelected.LockoutEnd = DateTime.Now.AddDays(1);
            }
            else
            {
                //await _userManager.SetLockoutEndDateAsync(userSelected, null);
                userSelected.LockoutEnd = null;
            }

            userSelected.Email = selUser.UserEmail;
            userSelected.PhoneNumber = selUser.UserPhone;
            userSelected.UserName = selUser.UserName;
            userSelected.WebSiteName = selUser.UserWebSite;

            //Pass Reset
            if (selUser.UserPassReset)
            {
                await _userManager.RemovePasswordAsync(userSelected);
                await _userManager.AddPasswordAsync(userSelected, "temporal");
                selUser.UserPassReset = false;
            }

            

            //await _userManager.SetEmailAsync(userSelected, selUser.UserEmail);
            //await _userManager.SetPhoneNumberAsync(userSelected, selUser.UserPhone);
            //await _userManager.SetUserNameAsync(userSelected, selUser.UserName);

            await _userManager.UpdateAsync(userSelected);
            //_userManager.

            await SetPageAsync();

            return Page();
        }
    }
}
