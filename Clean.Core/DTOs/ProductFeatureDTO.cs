using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Core.DTOs
{
    public class ProductFeatureDTO
    {
        public int Id { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public string? Color { get; set; }
        public int ProductId { get; set; }
    }
}
