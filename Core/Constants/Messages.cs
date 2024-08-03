using System.Threading.Channels;

namespace Core.Constants;

public class Messages
{
    public static void ErrorHasOccuredMessage() => Console.WriteLine("Error has occured");
    public static void NotEnoughMessage(string title) => Console.WriteLine($"There is not enough {title}");
    public static void NotFoundMessage(string title) => Console.WriteLine($"{title} not found");
    public static void InputMessage(string title) => Console.WriteLine($"Enter {title}");
    public static void InvalidInputMessage(string title) => Console.WriteLine($"{title} is invalid");
    public static void AlreadyExistMessage(string title) => Console.WriteLine($"{title} already exists");
    public static void WrongInputMessage() => Console.WriteLine($"Inputs are not correct");
    public static void SuccessLoginMessage() => Console.WriteLine("Logged in successfully");
    public static void SuccessMessage(string title, string process) => Console.WriteLine($"{title} {process} successfully");
    public static void ThereIsNotMessage(string title) => Console.WriteLine($"There is not saved {title}");
}
