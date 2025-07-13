using TaskManager.Datas;
using TaskManager.Models;

namespace TaskManager.TaskActions;

public class TaskCreator {
    private readonly IDataRepository _dataRepository;
    public TaskCreator(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }
    public void Create(string title,string description , DateTime date, bool isCompleted) {

        CustomTask task = new CustomTask(title, description, date, isCompleted);
        Console.WriteLine(task);
        _dataRepository.Add(task);

        Console.WriteLine("Task Created Successfully!");
    }
}
