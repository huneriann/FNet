using System.IO;
using System.Threading.Tasks;

namespace FNet.FSA.Core
{
    public delegate void Log(Model.PathInfo info);
    public delegate Task LogAsync(Model.PathInfo info);

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
            if (obj == null)
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
                return;

            log?.Invoke(new Model.PathInfo(e.FullPath, Model.PathState.Changed, ""));
        }

        public void OnCreated(object sender, FileSystemEventArgs e)
        {
            log?.Invoke(new Model.PathInfo(e.FullPath, Model.PathState.Created, ""));
        }

        public void OnDeleted(object sender, FileSystemEventArgs e)
        {
            log?.Invoke(new Model.PathInfo(e.FullPath, Model.PathState.Deleted, ""));
        }

        public void OnRenamed(object sender, RenamedEventArgs e)
        {
            log?.Invoke(new Model.PathInfo(e.OldFullPath, Model.PathState.Renamed, "\nOld Path: " + e.OldFullPath + "\nNew Path: " + e.FullPath));
        }

        public void OnError(object sender, ErrorEventArgs e)
        {
            log?.Invoke(new Model.PathInfo(e.GetException().Message, Model.PathState.Error, ""));
        }
        #endregion
    }
}
