using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    public sealed class ProxyClonerUnit : WrapperUnit, IClonerUnit
    {
        public ProxyClonerUnit(IClonerUnit baseUnit, ILogger logger)
            : base(baseUnit as Unit) { Logger = logger; }
        public ILogger Logger { get; }

        public override int CurrentHealth
        {
            set
            {
                if (BaseUnit.CurrentHealth == value)
                    return;
                if (value <= 0)
                    Logger?.Log($"{DateTime.Now}: Cloner is dead. " +
                            $"{this} health is changed from {this.CurrentHealth} to {value}");
                BaseUnit.CurrentHealth = value;
            }
        }

        public int CloneRange
        {
            get => (BaseUnit as IClonerUnit).CloneRange;
            set => (BaseUnit as IClonerUnit).CloneRange = value;
        }
    }
}
