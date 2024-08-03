using System.Text.RegularExpressions;

namespace Core.Extensions;

public static class StringExtensions
{
    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public static bool isValidChoice(this string choice)
    {
        if (choice.ToLower() == "y" || choice.ToLower() == "n")
            return true;
        return false;
    }

    public static bool isValidPassword(this string password)
    {
        if (password.Length >= 8)
            return true;
        return false;
    }

}
