using HW5.Domain;
using System.Text.RegularExpressions;

namespace HW5.Interface
{
    public class ProductRepository : IProductRepository
    {
        public string AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public string GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductList()
        {
            throw new NotImplementedException();
        }

        public bool CheckProductName(string productName)
        {
            return Regex.IsMatch(productName, "^[A-Z]{1}[a-z]{3}[a-zA-Z0-9]{1}_{1}[0-9]{3}$");
        }
    }
}
