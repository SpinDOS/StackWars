using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackWars
{
    class CostAttribute : Attribute
    {
        public int Cost { get; set; } = 0;
    }
}
