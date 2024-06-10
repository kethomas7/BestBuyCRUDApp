using BestBuyCRUDApp.Models;

namespace BestBuyCRUDApp.Data
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
        void UpdateProduct(Product product);
        void InsertProduct(Product productToInsert);
        IEnumerable<Category> GetCategories();
        Product AssignCategory();

        void DeleteProduct(Product product);
    }
}
