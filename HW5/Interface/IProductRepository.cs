
using HW5.Domain;

namespace HW5.Interface
{
    public interface IProductRepository
    {
        string Add(Product product);
        List<Product> GetList();
        string GetProductById(int? Id);
    }
}
