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
        [HttpGet("~/api/author/{authorID}/pages")]
        public ActionResult GetPagesbyAuthor(string authorID) //ContentResult
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("userId")).Value;
            var userName = User.Claims.FirstOrDefault(c => c.Type.Equals("userName")).Value;
            var userEmail = User.Claims.FirstOrDefault(c => c.Type.Equals("userEmail")).Value;
            var userRole = User.Claims.FirstOrDefault(c => c.Type.Equals("userRole")).Value;
            
            //var dato = User.Claims.FirstOrDefault(c => c.Type == "email").Value;


            List<WebPage> pages = _publikoAPIServices.GetPagesByAuthor(authorID);
            if (pages.Count > 0)
                return Ok(pages);
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



        [HttpDelete("~/api/deletepage/{id}")]
        public async Task<ActionResult> DeletePageByID(string id = null)
        {
            string result = await _publikoAPIServices.DeletePageAsync(id);

            if (result.ToLower().Equals("ok"))
                return Ok("Page Deleted");
            else
                return NotFound();
        }
    }
}
