using System;

namespace StackWars
{
    internal class CostAttribute : Attribute
    {
        public int Cost { get; set; } = 0;
    }
}