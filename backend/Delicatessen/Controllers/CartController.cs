using BLL.BLLInterfaces;
using DTOs;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Delicatessen.Controllers
{
    [Route("api/[controller]")] // הנתיב יהיה: /api/cart
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBLLService _cartService;

        public CartController(ICartBLLService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("AddToCart")] // POST /api/cart/AddToCart
        public ActionResult<OrderDto> AddToCart([FromBody] CartItemRequestDto itemDto)
        {
            if (itemDto.CustomerId <= 0)
            {
                return Unauthorized("נדרש להתחבר למערכת.");
            }

            OrderDto updatedCart = _cartService.AddToCart(itemDto);

            if (updatedCart == null)
            {
                return StatusCode(500, "שגיאה פנימית בעת עדכון הסל.");
            }

            // קוד 200 OK עם נתוני הסל המעודכנים
            return Ok(updatedCart);
        }

        [HttpGet("{customerId}")] // GET /api/cart/{customerId}
        public ActionResult<CartResponseDto> GetCart(int customerId)
        {
            // נניח שאת מוודאת שה-customerId תואם למשתמש המחובר (לוגיקת אבטחה)

            CartResponseDto cart = _cartService.GetCart(customerId);

            if (cart == null)
            {
                // אם אין סל, אפשר להחזיר 200 עם סל ריק או 204 No Content
                return Ok(new CartResponseDto { CustomerId = customerId }); // מחזיר סל ריק
            }

            return Ok(cart);
        }


        [HttpPut("updateItem")] // PUT /api/cart/updateItem
        public ActionResult<CartResponseDto> UpdateItemQuantity([FromBody] UpdateCartItemQuantityDto updateDto)
        {
            try
            {
                CartResponseDto updatedCart = _cartService.UpdateItemQuantity(updateDto);
                return Ok(updatedCart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("deleteItem/{orderItemId}/{customerId}")] // DELETE /api/cart/deleteItem/123/50
        public ActionResult<CartResponseDto> DeleteItem(int orderItemId, int customerId)
        {
            try
            {
                CartResponseDto updatedCart = _cartService.DeleteItem(orderItemId, customerId);
                return Ok(updatedCart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("place")] // POST /api/cart/place
        public ActionResult<OrderDto> PlaceOrder([FromBody] PlaceOrderRequestDto orderDto)
        {
            try
            {
                OrderDto newOrder = _cartService.PlaceOrder(orderDto);
                // קוד 201 Created מצוין להזמנה שבוצעה
                return StatusCode(201, newOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetCustomerOrderHistory/{customerId}")] // GET /api/orders/GetCustomerOrderHistory/{customerId}
        public ActionResult<List<OrderDto>> GetCustomerOrderHistory(int customerId)
        {
            List<OrderDto> orders = _cartService.GetCustomerOrderHistory(customerId);

            return Ok(orders);
        }
    }
}
