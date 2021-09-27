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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;

namespace PublikoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PagesController : ControllerBase
    {
        public ILogger _logger { get; }
        public PublikoPagesDBContext _pagesDBContext { get; }
        public IGlobalIDServices _globalServices { get; }
        public IPublikoAPIServices _publikoAPIServices { get; }

        public PagesController(ILogger<PagesController> logger,
                               PublikoPagesDBContext pagesDBContext,
                               IGlobalIDServices globalServices,
                               IPublikoAPIServices publikoAPIServices)
        {
            _logger = logger;
            _pagesDBContext = pagesDBContext;
            _globalServices = globalServices;
            _publikoAPIServices = publikoAPIServices;
        }







        // OK!
        [HttpGet("~/api/page/{pageID}")]
        public async Task<ActionResult> GetPageByID(string pageID) //ContentResult
        {
            WebPage page = await _publikoAPIServices.GetPageByIdAsync(pageID);

            if (page != null)
                return Ok(page);
            else
                return BadRequest("Web page that not exist or there is a problem with the server.");
        }



        // OK!
        [HttpGet("~/api/post/{postID}")]
        public async Task<ActionResult> GetPostByID(string postID) //ContentResult
        {
            WebPost post = await _publikoAPIServices.GetPostByIdAsync(postID);

            if (post != null)
                return Ok(post);
            else
                return BadRequest("Web post that not exist or there is a problem with the server.");
        }



        // OK!
        [HttpGet("~/api/author/{authorID}/pages")]
        public ActionResult GetPagesbyAuthor(string authorID) //ContentResult
        {
            List<WebPage> pages = _publikoAPIServices.GetPagesByAuthor(authorID);
            if (pages.Count > 0)
                return Ok(pages);
            else
                return BadRequest("There was an error processing your request.");
        }



        // OK!
        [HttpGet("~/api/author/{authorID}/posts")]
        public ActionResult GetPostsbyAuthor(string authorID) //ContentResult
        {
            List<WebPost> posts = _publikoAPIServices.GetPostsByAuthor(authorID);
            if (posts.Count > 0)
                return Ok(posts);
            else
                return BadRequest("There was an error processing your request.");
        }




        // OK!
        [HttpPost("~/api/page/create")]
        public async Task<ActionResult> CreatePage(WebPageIncomming webPageIncomming)
        {
            if (ModelState.IsValid)
            {
                var result = await _publikoAPIServices.CreatePage(webPageIncomming);

                if (result.ToLower().Equals("ok"))
                    return Ok("Page Created");
            }
            
            return BadRequest();
        }



        // OK!
        [HttpPost("~/api/post/create")]
        public async Task<ActionResult> CreatePost(WebPostIncomming webPostIncomming) // user id: 605ad860-4a7c-4a63-821e-09f0af97476e
        {
            if (ModelState.IsValid)
            {
                var result = await _publikoAPIServices.CreatePost(webPostIncomming);

                if (result.ToLower().Equals("ok"))
                    return Ok("Post Created");
            }

            return BadRequest();
        }



        // OK!
        [HttpPut("~/api/editpage")]
        public async Task<ActionResult> EditPageAsync(WebPageEditIncomming editInc)
        {
            if (ModelState.IsValid)
            {
                var result = await _publikoAPIServices.EditPageAsync(editInc);

                if (result.ToLower().Equals("ok"))
                    return Ok("Page Updated");
            }

            return BadRequest();
        }



        // OK!
        [HttpPut("~/api/editpost")]
        public async Task<ActionResult> EditPostAsync(WebPostEditIncomming editInc)
        {
            if (ModelState.IsValid)
            {
                var result = await _publikoAPIServices.EditPostAsync(editInc);

                if (result.ToLower().Equals("ok"))
                    return Ok("Post Updated");
            }

            return BadRequest();
        }



        [HttpDelete("~/api/deletepage/{id}")]
        public async Task<ActionResult> DeletePageByID(string id = null)
        {
            string result = await _publikoAPIServices.DeletePageAsync(id);

            if (result.ToLower().Equals("ok"))
                return Ok("Page Deleted");
            else
                return NotFound();
        }



        [HttpDelete("~/api/deletepost/{id}")]
        public async Task<ActionResult> DeletePostByID(string id = null)
        {
            string result = await _publikoAPIServices.DeletePostAsync(id);

            if (result.ToLower().Equals("ok"))
                return Ok("Post Deleted");
            else
                return NotFound();
        }
    }
}
