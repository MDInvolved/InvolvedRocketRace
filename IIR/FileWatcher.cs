using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIR
{
    public class FileWatcher
    {
        public FileWatcher(string path, string fileName, Action<object, FileSystemEventArgs> onChange)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;

            watcher.NotifyFilter = NotifyFilters.LastWrite;

            watcher.Filter = fileName;
            
            watcher.Changed += new FileSystemEventHandler(onChange);
            watcher.Created += new FileSystemEventHandler(onChange);
            
            watcher.EnableRaisingEvents = true;
        }
    }
}
