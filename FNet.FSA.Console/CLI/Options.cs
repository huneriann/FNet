using FNet.FSA.Core;

namespace FNet.FSA.Console.CLI
{
    public static class Options
    {
        [Command("-h")]
        [Command("--help")]
        public static void ShowHelp()
        {
            System.Console.WriteLine("-p|--path <Path> - Set path for analyze\n\tFor Example:\n\t-p C:\\");
            System.Console.WriteLine();
            System.Console.WriteLine("-h|--help - Show hints");
        }

        [Command("-p")]
        [Command("--path")]
        public static void SetPath(string path)
        {
            Analyzer.GetObject().Path = path;
        }
    }
}
