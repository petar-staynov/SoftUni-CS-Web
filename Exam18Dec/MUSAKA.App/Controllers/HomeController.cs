using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MUSAKA.App.Controllers;
using MUSAKA.Common.Models.View;
using MUSAKA.Domain;
using SIS.Framework.ActionResults;
using SIS.Framework.Attributes.Action;

namespace MUSAKA.App.Controllers
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
                    return UserIndex();
                }
            }

            Console.WriteLine("no user view");
            return this.View();
        }

        [Authorize("Admin")]
        public IActionResult AdminIndex()
        {
            var orders = context.Orders
                .Include(o => o.Cashier)
                .Include(o => o.Product)
                .Include(o => o.OrderStatus)
                .Where(o => o.Cashier.Username == this.Identity.Username && o.OrderStatus.Name == "Active")
                .ToList();

            List<UserOrderViewModel> userOrdersViewModels = new List<UserOrderViewModel>();
            decimal total = 0;
            foreach (Order order in orders)
            {
                var userOrderViewModel = new UserOrderViewModel()
                {
                    Id = order.Id,
                    Name = order.Product.Name,
                    Price = order.Product.Price,
                    Quantity = order.Quantity,
                };
                total += order.Product.Price * order.Quantity;

                userOrdersViewModels.Add(userOrderViewModel);
            }

            this.Model["Orders"] = userOrdersViewModels;
            this.Model["Total"] = total;

            return this.View();
        }

        [Authorize("User")]
        public IActionResult UserIndex()
        {
            var orders = context.Orders
                .Include(o => o.Cashier)
                .Include(o => o.Product)
                .Include(o => o.OrderStatus)
                .Where(o => o.Cashier.Username == this.Identity.Username && o.OrderStatus.Name == "Active")
                .ToList();

            List<UserOrderViewModel> userOrdersViewModels = new List<UserOrderViewModel>();
            decimal total = 0;
            foreach (Order order in orders)
            {
                var userOrderViewModel = new UserOrderViewModel()
                {
                    Id = order.Id,
                    Name = order.Product.Name,
                    Price = order.Product.Price,
                    Quantity = order.Quantity,
                };
                total += order.Product.Price * order.Quantity;

                userOrdersViewModels.Add(userOrderViewModel);
            }

            this.Model["Orders"] = userOrdersViewModels;
            this.Model["Total"] = total;

            return this.View();
        }
    }
}