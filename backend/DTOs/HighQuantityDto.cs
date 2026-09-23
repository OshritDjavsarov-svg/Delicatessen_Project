using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    // העתק של הדאטה ללא קשרי גומלין
    public class HighQuantityDto
    {
        public int Hqid { get; set; }

        public int ProductId { get; set; }

        public bool HighSalt { get; set; }

        public bool HighSugar { get; set; }

        public bool HighFat { get; set; }
    }
}
