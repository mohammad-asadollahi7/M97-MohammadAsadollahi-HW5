
using HW5.Domain;

namespace HW5.Interface
{
    public interface IStockRepository
    {
        string Sale(int ProductId, int cnt);
        string Buy(Stock productInStock);
        List<StockProductViewModel> GetSalesProductList();

    }
}
