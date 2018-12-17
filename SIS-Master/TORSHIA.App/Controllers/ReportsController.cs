using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SIS.Framework.ActionResults;
using TORSHIA.Common.Models.View;
using TORSHIA.Domain;

namespace TORSHIA.App.Controllers
{
    public class ReportsController : BaseController
    {
        public IActionResult All()
        {
            Report[] reports = this.context.Reports.Include(r => r.Status).Include(r => r.Task).ToArray();

            List<AllReportViewModel> AllReportViewModels = new List<AllReportViewModel>();
            foreach (var report in reports)
            {
                var reportId = report.Id;
                var taskTitle = report.Task.Title;
                var level = report.Task.AffectedSectors.Count();
                var status = report.Status.Name;

                AllReportViewModel reportViewModel = new AllReportViewModel()
                {
                    Id = reportId,
                    Task = taskTitle,
                    Level = level,
                    Status = status,
                };
                AllReportViewModels.Add(reportViewModel);
            }

            this.Model["Reports"] = AllReportViewModels;
            return this.View();
        }

        public IActionResult Details(int id)
        {
            Report report = context.Reports
                .Include(r => r.Task)
                .Include(r => r.Status)
                .FirstOrDefault(r => r.Id == id);

            string[] affectedSectorsIds = report.Task.AffectedSectors.Split(',').ToArray();
            List<string> affectedSectorsNames = new List<string>();
            foreach (string affectedSectorId in affectedSectorsIds)
            {
                var sector = context.SectorTypes.FirstOrDefault(s => s.Id == int.Parse(affectedSectorId));
                if (sector != null)
                {
                    affectedSectorsNames.Add(sector.Name);
                }
            }

            var user = context.Users.FirstOrDefault(u => u.Id == report.ReporterId);

            this.Model["ReportId"] = report.Id;
            this.Model["TaskTitle"] = report.Task.Title;
            this.Model["TaskLevel"] = report.Task.AffectedSectors.Split(',').Length;
            this.Model["TaskStatus"] = report.Status.Name;
            this.Model["TaskDueDate"] = report.Task.DueDate.ToShortDateString();
            this.Model["ReportedOn"] = report.ReportedOn.ToShortDateString();
            this.Model["Reporter"] = user.Username;
            this.Model["Participants"] = report.Task.Participants;
            this.Model["AffectedSectors"] = string.Join(", ", affectedSectorsNames);
            this.Model["TaskDescription"] = report.Task.Description;


            return this.View();
        }
    }
}