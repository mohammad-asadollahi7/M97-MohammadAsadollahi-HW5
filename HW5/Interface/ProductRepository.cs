using HW5.Domain;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace HW5.Interface
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product>? _products;
        private string? jsonFilePath;
        private string? jsonString;
        public ProductRepository()
        {
            string? projectPath = Directory.GetParent
                   (AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            jsonFilePath = Path.Combine(projectPath, "DataBase/Product.json");
            jsonString = File.ReadAllText(jsonFilePath);
            _products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
        }

        public string AddProduct(Product product)
        {
            
            bool isValid = CheckProductName(product.Name);
            if (isValid)
            {
                string json2 = JsonConvert.SerializeObject(product);
                using (FileStream fs = new FileStream(jsonFilePath, FileMode.Append))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(json2);
                    }


                }
                _products.Add(product);
                SetData(_products);
                return "The product was added successfully.";
                
            }
            else
            {
                return "The selected product name is not correct.";
            }
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

        public void SetData(List<Product> products)
        {
            jsonString = JsonConvert.SerializeObject(products);
            File.WriteAllText(jsonFilePath, jsonString);
        }
    }
}
