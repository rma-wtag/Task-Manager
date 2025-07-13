using TaskManager.Models;

namespace TaskManager.ConsoleInteractions;

public class ConsoleInteraction : IConsoleInteraction
{
    public void ShowOperations()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Operations that you can perform ");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("0.EXIT");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("1.GET");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("2.CREATE");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("3.UPDATE");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("4.DELETE");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("5.QUERY");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void ShowOutput(List<CustomTask> tasks)
    {
        foreach (var task in tasks) {
            Console.WriteLine(task.ToString());
        }
    }

    public int TakeInput()
    {
        Console.Write("Enter operation number you want to perform : ");

        if (int.TryParse(Console.ReadLine(), out int res) && res>=0 && res<=5)
        {
            return res;
        }
        else {
            throw new InvalidDataException("Not a valid input.");
        }
    }
}
