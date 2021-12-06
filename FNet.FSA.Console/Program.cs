using System;
using System.Linq;
using FNet.FSA.Console.CLI;
using FNet.FSA.Core;

namespace FNet.FSA.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //-h|--help 
            //-ht|--holdtime <seconds>
            //-p|--path <path>

            Analyzer.GetObject().log = Log;
            
            if (args.Contains("-h") || args.Contains("--help"))
            {
                Call("-h", null);
                return;
            }
            
            if (args.Contains("-p") || args.Contains("--path")) 
                Call("-p", args[1]);
            
            Analyzer.GetObject().Execute();
            System.Console.ReadLine();
        }

        static void Call(string option, params string[] parameters)
        {
            typeof(Options)
                    .GetMethods()
                    .Where(
                        x => x.GetCustomAttributes(false)
                              .OfType<CommandAttribute>()
                              .Count() > 0)
                    .Where(
                        x => x.GetCustomAttributes(false)
                              .OfType<CommandAttribute>()
                              .Where(
                                  x => option == x.Path)
                              .Any())
                    .ToList()
                        .ForEach(
                            x => x.Invoke(null, parameters));
        }

        public static void Log(FNet.FSA.Core.Model.DirectoryInfo di)
        {
            if(di.State == FNet.FSA.Core.Model.DirectoryState.Changed)
            {
                System.Console.WriteLine($"[Changed] [{di.DateTime.ToString("HH:mm:ss")}] {di.Path}");
            }
            else if(di.State == FNet.FSA.Core.Model.DirectoryState.Created)
            {
                System.Console.WriteLine($"[Created] [{di.DateTime.ToString("HH:mm:ss")}] {di.Path}");
            }
            else if (di.State == FNet.FSA.Core.Model.DirectoryState.Deleted)
            {
                System.Console.WriteLine($"[Deleted] [{di.DateTime.ToString("HH:mm:ss")}] {di.Path}");
            }
            else if (di.State == FNet.FSA.Core.Model.DirectoryState.Renamed)
            {
                System.Console.WriteLine($"[Renamed] [{di.DateTime.ToString("HH:mm:ss")}] {di.Path}");
            }
            else if (di.State == FNet.FSA.Core.Model.DirectoryState.Error)
            {
                System.Console.WriteLine($"[Renamed] [{di.DateTime.ToString("HH:mm:ss")}] {di.Path}");
            }
        }
    }
}