using Jg.wpf.core.Notify;

namespace AutoShutdown.Contents
{
    internal class NormalContent : ViewModelBase, IContent
    {
        private string _timeCounter;
        public string Display => "定时关机";

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

        public int ShutdownProgress => 0;

        public NormalContent(int minutes)
        {
            _timeCounter = minutes.ToString();
        }
    }
}
