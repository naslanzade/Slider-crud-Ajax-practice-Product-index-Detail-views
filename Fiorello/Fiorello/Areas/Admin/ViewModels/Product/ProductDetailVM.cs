namespace Fiorello.Areas.Admin.ViewModels.Product
{
    public class ProductDetailVM
    {
      
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<string> Images { get; set; }
        public string  CreatedDate { get; set; }
    }
}
