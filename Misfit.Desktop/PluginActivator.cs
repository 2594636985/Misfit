using Misfit.AddIn;
using Misfit.AddIn.Cmd;
using Misfit.AddIn.Pipe;
using Misfit.Desktop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace Misfit.Desktop
{
    [Serializable]
    public class PluginActivator : AddInActivator
    {
        public Dispatcher _dispatcher;
        public override void Start(string[] args)
        {
            ThreadStart start = new ThreadStart(() =>
            {

                App app = new App();
                app.InitializeComponent();
                this._dispatcher = app.Dispatcher;
                //app.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
                //Thread.GetDomain().SetData("MainWindow", app.MainWindow);
                //System.Windows.Threading.Dispatcher.Run();
                app.Run();

                //Console.WriteLine("aaa");

            });

            Thread uiThread = new Thread(start);
            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.IsBackground = false;
            uiThread.Start();
        }

        public override void Stop(string[] args)
        {
            try
            {
                //MainWindow mainWindow = (MainWindow)Thread.GetDomain().GetData("MainWindow");
                //mainWindow.Dispatcher.Invoke(new Action(() =>
                //{
                //    mainWindow.Close();
                //}));
                //System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                //{
                //    System.Windows.Application.Current.Shutdown();
                //}), System.Windows.Threading.DispatcherPriority.ContextIdle);
                if (this._dispatcher != null)
                    _dispatcher.BeginInvokeShutdown(DispatcherPriority.Normal);

            }
            catch (Exception EX)
            {

            }

            //ThreadStart start = new ThreadStart(() =>
            //{
            //    try
            //    {
            //        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            //        {
            //            System.Windows.Application.Current.MainWindow.Close();
            //        }));
            //    }
            //    catch (Exception EX)
            //    {

            //    }

            //});

            //Thread uiThread = new Thread(start);
            //uiThread.SetApartmentState(ApartmentState.STA);
            //uiThread.IsBackground = false;
            //uiThread.Start();

        }
    }
}
