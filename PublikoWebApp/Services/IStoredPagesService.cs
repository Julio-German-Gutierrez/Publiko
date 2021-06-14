using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PublikoWebApp.Services
{
    public interface IStoredPagesService
    {
        public Task<string> GetPage(string nameOfPage, string userName);
    }
}
