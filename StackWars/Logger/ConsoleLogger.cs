using System;

namespace StackWars.Logger
{
    public sealed class ConsoleLogger : ILogger
    {
        public void Log(string message) { Console.WriteLine(message); }
    }
}