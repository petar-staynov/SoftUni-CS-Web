using System;
using System.Collections.Generic;
using System.Linq;
using MUSAKA.Common.Models.View;
using MUSAKA.Domain;
using SIS.Framework.ActionResults;
using SIS.Framework.Attributes.Action;
using SIS.Framework.Attributes.Method;

namespace MUSAKA.App.Controllers
{
    public class ProductsController : BaseController
    {
        [Authorize("User", "Admin")]
        public IActionResult All()
        {
            List<Product> products = context.Products.ToList();

            List<AllProductViewModel> productsViewModels = new List<AllProductViewModel>();
            foreach (Product product in products)
            {
                int productId = product.Id;
                string name = product.Name;
                decimal price = product.Price;
                long barcode = product.Barcode;
                string picture = product.Picture;

                AllProductViewModel productViewModel = new AllProductViewModel()
                {
                    Id = productId,
                    Name = name,
                    Price = price,
                    Barcode = barcode,
                    Picture = picture,
                };
                productsViewModels.Add(productViewModel);
            }

            this.Model["Products"] = productsViewModels;
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
        public IActionResult Create(CreateProductBindingModel bindingModel)
        {
            //Redirect to index if barcode is less than 12 characters
            if (bindingModel.Barcode.Length != 12)
            {
                return this.RedirectToAction("/");
            }

            //Use default picture if no picture is specified
            if (string.IsNullOrEmpty(bindingModel.Picture) || string.IsNullOrWhiteSpace(bindingModel.Picture))
            {
                bindingModel.Picture = @"https://i.postimg.cc/SxdKDPn0/product.png";
            }

            var product = new Product()
            {
                Name = bindingModel.Name,
                Picture = bindingModel.Picture,
                Price = bindingModel.Price,
                Barcode = long.Parse(bindingModel.Barcode),
            };

            context.Products.Add(product);
            context.SaveChanges();

            return this.RedirectToAction("/products/all");
        }

        [HttpPost]
        [Authorize("User", "Admin")]
        public IActionResult Order(AddProductBindingModel bindingModel)
        {
            var barcode = long.Parse(bindingModel.Barcode);
            var quantity = int.Parse(bindingModel.Quantity);

            if (barcode.ToString().Length != 12)
            {
                return this.RedirectToAction("/");
            }

            var product = context.Products.FirstOrDefault(p => p.Barcode == barcode);
            if (product == null)
            {
                return this.RedirectToAction("/");
            }

            var user = context.Users.FirstOrDefault(u => u.Username == this.Identity.Username);
            if (user == null)
            {
                this.RedirectToAction("/");
            }


            var orderStatus = context.OrderStatuses.FirstOrDefault(s => s.Name == "Active");
            var order = new Order()
            {
                OrderStatus = orderStatus,
                Product = product,
                Cashier = user,
                Quantity = quantity,
                OrderStatusId = 1,
            };

            context.Orders.Add(order);
            context.SaveChanges();

            return this.RedirectToAction("/");
        }
    }
}