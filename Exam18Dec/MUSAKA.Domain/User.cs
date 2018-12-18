using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SIS.Framework.Security;

namespace MUSAKA.Domain
{
    public class User : IdentityUser
    {
        /*
         * •	Has an Id – a GUID String or an Integer.
           •	Has an Username
           •	Has a Password
           •	Has an Email
           •	Has an Role – can be one of the following values (“User”, “Admin”)
         */

        public int Id { get; set; }

        public int RoleId { get; set; }
        public UserRole Role { get; set; }

        [NotMapped]
        public override IEnumerable<string> Roles => new[] { this.Role.Name };
    }
}