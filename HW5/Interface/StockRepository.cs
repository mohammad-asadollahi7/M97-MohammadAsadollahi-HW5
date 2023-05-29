using HW5.DataBase;
using HW5.Domain;


namespace HW5.Interface
{
    public class StockRepository : IStockRepository
    {
        private readonly DBContext<Stock> _dbContext;
        private readonly DBContext<Product> _productDbContext;

        public StockRepository(DBContext<Stock> dbContext,
                                DBContext<Product> productDbContext)
        {
            _dbContext = dbContext;
            _productDbContext = productDbContext;
        }

        public string Buy(Stock productInStock)
        {
            var existProduct = _dbContext.db.FirstOrDefault
                            (s => s.Name == productInStock.Name);

            if (existProduct != null)
            {

                decimal productPrice = ((productInStock.ProductPrice * productInStock.ProductQuantity) +
                    (existProduct.ProductPrice * existProduct.ProductQuantity))
                    / (productInStock.ProductQuantity + existProduct.ProductQuantity);
                Math.Round(productPrice, 1);
                existProduct.ProductPrice = productPrice;

                existProduct.ProductQuantity += productInStock.ProductQuantity;

                _dbContext.SetData();
                return $"The {existProduct.Name} was updated.";
            }

            else
            {
                productInStock.StockId = _dbContext.db.Count() + 1000;

                var product = (from p in _productDbContext.db
                               where p.Name == productInStock.Name
                               select p).FirstOrDefault();
                if (product != null )
                {
                    productInStock.ProductId = product.Id;

                    _dbContext.db.Add(productInStock);
                    _dbContext.SetData();

                    return $"The {product.Name} was added to stock.";
                }
                else
                {
                    return $"The {product.Name} was not in the products list.";
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
                _dbContext.SetData();
                return $"{cnt} items of {product.Name} were sold successfully";
            }
            else
            {
                return "Insufficient stock";
            }
        }

        public List<StockProductViewModel> GetSalesProductList()
        {
            var saleslist = (from s in _dbContext.db
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

            string? projectPath = Directory.GetParent
                                     (AppDomain.CurrentDomain.BaseDirectory)?
                                     .Parent?.Parent?.Parent?.FullName;
            string? txtFilePath = Path.Combine(projectPath,
                                $"DataBase/SalesList.txt");

            using (TextWriter tw = File.CreateText(txtFilePath))
            {
                foreach (var s in saleslist)
                {
                    tw.WriteLine(s.ProductId + " " + s.BarCode + " " +
                                    s.Name + " " + s.ProductPrice +
                                    " " + s.ProductQuantity);
                }
            }
            return saleslist;
        }


        private int GetProductQuantity(int productId)
        {
            return (from s in _dbContext.db
                    where s.ProductId == productId
                    select s.ProductQuantity).FirstOrDefault();
        }
    }
}
