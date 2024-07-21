using System.Reflection;
using System.Windows;
using AutoShutdown.Contents;

namespace AutoShutdown
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainVm();
        }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is MainVm vm)
            {
                if (vm.Content is RunningContent runningContent)
                {
                    var choice = MessageBox.Show("关闭软件会使定时失效，确定要关闭吗？", "提示", MessageBoxButton.OKCancel,
                        MessageBoxImage.Information);
                    if (choice == MessageBoxResult.OK)
                    {
                        runningContent.Stop();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }

        }

        private void OnWindowMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (DataContext is MainVm vm)
            {
                if (vm.Content is NormalContent)
                {
                    int deltaMinute;
                    if (e.Delta > 0)
                    {
                        deltaMinute = 1;
                    }
                    else
                    {
                        deltaMinute = -1;
                    }

                    var finalMinute = vm.Minutes + deltaMinute;

                    if (finalMinute >= 1 && finalMinute <= 60)
                    {
                        vm.Minutes = finalMinute;
                    }
                }
            }
        }
    }
}
