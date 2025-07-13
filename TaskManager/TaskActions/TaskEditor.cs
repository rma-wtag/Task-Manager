using TaskManager.Datas;
using TaskManager.FileAccess;
using TaskManager.Models;

namespace TaskManager.TaskActions;

public class TaskEditor
{
    private readonly IDataRepository _dataRepository;
    public TaskEditor(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }

    public void Edit(string id,string title,string description,DateTime date,bool isCompleted) {
        _dataRepository.Update(id,title,description,date,isCompleted);

        Console.WriteLine("Task edited successfully");
    }

}
