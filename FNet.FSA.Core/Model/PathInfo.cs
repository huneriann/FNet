using System;
using System.IO;

namespace FNet.FSA.Core.Model
{
    public class PathInfo
    {
        private PathState _state;
        public PathState State
        {
            get { return _state; }
            set { _state = value; }
        }

        private string _path;
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }

        private string _info;
        public string Info
        {
            get { return _info; }
            set { _info = value; }
        }

        private DirectoryInfo directoryInfo;
        public DirectoryInfo DirectoryInfo
        {
            get { return directoryInfo; }
            set { directoryInfo = value; }
        }

        private FileInfo fileInfo;
        public FileInfo FileInfo
        {
            get { return fileInfo; }
            set { fileInfo = value; }
        }


        public PathInfo(string path, PathState state, string info)
        {
            this.Path = path;
            this.State = state;
            this.Info = info;
            this.DateTime = DateTime.Now;
            this.DirectoryInfo = new DirectoryInfo(path);
            this.FileInfo = new FileInfo(path);
        }

        public PathInfo() { }
    }
}
