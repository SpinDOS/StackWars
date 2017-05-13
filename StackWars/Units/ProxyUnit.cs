using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    public sealed class ProxyUnit : WrapperUnit
    {
        public ProxyUnit(Unit baseUnit, ILogger logger) : base(baseUnit) { Logger = logger; }
        public ILogger Logger { get; }

        public override int CurrentHealth
        {
            set
            {
                if (BaseUnit.CurrentHealth == value)
                    return;
                Logger?.Log($"{DateTime.Now}: {this} health is changed from {this.CurrentHealth} to {value}");
                BaseUnit.CurrentHealth = value;
            }
        }

        public override Unit Clone() => new ProxyUnit(BaseUnit.Clone(), Logger);
    }
}
