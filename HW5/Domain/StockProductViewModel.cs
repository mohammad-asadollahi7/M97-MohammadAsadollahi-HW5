
using IronBarCode;

namespace HW5.Domain;

public class StockProductViewModel
{
    public string Name { get; set; }
    public string BarCode { get; set; }
    public int ProductId { get; set; }
    public int ProductQuantity { get; set; }
    public decimal ProductPrice { get; set; }

}
