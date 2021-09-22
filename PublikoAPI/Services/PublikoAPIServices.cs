using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PublikoAPI.Controllers;
using PublikoAPI.Data;
using PublikoSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublikoAPI.Services
{
    public interface IPublikoAPIServices
    {
        Task<WebPage> GetPageByIdAsync(string pageID);
        Task<WebPost> GetPostByIdAsync(string postID);
        List<WebPage> GetPagesByAuthor(string authorID);
        List<WebPost> GetPostsByAuthor(string authorID);
        Task<string> CreatePage(WebPageIncomming newPage);
        Task<string> CreatePost(WebPostIncomming webPostIncomming);
        Task<string> EditPageAsync(WebPageEditIncomming editInc);
        Task<string> EditPostAsync(WebPostEditIncomming editInc);
        Task<string> DeletePageAsync(string pageId);
        Task<string> DeletePostAsync(string postId);
    }
    public class PublikoAPIServices : IPublikoAPIServices
    {
        public ILogger<PublikoAPIServices> _logger { get; }
        public PublikoPagesDBContext _publikoPagesDB { get; }
        public IGlobalIDServices _globalServices { get; }

        public PublikoAPIServices(ILogger<PublikoAPIServices> logger,
                                  PublikoPagesDBContext publikoPagesDB,
                                  IGlobalIDServices globalServices)
        {
            _logger = logger;
            _publikoPagesDB = publikoPagesDB;
            _globalServices = globalServices;
        }


        public async Task<WebPage> GetPageByIdAsync(string pageID)
        {
            return await _publikoPagesDB.Pages.FindAsync(pageID);
        }


        public async Task<WebPost> GetPostByIdAsync(string postID)
        {
            return await _publikoPagesDB.Posts.FindAsync(postID);
        }


        public List<WebPage> GetPagesByAuthor(string authorID)
        {
            return _publikoPagesDB.Pages
                .Where(p => p.UserID == authorID)
                .OrderBy(p => p.PageDateCreated)
                .ToList();
        }

        public List<WebPost> GetPostsByAuthor(string authorID)
        {
            return _publikoPagesDB.Posts
                .Where(p => p.UserID == authorID)
                .OrderBy(p => p.PostDateCreated)
                .ToList();
        }


        public async Task<string> CreatePage(WebPageIncomming newIncomming)
        {
            await _publikoPagesDB.Pages.AddAsync(ConvertToWebPage(newIncomming));
            int result = await _publikoPagesDB.SaveChangesAsync();

            if (result > 0)
                return "ok";
            else
                return "fail";
        }

        public WebPage ConvertToWebPage(WebPageIncomming webIn)
        {
            DateTime now = DateTime.Now;
            return new WebPage
            {
                PageID = _globalServices.GuidFromString(_globalServices.GetSeed()),
                PageTitle = webIn.PageTitle,
                PageBody = webIn.PageBody,
                PageOrder = webIn.PageOrder,
                UserID = webIn.PageUserID,
                PageDateCreated = now,
                PageDateUpdated = now
            };
        }

        public async Task<string> CreatePost(WebPostIncomming webPostIncomming)
        {
            await _publikoPagesDB.Posts.AddAsync(ConvertToWebPost(webPostIncomming));
            int result = await _publikoPagesDB.SaveChangesAsync();

            if (result > 0)
                return "ok";
            else
                return "fail";
        }

        private WebPost ConvertToWebPost(WebPostIncomming webIn)
        {
            DateTime now = DateTime.Now;
            return new WebPost
            {
                PostID = _globalServices.GuidFromString(_globalServices.GetSeed()),
                PostTitle = webIn.PostTitle,
                PostContent = webIn.PostContent,
                UserID = webIn.PostUserID,
                PostDateCreated = now,
                PostDateModified = now
            };
        }

        public async Task<string> EditPageAsync(WebPageEditIncomming editInc)
        {
            WebPage webPage = await _publikoPagesDB.Pages
                .FindAsync(editInc.PageId);

            if (webPage.UserID.Equals(editInc.PageUserID))
            {
                webPage.PageTitle = editInc.PageTitle;
                webPage.PageBody = editInc.PageBody;
                webPage.PageOrder = editInc.PageOrder;
                webPage.PageDateUpdated = DateTime.Now;

                _publikoPagesDB.Pages.Update(webPage);
                int result = await _publikoPagesDB.SaveChangesAsync();

                if (result > 0) return "ok";
            }
            
            return "fail";
        }

        public async Task<string> EditPostAsync(WebPostEditIncomming editInc)
        {
            WebPost webPost = await _publikoPagesDB.Posts
                .FindAsync(editInc.PostId);

            if (webPost.UserID.Equals(editInc.PostUserID))
            {
                webPost.PostTitle = editInc.PostTitle;
                webPost.PostContent = editInc.PostContent;
                webPost.PostDateModified = DateTime.Now;

                _publikoPagesDB.Posts.Update(webPost);
                int result = await _publikoPagesDB.SaveChangesAsync();

                if (result > 0) return "ok";
            }
            
            return "fail";
        }

        public async Task<string> DeletePageAsync(string pageId)
        {
            var record = await _publikoPagesDB.Pages.FindAsync(pageId);

            if (record != null)
            {
                _publikoPagesDB.Pages.Remove(record);
                int result = await _publikoPagesDB.SaveChangesAsync();

                if (result > 0) return "ok";
            }

            return "fail";
        }

        public async Task<string> DeletePostAsync(string postId)
        {
            var record = await _publikoPagesDB.Posts.FindAsync(postId);

            if (record != null)
            {
                _publikoPagesDB.Posts.Remove(record);
                int result = await _publikoPagesDB.SaveChangesAsync();

                if (result > 0) return "ok";
            }

            return "fail";
        }
    }
}
