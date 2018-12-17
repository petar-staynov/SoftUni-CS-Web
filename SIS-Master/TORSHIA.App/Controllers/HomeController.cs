using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SIS.Framework.ActionResults;
using SIS.Framework.Attributes.Action;
using TORSHIA.Common.Models.View;
using TORSHIA.Data;
using TORSHIA.Domain;

namespace TORSHIA.App.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            if (this.Identity != null)
            {
                if (this.Identity.Roles.Contains("Admin"))
                {
                    Console.WriteLine("admin view");
                    return AdminIndex();
                }
                else if (this.Identity.Roles.Contains("User"))
                {
                    Console.WriteLine("user view");
                    return LoggedInIndex();
                }
            }

            Console.WriteLine("no user view");
            return this.View();
        }

        [Authorize("Admin")]
        public IActionResult AdminIndex()
        {
            DbSet<Task> tasks = this.context.Tasks;
            var taskViewModelList = new List<TaskViewModel>();

            foreach (Task task in tasks)
            {
                taskViewModelList.Add(new TaskViewModel()
                {
                    Id = task.Id,
                    Title = task.Title,
                    Level = task.AffectedSectors.Split(",").Length,
                });
            }

            var tasksRowList = new List<TasksRowViewModel>();
            for (int i = 0; i < taskViewModelList.Count; i++)
            {
                if (i % 5 == 0)
                {
                    tasksRowList.Add(new TasksRowViewModel());
                }

                tasksRowList[tasksRowList.Count - 1].Tasks.Add(taskViewModelList[i]);
            }

            this.Model.Data["TasksRows"] = tasksRowList;
            return this.View();
        }

        [Authorize("User")]
        public IActionResult LoggedInIndex()
        {
            DbSet<Task> tasks = this.context.Tasks;
            var taskViewModelList = new List<TaskViewModel>();

            foreach (Task task in tasks)
            {
                taskViewModelList.Add(new TaskViewModel()
                {
                    Id = task.Id,
                    Title = task.Title,
                    Level = task.AffectedSectors.Split(",").Length,
                });
            }

            var tasksRowList = new List<TasksRowViewModel>();
            for (int i = 0; i < taskViewModelList.Count; i++)
            {
                if (i % 5 == 0)
                {
                    tasksRowList.Add(new TasksRowViewModel());
                }

                tasksRowList[tasksRowList.Count - 1].Tasks.Add(taskViewModelList[i]);
            }

            this.Model.Data["TasksRows"] = tasksRowList;
            return this.View();
        }
    }
}