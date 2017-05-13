using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackWars.Logger
{
    public sealed class FileLogger : ILogger
    {
        public FileLogger(string filename)
        {
            File.Create(filename);
            Filename = filename;
        }
        public string Filename { get; }
        public void Log(string message) {  File.AppendAllText(Filename, message + Environment.NewLine);}
    }
}
