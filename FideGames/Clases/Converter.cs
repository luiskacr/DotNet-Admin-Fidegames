using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace FideGames.Clases
{
    public class Converter
    {

        //Convert text to Sha256
        public String ConverttoSha256(String texto)
        {
            StringBuilder SB = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] resutl = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in resutl)
                {
                    SB.Append(b.ToString("x2"));
                }
            }
            return SB.ToString();
        }
    }
}