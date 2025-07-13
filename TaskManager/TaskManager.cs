using TaskManager.Datas;
using TaskManager.FileAccess;
using TaskManager.TaskActions;

namespace TaskManager;

public class TaskManager
{
    private readonly IDataRepository _dataRepository;

    public TaskGetter Getter { get; }
    public TaskCreator Creator { get; }
    public TaskEditor Editor { get; }
    public TaskDeletor Deletor { get; }
    public TaskQuery Query { get; }

    public TaskManager(IDataRepository dataRepository)
    {
        
        _dataRepository = dataRepository;
        Creator = new TaskCreator(_dataRepository);
        Getter = new TaskGetter(_dataRepository);
        Editor = new TaskEditor(_dataRepository);
        Deletor = new TaskDeletor(_dataRepository);
        Query = new TaskQuery(_dataRepository);
    }

    public void SaveAll()
    {
        _dataRepository.Save();
    }
}
