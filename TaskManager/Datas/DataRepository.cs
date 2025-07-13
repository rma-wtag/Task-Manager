using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.FileAccess;
using TaskManager.Models;

namespace TaskManager.Datas
{
    public class DataRepository : IDataRepository
    {
        private readonly IFileRepository _fileRepository;
        private readonly string _filePath;

        private List<CustomTask> _tasks { get; set; }
        public DataRepository(IFileRepository fileRepository,string path)
        {
            _fileRepository = fileRepository;
            _filePath = path;
            _tasks = fileRepository.Read(path);
        }

        public void Add(CustomTask task)
        {
            _tasks.Add(task);
            _fileRepository.Write(_filePath, _tasks);
        }

        public List<CustomTask> Get()
        {
            return _tasks;
        }

        public void Update(string id, string title,string description, DateTime date, bool isCompleted)
        {
            Guid gid = Guid.Parse(id);
            for (int i = 0; i < _tasks.Count(); i++)
            {
                if (_tasks[i].Id == gid)
                {
                    _tasks[i] = new CustomTask
                    {
                        Id = _tasks[i].Id,
                        DueDate = date,
                        Title = title,
                        Description = description,
                        IsCompleted = isCompleted

                    };
                }
            }

        }

        public void Delete(string id)
        {
            Guid gid = Guid.Parse(id);
            var newTasks = new List<CustomTask>();
            foreach (var task in _tasks)
            {
                if (task.Id != gid)
                {
                    newTasks.Add(task);
                }
            }

            _tasks = newTasks;
        }

        public void Save() {
            _fileRepository.Write(_filePath, _tasks);
        }
    }
}
