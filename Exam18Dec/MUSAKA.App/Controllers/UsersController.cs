using SIS.Framework.ActionResults;
using SIS.Framework.Attributes.Method;
using SIS.Framework.Security;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MUSAKA.Common.Models.View;
using SIS.Framework.Attributes.Action;
using MUSAKA.Domain;

namespace MUSAKA.App.Controllers
{
    public class UsersController : BaseController
    {
        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserBindingModel bindingModel)
        {
            var user = this.context
                .Users
                .Select(u => new
                {
                    Username = u.Username,
                    Password = u.Password,
                    Email = u.Email,
                    Role = context.UserRoles.FirstOrDefault(r => r.Id == u.RoleId)
                })
                .FirstOrDefault(u => u.Username == bindingModel.username && u.Password == bindingModel.password);

            if (user != null)
            {
                this.SignIn(new IdentityUser()
                {
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                    Roles = new List<string>() {user.Role.Name}
                });
            }
            else
            {
                RedirectToAction("/users/register");
            }

            return RedirectToAction("/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel bindingModel)
        {
            UserRole role = new UserRole();
            if (!context.Users.Any())
            {
                role = context.UserRoles.FirstOrDefault(r => r.Name == "Admin");
            }
            else
            {
                role = context.UserRoles.FirstOrDefault(r => r.Name == "User");
            }

            User user = new User()
            {
                Username = bindingModel.username,
                Password = bindingModel.password,
                Email = bindingModel.email,
                IsValid = true,
                Role = role,
            };

            context.Users.Add(user);
            context.SaveChanges();


            Login(new LoginUserBindingModel()
            {
                username = user.Username,
                password = user.Password
            });

            return RedirectToAction("/");
        }

        [Authorize("User", "Admin")]
        public IActionResult Profile()
        {
            var user = context.Users.FirstOrDefault(u => u.Username == this.Identity.Username);
            var receipts = context.Receipts
                //.Include(r => r.Orders)
                .Where(r => r.Cashier == user)
                .ToList();

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

        [Authorize("User", "Admin")]
        public IActionResult Logout()
        {
            this.SignOut();
            return RedirectToAction("/");
        }
    }
}