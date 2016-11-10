using FootballClient.UWP.ViewModels;
using Windows.UI.Xaml.Controls;

namespace FootballClient.UWP
{
    public sealed partial class AppShell : UserControl
    {
        private Frame _rootFrame;

        public AppShell()
        {
            this.InitializeComponent();
            DataContextChanged += (s, e) =>
            {
                ViewModel = (AppShellViewModel)DataContext;
            };
        }

        public void SetContentFrame(Frame frame)
        {
            rootSplitView.Content = frame;
            _rootFrame = frame;
        }


        public AppShellViewModel ViewModel { get; private set; }
    }
}
