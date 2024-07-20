using System;
using System.Threading;
using System.Windows;

namespace AutoShutdown
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private static Mutex _mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            var title = "定时关机小工具";
            _mutex = new Mutex(true, title, out var isNewInstance);

            if (!isNewInstance)
            {
                ActivateOtherWindow(title);
                Shutdown();
            }
            else
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }

        private static void ActivateOtherWindow(string title)
        {
            var existInstance = Natives.NativeMethods.FindWindow(null, title);
            if (existInstance != IntPtr.Zero)
            {
                Natives.NativeMethods.SetForegroundWindow(existInstance);
                if (Natives.NativeMethods.IsIconic(existInstance))
                {
                    //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
                    Natives.NativeMethods.ShowWindow(existInstance, 1);
                }
            }
        }
    }
}
