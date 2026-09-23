using DAL.DALInterfaces;
using DAL.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALServises
{
    public class ProductDALServise:IProductDALServise
    {
        //IMapper mapper;
        DelicatessenProjectContext DelicatessenContext;
        public ProductDALServise(DelicatessenProjectContext DelicatessenProjectContext)
        {
            DelicatessenContext = DelicatessenProjectContext;
        }
        public List<Product> getAllProductsCards()
        {
            return DelicatessenContext.Products.Include(p => p.Category).ToList();
        }

        public Product GetProductByName (string name)
        {
            return DelicatessenContext.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductName == name);
        }

        public List<Product> GetProductsByCategoryName(string categoryName)
        {
            return DelicatessenContext.Products
                       .Include(p => p.Category)
                       // סינון המוצרים כאשר שם הקטגוריה שווה לשם שהתקבל
                       .Where(p => p.Category.CategoryName == categoryName)
                       .ToList();
        }

        public List<Product> GetProductsByFilter(string filter)
        {
            return DelicatessenContext.Products
                       .Include(p => p.Category)
                       .Where(p => p.FilterProduct == filter)
                       .ToList();
        }

        public List<Category> GetAllCategories()
        {
            return DelicatessenContext.Categories.ToList();
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            return await DelicatessenContext.Products
                .Include(p => p.Category) // מביא את שם הקטגוריה
                .Include(p => p.HighQuantities) // מביא את המדבקות מהטבלה השנייה
                .FirstOrDefaultAsync(p => p.ProductName == name);
        }
    }
}
