using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SIS.Framework.Api;
using SIS.Framework.Services;
using TORSHIA.Data;
using TORSHIA.Domain;
using TORSHIA.Services;
using TORSHIA.Services.Contracts;

namespace TORSHIA.App
{
    public class App : MvcApplication
    {
        private void SeedDatabase()
        {
            using (var context = new TorshiaContext())
            {
                context.UserRoles.Add(new UserRole {Name = "User"});
                context.UserRoles.Add(new UserRole {Name = "Admin"});

                context.SectorTypes.Add(new SectorType {Name = "Customers"});
                context.SectorTypes.Add(new SectorType {Name = "Marketing"});
                context.SectorTypes.Add(new SectorType {Name = "Finances"});
                context.SectorTypes.Add(new SectorType {Name = "Internal"});
                context.SectorTypes.Add(new SectorType {Name = "Management"});

                context.ReportStatuses.Add(new ReportStatus {Name = "Completed"});
                context.ReportStatuses.Add(new ReportStatus {Name = "Archived"});

                context.SaveChanges();
            }
        }

        private void SeedTasks()
        {
            using (var context = new TorshiaContext())
            {
                var affectedSector = context.SectorTypes.FirstOrDefault(s => s.Name == "Customers");

                for (int i = 0; i < 5; i++)
                {
                    Random rand = new Random();
                    int randomSectors = rand.Next(1, context.SectorTypes.Count());

                    context.Tasks.Add(new Task()
                    {
                        Title = $"GeneratedTitle {new string(char.Parse(i.ToString()), 5)}",
                        DueDate = DateTime.ParseExact("01-12-2018", "dd-MM-yyyy", CultureInfo.InstalledUICulture),
                        IsReported = false,
                        Participants = "Pesho, Gosho, User",
                        Description = $"{new string('A', 10)}",
                        AffectedSectors = $"{randomSectors}",
                    });
                }


                context.SaveChanges();
            }
        }

        public override void Configure()
        {
            using (var context = new TorshiaContext())
            {
                if (!context.Users.Any())
                {
                    Console.WriteLine("SEEDING DATABASE");
                    SeedDatabase();
                }

                if (!context.Tasks.Any())
                {
                    SeedTasks();
                }
            }
        }

        public override void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<TorshiaContext, TorshiaContext>();

            dependencyContainer.RegisterDependency<IUserService, UserService>();

            base.ConfigureServices(dependencyContainer);
        }
    }
}