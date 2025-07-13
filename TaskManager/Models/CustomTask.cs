using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class CustomTask
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
        public CustomTask() { }
        public CustomTask(string title,string description, DateTime dueDate,bool isCompleted)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            DueDate = dueDate;
            IsCompleted = isCompleted;
        }
        public override string ToString()
        {
            return $"{Id},{Title},{Description},{DueDate},{IsCompleted}";
        }
    }
}
