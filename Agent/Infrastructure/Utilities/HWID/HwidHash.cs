using System.Security.Cryptography;
using System.Text;

namespace AgentClient.Infrastructure.Utilities.HWID
{
    public static class HwidHash
    {
        public static string Sha256Hex(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                input = "";
            }

            var bytes = Encoding.UTF8.GetBytes(input);

            byte[] hash;
            using (var sha = SHA256.Create())
            {
                hash = sha.ComputeHash(bytes);
            }

            var sb = new StringBuilder(hash.Length * 2);
            
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
