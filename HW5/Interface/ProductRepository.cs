using HW5.DataBase;
using HW5.Domain;
using System.Text.RegularExpressions;

namespace HW5.Interface
{
    public class ProductRepository : IProductRepository
    {
        private readonly DBContext<Product> _dbContext;
        public ProductRepository(DBContext<Product> dBContext)
        {
            _dbContext = dBContext;
        }

        public string AddProduct(Product product)
        {
            
            bool isValid = CheckProductName(product.Name);
            if (isValid)
            {
                _dbContext.db.Add(product);
                _dbContext.SetData();
                return "The product was added successfully.";
            }
            else
            {
                return "The selected product name is not correct.";
            }
        }

        public string GetProductById(int Id)
        {
            var product = _dbContext.db.FirstOrDefault(p => p.Id == Id);
            return product.Name;
        }

        public List<Product> GetProductList()
        {
            return _dbContext.db;
        }

        public bool CheckProductName(string productName)
        {
            return Regex.IsMatch(productName, "^[A-Z]{1}[a-z]{3}[a-zA-Z0-9]{1}_{1}[0-9]{3}$");
        }

    }
}
