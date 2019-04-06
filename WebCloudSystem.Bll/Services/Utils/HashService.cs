using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WebCloudSystem.Bll.Services.Utils {
    public class HashService : IHashService {
        public string GetHash (string input) {
            var result = "";
            using (var sha256 = SHA256.Create ()) {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash (Encoding.UTF8.GetBytes (input));
                // Get the hashed string.  
                result = BitConverter.ToString (hashedBytes).Replace ("-", "").ToLower ();
            }

            return result;
        }

        public string GetRandomActivationCode () {
            var result = RandomString(4);
            return result;
        }

        private string RandomString (int length) {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string (Enumerable.Repeat (chars, length)
                .Select (s => s[random.Next (s.Length)]).ToArray ());
        }
    }
}