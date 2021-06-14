using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublikoAPI.Data;

namespace PublikoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PagesController : ControllerBase
    {
        public PagesController(PublikoPagesDBContext pagesDBContext)
        {
            _pagesDBContext = pagesDBContext;
        }

        public PublikoPagesDBContext _pagesDBContext { get; }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public string GetPages(string pageName)
        {
            var page = _pagesDBContext.Pages.FirstOrDefault(p => p.PageName == pageName);

            //if (page == null)
            //{
            //    return "La página no existe";
            //}

            //return $"<h1>LA PÁGINA NO CONTIENE NADA</h1>";

            if (pageName == "Linnea")
            {
                return $"<h2>Qué perra por favor!</h2>";
            }
            else if (pageName == "Anneli")
            {
                return "Qué chupada de concha que le daría!";
            }
            else
            {
                return "No sabe, no contesta";
            }
        }
    }
}
