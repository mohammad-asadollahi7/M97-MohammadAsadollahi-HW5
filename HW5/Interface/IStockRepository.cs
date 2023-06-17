
using HW5.Domain;
using HW5.Interface.Dto;

namespace HW5.Interface;

public interface IStockRepository
{
    string Sale(int ProductId, int cnt);
    string Buy(BuyStockDto productInStock);
    List<StockProductViewModel> GetSalesProductList();

}
