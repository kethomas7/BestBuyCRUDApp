using BestBuyCRUDApp.Models;
using Dapper;
using System.Data;

namespace BestBuyCRUDApp.Data
{
    public class ProductRepository : IProductRepository
    {

        private readonly IDbConnection _connection;

        public ProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
       public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM products");
        }

        public Product GetProduct(int id)

        {
            return _connection.QuerySingle<Product>("SELECT * FROM products WHERE ProductID = @id", new {id});
        }

        public void UpdateProduct(Product product)
        {
            _connection.Execute("UPDATE products SET Name = @name, Price = @price WHERE ProductID = @id", new {name = product.Name , price = product.Price, id= product.ProductID});
        }

       public Product AssignCategory()
        {
            var categoryList = GetCategories();
            var product = new Product();
            product.Categories = categoryList;
            return product;
        }

       public IEnumerable<Category> GetCategories()
        {

            return _connection.Query<Category>("SELECT * FROM categories");
        }

       public void InsertProduct(Product productToInsert)
        {
            _connection.Execute("INSERT INTO products (Name,Price,CategoryID) VALUES (@name,@price,@categoryID)", new { name = productToInsert.Name, price = productToInsert.Price, categoryID = productToInsert.CategoryID });
        }

        void IProductRepository.DeleteProduct(Product product)
        {
            _connection.Execute("DELETE FROM REVIEWS WHERE ProductID = @id;", new { id = product.ProductID });
            _connection.Execute("DELETE FROM Sales WHERE ProductID = @id;", new { id = product.ProductID });
            _connection.Execute("DELETE FROM Products WHERE ProductID = @id;", new { id = product.ProductID });
        }
    }
}
