
namespace HW5.Domain
{
    public class Stock
    {
        public Guid StockId { get; set; }
        public string Name { get; set; }
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }

    }
}
