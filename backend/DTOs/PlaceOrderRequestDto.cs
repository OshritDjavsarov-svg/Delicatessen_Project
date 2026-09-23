using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class PlaceOrderRequestDto
    {
        // חובה
        public int CustomerId { get; set; }
        // אם יש צורך להעביר פרטי כרטיס אשראי, זה *חייב* להיות דרך שירות תשלום מאובטח (Stripe/PayPal וכו')
        // כרגע נניח שאנו מסתפקים ב-CustomerID בלבד לצורך המטרה הפונקציונלית.
    }
}
