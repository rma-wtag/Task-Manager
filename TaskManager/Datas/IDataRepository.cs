using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Datas
{
    public interface IDataRepository
    {
        void Add(CustomTask task);
        List<CustomTask> Get();
        void Update(string id, string title,string description, DateTime date, bool isCompleted);
        void Delete(string id);
        void Save();
    }
}
