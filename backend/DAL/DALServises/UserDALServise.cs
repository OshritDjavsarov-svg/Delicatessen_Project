using DAL.DALInterfaces;
using DAL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALServises
{
    public class UserDALServise: IUserDALServise
    {
        DelicatessenProjectContext DelicatessenContext;
        public UserDALServise(DelicatessenProjectContext DelicatessenProjectContext)
        {
            DelicatessenContext = DelicatessenProjectContext;
        }

        // מתודה 1: הוספת משתמש חדש
        public Customer AddCustomer(Customer customer)
        {
            this.DelicatessenContext.Customers.Add(customer);
            this.DelicatessenContext.SaveChanges();
            return customer; // מחזיר את האובייקט המעודכן עם CustomerId
        }

        // מתודה 2: בדיקת קיום משתמש לפי אימייל (חיוני להרשמה)
        public Customer GetCustomerByEmail(string email)
        {
            return this.DelicatessenContext.Customers.FirstOrDefault(c => c.Email == email);
        }
    }
}
