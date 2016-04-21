using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Misfit.AddIn;

namespace Misfit.Console
{
    /// <summary>
    /// The console service.
    /// </summary>
    internal class ConsoleService : IConsoleService
    {
        private static Exception lastException = null;
        private bool quit = false;

        public static Exception LastException
        {
            get
            {
                return lastException;
            }
        }

        [DllImport("kernel32")]
        static extern bool AllocConsole();


        public void Start()
        {
            // Alloc the console window
            AllocConsole();
            
            RunConsoleThread();
        }

        public void Stop()
        {
            quit = true;
            System.Console.WriteLine("Type \"exit\" to exit.");
        }

        private void RunConsoleThread()
        {
            ThreadStart start = new ThreadStart(LaunchConsoleRun);
            Thread uiThread = new Thread(start);
            uiThread.IsBackground = false;
            uiThread.Start();
        }

        private void LaunchConsoleRun()
        {
            System.Console.Title = "开发日志";
            System.Console.ForegroundColor = ConsoleColor.Magenta;
            System.Console.Write(">>>");
            System.Console.ResetColor();
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("下列显示日志追踪信息");
            System.Console.ResetColor();

            while (!quit)
            {
                try
                {
                    System.Console.ForegroundColor = ConsoleColor.Magenta;
                    System.Console.Write(">>>");
                    System.Console.ResetColor();

                    string commandLine = System.Console.ReadLine();
                    if (string.IsNullOrEmpty(commandLine))
                    {
                        Thread.Sleep(400);
                        continue;
                    }

                }
                catch (Exception ex)
                {
                    lastException = ex;
                    System.Console.Error.WriteLine(ex.Message);
                }
            }
        }


    }
}
