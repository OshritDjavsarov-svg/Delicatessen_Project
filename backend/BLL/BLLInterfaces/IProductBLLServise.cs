using DAL.models;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLLInterfaces
{
    public interface IProductBLLServise
    {
        public List<ProductCardDto> getAllProductsCards();
        public ProductShowDto GetProductByName(string name);
        public List<ProductCardDto> GetProductsByCategoryName(string categoryName);
        public List<ProductCardDto> GetProductsByFilter(string filter);
        public List<CategoryDto> GetAllCategories();

        public Task<ProductShowDto> GetProductDetailsByName(string name);
    }
}
