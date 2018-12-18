using System;
using System.Linq;
using MUSAKA.App.Controllers;
using MUSAKA.Common.Models.View;
using MUSAKA.Data;
using MUSAKA.Domain;
using SIS.Framework.Api;
using SIS.Framework.Services;

namespace MUSAKA.App
{
    public class MusakaApp : MvcApplication
    {
        private void InitialSeed()
        {
            using (var context = new MusakaDbContext())
            {
                context.UserRoles.Add(new UserRole {Name = "Admin"});
                context.UserRoles.Add(new UserRole {Name = "User"});

                context.OrderStatuses.Add(new OrderStatus {Name = "Active"});
                context.OrderStatuses.Add(new OrderStatus {Name = "Completed"});

                context.SaveChanges();
            }
        }


        public override void Configure()
        {
            using (var context = new MusakaDbContext())
            {
                if (!context.UserRoles.Any() && !context.OrderStatuses.Any())
                {
                    Console.WriteLine("SEEDING DATABASE - INITIAL");
                    InitialSeed();
                }
            }
        }

        public override void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<MusakaDbContext, MusakaDbContext>();

            base.ConfigureServices(dependencyContainer);
        }
    }
}