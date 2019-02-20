using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace Valkyrie.Updater
{
    public class Program
    {
        static bool success = false;

        static void Main(string[] args)
        {
            Process[] pname = Process.GetProcessesByName("Valkyrie");
            var spin = new ConsoleSpinner();

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            if (!Directory.Exists("Libs"))
            {
                Directory.CreateDirectory("Libs");
            }

            Console.Title = "Valkyrie Updater";

            if (pname.Length > 0)
            {         
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("[Valkyrie.Updater] ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Please close Valkyrie Loader before update... ");

                while (pname.Length > 0)
                {
                    spin.Turn();
                    pname = Process.GetProcessesByName("notepad");
                }
                Console.Clear();
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("[Valkyrie.Updater] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Getting the items... ");

            DownloadFile();

            if (!success)
            {
                Console.WriteLine("");
                Console.WriteLine("Something went wrong...");
                Console.WriteLine("");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("[Valkyrie.Updater] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Update complete!");
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue...");

            Console.ReadKey();
        }

        private static void DownloadFile()
        {
            string coreUrl = "https://github.com/madgwee/RO2-Hacking/raw/master/Valkyrie.System/Valkyrie.Core.dll";
            string loaderUrl = "https://github.com/madgwee/RO2-Hacking/blob/master/Valkyrie.System/Valkyrie.exe?raw=true";

            WebClient downloader = new WebClient();

            try
            {
                downloader.DownloadFile(coreUrl, "Libs/Valkyrie.Core.dll");

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("[Valkyrie.Updater] ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Got the Core!");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[Valkyrie.Updater] ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("We couldn't get the Core, connection lost...");
                return;
            }

            try
            {
                downloader.DownloadFile(loaderUrl, "Valkyrie.exe");

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("[Valkyrie.Updater] ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Got the Loader");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[Valkyrie.Updater] ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("We couldn't get the Loader, connection lost...");
                return;
            }

            success = true;
        }
    }
}
