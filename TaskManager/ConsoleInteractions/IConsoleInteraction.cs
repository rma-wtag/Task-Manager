using TaskManager.Models;

namespace TaskManager.ConsoleInteractions;

public interface IConsoleInteraction {
    void ShowOperations();
    int TakeInput();
    void ShowOutput(List<CustomTask> customTasks);
}