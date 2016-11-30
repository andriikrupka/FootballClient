using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FootballClient.UWP.ControlExtensions
{
    public class ListViewBaseExtensions
    {
        public static ICommand GetItemClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ItemClickCommandProperty);
        }

        public static void SetItemClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ItemClickCommandProperty, value);
        }

        public static readonly DependencyProperty ItemClickCommandProperty =
            DependencyProperty.RegisterAttached("ItemClickCommand", typeof(ICommand), typeof(ListViewBaseExtensions), new PropertyMetadata(null, OnItemClickCommandPropertyChanged));

        private static void OnItemClickCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listView = d as ListViewBase;
            if (listView != null)
            {
                if (e.OldValue != null)
                {
                    listView.ItemClick -= OnItemClick;
                }

                if (e.NewValue != null)
                {
                    listView.ItemClick += OnItemClick;
                }
            }
        }

        private static void OnItemClick(object sender, ItemClickEventArgs e)
        {
            var dependencyObject = (DependencyObject)sender;

            var command = GetItemClickCommand(dependencyObject);
            if (command != null)
            {
                if (command.CanExecute(e.ClickedItem))
                {
                    command.Execute(e.ClickedItem);
                }
            }
        }
    }
}
