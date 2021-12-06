using System;

namespace FNet.FSA.Console.CLI
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class CommandAttribute : Attribute
    {
        readonly string _path;
        public string Path
        {
            get { return _path; }
        }

        public CommandAttribute(string path)
        {
            this._path = path;
        }
    }
}
