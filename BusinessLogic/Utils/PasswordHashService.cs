using System.Text;

namespace BusinessLogic.Utils
{
    public static class PasswordHashService
    {
        public static string GeneratePasswordHash(string salt, string password)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes($@"{password}{salt}"));
                
            foreach (var theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
                
            return hash.ToString();
        }
    }
}