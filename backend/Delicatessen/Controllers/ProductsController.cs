using BLL.BLLInterfaces;
using DAL.models;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Delicatessen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        IProductBLLServise bLLServise;
        public ProductsController(IProductBLLServise bLLServise)
        {
            this.bLLServise = bLLServise;
        }

        [HttpGet("getAllProductsCards")]
        public List<ProductCardDto> getAllProductsCards()
        {
            return bLLServise.getAllProductsCards();
        }

        // פונקציה המחזירה את פרטי המוצר המלאים לא כולל המדבקות האדומות
        [HttpGet("GetProductByName/{name}")] // נתיב: /api/products/GetProductByName/פטונה
        public ActionResult<ProductShowDto> GetProductByName(string name)
        {
            var productDto = bLLServise.GetProductByName(name);

            if (productDto == null)
            {
                return NotFound($"מוצר בשם {name} לא נמצא.");
            }

            return Ok(productDto);
        }

        [HttpGet("GetProductsByCategoryName/{categoryName}")]
        public ActionResult<List<ProductCardDto>> GetProductsByCategoryName(string categoryName)
        {
            List<ProductCardDto> productDtos = bLLServise.GetProductsByCategoryName(categoryName);

            if (productDtos == null || productDtos.Count == 0)
            {
                return NotFound($"לא נמצאו מוצרים בקטגוריה '{categoryName}'.");
            }

            return Ok(productDtos);
        }

        [HttpGet("GetProductsByFilter/{filter}")]
        public ActionResult<List<ProductCardDto>> GetProductsByFilter(string filter)
        {
            List<ProductCardDto> productDtos = bLLServise.GetProductsByFilter(filter);

            if (productDtos == null || productDtos.Count == 0)
            {
                return NotFound($"לא נמצאו מוצרים תחת הסינון: '{filter}'.");
            }

            return Ok(productDtos);
        }


        [HttpGet("GetAllCategories")]
        public ActionResult<List<CategoryDto>> GetAllCategories()
        {
            var categoryDtos = bLLServise.GetAllCategories();

            if (categoryDtos == null || !categoryDtos.Any())
            {
                return NotFound("לא נמצאו קטגוריות.");
            }

            return Ok(categoryDtos);
        }


        // פונקציה המחזירה את פרטי המוצר המלאים כולל המדבקות האדומות
        [HttpGet("GetProductByNamefull/{name}")]
        public async Task<ActionResult<ProductShowDto>> GetProductByNamefull(string name)
        {
            var product = await bLLServise.GetProductDetailsByName(name);

            if (product == null) return NotFound();

            return Ok(product);
        }
    }
}
