using Fiorello.Areas.Admin.ViewModels.Product;
using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Services
{
    public class ProductService : IProductService
    {


        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(m => m.Images).ToListAsync();
        }

        public async Task<List<Product>> GetAllWithIncludesAsync()
        {
            return await _context.Products.Include(m=>m.Images).Include(m=>m.Category).ToListAsync();
        }

        public async Task<Product> GetByIdAsnyc(int? id)
        {
            return await _context.Products.FindAsync(id); ;
        }

        public async Task<Product> GetByIdWithImageAsnyc(int? id)
        {
           return await _context.Products.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);
        }      

        public List<ProductVM> GetMappedDatas(List<Product> products)
        {
            List<ProductVM> list = new();
            foreach (var product in products)
            {
                list.Add(new ProductVM
                {
                    Id=product.Id,
                    Name=product.Name,
                    Description=product.Description,
                    Image=product.Images.Where(m=>m.IsMain).FirstOrDefault().Image,
                    CategoryName=product.Category.Name,
                    Price=product.Price
                    

                });

            }

            return list;
        }

        public async Task<Product> GetWithIncludesAsync(int? id)
        {
            return await _context.Products.Where(m => m.Id == id).Include(m => m.Images).Include(m => m.Category).FirstOrDefaultAsync();
        }

        public ProductDetailVM GetMappedData(Product product)
        {
            return new ProductDetailVM
            {

                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryName=product.Category.Name,
                CreatedDate=product.CreatedDate.ToString("MMMM dd, yyyy"),
                Images=product.Images.Select(m=> m.Image)

            };
        }
    }
}
