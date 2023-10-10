using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Model
{
    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

    public class TaskToDo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Deadline { get; set; }
        public int Priority { get; set; }
        public TaskStatus Status { get; set; }
    }
}
