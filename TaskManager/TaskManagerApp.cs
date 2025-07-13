using System;
using TaskManager.ConsoleInteractions;
using TaskManager.Datas;
using TaskManager.FileAccess;

namespace TaskManager;

public class TaskManagerApp
{
    private readonly IConsoleInteraction _consoleInteraction;
    private readonly TaskManager _taskManager;
    private int _userInput;

    public TaskManagerApp(IConsoleInteraction consoleInteraction)
    {
        _consoleInteraction = consoleInteraction;

        const string filePath = "tasks.json";
        var dataRepo = new DataRepository(new FileRepository(), filePath);
        _taskManager = new TaskManager(dataRepo);
    }

    public void Run()
    {
        Console.Title = "TaskManager App";
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("***** Welcome to Task Manager App *****\n");

        while (true)
        {
            _consoleInteraction.ShowOperations();

            try
            {
                _userInput = _consoleInteraction.TakeInput();
            }
            catch (InvalidDataException ex)
            {
                Console.WriteLine($"Data format exception found: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                continue;
            }

            bool endProgram = false;

            switch (_userInput)
            {
                case 0:
                    endProgram = HandleExit();
                    break;
                case 1:
                    HandleTaskRead();
                    break;
                case 2:
                    HandleTaskCreate();
                    break;
                case 3:
                    HandleTaskUpdate();
                    break;
                case 4:
                    HandleTaskDelete();
                    break;
                case 5:
                    HandleTaskQuery();
                    break;
                default:
                    Console.WriteLine("Unknown option. Try again.");
                    break;
            }

            if (endProgram)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Thank you for using our app!");
                Console.ResetColor();
                break;
            }

            Console.WriteLine();
        }
    }

    private bool HandleExit()
    {
        _taskManager.SaveAll();
        return true;
    }

    private void HandleTaskRead()
    {
        Console.WriteLine("\t1. Get by Id");
        Console.WriteLine("\t2. Get all tasks");
        Console.Write("\tEnter operation: ");

        if (!int.TryParse(Console.ReadLine(), out int op)) return;

        if (op == 1)
        {
            Console.Write("Enter task Id: ");
            string id = Console.ReadLine();
            Console.WriteLine(_taskManager.Getter.GetTaskById(id));
        }
        else
        {
            ShowPaginatedTasks();
        }
    }

    private void HandleTaskCreate()
    {
        Console.Write("Enter Task title: ");
        string title = Console.ReadLine();

        Console.Write("Enter Task description: ");
        string description = Console.ReadLine();

        Console.Write("Enter Due Date (MM/DD/YY): ");
        DateTime.TryParse(Console.ReadLine(), out DateTime dueDate);

        Console.Write("Is completed? (true/false): ");
        bool.TryParse(Console.ReadLine(), out bool isCompleted);

        _taskManager.Creator.Create(title, description, dueDate, isCompleted);
    }

    private void HandleTaskUpdate()
    {
        Console.Write("Enter Id: ");
        string id = Console.ReadLine();

        Console.Write("Enter Task title: ");
        string title = Console.ReadLine();

        Console.Write("Enter Task description: ");
        string description = Console.ReadLine();

        Console.Write("Enter Due Date (MM/DD/YY): ");
        DateTime.TryParse(Console.ReadLine(), out DateTime dueDate);

        Console.Write("Is completed? (true/false): ");
        bool.TryParse(Console.ReadLine(), out bool isCompleted);

        _taskManager.Editor.Edit(id, title, description, dueDate, isCompleted);
    }

    private void HandleTaskDelete()
    {
        Console.Write("Enter Task Id: ");
        string id = Console.ReadLine();
        _taskManager.Deletor.Delete(id);
    }

    private void HandleTaskQuery()
    {
        Console.WriteLine("\t1. Get all Pending tasks");
        Console.WriteLine("\t2. Get all Completed tasks");
        Console.WriteLine("\t3. Get tasks pending next week");
        Console.WriteLine("\t4. Get pending tasks for today");
        Console.WriteLine("\t5. Get completed vs pending count");
        Console.WriteLine("\t6. Get the top 5 tasks with the nearest due dates.");
        Console.Write("Enter your desired query: ");

        if (!int.TryParse(Console.ReadLine(), out int option)) return;

        switch (option)
        {
            case 1:
                _consoleInteraction.ShowOutput(_taskManager.Query.GetAllPendingTasks());
                break;
            case 2:
                _consoleInteraction.ShowOutput(_taskManager.Query.GetAllCompletedTasks());
                break;
            case 3:
                _consoleInteraction.ShowOutput(_taskManager.Query.GetPendingTaskNextWeek());
                break;
            case 4:
                _consoleInteraction.ShowOutput(_taskManager.Query.GetTasksForToday());
                break;
            case 5:
                _taskManager.Query.GetCountCompletedVsPending();
                break;
            case 6:
                _consoleInteraction.ShowOutput(_taskManager.Query.GetTop5Pending());
                break;
            default:
                Console.WriteLine("Unknown query option.");
                break;
        }
    }

    private void ShowPaginatedTasks()
    {
        const int pageSize = 4;
        int total = _taskManager.Getter.GetTotalTaskCount();
        int pages = (int)Math.Ceiling(total / (double)pageSize);
        int current = 1;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"All Tasks — page {current}/{pages}\n");

            var page = _taskManager.Getter.GetTasksPage(current, pageSize);
            _consoleInteraction.ShowOutput(page);

            Console.WriteLine("\nN-next, P-prev, Q-quit");
            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.N && current < pages) current++;
            else if (key == ConsoleKey.P && current > 1) current--;
            else if (key == ConsoleKey.Q) break;
        }
    }
}
