using System;

namespace MUSAKA.Common.Models.View
{
    public class ReceiptRowViewModel
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public string IssuedOn { get; set; }
        public string Cashier { get; set; }
    }
}