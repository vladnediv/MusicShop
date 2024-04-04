using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MusicAlbumsEF.Services
{
    public class PasswordHasher
    {
        public string HasPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hasbytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                var builder = new StringBuilder();

                foreach (byte b in hasbytes)
                {
                    builder.Append(b.ToString("X2"));
                }

                return builder.ToString();

            }
        }
    }
}
