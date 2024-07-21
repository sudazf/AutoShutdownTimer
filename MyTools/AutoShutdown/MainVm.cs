using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using AutoShutdown.Contents;
using Jg.wpf.core.Command;
using Jg.wpf.core.Notify;

namespace AutoShutdown
{
    public class MainVm : ViewModelBase
    {
        private int _minutes;
        private IContent _content;
        private bool _canChangeTime;

        public int Minutes
        {
            get => _minutes;
            set
            {
                if (value == _minutes) return;
                _minutes = value;

                if (_content is NormalContent normalContent)
                {
                    normalContent.TimeCounter = _minutes.ToString();
                }

                RaisePropertyChanged(nameof(Minutes));
            }
        }
  
        public bool CanChangeTime
        {
            get => _canChangeTime;
            set
            {
                if (value == _canChangeTime) return;
                _canChangeTime = value;
                RaisePropertyChanged(nameof(CanChangeTime));
            }
        }

        public IContent Content
        {
            get => _content;
            set
            {
                if (Equals(value, _content)) return;
                _content = value;
                RaisePropertyChanged(nameof(Content));
            }
        }

        public JCommand ShutdownCommand { get; }

        public MainVm()
        {
            _minutes = 15;
            _canChangeTime = true;
            _content = new NormalContent(_minutes);

            ShutdownCommand = new JCommand("ShutdownCommandId", OnShutdown, CanShutdown);
        }

        private void OnShutdown(object obj)
        {
            if (_content is NormalContent)
            {
                Content = new RunningContent(_minutes);
                CanChangeTime = false;
            }
            else
            {
                if (_content is RunningContent runningContent)
                {
                    runningContent.Stop();
                }

                Content = new NormalContent(_minutes);
                CanChangeTime = true;
            }
        }

        private bool CanShutdown(object arg)
        {
            return true;
        }
    }
}
