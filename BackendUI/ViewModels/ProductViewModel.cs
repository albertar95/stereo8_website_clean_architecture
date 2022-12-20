using Domain;

namespace BackendUI.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; } = new Product()!;
        public IEnumerable<Category> Categories { get; set; } = new List<Category>()!;
        public IEnumerable<Product> Products { get; set; } = new List<Product>()!;
        public IEnumerable<Domain.File> Files { get; set; } = new List<Domain.File>()!;
    }
}
