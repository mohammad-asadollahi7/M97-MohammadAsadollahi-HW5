using HW5.DataBase;
using HW5.Interface;
using HW5.Domain;

class Program
{
    static void Main()
    {
        DBContext<Product> productDbContext = new DBContext<Product>();
        DBContext<Stock> stockDbContext = new DBContext<Stock>();
        IProductRepository productRepository = new ProductRepository(productDbContext);
        IStockRepository stockRepository = new StockRepository(stockDbContext, productDbContext);

        do
        {
            Console.WriteLine("1.Add product\n2.Viewing products list\n" +
                             "3.Sale a product from stock" +
                              "\n4.Buy a product for stock\n" +
                              "5.Viewing sales prodcuts list\n6.Exit");
            string menuOption = Console.ReadLine();

            if (menuOption == "1")
            {
                Console.Clear();
                Console.WriteLine("Enter the name(pattern: Azxcs_123):");
                Product product = new Product();
                product.Name = Console.ReadLine();
                string message = productRepository.Add(product);
                Console.Clear();
                Console.WriteLine(message);
                Console.ReadKey();
            }
            else if (menuOption == "2")
            {
                Console.Clear();
                var products = productRepository.GetList();
                Console.WriteLine("product Id and product name:");
                foreach (var product in products)
                {
                    Console.WriteLine(product.Id + "  " + product.Name);
                }
                Console.ReadKey();
            }


        } while (true);
    }
}