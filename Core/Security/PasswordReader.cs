namespace Core.Security;

public class PasswordReader
{
    public static string ReadPassword()
    {
        string password = string.Empty;
        ConsoleKeyInfo keyInfo;

        while (true)
        {
            keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                break;
            }
            else if (keyInfo.Key == ConsoleKey.Backspace)
            {
                if (password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            else
            {
                password += keyInfo.KeyChar;
                Console.Write("*");
            }
        }
        Console.WriteLine();
        return password;
    }
}
