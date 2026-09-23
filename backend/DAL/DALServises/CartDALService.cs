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
    public class CartDALService: ICartDALService
    {
        //IMapper mapper;
        DelicatessenProjectContext DelicatessenContext;
        public CartDALService(DelicatessenProjectContext DelicatessenProjectContext)
        {
            DelicatessenContext = DelicatessenProjectContext;
        }

        // 1. קבלת סל קניות פעיל
        public Order GetActiveCartByCustomerId(int customerId)
        {
            // נניח שסטטוס ה-Cart הוא "Open" או "Pending"
            return this.DelicatessenContext.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.CustomerId == customerId && o.Status == "Cart");
        }

        // 2. שמירת פריט בסל (הפונקציה תטפל בהוספה/עדכון בהתבסס על ה-ID)
        public OrderItem AddOrUpdateOrderItem(OrderItem item)
        {
            if (item.OrderItemId == 0)
            {
                this.DelicatessenContext.OrderItems.Add(item);
            }
            else
            {
                this.DelicatessenContext.OrderItems.Update(item);
            }
            this.DelicatessenContext.SaveChanges();
            return item;
        }

        // 3. הוספת הזמנה חדשה (סל חדש)
        public Order AddOrder(Order order)
        {
            this.DelicatessenContext.Orders.Add(order);
            this.DelicatessenContext.SaveChanges();
            return order;
        }

        public Product GetProductById(int productId)
        {
            return this.DelicatessenContext.Products.FirstOrDefault(p => p.ProductId == productId);
        }

        public OrderItem GetOrderItemById(int orderItemId)
        {
            return this.DelicatessenContext.OrderItems.Include(oi => oi.Order).FirstOrDefault(oi => oi.OrderItemId == orderItemId);
        }

        public void DeleteOrderItem(int orderItemId)
        {
            // 1. יצירת אובייקט 'דמה' או שליפת האובייקט
            OrderItem itemToDelete = new OrderItem { OrderItemId = orderItemId };

            // 2. חיבור האובייקט לקונטקסט
            this.DelicatessenContext.OrderItems.Attach(itemToDelete);

            // 3. סימונו למחיקה
            this.DelicatessenContext.OrderItems.Remove(itemToDelete);

            // 4. שמירת שינויים
            this.DelicatessenContext.SaveChanges();
        }


        public Order UpdateOrder(Order order)
        {
            // 1. הוראה ל-Entity Framework לסמן את האובייקט ככזה שצריך לעדכן
            this.DelicatessenContext.Orders.Update(order);

            // 2. שמירת השינויים במסד הנתונים
            this.DelicatessenContext.SaveChanges();

            // 3. החזרת האובייקט המעודכן
            return order;
        }

        public List<Order> GetCompletedOrdersByCustomerId(int customerId)
        {
            return this.DelicatessenContext.Orders
                       // חובה לטעון את פרטי הפריטים הקשורים לכל הזמנה
                       .Include(o => o.OrderItems)
                           // וגם את פרטי המוצר עבור כל פריט (כדי שיהיה שם המוצר)
                           .ThenInclude(oi => oi.Product)
                       // סינון: רק הזמנות שבוצעו (סטטוס "Ordered")
                       .Where(o => o.CustomerId == customerId && o.Status != "Cart")
                       // מיון: הצגת ההזמנות החדשות ביותר תחילה
                       .OrderByDescending(o => o.CreatedAt)
                       .ToList();
        }
    }
}
