using HW5.DataBase;
using HW5.Domain;
using HW5.Interface.Dto;

namespace HW5.Interface;

public class StockRepository : IStockRepository
{
    private readonly DBContext<Stock> _dbContext;
    private readonly DBContext<Product> _productDbContext;
    private readonly Log _log;

    public StockRepository(DBContext<Stock> dbContext,
                            DBContext<Product> productDbContext,
                            Log log)
    {
        _dbContext = dbContext;
        _productDbContext = productDbContext;
        _log = log;
    }

    public string Buy(BuyStockDto productInStockDto)
    {
        var productInStock = new Stock();
        productInStock.Name = productInStockDto.Name;
        productInStock.ProductPrice = productInStockDto.ProductPrice;
        productInStock.ProductQuantity = productInStockDto.ProductQuantity;

        var existProduct = _dbContext.db.FirstOrDefault
                        (s => s.Name == productInStock.Name);

        if (existProduct != null)
        {

            var productPrice = ((productInStock.ProductPrice * productInStock.ProductQuantity) +
                (existProduct.ProductPrice * existProduct.ProductQuantity))
                / (productInStock.ProductQuantity + existProduct.ProductQuantity);
            existProduct.ProductPrice = Math.Round(productPrice, 1);

            existProduct.ProductQuantity += productInStock.ProductQuantity;
            _log.Logger(productInStock, existProduct);
            _dbContext.Set();

            return $"The {existProduct.Name} was updated.";
        }

        else
        {
            var product = (from p in _productDbContext.db
                           where p.Name == productInStockDto.Name
                           select p).FirstOrDefault();

            if (product != null)
            {
                productInStock.StockId = _dbContext.db.Count() + 1000;
                productInStock.ProductId = product.Id;

                _dbContext.db.Add(productInStock);
                _dbContext.Set();
                _log.Logger(productInStock, productInStock);
                return $"The {product.Name} was added to stock.";
            }
            else
            {
                return $"The {productInStockDto.Name} was not in the products list.";
            }
        }
    }


    public string Sale(int ProductId, int cnt)
    {
        var product = _dbContext.db.FirstOrDefault(p => p.ProductId == ProductId);
        int quantity = GetProductQuantity(ProductId);
        if (quantity > cnt)
        {
            product.ProductQuantity -= cnt;
            _dbContext.Set();
            _log.Logger(product, cnt);
            return $"{cnt} items of {product.Name} were sold successfully";
        }
        else
        {
            return "Insufficient stock";
        }
    }

    public List<StockProductViewModel> GetSalesProductList()
    {
        var salesList = (from s in _dbContext.db
                         join p in _productDbContext.db
                         on s.ProductId equals p.Id
                         select new StockProductViewModel()
                         {
                             ProductId = p.Id,
                             BarCode = p.BarCode,
                             Name = s.Name,
                             ProductQuantity = s.ProductQuantity,
                             ProductPrice = s.ProductPrice
                         }).ToList();
        SaveInText(salesList);
        return salesList;
    }

    private void SaveInText(List<StockProductViewModel> salesList)
    {
        string? projectPath = Directory.GetParent
                              (AppDomain.CurrentDomain.BaseDirectory)?
                              .Parent?.Parent?.Parent?.FullName;
        string? txtFilePath = Path.Combine(projectPath,
                            $"DataBase/SalesList.txt");

        using (TextWriter tw = File.CreateText(txtFilePath))
        {
            foreach (var s in salesList)
            {
                tw.WriteLine(s.ProductId + " " + s.BarCode + " " +
                                s.Name + " " + s.ProductPrice +
                                " " + s.ProductQuantity);
            }
        }
    }
    private int GetProductQuantity(int productId)
    {
        return (from s in _dbContext.db
                where s.ProductId == productId
                select s.ProductQuantity).FirstOrDefault();
    }
}
