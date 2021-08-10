using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublikoAPI.Services
{
    public interface IGlobalIDServices
    {
        public string GuidFromString(string input);

        public string GetSeed();
    }
}
