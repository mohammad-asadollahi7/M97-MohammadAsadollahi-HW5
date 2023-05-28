using HW5.DataBase;
using HW5.Interface;
using HW5.Domain;

class Program
{
    static void Main()
    {
        DBContext<Product> dbContext = new DBContext<Product>();
        IProductRepository productRepository = new ProductRepository(dbContext);

        DBContext<Stock> dbContext2 = new DBContext<Stock>();
        IStockRepository stockRepository = new StockRepository(dbContext2, dbContext);

        var final = stockRepository.GetSalesProductList();

        foreach (var v in final)
        {
            Console.WriteLine(v.Name); 
        }
    }
}