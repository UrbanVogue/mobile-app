using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanVogue.Models.Response
{
    public class CartProductResponse
    {
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public decimal Discount { get; set; }
        public int Count { get; set; }
    }
}
