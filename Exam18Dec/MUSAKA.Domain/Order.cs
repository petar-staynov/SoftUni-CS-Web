namespace MUSAKA.Domain
{
    public class Order
    {
        /*
         * •	Has an Id – a GUID String or an Integer.
           •	Has a Status – can be one of the following values ("Active", "Completed").
           •	Has a Product – an Product object.
           •	Has a Quantity – an integer.
           •	Has a Cashier – a User object
         */
        public int Id { get; set; }

        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public int CashierId { get; set; }
        public User Cashier { get; set; }
    }
}