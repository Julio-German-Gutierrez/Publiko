using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublikoAPI.Data;
using PublikoSharedLibrary.Models;
using PublikoAPI.Services;

namespace PublikoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagesController : ControllerBase
    {
        public PagesController(PublikoPagesDBContext pagesDBContext,
                               IGlobalIDServices globalServices)
        {
            _pagesDBContext = pagesDBContext;
            _globalServices = globalServices;
        }

        public PublikoPagesDBContext _pagesDBContext { get; }
        public IGlobalIDServices _globalServices { get; }


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

        //Page by ID             ----------->>>>>>>>>>>>>>>>>>>>>>>> ID = 0d6fe52d-643d-5b37-e579-3b61caeb386e  OR  f6ccea1a-003a-5a3f-49ba-3014e0678c3c
        [HttpGet("~/api/page/{pageID}")]
        [Produces("application/json")]
        public async Task<WebPage> GetPageByID(string pageID) //ContentResult
        {
            var page = _pagesDBContext.Pages.FindAsync(pageID);// FirstOrDefault(i => pageID == i.PageID);

            return await page;
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


        [HttpPost("Create/page/title/{pageTitle}/body/{pageBody}/user/{userID}")]
        //[ValidateAntiForgeryToken]
        public async Task<string> CreatePage(string pageTitle, string pageBody, string userID) //[Bind("pageName,pageHead,pageBody,userID")]
        {
            if (ModelState.IsValid)
            {
                WebPage newPage = new WebPage()
                {
                    PageID = _globalServices.GuidFromString(_globalServices.GetSeed()),
                    PageTitle = pageTitle,
                    PageBody = pageBody,
                    UserID = userID
                };

                try
                {
                    await _pagesDBContext.Pages.AddAsync(newPage);
                    await _pagesDBContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    string message = "ERROR: Could not save to the database.\n";
                    return message + e.Message;
                }

                return "Page saved";
            }

            return "ERROR: Model invalid";
        }

        [HttpPost("Create/post/title/{postTitle}/content/{postContent}/user/{userID}")]
        //[ValidateAntiForgeryToken]
        public async Task<string> CreatePost(string postTitle, string postContent, string userID) //[Bind("pageName,pageHead,pageBody,userID")]
        {
            if (ModelState.IsValid)
            {
                WebPost newPost = new WebPost()
                {
                    PostID = _globalServices.GuidFromString(_globalServices.GetSeed()),
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
                    string message = "ERROR: Could not save to the database.\n";
                    return message + e.Message;
                }

                return "Post saved";
            }

            return "ERROR: Model invalid";
        }
    }
}
