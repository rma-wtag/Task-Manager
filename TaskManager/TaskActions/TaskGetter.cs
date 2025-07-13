using System.Diagnostics;
using TaskManager.Datas;
using TaskManager.FileAccess;
using TaskManager.Models;

namespace TaskManager.TaskActions;

public class TaskGetter {
    private readonly IDataRepository _dataRepository;
    public TaskGetter(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }
    public CustomTask GetTaskById(string id){
        Guid gid = Guid.Parse(id);

        var TaskList = _dataRepository.Get();
        return TaskList.FirstOrDefault(task => task.Id == gid);
    }

    public List<CustomTask> GetTasksPage(int pageNumber, int pageSize = 10)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        return _dataRepository.Get()
                              .OrderByDescending(t => t.DueDate)
                              .Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();
    }
    public int GetTotalTaskCount() => _dataRepository.Get().Count();

    public List<CustomTask> GetAllTask() {
        return _dataRepository.Get().OrderByDescending(task => task.DueDate).ToList();
    }

    public void GetOnlyTittles() {
        var tasks = _dataRepository.Get();
        var result = tasks.Select(task => task.Title);

        foreach (var r in result) {
            Console.WriteLine(r);
        }
    }
}