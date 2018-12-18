namespace MUSAKA.Domain
{
    public class ReceiptOrder
    {
        public int ReceiptId { get; set; }
        public Receipt Receipt { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}