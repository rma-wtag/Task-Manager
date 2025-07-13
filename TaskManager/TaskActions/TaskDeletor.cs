using TaskManager.Datas;
using TaskManager.FileAccess;
using TaskManager.Models;

namespace TaskManager.TaskActions;

public class TaskDeletor
{
    private readonly IDataRepository _dataRepository;
    public TaskDeletor(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }

    public void Delete(string id) {
        
        _dataRepository.Delete(id);
        Console.WriteLine("Task deleted Successfully!");
    }

}
