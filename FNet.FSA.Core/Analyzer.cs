using System.IO;
using System.Threading.Tasks;

namespace FNet.FSA.Core
{
    public delegate void Log(Model.DirectoryInfo info);
    public delegate Task LogAsync(Model.DirectoryInfo info);

    public class Analyzer
    {
        FileSystemWatcher watcher;
        public Log log;

        private string _path;
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public void Execute()
        {
            watcher = new FileSystemWatcher(this.Path);

            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.Error += OnError;

            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }

        public void Dispose()
        {
            watcher.Dispose();
        }

        #region Singleton
        private static Analyzer obj;
        private Analyzer() { }
        public static Analyzer GetObject()
        {
            if(obj == null)
            {
                obj = new Analyzer();
            }
            return obj;
        }
        #endregion

        #region handles
        public void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            string path = e.FullPath;
            log?.Invoke(new Model.DirectoryInfo(path, Model.DirectoryState.Changed));
        }

        public void OnCreated(object sender, FileSystemEventArgs e)
        {
            string path = e.FullPath;
            log?.Invoke(new Model.DirectoryInfo(path, Model.DirectoryState.Created));
        }

        public void OnDeleted(object sender, FileSystemEventArgs e)
        {
            string path = e.FullPath;
            log?.Invoke(new Model.DirectoryInfo(path, Model.DirectoryState.Deleted));
        }

        public void OnRenamed(object sender, RenamedEventArgs e)
        {
            string oldPath = e.OldFullPath;
            string newPath = e.FullPath;
            log?.Invoke(new Model.DirectoryInfo("\nOld Path: " + oldPath + "\nNew Path: " + newPath, Model.DirectoryState.Renamed));
        }

        public void OnError(object sender, ErrorEventArgs e)
        {
            log?.Invoke(new Model.DirectoryInfo(e.GetException().Message, Model.DirectoryState.Error));
        }
        #endregion
    }
}
