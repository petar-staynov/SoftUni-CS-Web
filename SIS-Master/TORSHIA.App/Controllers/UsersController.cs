using SIS.Framework.ActionResults;
using SIS.Framework.Attributes.Method;
using SIS.Framework.Security;
using System.Collections.Generic;
using System.Linq;
using SIS.Framework.Attributes.Action;
using TORSHIA.Common.Models.View;
using TORSHIA.Domain;

namespace TORSHIA.App.Controllers
{
    public class UsersController : BaseController
    {
        //private IUserService userService;

//        public UserController(IUserService userService)
//        {
//            this.userService = userService;
//        }

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
        public IActionResult Logout()
        {
            this.SignOut();
            return RedirectToAction("/");
        }
    }
}