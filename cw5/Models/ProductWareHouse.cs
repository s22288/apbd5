using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Models
{
    public class ProductWareHouse
    {
        public int IdProductWarehouse { get; set; }
        public int IdWarehouse { get; set; }
        public int IdProduct { get; set; }
        public int IdOrder { get; set; }
        public int Amout { get; set; }
        public double Price { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
