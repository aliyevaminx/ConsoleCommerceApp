﻿using System.Security.Cryptography;

namespace Core.Security;

public class PasswordHasher
{
    public static string HashPassword(string password, out byte[] salt)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            salt = new byte[16];
            rng.GetBytes(salt);
        }

        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
        {
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
