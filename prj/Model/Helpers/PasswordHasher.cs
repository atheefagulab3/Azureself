using BCryptNet = BCrypt.Net.BCrypt;

namespace prj.Model.Helpers
{
    public class PasswordHasher
    {
        public static string HashPassword(string Password)
        {
            return BCryptNet.HashPassword(Password);
        }

        public static bool VerifyPassword(string Password, string hashedPassword)
        {
            return BCryptNet.Verify(Password, hashedPassword);
        }
    }
}
