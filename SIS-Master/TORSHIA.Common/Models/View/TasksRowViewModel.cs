using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace TORSHIA.Common.Models.View
{
    public class TasksRowViewModel
    {
        private ICollection<TaskViewModel> tasks;

        public TasksRowViewModel()
        {
            this.Tasks = new List<TaskViewModel>();
        }

        public ICollection<TaskViewModel> Tasks
        {
            get => tasks;
            set => tasks = value;
        }

        public string[] Filler => new string[5 - Tasks.Count];
    }
}