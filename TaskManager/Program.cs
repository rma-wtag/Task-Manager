using TaskManager.ConsoleInteractions;

namespace TaskManager;

internal class program {
    public static void Main(string[] args) {
        TaskManagerApp taskManagerApp = new TaskManagerApp(new ConsoleInteraction());
        taskManagerApp.Run();
    }
}