using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace MUSAKA.Domain
{
    public class Receipt
    {
        /*
         * •	Has an Id – a GUID String.
           •	Has a Issued On – a DateTime object.
           •	Has a Orders – a collection of Order objects.
           •	Has a Cashier – a User object.
         */
        public int Id { get; set; }
        public DateTime IssuedOn { get; set; }

        [NotMapped]
        public ICollection<Order> Orders { get; set; }

        public int CashierId { get; set; }
        public User Cashier { get; set; }
    }
}