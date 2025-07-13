using System.Text.Json;
using TaskManager.Models;

namespace TaskManager.FileAccess;

public class FileRepository : IFileRepository
{
    public List<CustomTask> Read(string path)
    {
        var tasks = new List<CustomTask>();
        if (!File.Exists(path))
        {
            return tasks;
        }
        var data = File.ReadAllText(path);
        var result = JsonSerializer.Deserialize<List<CustomTask>> (data);
        return result;
    }

    public void Write(string path, List<CustomTask> customTasks)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        string JsonString = JsonSerializer.Serialize(customTasks, options);

        File.WriteAllText(path,JsonString);
    }
}
