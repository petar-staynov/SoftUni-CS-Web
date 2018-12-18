namespace MUSAKA.Common.Models.View
{
    public class AllProductViewModel
    {
        /*
         * var productId = product.Id;
           var name = product.Name;
           var price = product.Price;
           var barcode = product.Barcode;
         */

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public long Barcode { get; set; }
    }
}