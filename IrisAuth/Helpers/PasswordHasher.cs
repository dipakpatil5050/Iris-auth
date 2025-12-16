using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPICOM;
namespace IrisAuth.Helpers
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty");

            // Create CAPICOM Hash object
            var hash = new CAPICOM.HashedData();

            // SHA-256 algorithm
            hash.Algorithm = CAPICOM.CAPICOM_HASH_ALGORITHM.CAPICOM_HASH_ALGORITHM_SHA_256;

            // Hash the password
            hash.Hash(password);

            // Return Base64 string
            return hash.Value;
        }
    }
}
