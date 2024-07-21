using System;
using System.Diagnostics;
using System.Windows.Threading;
using Jg.wpf.core.Notify;

namespace AutoShutdown.Contents
{
    internal class RunningContent : ViewModelBase, IContent
    {
        private readonly DispatcherTimer _timer;
        private readonly int _secondsTotal;

        private int _seconds;
        private int _shutdownProgress;
        private string _timeCounter;

        public string Display => "取消关机";

        public string TimeCounter
        {
            get => _timeCounter;
            set
            {
                if (value == _timeCounter) return;
                _timeCounter = value;
                RaisePropertyChanged(nameof(TimeCounter));
            }
        }

        public int ShutdownProgress
        {
            get => _shutdownProgress;
            set
            {
                if (value == _shutdownProgress) return;
                _shutdownProgress = value;
                RaisePropertyChanged(nameof(ShutdownProgress));
            }
        }


        public RunningContent(int minutes)
        {
            _seconds = minutes * 60;
            _secondsTotal = _seconds;
            _timeCounter = $"{minutes}分00秒";

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTimerTick;

            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            _seconds--;

            var minutes = _seconds / 60;
            var seconds = _seconds % 60;

            TimeCounter = $"{minutes}分{seconds}秒";

            ShutdownProgress = (int)((1 - Math.Round(((double)_seconds /_secondsTotal), 2)) * 100);

            if (minutes == 0 && seconds == 0)
            {
                Stop();
                Process.Start("shutdown", "/f /s /t 0");
            }
        }
    }
}
