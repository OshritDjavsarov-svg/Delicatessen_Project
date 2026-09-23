using DAL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALInterfaces
{
    public interface IProductDALServise
    {
        public List<Product> getAllProductsCards();
        public Product GetProductByName(string name);
        public List<Product> GetProductsByCategoryName(string categoryName);
        public List<Product> GetProductsByFilter(string filter);
        public List<Category> GetAllCategories();

        public Task<Product> GetProductByNameAsync(string name);
    }
}
