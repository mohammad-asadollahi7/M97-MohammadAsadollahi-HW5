
using HW5.Domain;
using HW5.Interface.Dto;

namespace HW5.Interface;

public interface IProductRepository
{
    string Add(AddProductDto product);
    List<ProductsDto> GetList();
    string GetById(int? Id);
}
