/*
 * By Julio
 * 
 * Global Unique Identifier methods to create personal ID keys.
 * The idea is not allow the Batabase to create IDs.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PublikoAPI.Services
{
    public class GlobalIDServices : IGlobalIDServices
    {
        /// <summary>
        /// Global Unique ID from string. Use in conjunction with the GetSeed() method 
        /// or generate the hash with your own string.
        /// </summary>
        /// <param name="input">A string to be hashed.</param>
        /// <returns>A Global Unique Identifier as a string.</returns>
        public string GuidFromString(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(input));
                string globalID = (new Guid(hash)).ToString();

                return globalID;
            }
        }



        /// <summary>
        /// Generates a seed with the actual time and ticks.
        /// </summary>
        /// <returns>A string containing a seed of time to the ticks.</returns>
        public string GetSeed()
        {
            DateTime nu = DateTime.Now;
            return nu.ToString() + nu.Ticks.ToString();
        }
    }
}
