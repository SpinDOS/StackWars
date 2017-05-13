using StackWars.Logger;

namespace StackWars.Commands
{
    public abstract class Command
    {
        protected Command(Army source, int? sourceUnitIndex)
        {
            SourceArmy = source;
            SourceUnitIndex = sourceUnitIndex;
        }

        public Army SourceArmy { get; }
        public int? SourceUnitIndex { get; }
        public abstract void Execute(ILogger logger);
        public abstract void Undo(ILogger logger);
    }
}