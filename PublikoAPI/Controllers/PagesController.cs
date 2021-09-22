using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublikoAPI.Data;
using PublikoSharedLibrary.Models;
using PublikoAPI.Services;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace PublikoAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    
    public class PagesController : ControllerBase
    {
        public ILogger _logger { get; }
        public PublikoPagesDBContext _pagesDBContext { get; }
        public IGlobalIDServices _globalServices { get; }
        public PagesController(ILogger<PagesController> logger,
                               PublikoPagesDBContext pagesDBContext,
                               IGlobalIDServices globalServices)
        {
            _logger = logger;
            _pagesDBContext = pagesDBContext;
            _globalServices = globalServices;
        }



        //All pages
        [HttpGet("allpages")]
        [Produces("application/json")]
        public IEnumerable<WebPage> GetPages() //ContentResult
        {
            var pages = _pagesDBContext.Pages.AsEnumerable();

            return pages;
        }

        //All posts
        [HttpGet("allposts")]
        [Produces("application/json")]
        public IEnumerable<WebPost> GetPosts() //ContentResult
        {
            var posts = _pagesDBContext.Posts.AsEnumerable();

            return posts;
        }

        //Page by ID             ----------->>>>>>>>>>>>>>>>>>>>>>>> Elber: 605ad860-4a7c-4a63-821e-09f0af97476e
        [HttpGet("~/api/page/{pageID}")]
        [Produces("application/json")]
        public async Task<WebPage> GetPageByID(string pageID) //ContentResult
        {
            WebPage page = await _pagesDBContext.Pages.FindAsync(pageID);// FirstOrDefault(i => pageID == i.PageID);


            return page;
        }

        //Post by ID             ----------->>>>>>>>>>>>>>>>>>>>>>>> ID = 0d6fe52d-643d-5b37-e579-3b61caeb386e  OR  f6ccea1a-003a-5a3f-49ba-3014e0678c3c
        [HttpGet("~/api/post/{postID}")]
        [Produces("application/json")]
        public async Task<WebPost> GetPostByID(string postID) //ContentResult
        {
            var post = _pagesDBContext.Posts.FindAsync(postID);// FirstOrDefault(i => pageID == i.PageID);

            return await post;
        }


        //All pages by author    ----------->>>>>>>>>>>>>>>>>>>>>>>> ID = 605ad860-4a7c-4a63-821e-09f0af97476e
        [HttpGet("~/api/author/{authorID}/pages")]
        [Produces("application/json")]
        public IEnumerable<WebPage> GetPagesbyAuthor(string authorID) //ContentResult
        {
            var pages = _pagesDBContext.Pages.Where(i => authorID == i.UserID);

            return pages;
        }

        //All posts by author    ----------->>>>>>>>>>>>>>>>>>>>>>>> ID = 605ad860-4a7c-4a63-821e-09f0af97476e
        [HttpGet("~/api/author/{authorID}/posts")]
        [Produces("application/json")]
        public IEnumerable<WebPost> GetPostsbyAuthor(string authorID) //ContentResult
        {
            var posts = _pagesDBContext.Posts.Where(i => authorID == i.UserID);

            return posts;
        }


        [HttpPost("Create")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePage(WebPage newPage) //[Bind("pageName,pageHead,pageBody,userID")]
        {
            _logger.LogInformation("Inside Publiko API");

            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;

                newPage.PageID = _globalServices.GuidFromString(_globalServices.GetSeed());
                newPage.PageDateCreated = now;
                newPage.PageDateUpdated = now;


                //WebPage newPage = new WebPage()
                //{
                //    PageID = _globalServices.GuidFromString(_globalServices.GetSeed()),
                //    PageDateCreated = now,
                //    PageDateUpdated = now,
                //    PageOrder = pageOrder,
                //    PageTitle = pageTitle,
                //    PageBody = pageBody,
                //    UserID = userID
                //};

                try
                {
                    await _pagesDBContext.Pages.AddAsync(newPage);
                    await _pagesDBContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    string message = "ERROR: PagesController->CreatePage()->TryCatch .\n";
                    return Problem(message);
                }

                return Ok("Page saved");
            }

            return Problem("ERROR: Model invalid : PagesController->CreatePage()->if(ModelState.IsValid)");
        }

        [HttpPost("~/api/post/create/title/{URLPostTitle}/content/{URLPostContent}/user/{userID}")]
        //[ValidateAntiForgeryToken]
        public async Task<string> CreatePost(string URLPostTitle, string URLPostContent, string userID) // user id: 605ad860-4a7c-4a63-821e-09f0af97476e
        {
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;
                string postTitle = System.Web.HttpUtility.UrlDecode(URLPostTitle);
                string postContent = System.Web.HttpUtility.UrlDecode(URLPostContent);

                WebPost newPost = new WebPost()
                {
                    PostID = _globalServices.GuidFromString(_globalServices.GetSeed()),
                    PostDateCreated = now,
                    PostDateModified = now,
                    PostTitle = postTitle,
                    PostContent = postContent,
                    UserID = userID
                };

                try
                {
                    await _pagesDBContext.Posts.AddAsync(newPost);
                    await _pagesDBContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    string message = "ERROR: PagesController->CreatePost()->TryCatch .\n";
                    return message + e.Message;
                }

                return "Post saved";
            }

            return "ERROR: Model invalid : PagesController->CreatePost()->if(ModelState.IsValid)";
        }

        [HttpPut("~/api/edit/{pageID}/title/{URLPageTitle}/body/{URLPageBody}/order/{pageOrder}")]
        public async Task<string> EditPageAsync(string pageID, string URLPageTitle, string URLPageBody, int pageOrder)
        {
            WebPage webPage = await _pagesDBContext.Pages.FindAsync(pageID);

            if (webPage != null)
            {
                webPage.PageTitle = System.Web.HttpUtility.UrlDecode(URLPageTitle);
                webPage.PageBody = System.Web.HttpUtility.UrlDecode(URLPageBody);
                webPage.PageOrder = pageOrder;
                webPage.PageDateUpdated = DateTime.Now;

                _pagesDBContext.Pages.Update(webPage);
                await _pagesDBContext.SaveChangesAsync();

                return "Entry Updated";
            }

            return "Error: Could not find the entry : PagesController->EditPageAsync()->if(webPage != null)";
        }

        [HttpPut("~/api/postedit/{postID}/title/{URLPostTitle}/body/{URLPostContent}")]
        public async Task<string> EditPostAsync(string postID, string URLPostTitle, string URLPostContent)
        {
            WebPost toEditPost = await _pagesDBContext.Posts.FindAsync(postID);

            if (toEditPost != null)
            {
                toEditPost.PostTitle = System.Web.HttpUtility.UrlDecode(URLPostTitle);
                toEditPost.PostContent = System.Web.HttpUtility.UrlDecode(URLPostContent);
                toEditPost.PostDateModified = DateTime.Now;

                _pagesDBContext.Posts.Update(toEditPost);
                await _pagesDBContext.SaveChangesAsync();

                return "Entry Updated";
            }

            return "Error: Could not find the entry : PagesController->EditPostAsync()->if(toEditPost != null)";
        }



        //97437952-9e2f-8397-415b-c97cc00d7dc2
        //These last two methods are called not from c# but from Javascript (on the fly)
        //Still not protected by token. I will need to implement a Server service to deliver the tokens.
        [HttpGet("~/api/deletepage/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeletePageByID(string id = null)
        {
            if (id != null)
            {
                var toRemove = await _pagesDBContext.Pages.FindAsync(id);
                var response = _pagesDBContext.Pages.Remove(toRemove);
                await _pagesDBContext.SaveChangesAsync();

                return Ok();
            }
            //return "Error: PagesController->DeletePageByID()->if( id! = null )";
            //return new HttpResponseMessage(HttpStatusCode.NotFound);
            return NotFound();
        }

        [HttpGet("~/api/deletepost/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeletePostByID(string id = null)
        {
            if (id != null)
            {
                var toRemove = await _pagesDBContext.Posts.FindAsync(id);
                var response = _pagesDBContext.Posts.Remove(toRemove);
                await _pagesDBContext.SaveChangesAsync();

                return Ok();
            }
            //return "Error: PagesController->DeletePageByID()->if( id! = null )";
            //return new HttpResponseMessage(HttpStatusCode.NotFound);
            return NotFound();
        }
    }
}
