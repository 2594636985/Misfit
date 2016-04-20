using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Misfit.AddIn;
using Misfit.Console.CommandLineParser;

namespace Misfit.Console
{
    /// <summary>
    /// The console service.
    /// </summary>
    internal class ConsoleService : IConsoleService
    {
        private static Exception lastException = null;
        private IBootstrapper osgi;
        private string[] consoleArgs;
        private CommandLineHelper commandLineHelper;
        private CommandLineParser.CommandLineParser commandLineParser;
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

        public ConsoleService(IBootstrapper shell)
        {
            this.osgi = shell;
        }

        public void Start()
        {
            // Connect to the Shell
            this.consoleArgs = osgi.Setup.ConsoleArgs;

            commandLineHelper = new CommandLineHelper();
            commandLineHelper.RegisterCommand(typeof(ConsoleAddIn).Assembly);
            commandLineParser = new CommandLineParser.CommandLineParser(commandLineHelper);

            // Alloc the console window
            AllocConsole();
            
            RunConsoleThread();
        }

        public void Stop()
        {
            quit = true;
            WriteLine("Type \"exit\" to exit.");
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
            Initialize();

            while (!quit)
            {
                try
                {
                    System.Console.ForegroundColor = ConsoleColor.Magenta;
                    System.Console.Write(">>>");
                    System.Console.ResetColor();

                    string commandLine = ReadLine();
                    if (string.IsNullOrEmpty(commandLine))
                    {
                        Thread.Sleep(400);
                        continue;
                    }

                    DoCommand(commandLine);
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    System.Console.Error.WriteLine(ex.Message);
                }
            }
        }

        public void Initialize()
        {
            System.Console.Title = "开发日志";
            System.Console.ForegroundColor = ConsoleColor.Green;
            WriteLine("当前开发的系统版本号：" + typeof(IBootstrapper).Assembly.GetName().Version);
            WriteLine("");
            System.Console.ForegroundColor = ConsoleColor.Magenta;
            System.Console.Write(">>>");
            System.Console.ResetColor();
            System.Console.ForegroundColor = ConsoleColor.Green;
            WriteLine("下列显示日志追踪信息");

            System.Console.ResetColor();
        }

        public string ReadLine()
        {
            return System.Console.ReadLine();
        }

        public void WriteLine(object value)
        {
            System.Console.WriteLine(value);
        }

        public void DoCommand(string commandLine)
        {
            try
            {
                ICommand command = commandLineParser.ParseCommandLine(commandLine);
                if (command != null)
                {
                    command.Execute();
                }
            }
            catch (CommandLineException ex)
            {
                lastException = ex;

                KeyValuePair<ICommandInfo, ICommand> helpCommand = commandLineHelper.BuildUp("help");
                if (helpCommand.Key != null)
                {
                    helpCommand.Value.Execute();
                }
            }
        }
    }
}
