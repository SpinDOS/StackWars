namespace StackWars.Units
{
    public abstract class Unit
    {
        public virtual int MaxHealth { get; set; }
        public virtual int CurrentHealth { get; set; }
        public virtual int Attack { get; set; }
        public virtual int Defense { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name}: HP: {CurrentHealth}, Dmg: {Attack}, Def: {Defense}";
        }
    }
}