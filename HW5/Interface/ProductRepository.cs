using HW5.DataBase;
using HW5.Domain;
using HW5.Interface.Dto;
using IronBarCode;
using System;
using System.Reflection.Metadata;
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

        public string Add(AddProductDto addProductDto)
        {

            bool isValid = CheckProductName(addProductDto.Name);
            if (isValid)
            {
                var product = new Product
                {
                    Name = addProductDto.Name,
                    Id = SetProductId(),
                    BarCode = SetProductBarCode(),
                };

                _dbContext.db.Add(product);
                _dbContext.SetData();
                return "The product was added successfully.";
            }
            else
            {
                return "The selected product name is" +
                            " incorrect or duplicate.";
            }
        }

        public string GetProductById(int? Id)
        {
            var product = _dbContext.db.FirstOrDefault(p => p.Id == Id);
            return product.Name;
        }

        public List<ProductsDto> GetList()
        {
            var products = _dbContext.db;
            return MapToDto(products);
        }
        private List<ProductsDto> MapToDto(List<Product> products)
        {
            List<ProductsDto> productsDto = new List<ProductsDto>();
            foreach (var product in products)
            {
                productsDto.Add(new ProductsDto() 
                {
                    Id = product.Id, 
                    Name = product.Name 
                });
            }
            return productsDto;
        }


        private bool CheckProductName(string productName)
        {
            var sameName = _dbContext.db.FirstOrDefault(p => p.Name == productName);
            if (sameName == null)
            {
                return Regex.IsMatch(productName, "^[A-Z]{1}[a-z]{3}[a-zA-Z0-9]{1}_{1}[0-9]{3}$");
            }
            else
            {
                return false;
            }
        }

        private int SetProductId()
        {
            return _dbContext.db.Count() + 1;
        }
        private string SetProductBarCode()
        {
            System.Random random = new System.Random();
            return random.Next().ToString();
        }
    }
}
