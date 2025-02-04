namespace E_Commerce_API.Services
{
    public class HashingService
    {
        public static string CreatePasswordHash(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        public static bool VerifyPasswordHash(string password, string storedHash)
        {
            var computedHash = CreatePasswordHash(password);
            return computedHash == storedHash;
        }
    }
}
