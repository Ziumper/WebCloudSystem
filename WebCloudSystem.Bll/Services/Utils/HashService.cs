using System;
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
    }
}