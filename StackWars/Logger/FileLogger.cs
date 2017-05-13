using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackWars.Logger
{
    public sealed class FileLogger : ILogger, IDisposable
    {
        private StreamWriter _file;
        public FileLogger(string filename)
        {
            _file = new StreamWriter(filename);
        }
        public void Log(string message)
        {
            if (_file != null)
                _file.WriteLine(message);
            else
                throw new ObjectDisposedException("FileLogger");
        }
        public void Dispose()
        {
            _file?.Dispose();
            _file = null;
        }

        ~FileLogger() => Dispose();
    }
}
