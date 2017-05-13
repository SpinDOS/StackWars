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
        private static readonly Dictionary<string, FileLogger> Existing =
            new Dictionary<string, FileLogger>();

        public static FileLogger GetFileLogger(string filename)
        {
            if (Existing.ContainsKey(filename))
                return Existing[filename];
            var result = new FileLogger(filename);
            Existing.Add(filename, result);
            return result;
        }

        private StreamWriter _file;
        private FileLogger(string filename)
        {
            Filename = filename;
            _file = new StreamWriter(filename);
        }

        public string Filename { get; }

        public void Log(string message)
        {
            if (_file != null)
                _file.WriteLine(message);
            else
                throw new ObjectDisposedException("FileLogger");
        }

        public void Dispose()
        {
            if (_file == null)
                return;
            _file.Dispose();
            _file = null;
            Existing.Remove(Filename);
        }

        ~FileLogger() => Dispose();
    }
}
