using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MUSAKA.Common.Models.View;
using MUSAKA.Domain;
using SIS.Framework.ActionResults;
using SIS.Framework.Attributes.Action;

namespace MUSAKA.App.Controllers
{
    public class ReceiptsController : BaseController
    {
        [Authorize("Admin")]
        public IActionResult All()
        {
            var user = context.Users.FirstOrDefault(u => u.Username == this.Identity.Username);
            var receipts = context.Receipts.ToList();

            List<ReceiptRowViewModel> receiptsRows = new List<ReceiptRowViewModel>();
            foreach (Receipt receipt in receipts)
            {
                var receiptRowViewModel = new ReceiptRowViewModel()
                {
                    Id = receipt.Id,
                    Total = 0,
                    IssuedOn = receipt.IssuedOn.ToShortDateString(),
                    Cashier = user.Username,
                };
                receiptsRows.Add(receiptRowViewModel);
            }

            this.Model["Receipts"] = receiptsRows;

            return this.View();
        }

        [Authorize("Admin", "User")]
        public void Cashout()
        {
            var user = context.Users.FirstOrDefault(u => u.Username == this.Identity.Username);
            var orders = context.Orders.Where(o => o.CashierId == user.Id).ToList();

            var receipt = new Receipt()
            {
                Cashier = user,
                IssuedOn = DateTime.Now,
                Orders = new List<Order>()
            };

            foreach (Order order in orders)
            {
                order.OrderStatusId = 2;
                receipt.Orders.Add(order);
            }

            context.Receipts.Add(receipt);

            foreach (Order order in orders)
            {
                ReceiptOrder receiptOrder = new ReceiptOrder()
                {
                    Order = order,
                    Receipt = receipt
                };
                context.ReceiptsOrders.Add(receiptOrder);
            }

            context.SaveChanges();
            Details(receipt.Id);
        }

        [Authorize("User", "Admin")]
        public IActionResult Details(int id)
        {
            var receipt = context.Receipts.FirstOrDefault(r => r.Id == id);

            var user = context.Users.FirstOrDefault(u => u.Id == receipt.CashierId);


            var orders = context.Orders;
            var products = context.Products;

            var actualReceiptOrders = context.ReceiptsOrders.Where(ro => ro.ReceiptId == receipt.Id);
            var orderIds = actualReceiptOrders.Select(ro => ro.OrderId).ToList();

            List<Order> ordersList = new List<Order>();
            foreach (var orderId in orderIds)
            {
                var order = orders.FirstOrDefault(o => o.Id == orderId);
                ordersList.Add(order);
            }

            List<AllProductViewModel> productsViewModels = new List<AllProductViewModel>();
            decimal total = 0;
            foreach (Order order in ordersList)
            {
                var product = products.FirstOrDefault(p => p.Id == order.ProductId);
                AllProductViewModel productViewModel = new AllProductViewModel()
                {
                    Id = product.Id,
                    Barcode = product.Barcode,
                    Name = product.Name,
                    Picture = product.Picture,
                    Price = product.Price
                };
                productsViewModels.Add(productViewModel);

                total += product.Price * order.Quantity;
            }

            this.Model["Products"] = productsViewModels;
            this.Model["Total"] = total;
            this.Model["Cashier"] = user.Username;
            this.Model["IssuedOn"] = receipt.IssuedOn.ToShortDateString();
            this.Model["Receipt"] = receipt.Id;

            return this.View();
        }
    }
}