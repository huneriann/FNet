using System;

namespace FNet.FSA.Core.Model
{
    public class DirectoryInfo
    {
        private DirectoryState _state;
        public DirectoryState State
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

        public DirectoryInfo(string path, DirectoryState state, string info)
        {
            this._path = path;
            this._state = state;
            this._info = info;
            this._dateTime = DateTime.Now;
        }

        public DirectoryInfo() { }
    }
}
