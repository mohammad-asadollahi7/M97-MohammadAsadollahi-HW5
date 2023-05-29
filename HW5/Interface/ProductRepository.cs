using HW5.DataBase;
using HW5.Domain;
using IronBarCode;
using System;
using System.Text;
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

        public string Add(Product product)
        {

            bool isValid = CheckProductName(product.Name);
            if (isValid)
            {
                product.Id = SetProductId();
                product.BarCode = SetProductBarCode();
                _dbContext.db.Add(product);
                _dbContext.SetData();
                return "The product was added successfully.";
            }
            else
            {
                return "The selected product name is not correct.";
            }
        }

        public string GetProductById(int? Id)
        {
            var product = _dbContext.db.FirstOrDefault(p => p.Id == Id);
            return product.Name;
        }

        public List<Product> GetList()
        {
            return _dbContext.db;
        }

        private bool CheckProductName(string productName)
        {
            return Regex.IsMatch(productName, "^[A-Z]{1}[a-z]{3}[a-zA-Z0-9]{1}_{1}[0-9]{3}$");
        }

        private int SetProductId()
        {
            return _dbContext.db.Count() + 1;
        }
        private string SetProductBarCode()
        {
            System.Random r = new System.Random();
            int f = r.Next();

            GeneratedBarcode myBarcode = IronBarCode.BarcodeWriter.CreateBarcode
                                (Convert.ToString(f), BarcodeWriterEncoding.Code128);
            return myBarcode.ToString();
        }
    }
}
