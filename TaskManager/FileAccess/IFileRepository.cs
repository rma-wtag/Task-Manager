using TaskManager.Models;

namespace TaskManager.FileAccess;

public interface IFileRepository
{
    List<CustomTask> Read(string path);
    void Write(string path, List<CustomTask> customTasks);
}
