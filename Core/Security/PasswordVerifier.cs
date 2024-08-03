using System.Security.Cryptography;

namespace Core.Security;

public class PasswordVerifier
{
    public static bool VerifyPassword(string password, string storedHash, byte[] salt)
    {
        byte[] hashBytes = Convert.FromBase64String(storedHash);

        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
        {
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }
        }

        return true;
    }
}
