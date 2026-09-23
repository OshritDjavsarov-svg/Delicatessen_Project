using DAL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALInterfaces
{
    public interface ICartDALService
    {
        // קבלת סל קניות (Order) פעיל למשתמש
        public Order GetActiveCartByCustomerId(int customerId);

        //  שמירת (הוספה/עדכון) פריט בודד
        public OrderItem AddOrUpdateOrderItem(OrderItem item);

        //  הוספת הזמנה חדשה (תיק עגלה)
        public Order AddOrder(Order order);

        public Product GetProductById(int productId);

        public OrderItem GetOrderItemById(int orderItemId);

        public void DeleteOrderItem(int orderItemId);

        public Order UpdateOrder(Order order);

        public List<Order> GetCompletedOrdersByCustomerId(int customerId);

    }
}
