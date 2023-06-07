using Fiorello.Areas.Admin.ViewModels.Product;
using Fiorello.Models;

namespace Fiorello.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsnyc(int? id);
        Task<Product> GetByIdWithImageAsnyc(int? id);
        Task<List<Product>> GetAllWithIncludesAsync();
        List<ProductVM> GetMappedDatas(List<Product> products);
        Task<Product> GetWithIncludesAsync(int? id);
        ProductDetailVM GetMappedData(Product product);
    }
}
