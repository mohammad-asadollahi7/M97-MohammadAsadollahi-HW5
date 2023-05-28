
using HW5.Domain;
using Newtonsoft.Json;

namespace HW5.Interface
{
    public class StockRepository : IStockRepository
    {
        private readonly List<Stock>? _stock;
        private string? jsonFilePath;
        private string? jsonString;
        IProductRepository productRepository;
        public StockRepository()
        {
            string? projectPath = Directory.GetParent
                   (AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            jsonFilePath = Path.Combine(projectPath, "DataBase/Stock.json");
            jsonString = File.ReadAllText(jsonFilePath);
            _stock = JsonConvert.DeserializeObject<List<Stock>>(jsonString);
        }
        public string BuyProduct(Stock productInStock)
        {
            var existProduct = _stock.FirstOrDefault(s => s.ProductId == productInStock.ProductId);

            if (existProduct != null)
            {
                existProduct.ProductPrice = 
                    ((productInStock.ProductPrice * productInStock.ProductQuantity) +
                     (existProduct.ProductPrice * existProduct.ProductQuantity))
                     /(productInStock.ProductQuantity + existProduct.ProductQuantity);

                existProduct.ProductQuantity += productInStock.ProductQuantity;
                SetData(_stock);
                return existProduct.Name;

            }
            else
            {
                productInStock.StockId = _stock.Count() + 1;
                _stock.Add(productInStock);
                SetData(_stock);
                return productRepository.GetProductById(productInStock.ProductId);
            }
        }

        public List<StockProductViewModel> GetSalesProductList()
        {

            throw new NotImplementedException();
        }

        public string SaleProduct(int ProductId, int cnt)
        {

            throw new NotImplementedException();
        }

        public void SetData(List<Stock> stocks)
        {
            jsonString = JsonConvert.SerializeObject(stocks);
            File.WriteAllText(jsonFilePath, jsonString);
        }
    }
}
