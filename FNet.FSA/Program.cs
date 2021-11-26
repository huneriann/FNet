using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FNet.FSA
{
    class Program
    {
        private const int AppDurationMinutes = 20;

        private static DateTime startTime = DateTime.Now;
        private static DateTime endTime;

        private static List<string> paths = new List<string>();

        public static void Main()
        {
            using var watcher = new FileSystemWatcher(@"C:\");

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

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            string path = e.FullPath;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write($"[{DateTime.Now.ToString("hh:mm:ss")}] Changed:");
            Console.ResetColor();
            Console.Write($" {path}\n");

            log(path);
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string path = e.FullPath;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write($"[{DateTime.Now.ToString("hh:mm:ss")}] Created:");
            Console.ResetColor();
            Console.Write($" {path}\n");

            log(path);
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            string path = e.FullPath;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write($"[{DateTime.Now.ToString("hh:mm:ss")}] Deleted:");
            Console.ResetColor();
            Console.Write($" {path}\n");

            log(path);
        }

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            string path = e.FullPath;

            Console.WriteLine($"[{DateTime.Now.ToString("hh:mm:ss")}] Renamed:");
            Console.WriteLine($"\t\t╚Old: {e.OldFullPath}");
            Console.WriteLine($"\t\t╚New: {e.FullPath}");

            log(path);
        }

        public static void log(string path)
        {
            paths.Add(path);

            endTime = DateTime.Now;
            if ((endTime - startTime).TotalMinutes >= AppDurationMinutes)
            {
                System.Console.WriteLine("\n\n\n");
                System.Console.WriteLine($"Total path count:  {paths.Count()}");
                System.Console.WriteLine($"Unique path count: {paths.Distinct().Count()}");
                Environment.Exit(0);
            }
        }

        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception? ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }
    }
}
