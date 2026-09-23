using AutoMapper;
using BLL.BLLInterfaces;
using DAL.DALInterfaces;
using DAL.models;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLLServises
{
    public class ProductBLLServise: IProductBLLServise
    {
        IProductDALServise dALServise;
        private readonly IMapper _mapper;
        public ProductBLLServise(IProductDALServise dALServise, IMapper mapper)
        {
            this.dALServise = dALServise;
            _mapper = mapper;
        }
        public List<ProductCardDto> getAllProductsCards()
        {
            List<Product> products = dALServise.getAllProductsCards();
            List<ProductCardDto> productDtos = _mapper.Map<List<ProductCardDto>>(products);
            return productDtos;
        }

        public ProductShowDto GetProductByName(string name)
        {
            Product productEntity = dALServise.GetProductByName(name);
            if (productEntity == null)
            {
                return null;
            }
            ProductShowDto productDto = _mapper.Map<ProductShowDto>(productEntity);
            return productDto;
        }

        public List<ProductCardDto> GetProductsByCategoryName(string categoryName)
        {
            List<Product> products = dALServise.GetProductsByCategoryName(categoryName);

            List<ProductCardDto> productDtos = _mapper.Map<List<ProductCardDto>>(products);

            return productDtos;
        }

        public List<ProductCardDto> GetProductsByFilter(string filter)
        {
            List<Product> products = dALServise.GetProductsByFilter(filter);

            // 2. המרת רשימת Entities ל-List<ProductCardDto> באמצעות AutoMapper
            List<ProductCardDto> productDtos = _mapper.Map<List<ProductCardDto>>(products);

            return productDtos;
        }

        public List<CategoryDto> GetAllCategories()
        {
            List<Category> categories = dALServise.GetAllCategories();
            return _mapper.Map<List<CategoryDto>>(categories);
        }


        public async Task<ProductShowDto> GetProductDetailsByName(string name)
        {
            // 1. מבקשים מה-DAL את המוצר מהדאטה
            var productEntity = await dALServise.GetProductByNameAsync(name);

            if (productEntity == null) return null;

            // 2. ממירים אותו ל-DTO שנשלח לאנגולר
            return _mapper.Map<ProductShowDto>(productEntity);
        }

    }
}
