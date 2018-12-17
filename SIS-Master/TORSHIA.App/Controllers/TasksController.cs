using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SIS.Framework.ActionResults;
using SIS.Framework.Attributes.Action;
using SIS.Framework.Attributes.Method;
using TORSHIA.Common.Models.View;
using TORSHIA.Domain;

namespace TORSHIA.App.Controllers
{
    public class TasksController : BaseController
    {
        public IActionResult All()
        {
            return null;
        }

        [Authorize("Admin", "User")]
        public IActionResult Details(int id)
        {
            var task = context.Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return this.RedirectToAction("/");
            }

            var affectedSectors = task.AffectedSectors.Split(", ");
            List<string> afectedSectorsNames = new List<string>();
            foreach (string affectedSector in affectedSectors)
            {
                var sector = context.SectorTypes.FirstOrDefault(s => s.Id == int.Parse(affectedSector));
                afectedSectorsNames.Add(sector.Name);


                this.Model.Data["Title"] = task.Title;
                this.Model.Data["Description"] = task.Description;
                this.Model.Data["AffectedSectors"] = string.Join(", ", afectedSectorsNames);
                this.Model.Data["Participants"] = task.Participants;
                this.Model.Data["DueDate"] = task.DueDate.ToShortDateString();
                this.Model.Data["Level"] = task.AffectedSectors.Split(',').Length;
            }

            return this.View();
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize("Admin")]
        public IActionResult Create(CreateTaskBindingModel bindingModel)
        {
            var task = new Task()
            {
                Title = bindingModel.title,
                DueDate = DateTime.Parse(bindingModel.dueDate),
                IsReported = false,
                Description = bindingModel.description,
                Participants = bindingModel.participants,
            };

            this.context.Tasks.Add(task);
            context.SaveChanges();

            return RedirectToAction("/");
        }
        [Authorize("User", "Admin")]
        public IActionResult Report(int id)
        {
            var currentUser = context.Users.Include(u => u.Role)
                .FirstOrDefault(u => u.Username == this.Identity.Username);
            var reportStatus = context.ReportStatuses.FirstOrDefault(rs => rs.Name == "Completed");
            var task = context.Tasks.FirstOrDefault(t => t.Id == id);

            var report = new Report()
            {
                ReportedOn = DateTime.Now,
                Reporter = currentUser,
                ReporterId = currentUser.Id,
                Status = reportStatus,
                Task = task,
                TaskId = task.Id
            };

            task.IsReported = true;

            context.Reports.Add(report);
            context.SaveChanges();
            return RedirectToAction("/");
        }
    }
}