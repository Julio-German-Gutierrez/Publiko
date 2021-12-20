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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        public ILogger _logger { get; }
        public PublikoPagesDBContext _pagesDBContext { get; }
        public IGlobalIDServices _globalServices { get; }
        public IPublikoAPIServices _publikoAPIServices { get; }

        public PostsController(ILogger<PagesController> logger,
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
        [HttpGet("{postID}")]
        public async Task<ActionResult> GetPostByID(string postID) //ContentResult
        {
            WebPost post = await _publikoAPIServices.GetPostByIdAsync(postID);

            if (post != null)
                return Ok(post);
            else
                return BadRequest("Web post that not exist or there is a problem with the server.");
        }



        // OK!
        [HttpGet]
        public ActionResult GetPostsbyAuthor() //ContentResult
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("userId")).Value;

            List<WebPost> posts = _publikoAPIServices.GetPostsByAuthor(userId);
            if (posts.Count > 0)
                return Ok(posts);
            else
                return BadRequest("There was an error processing your request.");
        }




        // OK!
        [HttpPost]
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
        [HttpPut]
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



        [HttpDelete("{postId}")]
        public async Task<ActionResult> DeletePostByID(string postId = null)
        {
            string result = await _publikoAPIServices.DeletePostAsync(postId);

            if (result.ToLower().Equals("ok"))
                return Ok("Post Deleted");
            else
                return NotFound();
        }
    }
}
