using Prism.Windows.Mvvm;

namespace FootballClient.UWP.Views
{
    public sealed partial class CommentsPage : SessionStateAwarePage
    {
        public CommentsPage()
        {
            this.InitializeComponent();
            SizeChanged += CommentsPage_SizeChanged;
        }

        private void CommentsPage_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            contentOverlayColumn.Width = ContainerGrid.ActualWidth;
        }
    }
}
