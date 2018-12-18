using SIS.Framework.ActionResults;
using SIS.Framework.Controllers;
using System.Linq;
using System.Runtime.CompilerServices;
using MUSAKA.Data;

namespace MUSAKA.App.Controllers
{
    public abstract class BaseController : Controller
    {
        protected MusakaDbContext context = new MusakaDbContext();

        protected override IViewable View([CallerMemberName] string actionName = "")
        {
            if (this.Identity != null)
            {
                if (this.Identity.Roles.Contains("Admin"))
                {
                    this.Model.Data["LoggedIn"] = "none";
                    this.Model.Data["LoggedOut"] = "none";
                    this.Model.Data["Admin"] = "block";
                }
                else if (this.Identity.Roles.Contains("User"))
                {
                    this.Model.Data["LoggedIn"] = "block";
                    this.Model.Data["LoggedOut"] = "none";
                    this.Model.Data["Admin"] = "none";
                }

                this.Model.Data["Username"] = this.Identity.Username;
            }
            else
            {
                this.Model.Data["LoggedIn"] = "none";
                this.Model.Data["LoggedOut"] = "block";
                this.Model.Data["Admin"] = "none";
            }

            return base.View(actionName);
        }
    }
}