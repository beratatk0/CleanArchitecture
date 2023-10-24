using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Core.DTOs
{
    public abstract class BaseDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
