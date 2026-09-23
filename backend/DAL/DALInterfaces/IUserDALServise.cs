using DAL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALInterfaces
{
    public interface IUserDALServise
    {
        public Customer AddCustomer(Customer customer);

        // בדיקה אם המייל כבר קיים
        public Customer GetCustomerByEmail(string email);
    }
}
