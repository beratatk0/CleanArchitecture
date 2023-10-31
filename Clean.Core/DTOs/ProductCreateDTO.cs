using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Core.DTOs
{
    public class ProductCreateDTO
    {
        public string? Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
    }
}
