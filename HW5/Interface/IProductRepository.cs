
using HW5.Domain;

namespace HW5.Interface
{
    public interface IProductRepository
    {
        string AddProduct(Product product);
        List<Product> GetProductList();
        string GetProductById(int? Id);
    }
}
