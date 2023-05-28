
using HW5.Domain;

namespace HW5.Interface
{
    public interface IStockRepository
    {
        string SaleProduct(int ProductId, int cnt);
        string BuyProduct(Stock productInStock);
        List<StockProductViewModel> GetSalesProductList();

    }
}
