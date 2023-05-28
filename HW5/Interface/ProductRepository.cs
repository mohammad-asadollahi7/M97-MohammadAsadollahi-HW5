using HW5.Domain;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace HW5.Interface
{
    public class ProductRepository : IProductRepository
    {
        public List<Product>? products;
        private string? jsonFilePath;
        private string? jsonString;
        public ProductRepository()
        {
            string? projectPath = Directory.GetParent
                   (AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            jsonFilePath = Path.Combine(projectPath, "DataBase/Product.json");
            jsonString = File.ReadAllText(jsonFilePath);
            products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
        }

        public string AddProduct(Product product)
        {
            
            bool isValid = CheckProductName(product.Name);
            if (isValid)
            {
                products.Add(product);
                SetData(products);
                return "The product was added successfully.";
            }
            else
            {
                return "The selected product name is not correct.";
            }
        }

        public string GetProductById(int Id)
        {
            var product = products.FirstOrDefault(p => p.Id == Id);
            return product.Name;
        }

        public List<Product> GetProductList()
        {
            return products;
        }

        public bool CheckProductName(string productName)
        {
            return Regex.IsMatch(productName, "^[A-Z]{1}[a-z]{3}[a-zA-Z0-9]{1}_{1}[0-9]{3}$");
        }

        public void SetData(List<Product> products)
        {
            jsonString = JsonConvert.SerializeObject(products);
            File.WriteAllText(jsonFilePath, jsonString);
        }
    }
}
