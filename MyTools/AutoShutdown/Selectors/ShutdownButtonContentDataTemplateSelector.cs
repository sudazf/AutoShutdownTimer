using System.Windows;
using System.Windows.Controls;
using AutoShutdown.Contents;

namespace AutoShutdown.Selectors
{
    public class ShutdownButtonContentDataTemplateSelector : DataTemplateSelector
    {

        public DataTemplate NormalTemplate { get; set; }
        public DataTemplate RunningTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return item is NormalContent ? NormalTemplate : RunningTemplate;
        }
    }
}
