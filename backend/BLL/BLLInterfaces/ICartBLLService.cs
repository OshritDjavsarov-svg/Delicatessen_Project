using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLLInterfaces
{
    public interface ICartBLLService
    {
        public OrderDto AddToCart(CartItemRequestDto itemDto);

        public decimal GetProductPrice(int productId);

        public CartResponseDto GetCart(int customerId);

        public CartResponseDto UpdateItemQuantity(UpdateCartItemQuantityDto updateDto);

        public CartResponseDto DeleteItem(int orderItemId, int customerId);

        public OrderDto PlaceOrder(PlaceOrderRequestDto orderDto);

        public List<OrderDto> GetCustomerOrderHistory(int customerId);
    }
}
