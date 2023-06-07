using HW5.DataBase;
using HW5.Interface;
using HW5.Domain;
using HW5.Interface.Dto;

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
                                "3.Viewing sales products list" +
                              "\n4.Buy a product for stock\n" +
                              "5.Sale a product from stock\n6.Exit");
            string menuOption = Console.ReadLine();

            if (menuOption == "1")
            {
                Console.Clear();
                Console.Write("Enter the product name (pattern: Abcdf_123): ");
                AddProductDto addProductDto = new AddProductDto();
                addProductDto.Name = Console.ReadLine();
                string message = productRepository.Add(addProductDto);
                Console.Clear();
                Console.WriteLine(message);
                Console.ReadKey();
                Console.Clear();
            }

            else if (menuOption == "2")
            {
                Console.Clear();
                var productsDto = productRepository.GetList();
                Console.WriteLine("product Id and product name:");
                foreach (var productDto in productsDto)
                {
                    Console.WriteLine(productDto.Id + "  " + productDto.Name);
                }
                Console.ReadKey();
                Console.Clear();
            }

            else if (menuOption == "3")
            {
                Console.Clear();
                var productsInStock = stockRepository.GetSalesProductList();
                Console.WriteLine("Id, name, quantity and price:");
                foreach (var productInStock in productsInStock)
                {
                    Console.WriteLine(productInStock.ProductId + " " +
                                        productInStock.Name + " " +
                                        productInStock.ProductQuantity
                                        + " " + productInStock.ProductPrice);
                }
                Console.ReadKey();
                Console.Clear();
            }

            else if (menuOption == "4")
            {
                try
                {
                    Console.Clear();
                    BuyStockDto productInStockDto = new BuyStockDto();
                    Console.Write("Name: ");
                    productInStockDto.Name = Console.ReadLine();
                    Console.Write("Quantity: ");
                    productInStockDto.ProductQuantity = int.Parse(Console.ReadLine());
                    Console.Write("Price: ");
                    productInStockDto.ProductPrice = decimal.Parse(Console.ReadLine());
                    string message = stockRepository.Buy(productInStockDto);
                    Console.Clear();
                    Console.WriteLine(message);
                    Console.ReadKey();
                    Console.Clear();
                }
                catch
                {
                    Console.WriteLine("The format of quantity " +
                                 "and price of product is the integer.\n");
                }

            }

            else if (menuOption == "5")
            {
                try
                {
                    Console.Clear();
                    Console.Write("Product Id: ");
                    int productId = int.Parse(Console.ReadLine());
                    Console.Write("Number of product: ");
                    int cnt = int.Parse(Console.ReadLine());
                    string message = stockRepository.Sale(productId, cnt);
                    Console.Clear();
                    Console.WriteLine(message);
                    Console.ReadKey();
                    Console.Clear();

                }
                catch
                {
                    Console.WriteLine("The Id and number of product " +
                                         "is the integer.\n");
                }
            }
            else if (menuOption == "6")
            {
                Console.Clear();
                Console.WriteLine("Bye");
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid input\n");
            }

        } while (true);
    }
}