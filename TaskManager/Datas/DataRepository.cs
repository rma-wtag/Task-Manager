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
        public DataRepository(IFileRepository fileRepository, string path)
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

        public void Update(string id, string title, string description, DateTime date, bool isCompleted)
        {
            Guid gid = Guid.Parse(id);

            var taskUpdate = _tasks.FirstOrDefault(t => t.Id == gid);
            if (taskUpdate != null)
            {
                taskUpdate.Title = title;
                taskUpdate.Description = description;
                taskUpdate.DueDate = date;
                taskUpdate.IsCompleted = isCompleted;
            }

        }

        public void Delete(string id)
        {
            Guid gid = Guid.Parse(id);
            _tasks.RemoveAll(t => t.Id == gid);
        }

        public void Save()
        {
            _fileRepository.Write(_filePath, _tasks);
        }
    }
}
