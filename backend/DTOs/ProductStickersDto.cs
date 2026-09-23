using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProductStickersDto
    {
        public int HighFat { get; set; }
        public int HighSugar { get; set; }
        public int HighSalt { get; set; }

        // פונקציה שהופכת את הנתונים למערך כפי שהאנגולר  מצפה
        public int[] ToArray() => new int[] { HighFat, HighSugar, HighSalt };
    }
}
