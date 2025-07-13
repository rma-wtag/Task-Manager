using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Datas;
using TaskManager.FileAccess;
using TaskManager.Models;

namespace TaskManager.TaskActions
{
    public class TaskQuery
    {
        private readonly IDataRepository _dataRepository;
        public TaskQuery(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public List<CustomTask> GetAllPendingTasks() {
            var tasks = _dataRepository.Get();

            var result = tasks.Where(task => task.IsCompleted == false).ToList();

            return result;
        }
        public List<CustomTask> GetAllCompletedTasks() {
            var tasks = _dataRepository.Get();

            var result = tasks.Where(task => task.IsCompleted == true).ToList();

            return result;
        }
        public List<CustomTask> GetPendingTaskNextWeek() {
            var tasks = _dataRepository.Get();
            var dateToday = DateTime.Now;

            var result = tasks.Where(task => (task.DueDate - dateToday).TotalDays >=0 && (task.DueDate - dateToday).TotalDays <=7 && task.IsCompleted == false).ToList();

            return result;
        }

        public List<CustomTask> GetTasksForToday() {
            var tasks = _dataRepository.Get();
            var dateToday = DateTime.Now;

            var todayTasks = tasks.Where(task => (task.DueDate == dateToday.Date && task.IsCompleted == false)).ToList();
            return todayTasks;
        }

        public void GetCountCompletedVsPending() {
            var tasks = _dataRepository.Get();
            int completedCount = tasks.Count(task=> task.IsCompleted == true);
            int dueCount = tasks.Count(task => task.IsCompleted == false);

            Console.WriteLine($"Completed task count : {completedCount} , Due task count : {dueCount}");
        }

        public List<CustomTask> GetTop5Pending() { 
            var CurDate = DateTime.Now;
            var tasks = _dataRepository.Get().Where(task=>task.IsCompleted == false && (task.DueDate - CurDate).TotalDays > 0)
                                              .OrderBy(task => task.DueDate)
                                              .Take(5)
                                              .OrderByDescending(task => task.DueDate);
            return tasks.ToList();
        }
    }
}