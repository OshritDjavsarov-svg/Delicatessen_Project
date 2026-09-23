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
    public class CartBLLService: ICartBLLService
    {
        private readonly ICartDALService cartdALServise;
        private readonly IMapper _mapper;
        public CartBLLService(ICartDALService cartdALServise, IMapper mapper)
        {
            this.cartdALServise = cartdALServise;
            _mapper = mapper;
        }

        public decimal GetProductPrice(int productId)
        {
            // 1. קריאה ל-DAL לקבלת המוצר (לא צריך DTO, אלא את ה-Entity המלא)
            Product productEntity = cartdALServise.GetProductById(productId);

            // 2. בדיקה והחזרת המחיר
            if (productEntity == null)
            {
                // אם המוצר לא נמצא, אפשר לזרוק חריגה או להחזיר 0.
                throw new Exception($"Product with ID {productId} not found.");
            }

            return productEntity.ProductPrice;
        }

        public OrderDto AddToCart(CartItemRequestDto itemDto)
        {
            // 1. קבלת סל פעיל
            Order currentCart = cartdALServise.GetActiveCartByCustomerId(itemDto.CustomerId);

            if (currentCart == null)
            {
                // 2. אם אין סל: יצירת סל חדש
                currentCart = new Order
                {
                    CustomerId = itemDto.CustomerId,
                    Status = "Cart", // הגדרת סטטוס: סל קניות
                    CreatedAt = DateTime.Now,
                    OrderItems = new List<OrderItem>()
                };
                currentCart = cartdALServise.AddOrder(currentCart);
            }

            // 3. לוגיקה: בדיקה אם הפריט כבר קיים בסל
            OrderItem existingItem = currentCart.OrderItems.FirstOrDefault(oi => oi.ProductId == itemDto.ProductId);

            // חובה לקבל את המחיר הנוכחי של המוצר מה-BLLServise
            decimal productPrice = GetProductPrice(itemDto.ProductId);

            if (existingItem != null)
            {
                // 4א. אם הפריט קיים: עדכון כמות
                existingItem.Quantity += itemDto.Quantity;
                existingItem.OrderItemPrice = productPrice; // עדכון מחיר למקרה שהשתנה
                cartdALServise.AddOrUpdateOrderItem(existingItem);
            }
            else
            {
                // 4ב. אם הפריט חדש: יצירת OrderItem
                OrderItem newItem = new OrderItem
                {
                    OrderId = currentCart.OrderId,
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    OrderItemPrice = productPrice // שמירת מחיר המוצר הנוכחי
                };
                cartdALServise.AddOrUpdateOrderItem(newItem);
            }

            // 5. שליפת הסל המעודכן (יכול להיות פשוט currentCart, אבל עדיף לשלוף שוב לדיוק)
            Order updatedCart = cartdALServise.GetActiveCartByCustomerId(itemDto.CustomerId);

            // 6. המרה ל-OrderDto והחזרה
            return _mapper.Map<OrderDto>(updatedCart);
        }

        public CartResponseDto GetCart(int customerId)
        {
            // 1. קריאה ל-DAL לשליפת הסל המלא
            Order currentCart = cartdALServise.GetActiveCartByCustomerId(customerId);

            if (currentCart == null)
            {
                // אם אין סל, מחזירים סל ריק או null, תלוי איך ה-Controller מטפל בזה
                return null;
            }

            // 2. מיפוי ל-CartResponseDto והחזרה
            return _mapper.Map<CartResponseDto>(currentCart);
        }



        public CartResponseDto UpdateItemQuantity(UpdateCartItemQuantityDto updateDto)
        {
            var itemToUpdate = cartdALServise.GetOrderItemById(updateDto.OrderItemId);

            // בדיקה שהפריט קיים וגם שייך ללקוח הנכון
            if (itemToUpdate == null || itemToUpdate.Order.CustomerId != updateDto.CustomerId)
            {
                throw new Exception("הפריט לא נמצא בסל של הלקוח המבוקש");
            }

            itemToUpdate.Quantity = updateDto.NewQuantity;
            cartdALServise.AddOrUpdateOrderItem(itemToUpdate);

            return GetCart(updateDto.CustomerId);
        }



        public CartResponseDto DeleteItem(int orderItemId, int customerId)
        {
            // 1. קריאה ל-DAL לביצוע המחיקה
            cartdALServise.DeleteOrderItem(orderItemId);

            // 2. שליפת הסל המעודכן והחזרתו
            // שימוש חוזר ב-GetCart כדי להחזיר את נתוני הסל החדשים ל-Front-end
            return GetCart(customerId);
        }

        public OrderDto PlaceOrder(PlaceOrderRequestDto orderDto)
        {
            // 1. שליפת הסל הפעיל
            Order cartToOrder = cartdALServise.GetActiveCartByCustomerId(orderDto.CustomerId);

            if (cartToOrder == null || !cartToOrder.OrderItems.Any())
            {
                throw new Exception("Cannot place order: Cart is empty or not found.");
            }

            // 2. עדכון הסטטוס והמחיר הסופי (כולל עלות משלוח)
            cartToOrder.Status = "Ordered";

            // חישוב המחיר הסופי: סכום הפריטים + דמי משלוח (60)
            decimal itemsTotal = cartToOrder.OrderItems.Sum(oi => oi.Quantity * oi.OrderItemPrice);
            cartToOrder.TotalPrice = itemsTotal + 60; // כפי שמופיע ב-script_pay_place.js

            // 3. שמירת השינויים ב-DAL
            // חובה לוודא שקיימת מתודה לעדכון Order ב-DAL
            cartdALServise.UpdateOrder(cartToOrder);

            // 4. החזרה
            return _mapper.Map<OrderDto>(cartToOrder);
        }


        public List<OrderDto> GetCustomerOrderHistory(int customerId)
        {
            // 1. קריאה ל-DAL לשליפת רשימת ההזמנות
            List<Order> completedOrders = cartdALServise.GetCompletedOrdersByCustomerId(customerId);

            if (completedOrders == null || completedOrders.Count == 0)
            {
                return new List<OrderDto>(); // מחזיר רשימה ריקה אם לא נמצאו הזמנות
            }

            // 2. מיפוי הרשימה ל-List<OrderDto>
            return _mapper.Map<List<OrderDto>>(completedOrders);
        }
    }
}
