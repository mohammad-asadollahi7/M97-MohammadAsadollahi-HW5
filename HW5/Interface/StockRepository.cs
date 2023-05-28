
using HW5.Domain;
using Newtonsoft.Json;
using System.Security.Cryptography;

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
                     / (productInStock.ProductQuantity + existProduct.ProductQuantity);

                existProduct.ProductQuantity += productInStock.ProductQuantity;
                SetData(_stock);
                return $"The {existProduct.Name} was updated.";
            }
            
            else
            {
                productInStock.StockId = _stock.Count() + 1;
                _stock.Add(productInStock);
                SetData(_stock);
                var productName =  productRepository.GetProductById(productInStock.ProductId);
                return $"The {productName} was added to stock.";
            }
        }


        public string SaleProduct(int ProductId, int cnt)
        {
            var product = _stock.FirstOrDefault(p => p.ProductId == ProductId);
            int quantity = GetProductQuantity(ProductId);
            if (quantity > cnt)
            {
                product.ProductQuantity -= cnt;
                SetData(_stock);
                return $"{cnt} items of {product.Name} were sold successfully";
            }
            else
            {
                return "Insufficient stock";
            }
        }

        public List<StockProductViewModel> GetSalesProductList()
        {

            throw new NotImplementedException();
        }

        public int GetProductQuantity(int productId)
        {
            return (from s in _stock
                    where s.ProductId == productId
                    select s.ProductQuantity).FirstOrDefault();
        }


        public void SetData(List<Stock> stocks)
        {
            jsonString = JsonConvert.SerializeObject(stocks);
            File.WriteAllText(jsonFilePath, jsonString);
        }
    }
}
